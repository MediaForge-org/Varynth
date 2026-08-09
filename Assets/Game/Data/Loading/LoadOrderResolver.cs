using System;
using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Data.Sources;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Deterministic content-source load order. Hard dependencies alone determine
    /// loadability (DuplicateSourceId / MissingDependency / DependencyExcluded /
    /// CycleMember / BlockedByCycle all come only from the hard-dependency graph).
    /// Existing optional dependencies and loadAfter entries are applied strictly
    /// afterward, purely for ordering, and can never exclude an otherwise-valid source:
    /// a soft edge that would introduce a cycle is dropped instead, deterministically,
    /// with a Warning.
    /// </summary>
    public static class LoadOrderResolver
    {
        private enum SoftEdgeKind
        {
            OptionalDependency,
            LoadAfter
        }

        private readonly struct SoftEdgeCandidate
        {
            public ContentSourceId Dependent { get; }
            public ContentSourceId Target { get; }
            public SoftEdgeKind Kind { get; }

            public SoftEdgeCandidate(ContentSourceId dependent, ContentSourceId target, SoftEdgeKind kind)
            {
                Dependent = dependent;
                Target = target;
                Kind = kind;
            }
        }

        public static LoadOrderResult Resolve(IReadOnlyList<ContentSource> sources, ContentLoadReport report)
        {
            var exclusions = new List<LoadOrderExclusion>();

            // Phase A -- duplicate ids: every source sharing a duplicated id is excluded,
            // grouping-by-id makes this independent of input order.
            var byId = new Dictionary<ContentSourceId, List<ContentSource>>();
            foreach (var source in sources)
            {
                if (!byId.TryGetValue(source.Id, out var group))
                {
                    group = new List<ContentSource>();
                    byId[source.Id] = group;
                }

                group.Add(source);
            }

            var excludedReasons = new Dictionary<ContentSourceId, LoadOrderExclusionReason>();
            var active = new Dictionary<ContentSourceId, ContentSource>();

            foreach (var group in byId)
            {
                if (group.Value.Count > 1)
                {
                    foreach (var duplicate in group.Value)
                    {
                        Exclude(exclusions, report, duplicate, LoadOrderExclusionReason.DuplicateSourceId,
                            $"Multiple content sources share id '{duplicate.Id}'.");
                    }

                    excludedReasons[group.Key] = LoadOrderExclusionReason.DuplicateSourceId;
                }
                else
                {
                    active[group.Key] = group.Value[0];
                }
            }

            // Phase B -- direct missing hard dependency (one hop only).
            foreach (var id in new List<ContentSourceId>(active.Keys))
            {
                var source = active[id];
                foreach (var dependency in source.RequiredDependencies)
                {
                    if (!byId.ContainsKey(dependency))
                    {
                        Exclude(exclusions, report, source, LoadOrderExclusionReason.MissingDependency,
                            $"Missing required dependency '{dependency}'.");
                        excludedReasons[id] = LoadOrderExclusionReason.MissingDependency;
                        active.Remove(id);
                        break;
                    }
                }
            }

            // Phase C -- transitive exclusion propagation over hard dependencies, fixed point.
            bool changed;
            do
            {
                changed = false;
                foreach (var id in new List<ContentSourceId>(active.Keys))
                {
                    var source = active[id];
                    foreach (var dependency in source.RequiredDependencies)
                    {
                        if (excludedReasons.TryGetValue(dependency, out var dependencyReason))
                        {
                            Exclude(exclusions, report, source, LoadOrderExclusionReason.DependencyExcluded,
                                $"Depends on '{dependency}', which is excluded ({dependencyReason}).");
                            excludedReasons[id] = LoadOrderExclusionReason.DependencyExcluded;
                            active.Remove(id);
                            changed = true;
                            break;
                        }
                    }
                }
            } while (changed);

            // Phase D -- hard-dependency graph over the survivors; classify anything Kahn's
            // can't place as an actual cycle member or merely blocked by one.
            var hardGraph = BuildAdjacency(active.Keys);
            foreach (var id in active.Keys)
            {
                foreach (var dependency in active[id].RequiredDependencies)
                {
                    hardGraph[dependency].Add(id);
                }
            }

            var (_, hardLeftover) = KahnSort(active.Keys, hardGraph, active);
            if (hardLeftover.Count > 0)
            {
                var cycleMembers = FindCycleMembers(hardLeftover, hardGraph);
                foreach (var id in hardLeftover)
                {
                    var source = active[id];
                    if (cycleMembers.Contains(id))
                    {
                        Exclude(exclusions, report, source, LoadOrderExclusionReason.CycleMember,
                            "Part of a hard-dependency cycle.");
                        excludedReasons[id] = LoadOrderExclusionReason.CycleMember;
                    }
                    else
                    {
                        Exclude(exclusions, report, source, LoadOrderExclusionReason.BlockedByCycle,
                            "Blocked because a hard dependency is (possibly transitively) part of a cycle.");
                        excludedReasons[id] = LoadOrderExclusionReason.BlockedByCycle;
                    }

                    active.Remove(id);
                }
            }

            // Phase E -- apply soft ordering (existing optional dependencies + loadAfter) on
            // top of the now-acyclic hard-dependency graph, restricted to the loadable set.
            var finalIds = new HashSet<ContentSourceId>(active.Keys);
            var graph = BuildAdjacency(finalIds);
            foreach (var id in finalIds)
            {
                foreach (var dependency in active[id].RequiredDependencies)
                {
                    graph[dependency].Add(id);
                }
            }

            var candidates = new List<SoftEdgeCandidate>();
            foreach (var id in finalIds)
            {
                var source = active[id];

                foreach (var dependency in source.OptionalDependencies)
                {
                    if (finalIds.Contains(dependency))
                    {
                        candidates.Add(new SoftEdgeCandidate(id, dependency, SoftEdgeKind.OptionalDependency));
                    }
                }

                foreach (var target in source.LoadAfter)
                {
                    if (finalIds.Contains(target))
                    {
                        candidates.Add(new SoftEdgeCandidate(id, target, SoftEdgeKind.LoadAfter));
                    }
                }
            }

            candidates.Sort((a, b) =>
            {
                var byDependent = string.CompareOrdinal(a.Dependent.ToString(), b.Dependent.ToString());
                if (byDependent != 0)
                {
                    return byDependent;
                }

                var byTarget = string.CompareOrdinal(a.Target.ToString(), b.Target.ToString());
                if (byTarget != 0)
                {
                    return byTarget;
                }

                return a.Kind.CompareTo(b.Kind);
            });

            foreach (var candidate in candidates)
            {
                if (CanReach(graph, candidate.Dependent, candidate.Target))
                {
                    report.AddWarning(active[candidate.Dependent].Id, null, null,
                        $"Soft ordering edge from '{candidate.Dependent}' to '{candidate.Target}' dropped: would create a cycle.");
                    continue;
                }

                graph[candidate.Target].Add(candidate.Dependent);
            }

            // Phase F -- final deterministic order over the resulting DAG. Guaranteed
            // acyclic by construction (hard edges already cycle-free, soft edges only
            // ever accepted when they don't close a cycle), so nothing should be leftover.
            var (finalOrder, finalLeftover) = KahnSort(finalIds, graph, active);
            foreach (var id in finalLeftover)
            {
                // Defensive: should be unreachable given Phase D/E's guarantees.
                finalOrder.Add(id);
            }

            return new LoadOrderResult(finalOrder, exclusions);
        }

        private static void Exclude(List<LoadOrderExclusion> exclusions, ContentLoadReport report, ContentSource source,
            LoadOrderExclusionReason reason, string detail)
        {
            var exclusion = new LoadOrderExclusion(source, reason, detail);
            exclusions.Add(exclusion);
            report?.AddError(source.Id, null, null, $"{reason}: {detail}");
        }

        private static Dictionary<ContentSourceId, HashSet<ContentSourceId>> BuildAdjacency(IEnumerable<ContentSourceId> ids)
        {
            var graph = new Dictionary<ContentSourceId, HashSet<ContentSourceId>>();
            foreach (var id in ids)
            {
                graph[id] = new HashSet<ContentSourceId>();
            }

            return graph;
        }

        private static (List<ContentSourceId> order, HashSet<ContentSourceId> leftover) KahnSort(
            IEnumerable<ContentSourceId> nodes,
            Dictionary<ContentSourceId, HashSet<ContentSourceId>> edges,
            Dictionary<ContentSourceId, ContentSource> sourceById)
        {
            var remaining = new HashSet<ContentSourceId>(nodes);
            var inDegree = new Dictionary<ContentSourceId, int>();
            foreach (var id in remaining)
            {
                inDegree[id] = 0;
            }

            foreach (var from in remaining)
            {
                foreach (var to in edges[from])
                {
                    if (remaining.Contains(to))
                    {
                        inDegree[to]++;
                    }
                }
            }

            var order = new List<ContentSourceId>();

            while (remaining.Count > 0)
            {
                ContentSourceId? next = null;
                foreach (var id in remaining)
                {
                    if (inDegree[id] != 0)
                    {
                        continue;
                    }

                    if (next == null || CompareForTieBreak(sourceById[id], sourceById[next.Value]) < 0)
                    {
                        next = id;
                    }
                }

                if (next == null)
                {
                    break;
                }

                order.Add(next.Value);
                remaining.Remove(next.Value);

                foreach (var to in edges[next.Value])
                {
                    if (remaining.Contains(to))
                    {
                        inDegree[to]--;
                    }
                }
            }

            return (order, remaining);
        }

        private static int CompareForTieBreak(ContentSource a, ContentSource b)
        {
            var priorityCompare = a.Priority.CompareTo(b.Priority);
            if (priorityCompare != 0)
            {
                return priorityCompare;
            }

            return string.CompareOrdinal(a.Id.ToString(), b.Id.ToString());
        }

        private static HashSet<ContentSourceId> FindCycleMembers(
            HashSet<ContentSourceId> leftover,
            Dictionary<ContentSourceId, HashSet<ContentSourceId>> edges)
        {
            var cycleMembers = new HashSet<ContentSourceId>();

            foreach (var start in leftover)
            {
                var visited = new HashSet<ContentSourceId>();
                var stack = new Stack<ContentSourceId>();
                foreach (var next in edges[start])
                {
                    if (leftover.Contains(next))
                    {
                        stack.Push(next);
                    }
                }

                while (stack.Count > 0)
                {
                    var current = stack.Pop();
                    if (current.Equals(start))
                    {
                        cycleMembers.Add(start);
                        break;
                    }

                    if (!visited.Add(current))
                    {
                        continue;
                    }

                    foreach (var next in edges[current])
                    {
                        if (leftover.Contains(next) && !visited.Contains(next))
                        {
                            stack.Push(next);
                        }
                    }
                }
            }

            return cycleMembers;
        }

        private static bool CanReach(Dictionary<ContentSourceId, HashSet<ContentSourceId>> graph, ContentSourceId from, ContentSourceId to)
        {
            var visited = new HashSet<ContentSourceId>();
            var stack = new Stack<ContentSourceId>();
            stack.Push(from);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Equals(to))
                {
                    return true;
                }

                if (!visited.Add(current))
                {
                    continue;
                }

                if (graph.TryGetValue(current, out var neighbors))
                {
                    foreach (var next in neighbors)
                    {
                        stack.Push(next);
                    }
                }
            }

            return false;
        }
    }
}

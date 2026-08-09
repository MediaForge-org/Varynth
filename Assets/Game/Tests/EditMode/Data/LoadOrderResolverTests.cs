using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Data.Loading;
using Varynth.Data.Sources;
using Varynth.Data.Validation;

namespace Varynth.Tests.EditMode.Data
{
    public class LoadOrderResolverTests
    {
        private static ContentSource Src(
            string id,
            int priority = 0,
            string[] required = null,
            string[] optional = null,
            string[] loadAfter = null)
        {
            return new ContentSource(
                ContentSourceId.Parse(id),
                ContentSourceType.Test,
                rootPath: "/test/" + id,
                priority: priority,
                requiredDependencies: ToIds(required),
                optionalDependencies: ToIds(optional),
                loadAfter: ToIds(loadAfter));
        }

        private static IReadOnlyList<ContentSourceId> ToIds(string[] raw)
        {
            if (raw == null)
            {
                return null;
            }

            var list = new List<ContentSourceId>();
            foreach (var value in raw)
            {
                list.Add(ContentSourceId.Parse(value));
            }

            return list;
        }

        private static int IndexOf(LoadOrderResult result, string id)
        {
            var target = ContentSourceId.Parse(id);
            for (var i = 0; i < result.OrderedSourceIds.Count; i++)
            {
                if (result.OrderedSourceIds[i].Equals(target))
                {
                    return i;
                }
            }

            return -1;
        }

        private static LoadOrderExclusionReason? ReasonFor(LoadOrderResult result, string id)
        {
            var target = ContentSourceId.Parse(id);
            foreach (var exclusion in result.Exclusions)
            {
                if (exclusion.Source.Id.Equals(target))
                {
                    return exclusion.Reason;
                }
            }

            return null;
        }

        [Test]
        public void NoDependencies_OrdersByPriorityThenId()
        {
            var sources = new[] { Src("b"), Src("a"), Src("c") };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(3, result.OrderedSourceIds.Count);
            Assert.Less(IndexOf(result, "a"), IndexOf(result, "b"));
            Assert.Less(IndexOf(result, "b"), IndexOf(result, "c"));
        }

        [Test]
        public void ADependsOnB_BComesFirst()
        {
            var sources = new[] { Src("a", required: new[] { "b" }), Src("b") };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.Less(IndexOf(result, "b"), IndexOf(result, "a"));
            Assert.AreEqual(0, result.Exclusions.Count);
        }

        [Test]
        public void MultipleDependencies_AllPredecessorsBeforeDependent()
        {
            var sources = new[] { Src("c", required: new[] { "a", "b" }), Src("a"), Src("b") };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.Less(IndexOf(result, "a"), IndexOf(result, "c"));
            Assert.Less(IndexOf(result, "b"), IndexOf(result, "c"));
        }

        [Test]
        public void LoadAfter_AcceptedCase_OrdersCorrectly()
        {
            var sources = new[] { Src("a", loadAfter: new[] { "b" }), Src("b") };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.Less(IndexOf(result, "b"), IndexOf(result, "a"));
            Assert.AreEqual(0, result.Exclusions.Count);
        }

        [Test]
        public void OptionalDependency_Existing_CreatesSoftOrderingEdge()
        {
            var sources = new[] { Src("a", optional: new[] { "b" }), Src("b") };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.Less(IndexOf(result, "b"), IndexOf(result, "a"));
            Assert.AreEqual(0, result.Exclusions.Count);
        }

        [Test]
        public void StableTieBreak_SamePriority_OrdersAlphabetically()
        {
            var sources = new[] { Src("z", priority: 3), Src("y", priority: 3), Src("x", priority: 3) };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.Less(IndexOf(result, "x"), IndexOf(result, "y"));
            Assert.Less(IndexOf(result, "y"), IndexOf(result, "z"));
        }

        [Test]
        public void MissingOptionalDependency_IsIgnored_NoError()
        {
            var report = new ContentLoadReport();
            var sources = new[] { Src("a", optional: new[] { "ghost" }) };
            var result = LoadOrderResolver.Resolve(sources, report);

            Assert.AreEqual(1, result.OrderedSourceIds.Count);
            Assert.AreEqual(0, result.Exclusions.Count);
            Assert.AreEqual(0, report.ErrorCount);
        }

        [Test]
        public void IdenticalInputs_ProduceIdenticalOrder()
        {
            var sources = new[]
            {
                Src("c", required: new[] { "a" }),
                Src("a"),
                Src("b", loadAfter: new[] { "a" })
            };

            var first = LoadOrderResolver.Resolve(sources, new ContentLoadReport());
            var second = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            CollectionAssert.AreEqual(first.OrderedSourceIds, second.OrderedSourceIds);
        }

        [Test]
        public void MissingHardDependency_ExcludesWithMissingDependencyReason()
        {
            var sources = new[] { Src("a", required: new[] { "b" }) };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(-1, IndexOf(result, "a"));
            Assert.AreEqual(LoadOrderExclusionReason.MissingDependency, ReasonFor(result, "a"));
        }

        [Test]
        public void TransitiveDependencyExclusion_PropagatesThroughChain()
        {
            var sources = new[]
            {
                Src("c", required: new[] { "a" }),
                Src("a", required: new[] { "b" })
                // "b" does not exist at all
            };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(LoadOrderExclusionReason.MissingDependency, ReasonFor(result, "a"));
            Assert.AreEqual(LoadOrderExclusionReason.DependencyExcluded, ReasonFor(result, "c"));
        }

        [Test]
        public void HardCycle_TwoNodes_BothAreCycleMembers()
        {
            var sources = new[]
            {
                Src("a", required: new[] { "b" }),
                Src("b", required: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "a"));
            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "b"));
        }

        [Test]
        public void HardCycle_WithDependentC_CIsBlockedNotCycleMember()
        {
            var sources = new[]
            {
                Src("a", required: new[] { "b" }),
                Src("b", required: new[] { "a" }),
                Src("c", required: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "a"));
            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "b"));
            Assert.AreEqual(LoadOrderExclusionReason.BlockedByCycle, ReasonFor(result, "c"));
        }

        [Test]
        public void HardCycle_WithIndependentD_DStillLoadsNormally()
        {
            var sources = new[]
            {
                Src("a", required: new[] { "b" }),
                Src("b", required: new[] { "a" }),
                Src("d")
            };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.IsNull(ReasonFor(result, "d"));
            Assert.GreaterOrEqual(IndexOf(result, "d"), 0);
        }

        [Test]
        public void HardCycle_ThreeNodes_AllAreCycleMembers()
        {
            var sources = new[]
            {
                Src("a", required: new[] { "b" }),
                Src("b", required: new[] { "c" }),
                Src("c", required: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, new ContentLoadReport());

            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "a"));
            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "b"));
            Assert.AreEqual(LoadOrderExclusionReason.CycleMember, ReasonFor(result, "c"));
        }

        [Test]
        public void DuplicateSourceId_BothExcluded_RegardlessOfInputOrder()
        {
            var sourceA = Src("dup");
            var sourceB = Src("dup");

            var forward = LoadOrderResolver.Resolve(new[] { sourceA, sourceB }, new ContentLoadReport());
            var reversed = LoadOrderResolver.Resolve(new[] { sourceB, sourceA }, new ContentLoadReport());

            Assert.AreEqual(0, forward.OrderedSourceIds.Count);
            Assert.AreEqual(2, forward.Exclusions.Count);
            Assert.AreEqual(LoadOrderExclusionReason.DuplicateSourceId, ReasonFor(forward, "dup"));

            Assert.AreEqual(0, reversed.OrderedSourceIds.Count);
            Assert.AreEqual(2, reversed.Exclusions.Count);
            Assert.AreEqual(LoadOrderExclusionReason.DuplicateSourceId, ReasonFor(reversed, "dup"));
        }

        [Test]
        public void SoftCycle_TwoNodes_OneEdgeDroppedWithWarning_BothRemainLoadable()
        {
            var report = new ContentLoadReport();
            var sources = new[]
            {
                Src("a", loadAfter: new[] { "b" }),
                Src("b", loadAfter: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, report);

            Assert.AreEqual(2, result.OrderedSourceIds.Count);
            Assert.AreEqual(0, result.Exclusions.Count);
            Assert.GreaterOrEqual(report.WarningCount, 1);
        }

        [Test]
        public void SoftCycle_ThreeNodes_OneEdgeDroppedWithWarning_AllRemainLoadable()
        {
            var report = new ContentLoadReport();
            var sources = new[]
            {
                Src("a", loadAfter: new[] { "b" }),
                Src("b", loadAfter: new[] { "c" }),
                Src("c", loadAfter: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, report);

            Assert.AreEqual(3, result.OrderedSourceIds.Count);
            Assert.AreEqual(0, result.Exclusions.Count);
            Assert.GreaterOrEqual(report.WarningCount, 1);
        }

        [Test]
        public void MixedConflict_HardDependencyWins_ConflictingSoftEdgeDropped()
        {
            var report = new ContentLoadReport();
            var sources = new[]
            {
                Src("a", required: new[] { "b" }),
                Src("b", loadAfter: new[] { "a" })
            };
            var result = LoadOrderResolver.Resolve(sources, report);

            Assert.AreEqual(2, result.OrderedSourceIds.Count);
            Assert.AreEqual(0, result.Exclusions.Count);
            Assert.Less(IndexOf(result, "b"), IndexOf(result, "a"));
            Assert.GreaterOrEqual(report.WarningCount, 1);
        }
    }
}

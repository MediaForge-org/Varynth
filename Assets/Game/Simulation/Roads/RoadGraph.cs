using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;

namespace Varynth.World.Roads
{
    /// <summary>
    /// One island's deterministic road network -- plain C#, no MonoBehaviour, no
    /// GameObject identity. Nodes are keyed by GridCoordinate directly (a node's
    /// identity IS its cell); segments have their own RoadSegmentId. Accumulates a
    /// dirty-cell set for later mesh-rebuild consumption (Phase 2D chunking
    /// future-proofing) -- a rendering-update optimization only, not a pathfinding
    /// scalability mechanism.
    /// </summary>
    public sealed class RoadGraph
    {
        private readonly Dictionary<GridCoordinate, RoadNode> _nodes = new Dictionary<GridCoordinate, RoadNode>();
        private readonly Dictionary<RoadSegmentId, RoadSegment> _segments = new Dictionary<RoadSegmentId, RoadSegment>();
        private readonly Dictionary<(GridCoordinate, GridCoordinate), RoadSegmentId> _pairIndex =
            new Dictionary<(GridCoordinate, GridCoordinate), RoadSegmentId>();
        private readonly HashSet<GridCoordinate> _dirtyCells = new HashSet<GridCoordinate>();

        public IReadOnlyCollection<RoadSegment> Segments => _segments.Values;
        public IReadOnlyCollection<RoadNode> Nodes => _nodes.Values;

        public bool TryGetNode(GridCoordinate cell, out RoadNode node)
        {
            return _nodes.TryGetValue(cell, out node);
        }

        public bool HasSegmentBetween(GridCoordinate a, GridCoordinate b)
        {
            return _pairIndex.ContainsKey(CanonicalKey(a, b));
        }

        public bool TryGetSegmentBetween(GridCoordinate a, GridCoordinate b, out RoadSegmentId id)
        {
            return _pairIndex.TryGetValue(CanonicalKey(a, b), out id);
        }

        public bool TryGetSegment(RoadSegmentId id, out RoadSegment segment)
        {
            return _segments.TryGetValue(id, out segment);
        }

        /// <summary>
        /// Adds one segment. Callers (RoadNetworkState) are expected to have already
        /// validated the edge -- this method only mutates graph state, it does not
        /// re-validate terrain/occupancy rules.
        /// </summary>
        public RoadSegment AddSegment(RoadSegmentId id, ContentId definitionId, GridCoordinate from, GridCoordinate to, RoadDirection direction, PlayerId owner)
        {
            var segment = new RoadSegment(id, definitionId, from, to, direction, owner);
            _segments[id] = segment;
            _pairIndex[CanonicalKey(from, to)] = id;

            GetOrCreateNode(from).ConnectedDirectionsMask |= (byte)(1 << (int)direction);
            GetOrCreateNode(to).ConnectedDirectionsMask |= (byte)(1 << (int)direction.Opposite());

            _dirtyCells.Add(from);
            _dirtyCells.Add(to);

            return segment;
        }

        public bool RemoveSegment(RoadSegmentId id, out RoadSegment removed)
        {
            if (!_segments.TryGetValue(id, out removed))
            {
                return false;
            }

            _segments.Remove(id);
            _pairIndex.Remove(CanonicalKey(removed.From, removed.To));

            ClearDirectionAndPruneNode(removed.From, removed.Direction);
            ClearDirectionAndPruneNode(removed.To, removed.Direction.Opposite());

            _dirtyCells.Add(removed.From);
            _dirtyCells.Add(removed.To);

            return true;
        }

        /// <summary>Returns and clears the set of cells touched since the last consumption (rendering-update optimization, see RoadMeshBuilder/B12).</summary>
        public IReadOnlyCollection<GridCoordinate> ConsumeDirtyCells()
        {
            var result = new List<GridCoordinate>(_dirtyCells);
            _dirtyCells.Clear();
            return result;
        }

        private RoadNode GetOrCreateNode(GridCoordinate cell)
        {
            if (!_nodes.TryGetValue(cell, out var node))
            {
                node = new RoadNode(cell);
                _nodes[cell] = node;
            }

            return node;
        }

        private void ClearDirectionAndPruneNode(GridCoordinate cell, RoadDirection direction)
        {
            if (!_nodes.TryGetValue(cell, out var node))
            {
                return;
            }

            node.ConnectedDirectionsMask &= (byte)~(1 << (int)direction);
            if (node.ConnectedDirectionsMask == 0)
            {
                _nodes.Remove(cell);
            }
        }

        private static (GridCoordinate, GridCoordinate) CanonicalKey(GridCoordinate a, GridCoordinate b)
        {
            return IsBefore(a, b) ? (a, b) : (b, a);
        }

        private static bool IsBefore(GridCoordinate a, GridCoordinate b)
        {
            return a.X != b.X ? a.X < b.X : a.Z < b.Z;
        }
    }
}

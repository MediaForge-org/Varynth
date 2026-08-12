using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;
using Varynth.World.Roads;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class RoadGraphTests
    {
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        [Test]
        public void EmptyGraph_HasNoNodesOrSegments()
        {
            var graph = new RoadGraph();
            Assert.AreEqual(0, graph.Nodes.Count);
            Assert.AreEqual(0, graph.Segments.Count);
        }

        [Test]
        public void AddOrthogonalSegment_CreatesTwoNodes_WithConnectivity()
        {
            var graph = new RoadGraph();
            var from = new GridCoordinate(0, 0);
            var to = new GridCoordinate(1, 0);

            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, from, to, RoadDirection.E, PlayerId.None);

            Assert.AreEqual(2, graph.Nodes.Count);
            Assert.IsTrue(graph.TryGetNode(from, out var fromNode));
            Assert.IsTrue(fromNode.IsConnected(RoadDirection.E));
            Assert.IsTrue(graph.TryGetNode(to, out var toNode));
            Assert.IsTrue(toNode.IsConnected(RoadDirection.W));
            Assert.IsTrue(graph.HasSegmentBetween(from, to));
            Assert.IsTrue(graph.HasSegmentBetween(to, from));
        }

        [Test]
        public void AddDiagonalSegment_ConnectivityUsesDiagonalDirections()
        {
            var graph = new RoadGraph();
            var from = new GridCoordinate(0, 0);
            var to = new GridCoordinate(1, 1);

            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, from, to, RoadDirection.NE, PlayerId.None);

            graph.TryGetNode(from, out var fromNode);
            graph.TryGetNode(to, out var toNode);
            Assert.IsTrue(fromNode.IsConnected(RoadDirection.NE));
            Assert.IsTrue(toNode.IsConnected(RoadDirection.SW));
        }

        [Test]
        public void RemoveSegment_ClearsConnectivity_AndOrphanNodeIsPruned()
        {
            var graph = new RoadGraph();
            var from = new GridCoordinate(0, 0);
            var to = new GridCoordinate(1, 0);
            var id = RoadSegmentId.FromRaw(1);
            graph.AddSegment(id, RoadId, from, to, RoadDirection.E, PlayerId.None);

            Assert.IsTrue(graph.RemoveSegment(id, out var removed));
            Assert.AreEqual(id, removed.Id);
            Assert.AreEqual(0, graph.Nodes.Count, "Both endpoint nodes should be pruned once their connection count reaches zero.");
            Assert.IsFalse(graph.HasSegmentBetween(from, to));
        }

        [Test]
        public void RemoveSegment_NodeWithOtherConnections_StaysButLosesOnlyThatDirection()
        {
            var graph = new RoadGraph();
            var center = new GridCoordinate(5, 5);
            var east = new GridCoordinate(6, 5);
            var north = new GridCoordinate(5, 6);

            var eastId = RoadSegmentId.FromRaw(1);
            var northId = RoadSegmentId.FromRaw(2);
            graph.AddSegment(eastId, RoadId, center, east, RoadDirection.E, PlayerId.None);
            graph.AddSegment(northId, RoadId, center, north, RoadDirection.N, PlayerId.None);

            graph.RemoveSegment(eastId, out _);

            Assert.IsTrue(graph.TryGetNode(center, out var centerNode));
            Assert.IsFalse(centerNode.IsConnected(RoadDirection.E));
            Assert.IsTrue(centerNode.IsConnected(RoadDirection.N));
        }

        [Test]
        public void NegativeGlobalCoordinates_Work()
        {
            var graph = new RoadGraph();
            var from = new GridCoordinate(-50, -50);
            var to = new GridCoordinate(-49, -50);

            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, from, to, RoadDirection.E, PlayerId.None);

            Assert.IsTrue(graph.HasSegmentBetween(from, to));
        }

        [Test]
        public void Segment_PersistsDefinitionId()
        {
            var graph = new RoadGraph();
            var segment = graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, PlayerId.None);

            Assert.AreEqual(RoadId, segment.DefinitionId);
            Assert.IsTrue(graph.TryGetSegment(RoadSegmentId.FromRaw(1), out var fetched));
            Assert.AreEqual(RoadId, fetched.DefinitionId);
        }

        [Test]
        public void Deterministic_SameOperations_SameResultingGraphShape()
        {
            RoadGraph BuildGraph()
            {
                var g = new RoadGraph();
                g.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, PlayerId.None);
                g.AddSegment(RoadSegmentId.FromRaw(2), RoadId, new GridCoordinate(1, 0), new GridCoordinate(2, 1), RoadDirection.NE, PlayerId.None);
                return g;
            }

            var a = BuildGraph();
            var b = BuildGraph();

            Assert.AreEqual(a.Nodes.Count, b.Nodes.Count);
            Assert.AreEqual(a.Segments.Count, b.Segments.Count);
        }

        [Test]
        public void ConsumeDirtyCells_ReturnsTouchedCells_AndClears()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, PlayerId.None);

            var dirty = graph.ConsumeDirtyCells();
            Assert.AreEqual(2, dirty.Count);

            var secondCall = graph.ConsumeDirtyCells();
            Assert.AreEqual(0, secondCall.Count);
        }
    }
}

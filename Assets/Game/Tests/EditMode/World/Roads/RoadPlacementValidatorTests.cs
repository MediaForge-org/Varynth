using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class RoadPlacementValidatorTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => 0f;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = 0f; return true; }
        }

        private sealed class FuncHeightSource : IWorldHeightSource
        {
            private readonly System.Func<float, float, float> _fn;
            public FuncHeightSource(System.Func<float, float, float> fn) { _fn = fn; }
            public float GetHeightAt(float worldX, float worldZ) => _fn(worldX, worldZ);
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = _fn(worldX, worldZ); return true; }
        }

        private static WorldGrid Grid() => new WorldGrid(4f, Vector2.zero);

        private static RoadDefinition Road(bool allowsCoast = false) =>
            new RoadDefinition(ContentId.Parse("road.prototype.basic"), LocalizationKey.Parse("road.name"), "road", 1, true, allowsCoast);

        private static IslandSurfaceMap FlatBuildableMap(int width, int height)
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), width, height);
            for (var z = 0; z < height; z++)
            for (var x = 0; x < width; x++)
                map.SetFlags(new GridCoordinate(x, z), SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            return map;
        }

        [Test]
        public void ValidSegment_OnFlatBuildableGround_Passes()
        {
            var surface = FlatBuildableMap(10, 10);
            var graph = new RoadGraph();
            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig());

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Water_IsRejected()
        {
            var surface = FlatBuildableMap(10, 10);
            surface.SetFlags(new GridCoordinate(3, 2), SurfaceCellFlags.Water);
            var graph = new RoadGraph();

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & RoadPlacementIssue.Water) != 0);
        }

        [Test]
        public void Coast_RejectedUnlessAllowed()
        {
            var surface = FlatBuildableMap(10, 10);
            surface.SetFlags(new GridCoordinate(3, 2), SurfaceCellFlags.Coast);
            var graph = new RoadGraph();

            var rejected = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(allowsCoast: false), null, new RoadPlacementConfig());
            Assert.IsFalse(rejected.IsValid);
            Assert.IsTrue((rejected.Issues & RoadPlacementIssue.Coast) != 0);

            var accepted = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(allowsCoast: true), null, new RoadPlacementConfig());
            Assert.IsTrue(accepted.IsValid);
        }

        [Test]
        public void RockOrSteep_IsRejected()
        {
            var surface = FlatBuildableMap(10, 10);
            surface.SetFlags(new GridCoordinate(3, 2), SurfaceCellFlags.RockOrSteep);
            var graph = new RoadGraph();

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig());

            Assert.IsTrue((result.Issues & RoadPlacementIssue.RockOrSteep) != 0);
        }

        private sealed class AlwaysOccupied : Varynth.World.Placement.IBuildingOccupancyQuery
        {
            public bool IsCellOccupied(GridCoordinate cell) => true;
        }

        [Test]
        public void BuildingOccupied_IsRejected()
        {
            var surface = FlatBuildableMap(10, 10);
            var graph = new RoadGraph();

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), new AlwaysOccupied(), new RoadPlacementConfig());

            Assert.IsTrue((result.Issues & RoadPlacementIssue.BuildingOccupied) != 0);
        }

        [Test]
        public void DuplicateSegment_IsRejected_ForDirectValidationCall()
        {
            var surface = FlatBuildableMap(10, 10);
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), ContentId.Parse("road.prototype.basic"), new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig());

            Assert.IsTrue((result.Issues & RoadPlacementIssue.DuplicateSegment) != 0);
        }

        [Test]
        public void SteepHeightDelta_ExceedsThreshold_IsRejected()
        {
            var surface = FlatBuildableMap(10, 10);
            var graph = new RoadGraph();
            // Cell (2,2) center x=10, cell (3,2) center x=14 -- the threshold at 12
            // falls strictly between them so the two sampled heights actually differ.
            var heights = new FuncHeightSource((x, z) => x < 12f ? 0f : 100f);
            var config = new RoadPlacementConfig { MaxSegmentSlopeDegrees = 10f };

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(2, 2), new GridCoordinate(3, 2), RoadDirection.E,
                surface, graph, Grid(), heights, Road(), null, config);

            Assert.IsTrue((result.Issues & RoadPlacementIssue.SlopeTooSteep) != 0);
        }

        [Test]
        public void OutsideSurfaceMap_IsRejected()
        {
            var surface = FlatBuildableMap(4, 4);
            var graph = new RoadGraph();

            var result = RoadPlacementValidator.ValidateSegment(
                new GridCoordinate(100, 100), new GridCoordinate(101, 100), RoadDirection.E,
                surface, graph, Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & RoadPlacementIssue.OutsideSurfaceMap) != 0);
        }
    }
}

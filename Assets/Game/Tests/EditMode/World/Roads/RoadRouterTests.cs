using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class RoadRouterTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => 0f;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = 0f; return true; }
        }

        private sealed class AlwaysOccupied : IBuildingOccupancyQuery
        {
            private readonly System.Func<GridCoordinate, bool> _predicate;
            public AlwaysOccupied(System.Func<GridCoordinate, bool> predicate) { _predicate = predicate; }
            public bool IsCellOccupied(GridCoordinate cell) => _predicate(cell);
        }

        private static WorldGrid Grid() => new WorldGrid(4f, (0f, 0f));
        private static GridBounds Bounds() => new GridBounds(-20, -20, 40, 40);

        private static RoadDefinition Road() =>
            new RoadDefinition(ContentId.Parse("road.prototype.basic"), LocalizationKey.Parse("road.name"), "road", 1, true, false);

        private static IslandSurfaceMap FlatBuildableMap()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(-20, -20), 40, 40);
            for (var z = -20; z < 20; z++)
            for (var x = -20; x < 20; x++)
                map.SetFlags(new GridCoordinate(x, z), SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            return map;
        }

        [Test]
        public void HorizontalRoute_FindsDirectPath()
        {
            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(5, 0), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(6, path.Count);
        }

        [Test]
        public void VerticalRoute_FindsDirectPath()
        {
            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(0, 4), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(5, path.Count);
        }

        [Test]
        public void DiagonalRoute_UsesDiagonalSteps()
        {
            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(4, 4), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(5, path.Count, "Pure diagonal target should route in 4 diagonal steps, not a longer orthogonal zig-zag.");
        }

        [Test]
        public void MixedRoute_ReachesTarget()
        {
            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(6, 2), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(new GridCoordinate(0, 0), path[0]);
            Assert.AreEqual(new GridCoordinate(6, 2), path[path.Count - 1]);
        }

        [Test]
        public void SameStartAndEnd_DegenerateSingleCellPath()
        {
            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(3, 3), new GridCoordinate(3, 3), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(1, path.Count);
        }

        [Test]
        public void ReversedEndpoints_AlsoFindsARoute()
        {
            var forward = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(5, 3), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var forwardPath);
            var reversed = RoadRouter.TryFindRoute(
                new GridCoordinate(5, 3), new GridCoordinate(0, 0), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var reversedPath);

            Assert.IsTrue(forward);
            Assert.IsTrue(reversed);
            Assert.AreEqual(forwardPath.Count, reversedPath.Count);
        }

        [Test]
        public void WaterBlocksDirectPath_ButDetourIsFound()
        {
            var surface = FlatBuildableMap();
            for (var z = -5; z <= 5; z++)
            {
                surface.SetFlags(new GridCoordinate(2, z), SurfaceCellFlags.Water);
            }

            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(4, 0), Bounds(), surface, new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found, "A detour around the water wall should still be found.");
            foreach (var cell in path)
            {
                Assert.IsFalse(cell.X == 2 && cell.Z >= -5 && cell.Z <= 5, "Route must not cross the water wall.");
            }
        }

        [Test]
        public void FullyEnclosedByWater_NoRouteFound()
        {
            var surface = new IslandSurfaceMap(new GridCoordinate(-20, -20), 40, 40); // all None -- effectively all "Water-ish" (NotBuildable, but not literally Water flag; use explicit Water ring)
            for (var z = -20; z < 20; z++)
            for (var x = -20; x < 20; x++)
                surface.SetFlags(new GridCoordinate(x, z), SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);

            for (var dz = -1; dz <= 1; dz++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if (dx == 0 && dz == 0) continue;
                surface.SetFlags(new GridCoordinate(dx, dz), SurfaceCellFlags.Water);
            }

            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(10, 10), Bounds(), surface, new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out _);

            Assert.IsFalse(found, "Start cell fully enclosed by Water on every side must have no route out.");
        }

        [Test]
        public void BuildingCollision_BlocksDirectPath_DetourFound()
        {
            var occupancy = new AlwaysOccupied(c => c.X == 2 && c.Z >= -5 && c.Z <= 5);

            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(4, 0), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), new FlatHeightSource(), Road(), occupancy, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            foreach (var cell in path)
            {
                Assert.IsFalse(occupancy.IsCellOccupied(cell));
            }
        }

        [Test]
        public void SteepSlope_ExcludesCell_DetourFound()
        {
            // Cell (2,0)'s world center sits at x=10 (Origin 0 + (2+0.5)*4). Spike its
            // height so every edge touching it has an excessive slope relative to its
            // flat neighbors, forcing the router around it rather than through it.
            var heights = new FuncHeightSource((x, z) => Mathf.Approximately(x, 10f) && Mathf.Approximately(z, 2f) ? 500f : 0f);

            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(4, 0), Bounds(), FlatBuildableMap(), new RoadGraph(),
                Grid(), heights, Road(), null, new RoadPlacementConfig { MaxSegmentSlopeDegrees = 5f }, out var path);

            Assert.IsTrue(found, "A detour around the steep cell should still be found.");
            foreach (var cell in path)
            {
                Assert.IsFalse(cell.Equals(new GridCoordinate(2, 0)), "Route must avoid the excessively steep cell.");
            }
        }

        private sealed class FuncHeightSource : IWorldHeightSource
        {
            private readonly System.Func<float, float, float> _fn;
            public FuncHeightSource(System.Func<float, float, float> fn) { _fn = fn; }
            public float GetHeightAt(float worldX, float worldZ) => _fn(worldX, worldZ);
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = _fn(worldX, worldZ); return true; }
        }

        [Test]
        public void Deterministic_SameInputs_ProduceIdenticalPath()
        {
            IReadOnlyList<GridCoordinate> RunOnce()
            {
                RoadRouter.TryFindRoute(
                    new GridCoordinate(0, 0), new GridCoordinate(7, 5), Bounds(), FlatBuildableMap(), new RoadGraph(),
                    Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);
                return path;
            }

            var a = RunOnce();
            var b = RunOnce();

            CollectionAssert.AreEqual(a, b);
        }

        [Test]
        public void ExistingRoad_IsReused_NotDetouredAround()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), ContentId.Parse("road.prototype.basic"), new GridCoordinate(0, 0), new GridCoordinate(1, 0), RoadDirection.E, Varynth.Core.Simulation.Common.PlayerId.None);

            var found = RoadRouter.TryFindRoute(
                new GridCoordinate(0, 0), new GridCoordinate(1, 0), Bounds(), FlatBuildableMap(), graph,
                Grid(), new FlatHeightSource(), Road(), null, new RoadPlacementConfig(), out var path);

            Assert.IsTrue(found);
            Assert.AreEqual(2, path.Count, "Should use the existing single segment directly, not a longer detour.");
        }
    }
}

using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Placement
{
    public class PlacementValidatorTests
    {
        private sealed class FuncHeightSource : IWorldHeightSource
        {
            private readonly System.Func<float, float, float> _fn;
            public FuncHeightSource(System.Func<float, float, float> fn) { _fn = fn; }
            public float GetHeightAt(float worldX, float worldZ) => _fn(worldX, worldZ);
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = _fn(worldX, worldZ); return true; }
        }

        private static WorldGrid Grid() => new WorldGrid(4f, (0f, 0f));

        private static BuildingDefinition House(bool allowsCoast = false) =>
            new BuildingDefinition(ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house", allowsCoast);

        private static IslandSurfaceMap FlatBuildableMap(int width, int height)
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), width, height);
            for (var z = 0; z < height; z++)
            for (var x = 0; x < width; x++)
                map.SetFlags(new GridCoordinate(x, z), SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            return map;
        }

        [Test]
        public void Validate_AllBuildableCells_IsValid()
        {
            var surface = FlatBuildableMap(4, 4);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(PlacementIssue.None, result.Issues);
        }

        [Test]
        public void Validate_OneWaterCell_IsInvalid()
        {
            var surface = FlatBuildableMap(4, 4);
            surface.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Water);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.Water) != 0);
        }

        [Test]
        public void Validate_OneCoastCell_InvalidForNormalBuilding()
        {
            var surface = FlatBuildableMap(4, 4);
            surface.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Coast);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.Coast) != 0);
        }

        [Test]
        public void Validate_CoastCell_ValidWhenDefinitionAllowsCoast()
        {
            var surface = FlatBuildableMap(4, 4);
            surface.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Coast);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(allowsCoast: true), new PlacementConfig());

            Assert.IsFalse((result.Issues & PlacementIssue.Coast) != 0);
        }

        [Test]
        public void Validate_OneRockOrSteepCell_IsInvalid()
        {
            var surface = FlatBuildableMap(4, 4);
            surface.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Land | SurfaceCellFlags.RockOrSteep);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.RockOrSteep) != 0);
        }

        [Test]
        public void Validate_OneOccupiedCell_IsInvalid()
        {
            var surface = FlatBuildableMap(4, 4);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            occupancy.Occupy(new[] { new GridCoordinate(1, 1) }, BuildingInstanceId.FromRaw(1));
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.AlreadyOccupied) != 0);
        }

        [Test]
        public void Validate_FootprintPartlyOutsideSurfaceMap_IsInvalid()
        {
            var surface = FlatBuildableMap(2, 2);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 2, 2);
            var heights = new FuncHeightSource((x, z) => 5f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(1, 1), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.OutsideSurfaceMap) != 0);
        }

        [Test]
        public void Validate_HeightVariationOverThreshold_IsInvalid()
        {
            var surface = FlatBuildableMap(4, 4);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            // Cell centers are at x=2,6 / z=2,6 for a 2x2 footprint at origin (0,0), cell size 4.
            var heights = new FuncHeightSource((x, z) => x < 4f ? 0f : 10f);
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue((result.Issues & PlacementIssue.HeightVariationTooLarge) != 0);
        }

        [Test]
        public void Validate_HighButFlatArea_StillValid()
        {
            var surface = FlatBuildableMap(4, 4);
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 500f); // high but perfectly flat
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            var result = PlacementValidator.Validate(cells, surface, occupancy, heights, Grid(), House(), new PlacementConfig());

            Assert.IsTrue(result.IsValid, $"Expected valid, got issues: {result.Issues}");
        }

        [Test]
        public void Validate_Rotation_ChangesWhichCellsAreChecked()
        {
            var surface = FlatBuildableMap(4, 4);
            surface.SetFlags(new GridCoordinate(0, 2), SurfaceCellFlags.Water); // only hit by the 90-degree footprint
            var occupancy = new IslandOccupancyMap(new GridCoordinate(0, 0), 4, 4);
            var heights = new FuncHeightSource((x, z) => 5f);

            var deg0Cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg0);
            var deg90Cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg90);

            var definition = new BuildingDefinition(ContentId.Parse("bld.prototype.production_block"), LocalizationKey.Parse("bld.production.name"), 3, 2, "production");
            var deg0Result = PlacementValidator.Validate(deg0Cells, surface, occupancy, heights, Grid(), definition, new PlacementConfig());
            var deg90Result = PlacementValidator.Validate(deg90Cells, surface, occupancy, heights, Grid(), definition, new PlacementConfig());

            Assert.IsTrue(deg0Result.IsValid);
            Assert.IsFalse(deg90Result.IsValid);
        }
    }
}

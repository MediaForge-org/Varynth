using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class SurfaceOverlayMeshBuilderTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => 0f;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = 0f; return true; }
        }

        private static WorldGrid Grid() => new WorldGrid(4f, (0f, 0f));

        [Test]
        public void BuildCategoryLineMesh_UsesLineTopology_NotFilledTriangles()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 2, 1);
            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Buildable);

            var source = new SurfaceOverlayMeshBuilder.IslandSurfaceSource(
                map, new RectInt(0, 0, 2, 1), new FlatHeightSource());
            var mesh = SurfaceOverlayMeshBuilder.BuildCategoryLineMesh(Grid(), new[] { source }, SurfaceCellFlags.Buildable, 0.05f);

            Assert.AreEqual(MeshTopology.Lines, mesh.GetTopology(0),
                "The Player Placement Grid must be thin line geometry, not a filled quad -- a solid fill covering a whole island reads as flooding.");
            Assert.AreEqual(1, mesh.subMeshCount);
        }

        [Test]
        public void BuildCategoryLineMesh_OnlyIncludesQualifyingCells()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 3, 1);
            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Buildable);
            map.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Water);
            map.SetFlags(new GridCoordinate(2, 0), SurfaceCellFlags.Buildable);

            var source = new SurfaceOverlayMeshBuilder.IslandSurfaceSource(
                map, new RectInt(0, 0, 3, 1), new FlatHeightSource());
            var mesh = SurfaceOverlayMeshBuilder.BuildCategoryLineMesh(Grid(), new[] { source }, SurfaceCellFlags.Buildable, 0.05f);

            // Exactly 2 qualifying cells -> 4 corner vertices + 4 edges (8 indices) each.
            Assert.AreEqual(2 * 4, mesh.vertexCount);
            Assert.AreEqual(2 * 8, mesh.GetIndices(0).Length);
        }

        [Test]
        public void BuildCategoryLineMesh_NoQualifyingCells_ProducesEmptyMesh()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 2, 1);
            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Water);
            map.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Water);

            var source = new SurfaceOverlayMeshBuilder.IslandSurfaceSource(
                map, new RectInt(0, 0, 2, 1), new FlatHeightSource());
            var mesh = SurfaceOverlayMeshBuilder.BuildCategoryLineMesh(Grid(), new[] { source }, SurfaceCellFlags.Buildable, 0.05f);

            Assert.AreEqual(0, mesh.vertexCount);
        }

        [Test]
        public void BuildCategoryMesh_StillFilledTriangles_UnaffectedByLineMeshAddition()
        {
            // Regression guard: the F2 debug Buildable/Coast/RockOrSteep overlay
            // (a developer tool, not the player-facing Placement Grid) must keep
            // using the original filled-quad builder unchanged.
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 1, 1);
            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Buildable);

            var source = new SurfaceOverlayMeshBuilder.IslandSurfaceSource(
                map, new RectInt(0, 0, 1, 1), new FlatHeightSource());
            var mesh = SurfaceOverlayMeshBuilder.BuildCategoryMesh(Grid(), new[] { source }, SurfaceCellFlags.Buildable, 0.05f);

            Assert.AreEqual(MeshTopology.Triangles, mesh.GetTopology(0));
            Assert.Greater(mesh.triangles.Length, 0);
        }
    }
}

using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World
{
    public class GridCellMeshBuilderTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            private readonly float _height;

            public FlatHeightSource(float height)
            {
                _height = height;
            }

            public float GetHeightAt(float worldX, float worldZ) => _height;

            public bool TryGetHeight(float worldX, float worldZ, out float height)
            {
                height = _height;
                return true;
            }
        }

        [Test]
        public void BuildCellQuad_VerticesCoverExactCellCorners()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var heights = new FlatHeightSource(10f);
            var cell = new GridCoordinate(2, 1);

            var mesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset: 0f);

            var xs = new[] { 8f, 12f };
            var zs = new[] { 4f, 8f };
            foreach (var vertex in mesh.vertices)
            {
                Assert.Contains(vertex.x, xs);
                Assert.Contains(vertex.z, zs);
                Assert.AreEqual(10f, vertex.y, 1e-4f);
            }

            Object.DestroyImmediate(mesh);
        }

        [Test]
        public void BuildCellQuad_AppliesHeightOffset()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var heights = new FlatHeightSource(5f);
            var cell = new GridCoordinate(0, 0);

            var mesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset: 0.5f);

            foreach (var vertex in mesh.vertices)
            {
                Assert.AreEqual(5.5f, vertex.y, 1e-4f);
            }

            Object.DestroyImmediate(mesh);
        }

        [Test]
        public void BuildCellQuad_NormalPointsUpward()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var heights = new FlatHeightSource(0f);
            var cell = new GridCoordinate(0, 0);

            var mesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset: 0f);

            Assert.Greater(mesh.normals.Length, 0);
            foreach (var normal in mesh.normals)
            {
                Assert.Greater(normal.y, 0.9f, "Expected the highlight quad to face upward so it is visible from a downward-looking camera.");
            }

            Object.DestroyImmediate(mesh);
        }

        [Test]
        public void BuildCellQuad_FollowsSlopedTerrain_CornersAtDifferentHeights()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var heights = new SlopedHeightSource();
            var cell = new GridCoordinate(0, 0);

            var mesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset: 0f);

            var distinctHeights = new System.Collections.Generic.HashSet<float>();
            foreach (var vertex in mesh.vertices)
            {
                distinctHeights.Add(vertex.y);
            }

            Assert.Greater(distinctHeights.Count, 1, "Expected corner heights to differ on sloped terrain rather than being flattened.");

            Object.DestroyImmediate(mesh);
        }

        private sealed class SlopedHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => worldX + worldZ;

            public bool TryGetHeight(float worldX, float worldZ, out float height)
            {
                height = worldX + worldZ;
                return true;
            }
        }
    }
}

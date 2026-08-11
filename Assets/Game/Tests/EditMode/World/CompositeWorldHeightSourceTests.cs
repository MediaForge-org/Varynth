using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World
{
    public class CompositeWorldHeightSourceTests
    {
        private GameObject _islandAGo;
        private GameObject _islandBGo;
        private UnityEngine.Terrain _islandA;
        private UnityEngine.Terrain _islandB;

        [SetUp]
        public void SetUp()
        {
            _islandA = CreateFlatTerrain("IslandA", new Vector3(0f, -15f, 0f), new Vector3(100f, 40f, 100f), 0.5f, out _islandAGo);
            _islandB = CreateFlatTerrain("IslandB", new Vector3(500f, -10f, 500f), new Vector3(80f, 40f, 80f), 0.6f, out _islandBGo);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_islandAGo);
            Object.DestroyImmediate(_islandBGo);
        }

        private static UnityEngine.Terrain CreateFlatTerrain(string name, Vector3 position, Vector3 size, float normalizedHeight, out GameObject go)
        {
            var data = new TerrainData
            {
                heightmapResolution = 33,
                size = size
            };

            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            {
                for (var x = 0; x < 33; x++)
                {
                    heights[y, x] = normalizedHeight;
                }
            }
            data.SetHeights(0, 0, heights);

            go = new GameObject(name);
            var terrain = go.AddComponent<UnityEngine.Terrain>();
            terrain.terrainData = data;
            go.transform.position = position;
            return terrain;
        }

        private CompositeWorldHeightSource BuildComposite()
        {
            var sources = new List<UnityTerrainHeightSource>
            {
                new UnityTerrainHeightSource(_islandA),
                new UnityTerrainHeightSource(_islandB)
            };
            return new CompositeWorldHeightSource(sources);
        }

        [Test]
        public void TryGetHeight_InsideIslandA_ReturnsIslandAHeight()
        {
            var composite = BuildComposite();

            var found = composite.TryGetHeight(50f, 50f, out var height);

            Assert.IsTrue(found);
            Assert.AreEqual(-15f + 0.5f * 40f, height, 0.01f);
        }

        [Test]
        public void TryGetHeight_InsideIslandB_ReturnsIslandBHeight()
        {
            var composite = BuildComposite();

            var found = composite.TryGetHeight(540f, 540f, out var height);

            Assert.IsTrue(found);
            Assert.AreEqual(-10f + 0.6f * 40f, height, 0.01f);
        }

        [Test]
        public void TryGetHeight_OutsideAllIslands_ReturnsFalse()
        {
            var composite = BuildComposite();

            var found = composite.TryGetHeight(250f, 250f, out _);

            Assert.IsFalse(found);
        }

        [Test]
        public void GetHeightAt_OutsideAllIslands_Throws()
        {
            var composite = BuildComposite();

            Assert.Throws<System.InvalidOperationException>(() => composite.GetHeightAt(250f, 250f));
        }

        [Test]
        public void GetHeightAt_InsideAnIsland_DoesNotThrowAndMatchesTryGetHeight()
        {
            var composite = BuildComposite();

            var value = composite.GetHeightAt(50f, 50f);

            Assert.AreEqual(-15f + 0.5f * 40f, value, 0.01f);
        }
    }
}

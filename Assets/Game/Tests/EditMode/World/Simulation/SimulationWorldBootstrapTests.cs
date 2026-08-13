using NUnit.Framework;
using UnityEngine;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Simulation;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Simulation
{
    // The one place UnityEngine.Terrain is still sampled for authoritative purposes
    // (Phase 2E point 12) -- deliberately kept separate from the headless
    // ManagedSimulation tests, which construct SimulationIslandData directly and
    // never need Terrain/GameObject at all.
    public class SimulationWorldBootstrapTests
    {
        private GameObject _terrainGo;

        [TearDown]
        public void TearDown()
        {
            if (_terrainGo != null) Object.DestroyImmediate(_terrainGo);
        }

        private Terrain CreateFlatTerrain(float worldHeight)
        {
            var data = new TerrainData { heightmapResolution = 33, size = new Vector3(40f, 40f, 40f) };
            var heights = new float[33, 33];
            var normalized = (worldHeight + 15f) / 40f; // matches TerrainVerticalSize=40 / TerrainTransformY=-15 convention
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = normalized;
            data.SetHeights(0, 0, heights);

            _terrainGo = new GameObject("BootstrapTestTerrain");
            var terrain = _terrainGo.AddComponent<Terrain>();
            terrain.terrainData = data;
            _terrainGo.transform.position = new Vector3(0f, -15f, 0f);
            return terrain;
        }

        private static IslandSurfaceRuntimeData CreateRuntimeData(int originX, int originZ, int width, int height)
        {
            var data = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[width * height];
            for (var i = 0; i < flags.Length; i++) flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            data.SetData(originX, originZ, width, height, flags);
            return data;
        }

        [Test]
        public void Build_BakesFlagsAndHeights_MatchingRealTerrainSample()
        {
            var terrain = CreateFlatTerrain(5f);
            var runtimeData = CreateRuntimeData(0, 0, 5, 5);
            var grid = new WorldGrid(4f, (0f, 0f));

            var worldData = SimulationWorldBootstrap.Build(grid, new[] { new SimulationWorldBootstrap.IslandSource("BootstrapTestTerrain", runtimeData, terrain) });

            Assert.AreEqual(1, worldData.Islands.Count);
            var island = worldData.Islands[0];
            Assert.AreEqual(5, island.Width);
            Assert.AreEqual(5, island.Height);
            Assert.AreEqual(25, island.Flags.Count);
            Assert.AreEqual(25, island.CellHeights.Count);

            foreach (var flags in island.Flags)
            {
                Assert.AreEqual(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable, flags);
            }

            foreach (var height in island.CellHeights)
            {
                Assert.AreEqual(5f, height, 0.1f, "Baked cell height should match the flat real terrain's sampled height.");
            }
        }

        [Test]
        public void Build_StableIslandId_DerivedFromName_NotArrayPosition()
        {
            var terrainA = CreateFlatTerrain(5f);
            var runtimeDataA = CreateRuntimeData(0, 0, 3, 3);
            var grid = new WorldGrid(4f, (0f, 0f));

            var firstOrder = SimulationWorldBootstrap.Build(grid, new[]
            {
                new SimulationWorldBootstrap.IslandSource("StableNameIsland", runtimeDataA, terrainA)
            });

            var idFromFirstBuild = firstOrder.Islands[0].Id;
            var idAgain = Varynth.Core.Common.IslandId.FromName("StableNameIsland");

            Assert.AreEqual(idAgain, idFromFirstBuild, "IslandId must be derivable purely from the stable name, independent of build/array order.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.World.Grid;
using Varynth.World.Interaction;

namespace Varynth.Tests.PlayMode
{
    public class WorldPointerTests
    {
        private const string SceneName = "WorldPrototype";

        // Matches WorldPrototypeIslands.GetDefaultConfigs(): TestIsland_Large is
        // centered at world (0,0) with a 260x260 footprint.
        private static readonly Vector3 IslandLargeAbovePoint = new Vector3(0f, 200f, 0f);

        // TestIsland_Medium is centered at world (520,40) with a 170x170 footprint.
        private static readonly Vector3 IslandMediumAbovePoint = new Vector3(520f, 200f, 40f);

        // Clearly outside every configured island's footprint -- open ocean.
        private static readonly Vector3 OpenWaterAbovePoint = new Vector3(250f, 200f, 250f);

        private static List<Collider> FindAllTerrainColliders()
        {
            var terrains = Object.FindObjectsByType<UnityEngine.Terrain>(FindObjectsSortMode.None);
            var colliders = new List<Collider>();
            foreach (var terrain in terrains)
            {
                var collider = terrain.GetComponent<TerrainCollider>();
                if (collider != null)
                {
                    colliders.Add(collider);
                }
            }
            return colliders;
        }

        [UnityTest]
        public IEnumerator Raycast_FromAboveIslandLarge_HitsValidWorldPositionAndCell()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var colliders = FindAllTerrainColliders();
            Assert.GreaterOrEqual(colliders.Count, 2, "Expected multiple island terrain colliders in the scene.");

            var grid = new WorldGrid(4f, Vector2.zero);
            var pointer = new WorldPointer(grid, colliders);

            var ray = new Ray(IslandLargeAbovePoint, Vector3.down);
            var hit = pointer.TryRaycast(ray, out var worldPosition);

            Assert.IsTrue(hit, "Expected the ray to hit TestIsland_Large's terrain.");
            Assert.Greater(worldPosition.y, -20f);
            Assert.Less(worldPosition.y, 30f);

            var cell = pointer.ToCell(worldPosition);
            var roundtrippedCenter = grid.CellToWorldCenter(cell);

            Assert.Less(Mathf.Abs(roundtrippedCenter.x - worldPosition.x), 4f);
            Assert.Less(Mathf.Abs(roundtrippedCenter.y - worldPosition.z), 4f);
        }

        [UnityTest]
        public IEnumerator Raycast_FromAboveIslandMedium_HitsThatIslandsTerrain()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var colliders = FindAllTerrainColliders();
            var grid = new WorldGrid(4f, Vector2.zero);
            var pointer = new WorldPointer(grid, colliders);

            var ray = new Ray(IslandMediumAbovePoint, Vector3.down);
            var hit = pointer.TryRaycast(ray, out var worldPosition);

            Assert.IsTrue(hit, "Expected the ray to hit TestIsland_Medium's terrain.");
            Assert.Less(Mathf.Abs(worldPosition.x - IslandMediumAbovePoint.x), 90f);
            Assert.Less(Mathf.Abs(worldPosition.z - IslandMediumAbovePoint.z), 90f);
        }

        [UnityTest]
        public IEnumerator Raycast_OverOpenWaterBetweenIslands_HitsNothing()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var colliders = FindAllTerrainColliders();
            var grid = new WorldGrid(4f, Vector2.zero);
            var pointer = new WorldPointer(grid, colliders);

            var ray = new Ray(OpenWaterAbovePoint, Vector3.down);
            var hit = pointer.TryRaycast(ray, out _);

            Assert.IsFalse(hit, "Expected no terrain hit over open water between islands.");
        }

        [UnityTest]
        public IEnumerator Raycast_IgnoresOtherColliders_OnlyTargetsRegisteredTerrainColliders()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var colliders = FindAllTerrainColliders();

            // Place an unrelated blocking collider directly in front of TestIsland_Large,
            // closer to the ray origin -- if WorldPointer used a generic Physics.Raycast, it
            // would hit this first. It must be ignored, proving the raycast unambiguously
            // targets only the registered terrain colliders.
            var blocker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            blocker.transform.position = new Vector3(0f, 100f, 0f);
            blocker.transform.localScale = new Vector3(20f, 20f, 20f);

            try
            {
                var grid = new WorldGrid(4f, Vector2.zero);
                var pointer = new WorldPointer(grid, colliders);

                var ray = new Ray(IslandLargeAbovePoint, Vector3.down);
                var hit = pointer.TryRaycast(ray, out var worldPosition);

                Assert.IsTrue(hit);
                Assert.Less(worldPosition.y, 90f, "Expected the hit to pass through the blocker and land on the terrain.");
            }
            finally
            {
                Object.Destroy(blocker);
            }
        }
    }
}

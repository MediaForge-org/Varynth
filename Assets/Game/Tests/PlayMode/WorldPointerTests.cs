using System.Collections;
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

        [UnityTest]
        public IEnumerator Raycast_FromAboveTerrainCenter_HitsValidWorldPositionAndCell()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var terrain = Object.FindFirstObjectByType<UnityEngine.Terrain>();
            Assert.IsNotNull(terrain, "Expected the generated TestIsland terrain to be present.");
            var terrainCollider = terrain.GetComponent<TerrainCollider>();
            Assert.IsNotNull(terrainCollider, "Expected the terrain to have a TerrainCollider.");

            var grid = new WorldGrid(4f, Vector2.zero);
            var pointer = new WorldPointer(grid, terrainCollider);

            var ray = new Ray(new Vector3(150f, 200f, 150f), Vector3.down);

            var hit = pointer.TryRaycast(ray, out var worldPosition);

            Assert.IsTrue(hit, "Expected the ray to hit the generated terrain.");
            Assert.Greater(worldPosition.y, -20f);
            Assert.Less(worldPosition.y, 30f);

            var cell = pointer.ToCell(worldPosition);
            var roundtrippedCenter = grid.CellToWorldCenter(cell);

            Assert.Less(Mathf.Abs(roundtrippedCenter.x - worldPosition.x), 4f);
            Assert.Less(Mathf.Abs(roundtrippedCenter.y - worldPosition.z), 4f);
        }

        [UnityTest]
        public IEnumerator Raycast_IgnoresOtherColliders_OnlyTargetsAssignedTerrainCollider()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var terrain = Object.FindFirstObjectByType<UnityEngine.Terrain>();
            var terrainCollider = terrain.GetComponent<TerrainCollider>();

            // Place an unrelated blocking collider directly in front of the terrain, closer to
            // the ray origin -- if WorldPointer used a generic Physics.Raycast, it would hit this
            // first. It must be ignored, proving the raycast unambiguously targets only the
            // assigned terrain collider.
            var blocker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            blocker.transform.position = new Vector3(150f, 100f, 150f);
            blocker.transform.localScale = new Vector3(20f, 20f, 20f);

            try
            {
                var grid = new WorldGrid(4f, Vector2.zero);
                var pointer = new WorldPointer(grid, terrainCollider);

                var ray = new Ray(new Vector3(150f, 200f, 150f), Vector3.down);
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

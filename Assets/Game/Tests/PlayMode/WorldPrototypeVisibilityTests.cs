using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Varynth.Tests.PlayMode
{
    // Regression coverage for the 0.1.1 framing blockers: pointing the camera rig at the
    // archipelago's bounds center, or having a TerrainCollider's full rectangular AABB
    // intersect the frustum, is NOT sufficient proof an island is actually visible -- each
    // terrain tile's AABB includes a large submerged "skirt" far beyond its own emerged
    // coastline, so that AABB can trivially intersect the frustum while the actual
    // above-water landmass sits outside the viewport. These assertions instead sample
    // real above-sea-level points on each island's actual terrain data (matching how the
    // scene builder itself now derives initial framing) and project them through the real
    // camera after at least one real PlayMode frame has run.
    public class WorldPrototypeVisibilityTests
    {
        private const string SceneName = "WorldPrototype";
        private const int SamplesPerAxis = 10;
        private const float SeaLevelWorldY = 0f;

        [UnityTest]
        public IEnumerator Archipelago_IsInFrontOfCamera_AfterFirstFrame()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var camera = Camera.main;
            Assert.IsNotNull(camera, "Main Camera missing");

            var archipelagoBounds = ComputeArchipelagoWorldBounds();
            var toCenter = (archipelagoBounds.center - camera.transform.position).normalized;
            var dot = Vector3.Dot(camera.transform.forward, toCenter);

            Assert.Greater(dot, 0f, "Archipelago bounds center must be in front of the camera at scene start.");
        }

        [UnityTest]
        public IEnumerator MultipleAboveWaterIslandAreas_AreVisibleInViewport_AfterFirstFrame()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var camera = Camera.main;
            Assert.IsNotNull(camera, "Main Camera missing");

            var terrains = Object.FindObjectsByType<Terrain>(FindObjectsSortMode.None);
            Assert.GreaterOrEqual(terrains.Length, 2, "Expected multiple island Terrains.");

            var islandsWithVisibleLand = 0;
            var islandsWithAnyAboveWaterSample = 0;

            foreach (var terrain in terrains)
            {
                if (HasAboveWaterSample(terrain))
                {
                    islandsWithAnyAboveWaterSample++;
                }

                if (HasVisibleAboveWaterPoint(terrain, camera))
                {
                    islandsWithVisibleLand++;
                }
            }

            Assert.Greater(islandsWithAnyAboveWaterSample, 0,
                "Sanity check: at least one island must have sampled above-sea-level points at all.");
            Assert.GreaterOrEqual(islandsWithVisibleLand, 2,
                "At least two islands must have a real above-sea-level point actually inside the camera " +
                "viewport at scene start -- proves initial framing shows real land, not just that a huge " +
                "terrain-tile AABB happens to graze the frustum.");
        }

        private static bool HasAboveWaterSample(Terrain terrain)
        {
            return SampleAboveWaterWorldPoints(terrain).Count > 0;
        }

        private static bool HasVisibleAboveWaterPoint(Terrain terrain, Camera camera)
        {
            foreach (var world in SampleAboveWaterWorldPoints(terrain))
            {
                var viewport = camera.WorldToViewportPoint(world);
                if (viewport.z > 0f && viewport.x >= 0f && viewport.x <= 1f && viewport.y >= 0f && viewport.y <= 1f)
                {
                    return true;
                }
            }

            return false;
        }

        private static System.Collections.Generic.List<Vector3> SampleAboveWaterWorldPoints(Terrain terrain)
        {
            var points = new System.Collections.Generic.List<Vector3>();
            var data = terrain.terrainData;
            var origin = terrain.transform.position;

            for (var ix = 0; ix < SamplesPerAxis; ix++)
            {
                for (var iz = 0; iz < SamplesPerAxis; iz++)
                {
                    var u = ix / (float)(SamplesPerAxis - 1);
                    var v = iz / (float)(SamplesPerAxis - 1);
                    var worldY = origin.y + data.GetInterpolatedHeight(u, v);
                    if (worldY <= SeaLevelWorldY) continue;

                    var worldX = origin.x + u * data.size.x;
                    var worldZ = origin.z + v * data.size.z;
                    points.Add(new Vector3(worldX, worldY, worldZ));
                }
            }

            return points;
        }

        private static Bounds ComputeArchipelagoWorldBounds()
        {
            var terrainColliders = Object.FindObjectsByType<TerrainCollider>(FindObjectsSortMode.None);
            Assert.Greater(terrainColliders.Length, 0, "Expected at least one TerrainCollider in the scene.");

            var bounds = terrainColliders[0].bounds;
            for (var i = 1; i < terrainColliders.Length; i++)
            {
                bounds.Encapsulate(terrainColliders[i].bounds);
            }

            return bounds;
        }
    }
}

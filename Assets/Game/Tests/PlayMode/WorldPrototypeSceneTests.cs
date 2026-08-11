using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation.Camera;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Visualization;

namespace Varynth.Tests.PlayMode
{
    public class WorldPrototypeSceneTests
    {
        private const string SceneName = "WorldPrototype";

        [UnityTest]
        public IEnumerator Scene_Loads_AndContainsExpectedHierarchy()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var scene = SceneManager.GetActiveScene();
            Assert.IsTrue(scene.IsValid());
            Assert.AreEqual(SceneName, scene.name);

            Assert.IsNotNull(UnityEngine.Camera.main, "Main Camera missing");
            Assert.IsNotNull(Object.FindFirstObjectByType<StrategyCameraController>(), "StrategyCameraController missing");
            Assert.IsNotNull(Object.FindFirstObjectByType<WorldInteractionController>(), "WorldInteractionController missing");

            var terrains = Object.FindObjectsByType<UnityEngine.Terrain>(FindObjectsSortMode.None);
            Assert.GreaterOrEqual(terrains.Length, 2, "Expected multiple island Terrain objects, not a single mega-terrain.");
            foreach (var terrain in terrains)
            {
                Assert.IsNotNull(terrain.terrainData, $"{terrain.name} has no TerrainData assigned");
                Assert.IsNotNull(terrain.GetComponent<TerrainCollider>(), $"{terrain.name} has no TerrainCollider");
            }

            // Grid overlay (1) + Buildable/Coast/RockOrSteep surface overlays (3).
            var gridDisplays = Object.FindObjectsByType<GridDisplay>(FindObjectsSortMode.None);
            Assert.GreaterOrEqual(gridDisplays.Length, 4, "Expected the debug grid plus 3 surface-overlay categories.");

            Assert.IsNotNull(Object.FindFirstObjectByType<GridCellHighlight>(), "GridCellHighlight missing");
            Assert.IsNotNull(Object.FindFirstObjectByType<ResourceCandidateMarkers>(), "ResourceCandidateMarkers missing");
            Assert.IsNotNull(GameObject.Find("Water"), "Water GameObject missing");
        }

        [UnityTest]
        public IEnumerator Scene_HasNoMissingScripts()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var component in root.GetComponentsInChildren<Component>(true))
                {
                    Assert.IsNotNull(component, $"Missing script found under '{root.name}'");
                }
            }
        }
    }
}

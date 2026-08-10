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

            var terrain = Object.FindFirstObjectByType<UnityEngine.Terrain>();
            Assert.IsNotNull(terrain, "Terrain (TestIsland) missing");
            Assert.IsNotNull(terrain.terrainData, "TestIsland has no TerrainData assigned");

            Assert.IsNotNull(Object.FindFirstObjectByType<GridDisplay>(), "GridDisplay missing");
            Assert.IsNotNull(Object.FindFirstObjectByType<GridCellHighlight>(), "GridCellHighlight missing");
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

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation.Visualization;

namespace Varynth.Tests.PlayMode
{
    public class GridDisplayTests
    {
        private const string SceneName = "WorldPrototype";

        [UnityTest]
        public IEnumerator GridRoot_DoesNotUseOneGameObjectPerCell()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var gridDisplay = Object.FindFirstObjectByType<GridDisplay>();
            Assert.IsNotNull(gridDisplay);

            var gridRoot = gridDisplay.gameObject;
            Assert.Less(gridRoot.transform.childCount, 10, "Grid root should not contain one GameObject per cell/line.");

            var meshFilter = gridRoot.GetComponent<MeshFilter>();
            Assert.IsNotNull(meshFilter);
            Assert.IsNotNull(meshFilter.sharedMesh);
            Assert.Greater(meshFilter.sharedMesh.vertexCount, 0);
        }

        [UnityTest]
        public IEnumerator SetVisible_TogglesRendererEnabled_AfterRealSceneLoad()
        {
            // Regression test for the G-toggle bug: GridDisplay's MeshFilter/MeshRenderer
            // references were previously only ever populated by the Editor-time Initialize()
            // call, never re-fetched at actual Play Mode start, so SetVisible() silently no-op'd
            // on a real scene load. This exercises SetVisible() against the genuinely loaded
            // scene (not a freshly-constructed GameObject) to prove Awake() now supplies them.
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var gridDisplay = Object.FindFirstObjectByType<GridDisplay>();
            Assert.IsNotNull(gridDisplay);
            var meshRenderer = gridDisplay.GetComponent<MeshRenderer>();
            Assert.IsNotNull(meshRenderer);

            var initiallyVisible = meshRenderer.enabled;
            Assert.IsTrue(initiallyVisible, "Grid should be visible by default.");

            gridDisplay.SetVisible(false);
            Assert.IsFalse(meshRenderer.enabled);

            gridDisplay.SetVisible(true);
            Assert.IsTrue(meshRenderer.enabled);
        }
    }
}

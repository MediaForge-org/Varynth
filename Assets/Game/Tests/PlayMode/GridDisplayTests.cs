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

        // Phase 2B reuses GridDisplay for the surface-overlay categories too (hidden by
        // default), so FindFirstObjectByType<GridDisplay>() is no longer specific enough --
        // tests about "the" debug grid must target the "Grid" root by name.
        private static GridDisplay FindGridDisplay()
        {
            var gridGo = GameObject.Find("Grid");
            Assert.IsNotNull(gridGo, "Expected a 'Grid' root GameObject.");
            var display = gridGo.GetComponent<GridDisplay>();
            Assert.IsNotNull(display, "Expected the 'Grid' root to carry a GridDisplay component.");
            return display;
        }

        [UnityTest]
        public IEnumerator GridRoot_DoesNotUseOneGameObjectPerCell()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            var gridDisplay = FindGridDisplay();

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

            var gridDisplay = FindGridDisplay();
            var meshRenderer = gridDisplay.GetComponent<MeshRenderer>();
            Assert.IsNotNull(meshRenderer);

            // 0.2.2 hotfix: this assertion previously required the grid to be visible
            // by default -- that default was itself the real bug behind a reported
            // "fine grid stays visible with no tool active" regression (confirmed via
            // real runtime instrumentation + a real captured screenshot: this exact
            // Debug Grid was the only grid-related renderer enabled at scene start).
            // WorldInteractionController now establishes a verified-hidden runtime
            // baseline; only an explicit G press reveals it.
            var initiallyVisible = meshRenderer.enabled;
            Assert.IsFalse(initiallyVisible, "Grid should be hidden by default -- only an explicit G press reveals it.");

            gridDisplay.SetVisible(true);
            Assert.IsTrue(meshRenderer.enabled);

            gridDisplay.SetVisible(false);
            Assert.IsFalse(meshRenderer.enabled);
        }

        [UnityTest]
        public IEnumerator SurfaceOverlayDisplays_AreHiddenByDefault()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;

            foreach (var name in new[] { "Buildable", "Coast", "RockOrSteep" })
            {
                var go = GameObject.Find(name);
                Assert.IsNotNull(go, $"Expected a surface overlay category GameObject named '{name}'.");
                var display = go.GetComponent<GridDisplay>();
                Assert.IsNotNull(display);
                var renderer = go.GetComponent<MeshRenderer>();
                Assert.IsFalse(renderer.enabled, $"Expected '{name}' overlay to be hidden by default (toggled via F2).");
            }
        }
    }
}

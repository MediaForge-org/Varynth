using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Placement;

namespace Varynth.Tests.PlayMode
{
    public class PlacementGridVisibilityTests : InputTestFixture
    {
        private const string SceneName = "WorldPrototype";

        private Keyboard _keyboard;
        private Mouse _mouse;

        public override void Setup()
        {
            base.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            _mouse = InputSystem.AddDevice<Mouse>();
        }

        [UnityTest]
        public IEnumerator OutsidePlacementMode_AllPlacementGridsDisabled()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var root = GameObject.Find("PlacementGrids");
            Assert.IsNotNull(root);
            foreach (Transform child in root.transform)
            {
                Assert.IsFalse(child.GetComponent<MeshRenderer>().enabled);
            }
        }

        [UnityTest]
        public IEnumerator InsidePlacementMode_OnlyHoveredIslandGridEnabled_IndependentOfDebugGrid()
        {
            // See PlacementAcceptanceTests for why this is needed -- an environmental
            // InputTestFixture/native-runtime interaction, not a real error.
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;

            var height = worldInteraction.HeightSource.TryGetHeight(0f, 0f, out var y) ? y : 0f;
            var screenPoint = camera.WorldToScreenPoint(new Vector3(0f, height, 0f));
            Set(_mouse.position, new Vector2(screenPoint.x, screenPoint.y));
            yield return null;

            // Toggle the unrelated developer debug grid (G) -- must not affect placement grids.
            PressAndRelease(_keyboard.gKey);
            yield return null;

            PressAndRelease(_keyboard.digit1Key);
            yield return null;
            yield return null;

            var root = GameObject.Find("PlacementGrids");
            var enabledCount = 0;
            foreach (Transform child in root.transform)
            {
                if (child.GetComponent<MeshRenderer>().enabled)
                {
                    enabledCount++;
                    Assert.AreEqual("TestIsland_Large", child.name, "Only the hovered island's grid should be visible");
                }
            }

            Assert.AreEqual(1, enabledCount, "Exactly one island's placement grid should be visible while hovering it");
        }

        [UnityTest]
        public IEnumerator FullToolLifecycle_GridVisibilityStaysCorrectAtEveryStep()
        {
            // Real regression for a fixed bug: a stale Player Placement Grid could
            // remain visible after Escape/tool-switch because each controller only
            // re-issued visibility requests when its OWN cached hovered-island index
            // changed -- state the ConstructionToolCoordinator (the actual, sole
            // owner) never saw. Exercises the full mandated sequence: start hidden,
            // Building active (only current island), Escape (all hidden), Road
            // active (only current island), Escape (all hidden), Building<->Road
            // switches (never two grids simultaneously), debug grid independence.
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;
            var gridsRoot = GameObject.Find("PlacementGrids");
            Assert.IsNotNull(gridsRoot);

            var height = worldInteraction.HeightSource.TryGetHeight(0f, 0f, out var y) ? y : 0f;
            var screenPoint = camera.WorldToScreenPoint(new Vector3(0f, height, 0f));
            Set(_mouse.position, new Vector2(screenPoint.x, screenPoint.y));
            yield return null;

            // 1. Start -> all hidden.
            AssertVisibleGridCount(gridsRoot, 0, "initial load");

            // 2. Building Tool active -> exactly one (the hovered island's) visible.
            PressAndRelease(_keyboard.digit1Key);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 1, "Building tool active");

            // 3. Escape -> all hidden again.
            PressAndRelease(_keyboard.escapeKey);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 0, "after Escape from Building tool");

            // 4. Road Tool active -> exactly one visible.
            PressAndRelease(_keyboard.digit4Key);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 1, "Road tool active");

            // 5. Escape -> all hidden again (no route was started, so this fully deactivates).
            PressAndRelease(_keyboard.escapeKey);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 0, "after Escape from Road tool");

            // 6. Building -> Road switch -> never two grids simultaneously, exactly one after settling.
            PressAndRelease(_keyboard.digit1Key);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 1, "Building tool active before switch");
            PressAndRelease(_keyboard.digit4Key);
            yield return null; // check the very next frame, before the road tool has necessarily re-evaluated hover
            Assert.LessOrEqual(CountVisibleGrids(gridsRoot), 1, "Never more than one Player Placement Grid visible during a tool switch.");
            yield return null;
            AssertVisibleGridCount(gridsRoot, 1, "Road tool active after Building->Road switch settles");

            // 7. Road -> Building switch -> same guarantee.
            PressAndRelease(_keyboard.digit1Key);
            yield return null;
            Assert.LessOrEqual(CountVisibleGrids(gridsRoot), 1, "Never more than one Player Placement Grid visible during a tool switch.");
            yield return null;
            AssertVisibleGridCount(gridsRoot, 1, "Building tool active after Road->Building switch settles");

            // 8. Force ConstructionToolMode.None -> every renderer disabled.
            PressAndRelease(_keyboard.escapeKey);
            yield return null; yield return null;
            AssertVisibleGridCount(gridsRoot, 0, "forced back to ConstructionToolMode.None");

            // 9. Debug Grid (G) stays fully independent -- toggling it must never
            // affect Player Placement Grid visibility, in either direction.
            var debugGrid = GameObject.Find("Grid");
            var debugGridRenderer = debugGrid != null ? debugGrid.GetComponent<MeshRenderer>() : null;
            Assert.IsNotNull(debugGridRenderer, "Expected a debug Grid renderer to check independence against.");
            var debugGridStateBefore = debugGridRenderer.enabled;
            PressAndRelease(_keyboard.gKey);
            yield return null;
            Assert.AreNotEqual(debugGridStateBefore, debugGridRenderer.enabled, "G should toggle the debug grid.");
            AssertVisibleGridCount(gridsRoot, 0, "Player Placement Grids unaffected by toggling the debug grid");
        }

        private static void AssertVisibleGridCount(GameObject gridsRoot, int expectedCount, string context)
        {
            var actual = CountVisibleGrids(gridsRoot);
            Assert.AreEqual(expectedCount, actual, $"Expected {expectedCount} visible Player Placement Grid(s) ({context}), found {actual}.");
        }

        private static int CountVisibleGrids(GameObject gridsRoot)
        {
            var count = 0;
            foreach (Transform child in gridsRoot.transform)
            {
                if (child.GetComponent<MeshRenderer>().enabled)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

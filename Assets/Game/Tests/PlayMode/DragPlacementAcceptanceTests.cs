using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Placement;

namespace Varynth.Tests.PlayMode
{
    // Real simulated Mouse press/move/release via InputTestFixture -- not direct
    // method calls -- against the real WorldPrototype scene's DragRepeat house.
    public class DragPlacementAcceptanceTests : InputTestFixture
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
        public IEnumerator DragRepeatHouse_PressMoveRelease_PlacesMultipleBuildings()
        {
            LogAssert.ignoreFailingMessages = true; // same documented InputTestFixture/native-runtime noise as PlacementAcceptanceTests

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<PlacementController>();
            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var camera = Camera.main;
            Assert.IsNotNull(controller);
            Assert.IsNotNull(worldInteraction);
            Assert.IsNotNull(driver);

            // House (bld.prototype.house) is DragRepeat -- centered on TestIsland_Large (world (0,0)).
            yield return HoverWorldPosition(camera, worldInteraction, -20f, 0f);
            PressAndRelease(_keyboard.digit1Key);
            yield return null;
            yield return null;
            Assert.IsTrue(controller.IsPlacing);

            var placedCountBefore = CountPlacedBuildings();

            // Press and hold, then drag the mouse across several cells before releasing.
            Press(_mouse.leftButton);
            yield return null;
            yield return null;

            yield return HoverWorldPosition(camera, worldInteraction, -4f, 0f);

            var previewGo = GameObject.Find("DragPreview");
            Assert.IsNotNull(previewGo, "DragPreview GameObject missing");
            var previewRenderer = previewGo.GetComponent<MeshRenderer>();
            Assert.IsTrue(previewRenderer.enabled, "Multi-ghost drag preview should be visible while dragging");

            Release(_mouse.leftButton);
            yield return null;
            driver.Simulation.AdvanceTicks(1);
            yield return null;
            yield return null;

            var placedCountAfter = CountPlacedBuildings();
            Assert.Greater(placedCountAfter, placedCountBefore + 1, "A drag across several cells should place more than one building via a single batch.");
            Assert.IsFalse(previewRenderer.enabled, "Drag preview should hide after the batch commits");

            // A Single-behavior building (production block) must still place
            // immediately on a plain click -- no regression from Phase 2C.
            yield return HoverWorldPosition(camera, worldInteraction, -20f, 20f);
            PressAndRelease(_keyboard.digit2Key);
            yield return null;
            yield return null;

            var beforeSingle = CountPlacedBuildings();
            PressAndRelease(_mouse.leftButton);
            yield return null;
            driver.Simulation.AdvanceTicks(1);
            yield return null;
            yield return null;

            Assert.AreEqual(beforeSingle + 1, CountPlacedBuildings(), "Single-behavior building must still place on a plain click, not require a drag.");
        }

        private IEnumerator HoverWorldPosition(Camera camera, WorldInteractionController worldInteraction, float worldX, float worldZ)
        {
            var height = worldInteraction.HeightSource.TryGetHeight(worldX, worldZ, out var y) ? y : 0f;
            var screenPoint = camera.WorldToScreenPoint(new Vector3(worldX, height, worldZ));

            Set(_mouse.position, new Vector2(screenPoint.x, screenPoint.y));
            yield return null;
            yield return null;
        }

        private static int CountPlacedBuildings()
        {
            var root = GameObject.Find("PlacedBuildings");
            return root == null ? 0 : root.transform.childCount;
        }
    }
}

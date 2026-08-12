using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Roads;

namespace Varynth.Tests.PlayMode
{
    // Real simulated Keyboard/Mouse via InputTestFixture -- not direct method calls.
    public class RoadAcceptanceTests : InputTestFixture
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
        public IEnumerator RoadTool_SelectStartMovePreviewConfirm_BuildsRoad()
        {
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<RoadPlacementController>();
            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;
            Assert.IsNotNull(controller);
            Assert.IsNotNull(worldInteraction);

            Assert.IsFalse(controller.IsActive, "Should start with the Road tool inactive");

            PressAndRelease(_keyboard.digit4Key);
            yield return null;
            yield return null;
            Assert.IsTrue(controller.IsActive, "Expected the Road tool active after pressing 4");

            var segmentCountBefore = CountSegments(controller);

            yield return HoverWorldPosition(camera, worldInteraction, -20f, 0f);
            PressAndRelease(_mouse.leftButton); // sets start
            yield return null;
            yield return null;

            yield return HoverWorldPosition(camera, worldInteraction, -12f, 0f);

            var previewGo = GameObject.Find("RoadPreview");
            Assert.IsNotNull(previewGo, "RoadPreview GameObject missing");
            var previewRenderer = previewGo.GetComponent<MeshRenderer>();
            Assert.IsTrue(previewRenderer.enabled, "Route preview should be visible once a start cell is set and a route exists");

            PressAndRelease(_mouse.leftButton); // confirms
            yield return null;
            yield return null;

            var segmentCountAfter = CountSegments(controller);
            Assert.Greater(segmentCountAfter, segmentCountBefore, "Confirming the route should add real segments to the road graph");
            Assert.IsFalse(previewRenderer.enabled, "Preview should hide after a confirmed build (back to no start cell)");
        }

        [UnityTest]
        public IEnumerator EscapeBeforeConfirm_CancelsWithoutChangingGraph()
        {
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<RoadPlacementController>();
            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;

            PressAndRelease(_keyboard.digit4Key);
            yield return null;
            yield return null;

            var segmentCountBefore = CountSegments(controller);

            yield return HoverWorldPosition(camera, worldInteraction, -20f, 0f);
            PressAndRelease(_mouse.leftButton); // sets start
            yield return null;
            yield return null;

            PressAndRelease(_keyboard.escapeKey); // cancels the in-progress route (still tool-active)
            yield return null;
            yield return null;

            Assert.AreEqual(segmentCountBefore, CountSegments(controller), "Escape before confirm must not change the road graph");

            PressAndRelease(_keyboard.escapeKey); // second Escape deselects the tool entirely
            yield return null;
            yield return null;
            Assert.IsFalse(controller.IsActive);
        }

        [UnityTest]
        public IEnumerator RemoveHoveredSegment_WhenNoToolActive_RemovesSegment()
        {
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<RoadPlacementController>();
            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;

            PressAndRelease(_keyboard.digit4Key);
            yield return null;
            yield return null;

            yield return HoverWorldPosition(camera, worldInteraction, -20f, 0f);
            PressAndRelease(_mouse.leftButton);
            yield return null;
            yield return null;
            yield return HoverWorldPosition(camera, worldInteraction, -12f, 0f);
            PressAndRelease(_mouse.leftButton);
            yield return null;
            yield return null;

            var segmentCountAfterBuild = CountSegments(controller);
            Assert.Greater(segmentCountAfterBuild, 0);

            // Deselect the tool entirely (coordinator mode None) before removal is allowed.
            PressAndRelease(_keyboard.escapeKey);
            yield return null;
            yield return null;
            Assert.IsFalse(controller.IsActive);

            yield return HoverWorldPosition(camera, worldInteraction, -20f, 0f);
            PressAndRelease(_keyboard.deleteKey);
            yield return null;
            yield return null;

            Assert.Less(CountSegments(controller), segmentCountAfterBuild, "Delete while no tool is active should remove the hovered segment");
        }

        private IEnumerator HoverWorldPosition(Camera camera, WorldInteractionController worldInteraction, float worldX, float worldZ)
        {
            var height = worldInteraction.HeightSource.TryGetHeight(worldX, worldZ, out var y) ? y : 0f;
            var screenPoint = camera.WorldToScreenPoint(new Vector3(worldX, height, worldZ));

            Set(_mouse.position, new Vector2(screenPoint.x, screenPoint.y));
            yield return null;
            yield return null;
        }

        private static int CountSegments(RoadPlacementController controller)
        {
            var total = 0;
            for (var i = 0; i < controller.State.IslandCount; i++)
            {
                total += controller.State.GetGraph(i).Segments.Count;
            }

            return total;
        }
    }
}

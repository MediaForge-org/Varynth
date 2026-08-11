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
    }
}

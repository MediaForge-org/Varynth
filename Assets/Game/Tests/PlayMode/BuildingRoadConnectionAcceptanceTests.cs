using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Presentation;
using Varynth.Presentation.Interaction;

namespace Varynth.Tests.PlayMode
{
    // One real, scene-level end-to-end confirmation that a building placed adjacent
    // to a real placed road reports connected (and not before the road exists) --
    // the bulk of connection-logic edge cases live in the faster
    // BuildingRoadConnectionQueryTests (EditMode).
    //
    // Phase 2E: reads connectivity through ISimulationPlacementQueries.
    // IsBuildingConnectedToRoad (an authoritative-state-derived read), never a live
    // RoadNetworkState -- Presentation no longer holds one.
    public class BuildingRoadConnectionAcceptanceTests : InputTestFixture
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
        public IEnumerator BuildingNextToRoad_ReportsConnected_OnlyAfterRoadExists()
        {
            LogAssert.ignoreFailingMessages = true;

            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var worldInteraction = Object.FindFirstObjectByType<WorldInteractionController>();
            var camera = Camera.main;
            var simulation = driver.Simulation;

            // Place a house at world (0,0).
            yield return HoverWorldPosition(camera, worldInteraction, 0f, 0f);
            PressAndRelease(_keyboard.digit1Key);
            yield return null;
            yield return null;
            var hoveredCell = worldInteraction.HoveredCell.Value;
            PressAndRelease(_mouse.leftButton);
            yield return null;
            driver.Simulation.AdvanceTicks(1); // force the placement to land deterministically, not relying on real elapsed frame time reaching the fixed tick rate
            yield return null;
            yield return null;

            Assert.IsTrue(simulation.TryGetOccupantAt(hoveredCell, out var instanceId), "Expected the house to have landed at the hovered cell by now.");
            Assert.IsFalse(simulation.IsBuildingConnectedToRoad(instanceId), "No road exists yet -- must report not connected.");

            PressAndRelease(_keyboard.escapeKey);
            yield return null;
            yield return null;

            // Build a short road segment directly adjacent to the house's footprint:
            // the house was placed hovering world (0,0) -> cell (0,0), a 2x2 footprint
            // occupying cells (0,0)-(1,1), so its east edge is at cell x=1. World x=9
            // falls in cell x=2 (range [8,12)), directly east-adjacent to that edge.
            PressAndRelease(_keyboard.digit4Key);
            yield return null;
            yield return null;
            yield return HoverWorldPosition(camera, worldInteraction, 9f, 0f);
            PressAndRelease(_mouse.leftButton);
            yield return null;
            yield return null;
            yield return HoverWorldPosition(camera, worldInteraction, 13f, 0f);
            PressAndRelease(_mouse.leftButton);
            yield return null;
            driver.Simulation.AdvanceTicks(1); // force the road build to land deterministically
            yield return null;
            yield return null;

            Assert.IsTrue(simulation.IsBuildingConnectedToRoad(instanceId), "A real adjacent road should now report the building as connected.");
        }

        private IEnumerator HoverWorldPosition(Camera camera, WorldInteractionController worldInteraction, float worldX, float worldZ)
        {
            var height = worldInteraction.HeightSource.TryGetHeight(worldX, worldZ, out var y) ? y : 0f;
            var screenPoint = camera.WorldToScreenPoint(new Vector3(worldX, height, worldZ));

            Set(_mouse.position, new Vector2(screenPoint.x, screenPoint.y));
            yield return null;
            yield return null;
        }
    }
}

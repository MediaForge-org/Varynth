using NUnit.Framework;
using UnityEngine;
using Varynth.Presentation;
using Varynth.Presentation.Visualization;

namespace Varynth.Tests.EditMode.Presentation
{
    /// <summary>
    /// Regression coverage for the ConstructionToolCoordinator being the sole owner
    /// of Player Placement Grid visibility (real bug fixed here: a stale grid from
    /// the previously active tool could survive both a tool switch and a full
    /// deactivation, because each controller only re-issued
    /// RequestPlacementGridVisibility when its OWN cached last-hovered-island index
    /// changed -- state the coordinator never saw and that could easily desync).
    /// </summary>
    public class ConstructionToolCoordinatorTests
    {
        private sealed class FakeTool : ConstructionToolCoordinator.IConstructionTool
        {
            public int CancelCount { get; private set; }
            public void CancelTool() => CancelCount++;
        }

        private static GridDisplay[] MakeGrids(int count)
        {
            var grids = new GridDisplay[count];
            for (var i = 0; i < count; i++)
            {
                var go = new GameObject($"PlacementGrid_{i}");
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>();
                grids[i] = go.AddComponent<GridDisplay>();
                grids[i].Initialize(new Mesh());
                grids[i].SetVisible(true); // deliberately start "wrong" (visible) to prove the coordinator corrects it
            }
            return grids;
        }

        private static bool[] EnabledStates(GridDisplay[] grids)
        {
            var states = new bool[grids.Length];
            for (var i = 0; i < grids.Length; i++)
            {
                states[i] = grids[i].GetComponent<MeshRenderer>().enabled;
            }
            return states;
        }

        [Test]
        public void HideAllPlacementGrids_DisablesEveryRenderer()
        {
            var coordinator = new ConstructionToolCoordinator();
            var grids = MakeGrids(3);
            coordinator.SetPlacementGrids(grids);

            coordinator.HideAllPlacementGrids();

            foreach (var enabled in EnabledStates(grids))
            {
                Assert.IsFalse(enabled);
            }
        }

        [Test]
        public void RequestActivate_ForcesHiddenBaseline_EvenIfPreviousToolLeftAGridVisible()
        {
            var coordinator = new ConstructionToolCoordinator();
            var grids = MakeGrids(2);
            coordinator.SetPlacementGrids(grids);
            coordinator.RegisterBuildingTool(new FakeTool());
            coordinator.RegisterRoadTool(new FakeTool());

            // Simulate the exact real-world bug: island 0's grid is left visible by
            // something outside the coordinator's own bookkeeping (e.g. a controller
            // whose cached hover index never changed).
            grids[0].SetVisible(true);

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);

            foreach (var enabled in EnabledStates(grids))
            {
                Assert.IsFalse(enabled, "RequestActivate must force every grid hidden before the newly active tool re-evaluates its own hover.");
            }
        }

        [Test]
        public void RequestDeactivate_ForcesAllHidden()
        {
            var coordinator = new ConstructionToolCoordinator();
            var grids = MakeGrids(2);
            coordinator.SetPlacementGrids(grids);
            coordinator.RegisterBuildingTool(new FakeTool());

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);
            coordinator.RequestPlacementGridVisibility(1); // simulate the tool showing island 1 while active
            Assert.IsTrue(grids[1].GetComponent<MeshRenderer>().enabled, "Sanity: grid 1 should be visible while active.");

            coordinator.RequestDeactivate();

            foreach (var enabled in EnabledStates(grids))
            {
                Assert.IsFalse(enabled);
            }
            Assert.AreEqual(ConstructionToolCoordinator.ConstructionToolMode.None, coordinator.ActiveMode);
        }

        [Test]
        public void SwitchingTools_NeverLeavesTwoGridsVisibleSimultaneously()
        {
            var coordinator = new ConstructionToolCoordinator();
            var grids = MakeGrids(3);
            coordinator.SetPlacementGrids(grids);
            coordinator.RegisterBuildingTool(new FakeTool());
            coordinator.RegisterRoadTool(new FakeTool());

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);
            coordinator.RequestPlacementGridVisibility(0);
            AssertExactlyOneOrNoneVisible(grids, expectedVisible: 0);

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Road);
            // Immediately after the switch (before the road tool has re-evaluated its
            // own hover), no grid must be visible -- never a leftover building-tool
            // grid overlapping a new road-tool grid.
            AssertExactlyOneOrNoneVisible(grids, expectedVisible: null);

            coordinator.RequestPlacementGridVisibility(2);
            AssertExactlyOneOrNoneVisible(grids, expectedVisible: 2);

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);
            AssertExactlyOneOrNoneVisible(grids, expectedVisible: null);
        }

        [Test]
        public void ConstructionToolModeNone_EveryPlacementGridRendererDisabled()
        {
            var coordinator = new ConstructionToolCoordinator();
            var grids = MakeGrids(4);
            coordinator.SetPlacementGrids(grids);
            coordinator.RegisterBuildingTool(new FakeTool());

            coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);
            coordinator.RequestPlacementGridVisibility(2);
            coordinator.RequestDeactivate();

            Assert.AreEqual(ConstructionToolCoordinator.ConstructionToolMode.None, coordinator.ActiveMode);
            foreach (var enabled in EnabledStates(grids))
            {
                Assert.IsFalse(enabled);
            }
        }

        private static void AssertExactlyOneOrNoneVisible(GridDisplay[] grids, int? expectedVisible)
        {
            var visibleCount = 0;
            var visibleIndex = -1;
            for (var i = 0; i < grids.Length; i++)
            {
                if (grids[i].GetComponent<MeshRenderer>().enabled)
                {
                    visibleCount++;
                    visibleIndex = i;
                }
            }

            Assert.LessOrEqual(visibleCount, 1, "Never more than one Player Placement Grid visible at once.");
            if (expectedVisible.HasValue)
            {
                Assert.AreEqual(expectedVisible.Value, visibleIndex);
            }
        }
    }
}

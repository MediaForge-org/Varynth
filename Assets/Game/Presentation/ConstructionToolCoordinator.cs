using Varynth.Presentation.Visualization;

namespace Varynth.Presentation
{
    /// <summary>
    /// Small, additive central coordinator for "which construction tool is active".
    /// PlacementController and RoadPlacementController never reference each other or
    /// each other's GridDisplays directly -- both go through this coordinator, which
    /// is the single arbiter of tool switching/cancellation and Player Placement Grid
    /// visibility (the grid is a placement-mode-wide concept, not owned by either
    /// individual tool). Additively extensible: a future Demolition/Farm/Harbor tool
    /// only adds one more ConstructionToolMode value and one more registered
    /// controller, no restructuring of the existing two.
    /// </summary>
    public sealed class ConstructionToolCoordinator
    {
        public enum ConstructionToolMode
        {
            None,
            Building,
            Road
        }

        public interface IConstructionTool
        {
            void CancelTool();
        }

        private IConstructionTool _buildingTool;
        private IConstructionTool _roadTool;
        private GridDisplay[] _placementGrids;

        public ConstructionToolMode ActiveMode { get; private set; } = ConstructionToolMode.None;

        public void RegisterBuildingTool(IConstructionTool tool)
        {
            _buildingTool = tool;
        }

        public void RegisterRoadTool(IConstructionTool tool)
        {
            _roadTool = tool;
        }

        public void SetPlacementGrids(GridDisplay[] placementGrids)
        {
            _placementGrids = placementGrids;
        }

        /// <summary>
        /// Cancels whichever tool was previously active (if different) before
        /// switching, and unconditionally forces every Player Placement Grid hidden
        /// first (root cause of a prior bug: a stale grid from the previously active
        /// tool could survive a switch, because each controller only re-issues
        /// RequestPlacementGridVisibility when ITS OWN cached last-hovered-island
        /// index changes -- a value the coordinator has no visibility into and that
        /// can easily desync across tools/sessions). Forcing hidden here, centrally,
        /// removes any dependency on that per-controller cache being in sync.
        /// </summary>
        public void RequestActivate(ConstructionToolMode mode)
        {
            if (ActiveMode == mode)
            {
                return;
            }

            CancelActiveToolOnly();
            HideAllPlacementGrids();
            ActiveMode = mode;
        }

        public void RequestDeactivate()
        {
            CancelActiveToolOnly();
            ActiveMode = ConstructionToolMode.None;
            HideAllPlacementGrids();
        }

        /// <summary>
        /// The only method allowed to leave every Player Placement Grid renderer
        /// disabled -- called on every mode transition (RequestActivate,
        /// RequestDeactivate) so ConstructionToolMode.None (or mid-switch) is always
        /// backed by a real, verified-hidden runtime renderer state, never just an
        /// assumption based on scene-build-time defaults or a controller's own cache.
        /// </summary>
        public void HideAllPlacementGrids()
        {
            RequestPlacementGridVisibility(-1);
        }

        private void CancelActiveToolOnly()
        {
            switch (ActiveMode)
            {
                case ConstructionToolMode.Building:
                    _buildingTool?.CancelTool();
                    break;
                case ConstructionToolMode.Road:
                    _roadTool?.CancelTool();
                    break;
            }
        }

        /// <summary>
        /// The single place that toggles the shared Player Placement Grid array --
        /// only the given island's grid is visible, every other island's (and open
        /// water) stays hidden. Pass -1 to hide all.
        /// </summary>
        public void RequestPlacementGridVisibility(int islandIndex)
        {
            if (_placementGrids == null)
            {
                return;
            }

            for (var i = 0; i < _placementGrids.Length; i++)
            {
                if (_placementGrids[i] != null)
                {
                    _placementGrids[i].SetVisible(i == islandIndex);
                }
            }
        }
    }
}

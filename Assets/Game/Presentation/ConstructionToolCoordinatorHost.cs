using UnityEngine;
using Varynth.Presentation.Visualization;

namespace Varynth.Presentation
{
    /// <summary>
    /// Thin MonoBehaviour host for the plain-C# ConstructionToolCoordinator, so the
    /// scene builder has a real Unity Object to wire the shared Player Placement
    /// Grid array onto (the coordinator itself is deliberately not a MonoBehaviour --
    /// it has no per-frame behavior of its own, just shared arbitration state).
    /// PlacementController/RoadPlacementController find this host once in Start()
    /// and call Configure(host.Coordinator).
    /// </summary>
    public sealed class ConstructionToolCoordinatorHost : MonoBehaviour
    {
        [SerializeField] private GridDisplay[] _placementGrids;

        public ConstructionToolCoordinator Coordinator { get; } = new ConstructionToolCoordinator();

        private void Awake()
        {
            Coordinator.SetPlacementGrids(_placementGrids);
            // Verified-hidden runtime baseline, independent of whatever the saved
            // scene asset's renderer.enabled defaults happen to be -- the coordinator
            // owns this state, not the scene builder.
            Coordinator.HideAllPlacementGrids();
        }
    }
}

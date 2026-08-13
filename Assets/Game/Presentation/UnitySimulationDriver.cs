using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Varynth.Core.Simulation.Boundary;
using Varynth.Core.Simulation.Common;
using Varynth.Data.Loading;
using Varynth.Presentation.Interaction;
using Varynth.World.Placement;
using Varynth.World.Simulation;

namespace Varynth.Presentation
{
    /// <summary>
    /// The one Unity-specific bridge (Phase 2E) -- everything it touches
    /// (Time.unscaledDeltaTime, Terrain, IslandSurfaceRuntimeData, StreamingAssets
    /// XML loading) is deliberately kept here, never inside ManagedSimulation, which
    /// stays fully engine-free. Owns the one ManagedSimulation instance; other
    /// Presentation components find this via FindFirstObjectByType in their own
    /// Start() (same idiom already used for ConstructionToolCoordinatorHost) and read
    /// Simulation through the narrow ISimulationPlacementQueries/
    /// ISimulationRoadQueries/ISimulation interfaces, never the concrete type.
    ///
    /// Accumulates via Time.unscaledDeltaTime, not Time.deltaTime, so the global
    /// Time.timeScale (meant for VFX/slow-mo) never silently changes simulation
    /// pacing (point 11). SpeedMultiplier is architecture-ready for a future
    /// pause/1x/2x/4x control -- unwired to any UI in 0.2.3.
    /// </summary>
    public sealed class UnitySimulationDriver : MonoBehaviour
    {
        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private IslandSurfaceRuntimeData[] _islandSurfaceData;
        [SerializeField] private double _ticksPerSecond = 20.0;
        [SerializeField] private int _maxCatchUpTicksPerFrame = 10;

        private SimulationTickConfig _tickConfig;
        private double _accumulatedSeconds;
        private float _speedMultiplier = 1f;

        public ManagedSimulation Simulation { get; private set; }

        public float InterpolationAlpha { get; private set; }

        public float SpeedMultiplier
        {
            get => _speedMultiplier;
            set => _speedMultiplier = Mathf.Max(0f, value);
        }

        private void Awake()
        {
            _tickConfig = new SimulationTickConfig(_ticksPerSecond, _maxCatchUpTicksPerFrame);

            var terrains = _worldInteraction.Terrains ?? Array.Empty<Terrain>();
            var surfaceData = _islandSurfaceData ?? Array.Empty<IslandSurfaceRuntimeData>();
            var islandCount = Mathf.Min(terrains.Length, surfaceData.Length);

            var sources = new List<SimulationWorldBootstrap.IslandSource>(islandCount);
            for (var i = 0; i < islandCount; i++)
            {
                if (terrains[i] != null && surfaceData[i] != null)
                {
                    sources.Add(new SimulationWorldBootstrap.IslandSource(terrains[i].name, surfaceData[i], terrains[i]));
                }
            }

            var worldData = SimulationWorldBootstrap.Build(_worldInteraction.Grid, sources);

            var buildingContentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Buildings");
            var buildingRegistry = BuildingContentBootstrap.LoadRegistry(buildingContentRoot);

            var roadContentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Roads");
            var roadRegistry = RoadContentBootstrap.LoadRegistry(roadContentRoot);

            // Session/profile-init-time PlayerId generation -- the one documented-safe
            // use of PlayerId.NewId(), same rationale as the previous per-controller
            // calls it replaces, now consolidated to exactly one call/one identity.
            var localPlayerId = PlayerId.NewId();

            Simulation = new ManagedSimulation(worldData, buildingRegistry, roadRegistry, localPlayerId);
        }

        private void Update()
        {
            _accumulatedSeconds += Time.unscaledDeltaTime * _speedMultiplier;

            var dueTicks = FixedTickAccumulator.ComputeDueTicks(_accumulatedSeconds, _tickConfig, out var remaining);
            _accumulatedSeconds = remaining;

            if (dueTicks > 0)
            {
                Simulation.AdvanceTicks(dueTicks);
            }

            InterpolationAlpha = FixedTickAccumulator.ComputeInterpolationAlpha(_accumulatedSeconds, _tickConfig);
        }
    }
}

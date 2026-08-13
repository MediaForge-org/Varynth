# Phase 2E — Simulation Boundary & Fixed Tick Foundation (Varynth 0.2.3)

## Context

Since Phase 1C, `Varynth.Core.Simulation` existed as an engine-free assembly (`GameTick`/`GameClock`, `SimulationScheduler`/`ISimulationSystem`, `PlayerId`, `ISimulationCommand`) but was never actually *driven* — `GameClock` had no runtime loop, and every command (`PlaceBuildingCommand`, `BuildRoadCommand`, etc.) was applied synchronously, directly from Presentation `Update()` methods, with `GameTick.Zero` hardcoded everywhere. Both `PlacementController` and `RoadPlacementController` owned their respective World-state instance directly and read it for both validation *and* rendering.

Phase 2E closes that gap: an engine-free `ISimulation` contract, a concrete `ManagedSimulation` that owns the existing (unmodified-in-logic) state/command-handler objects, driven by a real fixed-tick Unity bridge. Confirmed mutations now go through `Submit()` → tick → `Snapshot` instead of a direct synchronous call. No gameplay was added; no C++ was introduced.

## Unity vs. Simulation Responsibility

Unity/Presentation still owns rendering, terrain, camera, input, UI, ghosts/previews, animations, VFX, audio. The authoritative gameplay state (currently: building placement, road network) sits behind `ISimulation`, fully engine-free, in `Varynth.Core.Simulation`. Unity never mutates that state directly — every confirmed placement/removal goes through `ISimulation.Submit(ISimulationCommand)`.

## Assembly Boundary — What Actually Moved

A real per-file `UnityEngine` audit (not assumed) found that of ~30 files touching placement/road state, only 6 referenced `UnityEngine` at all, and every reference was narrow: `Terrain` parameters, `RectInt`/`Vector2Int`, or `Mathf`/`Vector2` math. Everything else (`RoadGraph`, `RoadSegment`, `RoadNode`, `BuildingInstance`, `BuildingFootprint`, `PlacementValidator`, `RoadPlacementValidator`, `RoadRouter`, `BuildingPlacementCommandHandler`, `RoadCommandHandler`, `CornerCuttingRule`, `DiagonalCrossingRule`, etc.) was already engine-free.

- **`Varynth.Core.Common`** (inside `Varynth.Core.Definitions`, `noEngineReferences: true`) gained `WorldGrid` (refactored from `Vector2`/`Vector3`/`Mathf` to plain floats and `System.Math`), `GridBounds` (replaces `RectInt`), and `IslandId`.
- **`Varynth.Core.Simulation`** (`noEngineReferences: true`, unchanged reference set — still only `Varynth.Core.Definitions`) gained, as an in-place *move* (not a duplicate): `ArchipelagoPlacementState`, `RoadNetworkState` (both refactored: `AddIsland(SimulationIslandData, IWorldHeightSource)` replaces `AddIsland(IslandSurfaceRuntimeData, Terrain)`), `RoadGraph`, `RoadSegment`, `RoadNode`, `BuildingInstance`, `BuildingFootprint`, `BuildingRepeatPlanner`, `IslandOccupancyMap`, `IslandSurfaceMap`, all placement/road validators and command handlers, `SlopeEstimator`, `SurfaceCellFlags`, `IWorldHeightSource` — plus new: `ISimulation`, `ManagedSimulation`, `ISimulationPlacementQueries`, `ISimulationRoadQueries`, the snapshot/result/ticket types, `SimulationTickConfig`, `FixedTickAccumulator`, `SimulationStateDigest` support, `SimulationIslandData`/`SimulationWorldData`, `DenseGridHeightSource`.
- **`Varynth.World`** (unchanged reference set) *keeps* `IslandSurfaceRuntimeData` (a genuine `ScriptableObject`), `UnityTerrainHeightSource`, `CompositeWorldHeightSource`, `IslandHeightmapGenerator`, `SurfaceMapGenerator`, `SurfaceOverlayMeshBuilder`, `RoadMeshBuilder`, `GridCellMeshBuilder`/`GridMeshBuilder` — *gains* `SimulationWorldBootstrap`, the one adapter that still samples live `Terrain`.
- **`Varynth.Presentation`** gains `UnitySimulationDriver`; `PlacementController`/`RoadPlacementController` depend only on `ISimulationPlacementQueries`/`ISimulationRoadQueries`/`ISimulation`, never the concrete `ManagedSimulation` class.

No new assembly reference edges were added anywhere — this is a file *move* between already-connected assemblies.

**Height sampling split.** `PlacementValidator`/`RoadPlacementValidator`/`SlopeEstimator` were verified (by reading their source, not assumed) to only ever sample height at `grid.CellToWorldCenter(cell)` — never arbitrary continuous positions. This makes a dense **baked per-cell height array** (same shape as the existing per-cell `SurfaceCellFlags` array) a complete replacement for live `Terrain.SampleHeight` in the authoritative validation path — `DenseGridHeightSource`. Presentation-side mesh building (`RoadMeshBuilder`, `GridCellMeshBuilder`) needs true continuous sampling for smooth terrain-following and is untouched, still reading the live `UnityTerrainHeightSource` via `WorldInteractionController.HeightSource`.

## `ISimulation`

```csharp
public interface ISimulation
{
    GameTick CurrentTick { get; }
    PlayerId LocalPlayerId { get; }
    SimulationCommandTicket Submit(ISimulationCommand command);
    void AdvanceTicks(int tickCount);
    SimulationSnapshot GetSnapshot();
    IReadOnlyList<SimulationCommandResult> ConsumeResults();
    IReadOnlyList<BuildingCommandResult> ConsumeBuildingResults();
    IReadOnlyList<RoadCommandResult> ConsumeRoadResults();
}
```

Deliberately minimal — no `Initialize`/`Reset`/`Dispose`. Construction *is* initialization; no scenario-restart requirement exists yet; nothing holds a disposable resource.

`ISimulationPlacementQueries`/`ISimulationRoadQueries` are the narrow, engine-free, read-only surfaces Presentation depends on for preview/validation (`ValidateBuildingPlacement`, `TryFindRoadRoute`, `TryFindIslandIndex`, `TryGetOccupantAt`, `IsBuildingConnectedToRoad`, `GetRoadStateVersion`, `GetIslandId`). A future `NativeSimulationBridge` would implement the same three interfaces.

## `ManagedSimulation`

Lives in `Varynth.Core.Simulation` and coordinates the existing, unmodified `ArchipelagoPlacementState`/`RoadNetworkState`/`BuildingPlacementCommandHandler`/`RoadCommandHandler` — no validation/routing/mesh logic was duplicated. Its constructor consolidates what `PlacementController.Awake()`/`RoadPlacementController.Awake()` each used to do independently (island-loop, XML registries, `PlayerId.NewId()`), removing that duplication.

**Deterministic, fully injected bootstrap**: `ManagedSimulation` never loads XML and never calls `PlayerId.NewId()` itself — `SimulationWorldData`, both `ContentRegistry<T>` instances, and `localPlayerId` all come from the caller (`UnitySimulationDriver.Awake()` in practice; a plain object literal in headless tests).

## Command Flow — Per-Tick Processing

Every `Submit()` call wraps the command in an internal `CommandEnvelope { TargetTick; SubmitSequence; Ticket; Command }`, with `TargetTick = CurrentTick.Add(1)` by default — a command queued between ticks takes effect at the *next* tick, never "whenever `AdvanceTicks(n)` gets around to it". `AdvanceTicks(n)` loops `n` times; **each** iteration applies only the envelopes whose `TargetTick` equals the tick about to run (in ascending `SubmitSequence` order), then runs the (currently empty) `SimulationScheduler`, then advances `GameClock` — never "apply everything once, then run n empty ticks". This also sets up future lockstep-style multiplayer scheduling (a remote command could target a few ticks ahead for latency buffering) essentially for free.

Command dispatch inside `ManagedSimulation` is a plain `switch` on the 5 known concrete command types — a registry/reflection abstraction would be over-engineering at this size.

## Fixed Tick

```csharp
public sealed class SimulationTickConfig
{
    public double TicksPerSecond { get; } // prototype default 20.0 -- non-final
    public int MaxCatchUpTicksPerFrame { get; } // prototype default 10
}
```

`FixedTickAccumulator` (pure, static, tested without any MonoBehaviour — mirrors the `CameraRigMath` precedent) computes how many ticks are due given accumulated real time, clamping to `MaxCatchUpTicksPerFrame` and **discarding** excess accumulated time under that clamp (not carrying it forward) — the standard fixed-step spiral-of-death guard: under sustained overload, simulated time deliberately falls behind real time rather than the backlog growing without bound.

## `UnitySimulationDriver`

The one Unity-specific bridge. Accumulates via `Time.unscaledDeltaTime` (not `Time.deltaTime`, so the global `Time.timeScale` — meant for VFX/slow-mo — never silently changes simulation pacing), multiplied by an internal `SpeedMultiplier` (default 1.0, settable, unwired to any UI in 0.2.3 — architecture-ready for a future pause/1×/2×/4× control). Calls `Simulation.AdvanceTicks(dueTicks)` when due, and publishes `InterpolationAlpha` (0..1, computed and tested, but visually unused in 0.2.3 since buildings/roads are static — ready for future people/vehicles/ships).

Owns the single `ManagedSimulation` instance; `PlacementController`/`RoadPlacementController` find it via `FindFirstObjectByType<UnitySimulationDriver>()` in their own `Start()` — the same "shared host found by sibling controllers" idiom already used for `ConstructionToolCoordinatorHost`.

## Snapshots

```csharp
public sealed class SimulationSnapshot
{
    public GameTick Tick { get; }
    public int BuildingStateVersion { get; }
    public int RoadStateVersion { get; }
    public IReadOnlyList<BuildingRenderSnapshot> Buildings { get; }
    public IReadOnlyList<RoadRenderSnapshot> Roads { get; }
}
```

`Tick` advances every tick unconditionally; `BuildingStateVersion`/`RoadStateVersion` only increment when that domain actually changed — Presentation gates its diff/rebuild work on the `*Version` fields, not `Tick`. Real **double buffering**: each domain has its own pair of reusable `List<T>` buffers and its own active index, flipped only when that domain's content changed — a previously-returned `SimulationSnapshot`'s `Buildings`/`Roads` references therefore never mutate later, even while newer ticks keep writing into the other (currently inactive) buffer. A fresh `SimulationSnapshot` *wrapper* is always constructed on every `AdvanceTicks` call regardless (cheap — a handful of references, no data copy) so `Tick` stays current even when nothing else changed.

`BuildingRenderSnapshot`/`RoadRenderSnapshot` are readonly structs of small value types (`BuildingInstanceId`, `ContentId`, `IslandId`, `GridCoordinate`, `BuildingRotation`/`RoadDirection`, `PlayerId`) — no `Transform`/`GameObject`/`Mesh`/`Material`. `ContentId` is the one non-blittable field (string-backed), used deliberately as an ID, never a display string.

**No live `RoadGraph` in Presentation.** No separate node/topology snapshot type was needed: `RoadGraph.AddSegment` already derives node connectivity masks itself, so `RoadRuntimeMeshRefresh` reconstructs a disposable `RoadGraph` *replica* from `GetSnapshot().Roads` (via the same, unchanged `AddSegment` calls) only when `ISimulationRoadQueries.GetRoadStateVersion(islandIndex)` changes, then feeds that replica into the existing, unchanged `RoadMeshBuilder`. The live authoritative `RoadGraph` inside `ManagedSimulation` is never exposed to Presentation.

## Events / Results vs. Snapshots

Snapshots are the current renderable state. `ConsumeResults()`/`ConsumeBuildingResults()`/`ConsumeRoadResults()` are one-shot, drain-and-clear result lists correlated to a `SimulationCommandTicket` — no event-sourcing architecture was built, just this one clear split. `BuildingCommandResult`/`RoadCommandResult` carry the real `PlacementValidationResult`/`RoadPlacementValidationResult` detail directly (both engine-free `Core.Simulation` types after the move) — safe to expose on `ISimulation` itself, not a `ManagedSimulation`-only escape hatch. The generic `SimulationCommandResult` (`SimulationCommandOutcome` + `SimulationCommandRejectionReason` enum, never a string) exists for any future command type not covered by the two typed lists.

## Headless Testing / Determinism

`ManagedSimulation` is constructible with zero `Terrain`/`GameObject`/`ScriptableObject`/`MonoBehaviour` — headless tests build a plain `SimulationIslandData` directly. This is a genuine simplification over the pre-Phase-2E test pattern, which still needed constructed `Terrain` GameObjects. `SimulationWorldBootstrap` (the one Unity-facing adapter) is tested separately, in isolation, with real `Terrain`.

Determinism: no `Dictionary` iteration or Unity `Update` order ever decides command order (explicit `SubmitSequence`); no `UnityEngine.Random`; no `Time.deltaTime` inside `ManagedSimulation` (only inside `UnitySimulationDriver`, explicitly outside the determinism boundary — the driver decides *how many* ticks to request from real time, which is inherently non-deterministic and expected; the computation *inside* those ticks is what must be, and is, deterministic).

`ManagedSimulation.ComputeStateDigest()` — a non-cryptographic FNV-1a hash over the *full* authoritative state (every instance/segment sorted by ID, plus `CurrentTick`, plus both next-ID counters) — not just the render snapshot, so two simulations that are visually identical right now but consumed different ID sequences (e.g. one placed-then-removed a building) do not falsely digest equal.

## Scale / GC

Synthetic stress tests (headless, `Varynth.Tests.EditMode`) confirmed: 10,000 buildings placed in 68 ms (one batched tick), 3,980 road segments built in 48 ms, 1,000 subsequent empty ticks over that populated world in 1 ms (proving the double-buffer gate works — no rebuild without a real change), and 5,000 consecutive empty ticks allocated 0 bytes/tick. Real, honestly measured numbers — no invented FPS target.

## Savegame Future

Not built now. The natural extension point is `ManagedSimulation.Save()`/`Load()`, serializing `ArchipelagoPlacementState`/`RoadNetworkState` by reference (definition IDs, not copies) — consistent with `DATA_SCHEMA.md`'s existing "references not copies" principle. Unity Presentation is not, and will not become, the source of truth for a savegame.

## Multiplayer Future

The `Submit`/`CommandEnvelope.TargetTick`/`SubmitSequence` design directly supports later host-authoritative lockstep scheduling (a remote command targeting a few ticks ahead for latency buffering) without further boundary changes. No networking code exists.

## Burst/Jobs Future

Documented escalation path, not triggered now: (1) normal managed C#, (2) measure/profile, (3) data-orient, (4) Burst/Jobs for real hotspots, (5) re-measure, (6) only at real, measured need, a C++23 native core. No technology introduced on assumption.

## C++23 Native Core Future

Target shape: `ISimulation` ← `ManagedSimulation` | `NativeSimulationBridge` → a small, stable, batch-oriented C API → `VarynthCore` C++23. Not implemented. Deliberate, documented trade-off: the core mutation/tick/snapshot path (`Submit`/`AdvanceTicks`/`GetSnapshot`/`ConsumeResults`) is fully `ISimulation`-mediated and would work unchanged under a future native bridge; a handful of `ManagedSimulation`-specific extras (`GetRoadStateVersion`, `IsBuildingConnectedToRoad`, etc.) couple Presentation to the *Managed* implementation specifically and would need an equivalent on any future bridge — not solved now, called out rather than glossed over.

## Non-Goals (unchanged)

No C++, CMake, native plugins, P/Invoke, Burst/Jobs migration, population/needs/goods/production/economy, ships/AI/diplomacy/research, savegame, multiplayer/Steamworks, new road features/buildings/map generation, final water/UI/art.

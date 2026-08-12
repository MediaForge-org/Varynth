# Phase 2D — Road Network Foundation + Repeat Building Placement (Varynth 0.2.1)

Extends 0.2.0's building-placement sandbox with (A) drag/repeat placement for
suitable buildings and (B) a real road network — nodes/segments, 8-direction
orthogonal+diagonal routing, corner-cutting rules, a command boundary, a mesh with
smoothed junctions, and a building↔road connectivity query. Still explicitly **no
economy** (no cost/upkeep/speed bonus/goods) — roads exist as a connectivity graph
only. `MEGA_PROMPT` §44 only describes a *future* 5-tier road-cost/speed data model,
explicitly marked "noch offene Zahlen" and explicitly ruling out any congestion/
intersection-capacity simulation — both are honored here (cost/speed stay out of
scope; no traffic simulation was built). Diagonal roads and smoothed visual
transitions are a new, explicit user instruction for 0.2.1, not an invented spec value.

## A. Repeat Building Placement

### A1. `BuildingPlacementBehavior`

`Varynth.Core.Definitions.Buildings.BuildingPlacementBehavior { Single, DragRepeat }`.
`BuildingDefinition` gained a `PlacementBehavior` property via a trailing optional
constructor parameter (default `Single`) — every existing call site that constructs
`BuildingDefinition` directly kept compiling unchanged. Every read site branches only
on `definition.PlacementBehavior`, never on an id/name string.

XML: optional `placementBehavior="single"|"dragRepeat"` attribute on
`<buildingDefinition>`, defaulting to `Single` when absent. Prototype content:
House = `dragRepeat` (the brief's own motivating case — classic housing rows),
Production Block = `single` (decided explicitly: no established repeat-UX precedent
for production buildings, conservative and easily revisited), Public Building =
`single` (explicit brief requirement).

### A2. `BuildingRepeatPlanner` — one combined line/area algorithm

`Varynth.World.Placement.BuildingRepeatPlanner.PlanOrigins(start, end, effectiveWidth,
effectiveLength)` — pure, deterministic: `countX`/`countZ` derived from
`|end - start| / effectiveSize + 1`; when either resolves to 1 the result is a line,
when both are `>1` it's a rectangular block — the same code path, no separate
line/area branches. Steps by the *effective* (post-rotation) footprint size, so
generated origins are edge-to-edge and never overlap by construction. `start == end`
produces exactly one origin (a plain click is just a zero-length drag through the
same code path).

### A3. Partial-Invalid Policy — place valid, skip invalid

Chosen deliberately (documented rationale, per the brief's explicit "nicht still
entscheiden"): matches the referenced classic city-builder drag UX, and an
all-or-nothing policy would make drag placement nearly unusable on the real patchy
prototype islands (a single Coast-touching cell would veto an entire otherwise-good
row). Roads use the **opposite** policy (A9) — the asymmetry is intentional: roads
need a real connected network, buildings don't.

### A4. Drag preview — pooled, not per-cell GameObjects

`DragPreviewDisplay` (`Varynth.Presentation.Placement`) — exactly two persistent
`MeshFilter`/`MeshRenderer` pairs (all-valid group, any-invalid group), reusing the
same merged-mesh-append pattern already proven by `PlacementGhostDisplay`/
`SurfaceOverlayMeshBuilder`. Footprint quads via `GridCellMeshBuilder.BuildCellQuad`;
building-preview boxes appended as raw transformed unit-cube vertices (no per-instance
GameObject/Transform). Rebuilt only when the planned-origin list actually changes.

### A5. `PlaceBuildingBatchCommand` + batch handling

`Varynth.Core.Simulation.Building.PlaceBuildingBatchCommand` — `ISimulationCommand`
carrying `BuildingId`, `Rotation`, and an explicit ordered `Origins` list (chosen over
a `Start`/`End` pair: the repeat *shape* is already fully resolved by
`BuildingRepeatPlanner` in Presentation, so the command stays dumb, replayable data).
Constructor defensively copies `Origins` into an internal array — mutating the
caller's original list afterward never changes the command.

`BuildingPlacementCommandHandler.Handle(PlaceBuildingBatchCommand, ...)` iterates
`Origins` in list order, calling the existing single-placement
`ArchipelagoPlacementState.TryPlace` per origin (no new World-state mutation
primitive) — never 50 separate `Handle(PlaceBuildingCommand)` calls; the whole batch
is one command, one `Handle` call. Because `TryPlace` already assigns sequential
`BuildingInstanceId`s strictly in call order, identical `Origins` order always yields
identical instance-id assignment — verified by a dedicated determinism test.

### A6. Input

`PlacementController` gained a `Dragging` sub-state (`_dragStartCell`): LMB press on a
`DragRepeat` selection sets the start cell; LMB release (checked in the same `Update`
without an early return after the press branch, so a plain same-frame click still
commits a single building) builds and hands off one `PlaceBuildingBatchCommand`. A
`Single`-behavior building still places immediately on press, unchanged from 0.2.0.

## B. Road Network Foundation

### B1. `RoadDefinition`

`Varynth.Core.Definitions.Roads.RoadDefinition : IContentDefinition` — mirrors
`BuildingDefinition`'s shape: `Id`, `NameKey`, `PrototypeVisualId`,
`LogicalWidthCells` (default 1), `AllowsDiagonalSegments` (default true — the
data-driven form of "AllowedSegmentTypes" for one prototype tier), `AllowsCoastPlacement`
(default false, mirrors the building flag). No cost/upkeep/speed/tier fields — matches
`DATA_SCHEMA.md`'s own `RoadDefinition` sketch. Content loads through the same Phase
1B XML pipeline (`RoadDefinitionXmlLoader`/`RoadContentBootstrap`), one real file at
`Assets/StreamingAssets/Content/Roads/prototype_road.xml` (`road.prototype.basic`).

### B2. Node/segment identity, `RoadDirection`

A `RoadNode`'s identity **is** its `GridCoordinate` (no separate id wrapper — unlike
buildings, a node is 1:1 with "a road exists at this cell"). `RoadSegmentId`
(`ulong`-backed, sequential, world-state-owned counter, same idiom as
`BuildingInstanceId`) lives in **`Varynth.Core.Common`**, not `Varynth.World.Roads`:
`RemoveRoadCommand` (`Varynth.Core.Simulation`) needs to carry one, and
`Core.Simulation` must never reference `Varynth.World`.

`RoadSegment { Id, DefinitionId, From, To, Direction, Owner }` — no stored cost;
`RoadDirectionExtensions.CostUnits()` derives it on demand from `Direction` (integer
constants `1000` orthogonal / `1414` diagonal — a fixed-point stand-in for
`1000·√2`, never `Mathf.Sqrt` at runtime). `Owner` (`PlayerId`) is stored additively,
mirroring `BuildingInstance.Owner`, for the same future host-authoritative co-op
reason. `RoadDirection { N, NE, E, SE, S, SW, W, NW }` — no float angles as gameplay
identity.

### B3. `RoadGraph` / `RoadNetworkState`

`RoadGraph` (one per island, plain C#): node dictionary keyed by `GridCoordinate`,
segment dictionary keyed by `RoadSegmentId`, an undirected pair-index for O(1)
duplicate/connectivity lookups, and a dirty-cell set (B10). `RoadNetworkState` is the
road-side counterpart to `ArchipelagoPlacementState` — built from the same runtime-safe
`IslandSurfaceRuntimeData`/`Terrain` data, its own independent per-island bounds
resolution (a small duplicated scan, not shared state, matching the decoupling
requirement below), command-agnostic public API.

**`ArchipelagoPlacementState` and `RoadNetworkState` never reference each other.**
Cross-validation (a building can't be placed on a road; a road can't be placed through
a building) goes through two small neutral interfaces, `IBuildingOccupancyQuery`/
`IRoadOccupancyQuery`, each implemented by the respective state and composed only at
the one call site that legitimately knows about both (`PlacementController`/
`RoadPlacementController`, wired to each other's `State` in `Start()` after every
`Awake()` has run). This check is **authoritative**, not preview-only: the same query
instance is passed into both the ghost-preview validation call and the real
`BuildingPlacementCommandHandler`/`RoadCommandHandler` `TryPlace`/`TryBuildPath` calls
— preview and final application can never diverge.

### B4. Diagonal segments, corner cutting, mid-cell crossing

A diagonal segment is simply a `RoadSegment` whose `Direction` is one of NE/SE/SW/NW.
`CornerCuttingRule` (tightened from the classic pathfinding rule for a width-1
prototype road): a diagonal is valid only if **both** orthogonal flanking cells are
passable — a single blocked flank (Water/disallowed Coast/RockOrSteep/building)
already invalidates it. `DiagonalCrossingRule` additionally rejects a diagonal that
would geometrically cross an existing opposite-orientation diagonal in the same grid
square without sharing a node (the classic "X" case) — detected deterministically via
the candidate's grid-square key and orientation. Both rules are enforced identically
by direct segment validation and inside the router's edge-legality check.

### B5. `RoadPlacementValidator`

`RoadPlacementIssue [Flags]`: `OutsideSurfaceMap, Water, Coast, RockOrSteep,
BuildingOccupied, DuplicateSegment, SlopeTooSteep, CornerCut, DiagonalCrossing,
DifferentIsland`. `RoadPlacementConfig.MaxSegmentSlopeDegrees = 30f`. Checks every
applicable rule and returns all matching issues, not just the first — same philosophy
as `PlacementValidator`. `DuplicateSegment` applies only to a genuine direct
single-segment validation call — the atomic route-commit path (B8) treats an
already-existing edge as a no-op and never calls this for it.

### B6. Routing — deterministic integer A\*

`RoadRouter.TryFindRoute` — classic 8-directional A\*, **integer** costs (1000/1414,
no float in routing state), integer octile heuristic. Chosen over Bresenham/supercover:
A\* gets real obstacle avoidance for free by excluding invalid edges from expansion,
leaving room for later cost-weighted preferences without a separate detour layer —
matches the brief's explicit permission to use A\* here. This is a small, bounded
per-island textbook A\*, not a hierarchical pathfinding engine — future chunked/
hierarchical pathfinding on much larger islands remains its own, separately deferred
scaling topic (the mesh dirty-region tracking in B10 is a rendering optimization only
and does not by itself address this).

**Total-order deterministic tie-breaking**: open-set priority key
`(f, h, X, Z, directionOrdinal)`, compared lexicographically — never dependent on
`Dictionary`/heap iteration order. Identical inputs always produce a bit-for-bit
identical path (verified by a repeated-run determinism test).

**Existing roads are always traversable**, never re-validated or penalized — a route
can freely use/extend existing network instead of detouring around it.

### B7. Road commands and atomic commit

`Varynth.Core.Simulation.Road.BuildRoadCommand` (`RoadDefinitionId`, ordered
`OrderedPath`, defensively copied) / `RemoveRoadCommand` (single-segment removal,
prototype UX decision). `RoadCommandHandler.Handle(BuildRoadCommand)` is
**atomic and two-pass**: pass 1 walks the path pairwise, treating already-existing
edges as no-ops and validating every genuinely missing edge against current world
state (never trusting a stale Presentation-side preview); if *any* missing edge is
invalid, the whole call aborts — zero segments created, the `RoadSegmentId` counter
unchanged. Only on full success does pass 2 actually create the missing segments, in
path order.

### B8. `RoadMeshBuilder` — generic junctions, real terrain sampling

Per island, one merged mesh (mirrors `SurfaceOverlayMeshBuilder`). Segment quads are
subdivided into multiple cross-sections along their length; every emitted vertex
(segment and junction alike) is height-sampled at its own real world X/Z via
`IWorldHeightSource`, not only at endpoints. Junction geometry is one generic
axis-aligned patch per node — the same code path regardless of connected-direction
count (1 through 8), no per-degree special-casing that would break above degree 4;
the segment quads (which already run cell-center to cell-center) act as the "stubs"
into the patch. Gameplay graph stays fully discrete — only this visual layer is
shaped to read as continuous ("keine freie Spline-Simulation" respected).

### B9. Runtime mesh ownership + dirty-region chunking future-proofing

`RoadGraph` accumulates a dirty-cell set on every add/remove, exposed via
`ConsumeDirtyCells()`. `RoadRuntimeMeshRefresh` (Presentation) consumes it after every
successful command, resolves the affected island(s), rebuilds via `RoadMeshBuilder`,
and writes the result into `RoadNetworkDisplay`'s **persistent, cloned runtime `Mesh`**
(`Object.Instantiate`'d once at init, then `Clear()`+`SetVertices`/`SetTriangles`
reused in place on every edit — never the Editor/`AssetDatabase`-saved mesh asset,
never a fresh allocation per edit, never `AssetDatabase` at runtime). Mesh output is
already per-island, not one archipelago-wide mesh — directly satisfies the "no single
gigantic mesh rebuilt whole on every change" requirement. Explicitly documented: this
is a rendering-update optimization only, and does not by itself solve future A\*
pathfinding performance on much larger islands (B6).

### B10. Road placement UX / preview

`RoadPlacementController` (own `Idle`/start-set/`Routing` sub-state, separate class
from `PlacementController`) — select tool → click sets start → mouse move recomputes
the route via `RoadRouter` only when the hovered cell changes → `RoadPreviewDisplay`
shows it as one merged mesh of straight segment quads in one of exactly two materials
(valid/invalid — since A\* only ever returns a fully valid path or none, the whole
preview is valid-or-invalid as one unit, never partially) → click again confirms.
**No partial-invalid road preview** — the opposite policy from buildings (A3),
because a discontinuous "half-road" would violate the product goal of a real connected
network.

### B11. Building↔road connection query

`BuildingRoadConnectionQuery.IsConnected(instance, definition, roads)` — computes the
footprint's occupied cells, checks each outer-edge cell's 4 **orthogonal** neighbors
outside the footprint for a `RoadNode`. **Diagonal-only corner touch does not count**
— explicit user decision, applied directly. Rotation is already baked into the
footprint cells used, so a rotated building's correct edge is checked without
special-casing. Computed live against current `RoadNetworkState` every call — never
cached, reports `false` again the instant the connecting segment is removed.

### B12. Removal picking

A hovered `GridCoordinate` alone is ambiguous at a busy junction (up to 8 segments can
touch one cell). `RoadSegmentPicker` uses the real continuous world hit position
(already available from the existing raycast) and picks the incident segment whose
centerline is geometrically closest to that point, ties broken by lowest
`RoadSegmentId` — a transient Presentation-side disambiguation, never stored as world
state. Removal (like building removal) only works when no construction tool is active
(`ConstructionToolCoordinator.ActiveMode == None`).

### B13. `ConstructionToolCoordinator`

Small additive Presentation type (`ConstructionToolMode { None, Building, Road }`) —
the single arbiter of which tool is active, tool-switch cancellation, and Player
Placement Grid visibility. `PlacementController`/`RoadPlacementController` never
reference each other directly; both register with and request activation/grid
visibility through the coordinator (found via `ConstructionToolCoordinatorHost` in
`Start()`). Additively extensible for a future Demolition/Farm/Harbor tool — one more
enum value, one more registered controller.

### B14. Multi-island

`RoadNetworkState` is built from *all* islands; every route/segment/connection query
is island-scoped. A route request spanning two islands is rejected before A\* even
runs. Tests explicitly exercise two independent islands.

## Performance (real measured numbers, real Unity 6000.5.7f1 batchmode)

- Drag batch: 100 planned origins on a synthetic fully-buildable map, all 100 placed
  via one `PlaceBuildingBatchCommand`, in **0 ms**.
- Real archipelago (existing regression): 169 buildings placed across 4 real islands
  in 104 ms.
- Synthetic large map (existing regression): 150 buildings in 1 ms.
- Road build: **9,950 real segments** created across 50 long rows (one `BuildRoadCommand`
  per row, not per segment) on a synthetic 200×200 island, in **29 ms**.
- Route lookup across an existing 199-segment row: found in **2 ms**.
- Full-island road mesh rebuild (9,950 segments, 99,700 vertices): **70 ms**.

No invented FPS numbers — these are the real measured values from
`DragPerformanceTests`/`RoadPerformanceTests`.

## Tests

**EditMode (new):** `BuildingRepeatPlannerTests`, `BuildingDefinitionXmlLoaderTests`
(extended for `placementBehavior`), `BuildingPlacementCommandHandlerTests` (extended
for batch determinism/partial-invalid), `PlaceBuildingBatchCommandTests`,
`RoadDefinitionXmlLoaderTests`, `RoadContentBootstrapTests`, `RoadDirectionTests`,
`RoadGraphTests`, `RoadNetworkStateTests`, `CornerCuttingRuleTests`,
`DiagonalCrossingRuleTests`, `RoadPlacementValidatorTests`, `RoadRouterTests`,
`RoadMeshBuilderTests` (incl. explicit degree-5/6/7/8 junction generation),
`BuildingRoadConnectionQueryTests`, `BuildRoadCommandTests`/`RemoveRoadCommandTests`,
`RoadCommandHandlerTests` (atomicity, existing-edge no-op, removal).

**PlayMode (new):** `DragPlacementAcceptanceTests` (`InputTestFixture` — real
simulated press/move/release), `RoadAcceptanceTests` (tool select/start/preview/
confirm/cancel/remove, all via real simulated input), `BuildingRoadConnectionAcceptanceTests`
(one real scene-level end-to-end confirmation), `DragPerformanceTests`,
`RoadPerformanceTests`.

**Results (real Unity 6000.5.7f1 batchmode runs):**
- EditMode: **436/436 passed**, 0 failed.
- PlayMode: **29/29 passed**, 0 failed.
- 0 compile errors, 0 shader errors.

Two real bugs were found and fixed by the tests themselves during this pass: a
triangle-winding error in `RoadMeshBuilder` (both the segment quad strip and the
junction patch initially produced downward-facing normals — fixed, verified by
`RoadMeshBuilderTests`), and a same-frame-click bug in `PlacementController` where a
plain click on a `DragRepeat` building (press+release processed in the same input
frame) set the drag start but an early `return` swallowed the release, silently
placing nothing — fixed by removing the early return so the release check still runs.

## Scope Confirmation

No goods/production logistics/transport carts/warehouses/shared island inventory, no
population/needs/workforce/housing upgrades/high-rises, no power/water utilities, no
ships/overseas trade/war/pirates/AI/diplomacy/research/quests/story, no savegame, no
Steamworks/multiplayer implementation, no final road/building art, no final
water/vegetation, no Blender assets, no traffic/congestion/intersection-capacity
simulation, no road cost/upkeep/speed-modifier values, no mid-drag rotation, no
multi-segment road-stretch removal (single segment only), no true freeform spline
roads.

## Varynth 0.2.1 Build

`PlayerSettings.bundleVersion = "0.2.1"`; `MilestoneBuild.BuildCurrentVersion()` used
completely unmodified. Real Linux x86_64 batchmode build:
`Varynth 0.2.1 built successfully at Builds/Varynth-0.2.1-linux-x64 (109,545,794 bytes)`.
Verified on disk: real ELF 64-bit executable, `Varynth_Data/` 65 MiB.

## Manual Visual Acceptance Checklist

1. Open `WorldPrototype.unity`, press Play. All 0.2.0 checks still hold (G/F2/F3,
   ghost, rotation, valid/invalid, occupancy, remove, multi-island).
2. Press `1` (House) — select it, left-click-drag across several cells — a multi-ghost
   preview follows, releasing places all the valid ones in one batch.
3. Press `2`/`3` (Production/Public) — still place immediately on a single click.
4. Press `4` (Road Tool) — click a start cell, move the mouse — a route preview
   follows, shown valid (yellow-ish) or invalid (red) as one unit; click again to
   confirm, Escape/right-click to cancel.
5. Build both a straight and a diagonal road segment — the diagonal should visibly
   avoid corners blocked by rock/water/buildings.
6. Hover an existing building or road segment in Idle (no tool selected) and press
   Delete — it's removed; Delete does nothing while actively placing/routing.
7. No magenta/missing-shader geometry anywhere; no console errors.

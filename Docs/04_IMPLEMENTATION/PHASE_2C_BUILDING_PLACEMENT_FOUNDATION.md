# Phase 2C — Building Placement Foundation (Varynth 0.2.0)

Extends 0.1.1's archipelago with the first real building-placement sandbox: select a
prototype building, see a ghost preview snapped to the grid, rotate it, get
valid/invalid feedback, place it, remove it. Explicitly **no economy** (no cost,
upkeep, workforce, goods) — see §13 Scope Confirmation. No binding numeric spec exists
for non-residential building footprints, rotation-snap increment, or a generic
buildable-area formula beyond `SurfaceCellFlags.Buildable` (Phase 2B's own doc already
deferred occupancy/ownership rules to a later package — this one); all new spatial
thresholds here are documented prototype values, same treatment as every prior phase.

## 1. Building Definition

`BuildingDefinition : IContentDefinition` (`Varynth.Core.Definitions.Buildings`) — the
first real (non-test) `IContentDefinition` in the project: `Id`, `NameKey`,
`FootprintWidth`/`FootprintLength`, `PrototypeVisualId` (string key into a
Presentation-side visual lookup — the definition itself carries no
Prefab/Material/GameObject reference), `AllowsCoastPlacement` (bool, default `false`,
the extensibility point for a later building that explicitly wants coast access, e.g.
a future harbor). No cost/upkeep/workforce/goods fields — out of scope, consistent with
`DATA_SCHEMA.md`'s `BuildingDefinition` sketch treating those as separate, later
concerns.

**Content is real XML, not a hardcoded catalog.** The Phase 1B content-load pipeline
(`ContentFileDiscovery`, `XmlDocumentReader`, `DefinitionLoadPipeline<T>`,
`ContentRegistry<T>`) existed but had never been exercised end-to-end outside tests. Two
small new pieces close the gap:
- `BuildingDefinitionXmlLoader : IContentDefinitionLoader<BuildingDefinition>`
  (`Varynth.Data`) — same shape as the existing test-only `TestDefinitionXmlLoader`.
- `ContentDocumentFileLoader.LoadFromDirectory(rootPath, source, report)` (`Varynth.Data`)
  — the one genuinely missing piece: discovers `.xml` files and wraps each into a
  `ContentDocument`, built entirely from already-hardened, already-tested primitives
  (`ContentFileDiscovery.DiscoverXmlFiles`, `XmlDocumentReader.TryLoad`).
- `BuildingContentBootstrap.LoadRegistry()` (`Varynth.Data`) ties both together and is
  called once from `PlacementController.Awake()`.

3 real files at `Assets/StreamingAssets/Content/Buildings/*.xml`
(`bld.prototype.house`, `bld.prototype.production_block`, `bld.prototype.public_building`
— 2×2/3×2/4×3). **`StreamingAssets` is a new, explicitly documented decision** (no prior
content-root convention existed): it is the one location that is a real folder on disk
both in the Editor and inside a built Player (`Application.streamingAssetsPath`),
unlike `Assets/` (Editor-only) or `Resources/` (disfavored by
`.claude/rules/03-unity-assets.md` for this kind of content).

## 2. Footprint / Rotation

`BuildingRotation` (`Varynth.Core.Common`): `Deg0/Deg90/Deg180/Deg270` +
`SwapsWidthAndLength()`. 90° increments, 4 orthogonal states — a documented prototype
value (MEGA_PROMPT confirms rotation exists as a concept but specifies no increment).

`BuildingFootprint.GetOccupiedCells(origin, width, length, rotation)`
(`Varynth.World.Placement`) — pure, integer-only, deterministic; swaps width/length at
90°/270°; correct for negative/global `GridCoordinate`s; no duplicate cells by
construction.

`BuildingInstanceId` (`Varynth.Core.Common`) — `ulong`-backed, same idiom as `GameTick`
(`Zero`, `FromRaw`). **Chosen over `Guid` deliberately**: a `BuildingInstanceId` is
assigned at the moment a placement is applied — a deterministic, reproducible event that
must yield the *same* id given the same input sequence (future host/client agreement,
replay, save/restore). A `Guid.NewGuid()` per placement would be non-deterministic
across machines; the sequential, world-state-owned counter (owned by
`ArchipelagoPlacementState`, not by the id type itself) is deterministic by construction.

## 3. Placement Mode / Ghost

`PlacementController` (`Varynth.Presentation.Placement`) — new `MonoBehaviour` on
`CameraRig`, alongside (not merged into) `WorldInteractionController`, which gained
small read-only accessors (`Grid`, `HeightSource`, `Pointer`, `Terrains`) so
`PlacementController` reuses its existing raycasting/hover setup instead of duplicating
it. State: `PlacementMode { Idle, Placing }` — no framework, no scattered globals.

`PlacementGhostDisplay` — **two reused (never Instantiate/Destroy'd) child renderers**:
a terrain-conforming footprint (merged `GridCellMeshBuilder.BuildCellQuad`s, same
primitive the debug cell-highlight uses) and a real building-shape preview (the actual
blockout mesh/material the placed building would use, scaled to the footprint). The
building preview stays **upright** — only yaw-rotated by `BuildingRotation`, never
tilted to the terrain normal — while its footprint quads still conform to terrain.
Rotation (`R`) rotates both together. Validity (valid/invalid materials) is shown on
both parts. Rebuilt only when origin/rotation/validity actually change.

## 4. Placement Validation

`PlacementIssue` `[Flags]`: `OutsideSurfaceMap, NotBuildable, Water, Coast, RockOrSteep,
AlreadyOccupied, HeightVariationTooLarge`. `PlacementValidationResult { IsValid;
Issues; }` — structured, not a bare bool, extensible for later road/harbor/ownership
checks without changing shape.

`PlacementConfig.MaxFootprintHeightVariation = 4f` — a new prototype threshold (max
sampled-height spread across a footprint's cells), calibrated empirically against real
generated terrain (see §11) rather than picked blind. `PlacementValidator.Validate(...)`
checks: inside the island's `SurfaceMap` bounds; every cell `Buildable`; no `Water`; no
`Coast` unless `definition.AllowsCoastPlacement`; no `RockOrSteep`; no already-occupied
cell; footprint height variation within threshold. Returns every applicable issue at
once, not just the first.

## 5. Occupancy

`IslandOccupancyMap` (`Varynth.World.Placement`) — same dense flat-array shape as
`IslandSurfaceMap` (`OriginCell`/`Width`/`Height`), storing raw `ulong` instance-id
values (`0` = unoccupied). `TryGetOccupant` O(1). `Occupy`/`Release` operate on a whole
footprint's cell list **atomically** — `Occupy` pre-validates every cell before writing
any of them, so a rejected batch never leaves partial occupancy.

## 6. Runtime Island Surface Data

**Problem solved:** the Editor-only `WorldPrototypeSceneBuilder.IslandBuildResult` type
(and the `IslandSurfaceMap` instances built from it) live in `Varynth.Tooling.Editor`,
which does not exist in a Player build. `ArchipelagoPlacementState` needed its own real,
serialized runtime data source.

`IslandSurfaceRuntimeData : ScriptableObject` (`Varynth.World.Placement`) — per-island
asset (`OriginCellX/Z`, `Width`, `Height`, `byte[] Flags` — a direct copy of the
generation-time `SurfaceCellFlags[]` array, **no re-classification at runtime**). Built
once, GUID-stably, by `WorldPrototypeSceneBuilder` right after each island's
`SurfaceMapGenerator.Generate(...)` already runs, at fixed per-island paths
(`Assets/Game/World/Art/Placement/<Island>_SurfaceRuntimeData.asset`).

New `IslandSurfaceMap.FromRawFlags(originCell, width, height, flags)` factory wraps the
array directly (cheap copy, not a recompute) — used by `ArchipelagoPlacementState`'s
bootstrap. `PlacementController` carries a serialized `IslandSurfaceRuntimeData[]`
parallel to the existing `Terrain[]`/`TerrainCollider[]` arrays (same per-island order,
wired by the same builder loop) — fully Player-build-safe, verified by an EditMode test
whose fixture never references `Varynth.Tooling.Editor`.

## 7. Building Instance / World State

`BuildingInstance` — plain data (`Id`, `DefinitionId`, `Origin`, `Rotation`, `Owner`),
no `GameObject`/`Transform` reference.

`ArchipelagoPlacementState` (`Varynth.World.Placement`) — the world-side building state,
built at runtime from `IslandSurfaceRuntimeData` (never from Editor-only types). Owns
one `IslandOccupancyMap` per island and a flat
`Dictionary<BuildingInstanceId, (BuildingInstance, islandIndex)>` for O(1) removal (pure
data, never a `GameObject` — the brief's "no `Dictionary<cell,GameObject>` as primary
identity" rule is about GameObjects specifically, not dictionaries). **Deliberately
command-agnostic**: its public API (`ValidatePlacementAt`, `TryPlace`, `TryRemove`)
takes plain values only, never `ISimulationCommand`/command types — kept reusable later
for AI-issued placement, replay, host-authoritative co-op reconciliation, or
save/restore without any coupling to the command boundary. This also means it needs
**no** `Varynth.World → Varynth.Core.Simulation` asmdef edge.

## 8. Command Boundary

`PlaceBuildingCommand`/`RemoveBuildingCommand` (`Varynth.Core.Simulation.Building`) —
both `ISimulationCommand`, immutable, ctor-only, structurally identical to the existing
test-only `TestCommand`. Every field type (`PlayerId`, `GameTick`, `ContentId`,
`GridCoordinate`, `BuildingRotation`, `BuildingInstanceId`) already lives in an
engine-reference-free assembly — no Mouse/Camera/GameObject/Transform/Material/UI is
representable even by accident. `IssuedAtTick` is always `GameTick.Zero` in this
package — no `GameClock` is wired into the runtime yet, documented as a known,
deliberate limitation. `IssuedBy` is one `PlayerId.NewId()` generated once in
`PlacementController.Awake()` (session-init time, the documented-safe use).

`BuildingPlacementCommandHandler` (`Varynth.World.Placement`) — the **only** new type
that knows about both commands and `ArchipelagoPlacementState`, translating one into
calls on the other. This is the sole place carrying the new, narrowly-scoped
`Varynth.World → Varynth.Core.Simulation` asmdef edge. `Varynth.Presentation →
Varynth.Core.Simulation` is the other new edge (so `PlacementController` can construct
commands). Both are additive, bottom-up, non-circular.

## 9. Player Placement Grid vs. Debug Grid

Architecturally separate. **Debug Grid** (`G`, existing `GridDisplay`) — unchanged,
developer-only. **Player Placement Grid** — **one `GridDisplay` per island** (not one
global aggregate), each fed its own mesh built once at scene-build time
(`WorldPrototypeSceneBuilder.BuildPlacementGrids`, reusing
`SurfaceOverlayMeshBuilder`'s per-island aggregation, restricted to that island's
`Buildable` cells — excludes Water/Coast/RockOrSteep by construction). All hidden by
default; `PlacementController` shows **only** the currently-hovered island's grid,
hides every other island's — hovering island A never lights up island B's grid, open
water shows none. Kept per-island-single-mesh (not chunked) deliberately, so a future
streaming/chunked mesh generator can later replace "one mesh per island" with "N chunks
per island" without touching the controller's per-island show/hide algorithm.

## 10. Multi-Island

No single-island assumption anywhere in the new types — `ArchipelagoPlacementState` is
built from *all* islands, and every EditMode/PlayMode test explicitly exercises two
different islands (place on A, place on B independently; hovering A never shows B's
grid).

## 11. Performance / Real Capacity Finding

Direct, real diagnosis (not guessed) of a validation-tuning question: an exhaustive
step-1 scan of every real terrain's actual cell bounds across all 4 prototype islands
initially found only ~38 non-overlapping valid 2×2 footprints in total (8,912 attempts).
Breaking down the per-cell `PlacementIssue` distribution confirmed this was a genuine
content-scale limit of the original small (110–260 unit) prototype islands — dominated
by `NotBuildable`/`Water` (most of each terrain tile is the submerged "skirt" beyond the
emerged coastline, documented since the 0.1.1 bugfix round), not by the
`MaxFootprintHeightVariation` rule (556 rejections, a minor contributor).

**0.2.0 follow-up ("Bau-Sandbox-Fläche vergrößern"):** the user reported that even the
achievable ~38 was too small to usably test all 3 prototype building types side by side.
`TestIsland_Large` (`WorldPrototypeIslands.cs`) was enlarged from 260×260 to 440×440 and
flattened (`IslandRadius01` 0.62→0.70, `Octaves` 5→3, `Persistence` 0.45→0.30,
`CoastNoiseStrength` 0.14→0.12) — no new terrain code: `HeightmapResolution` (257) stays
the shared fixed constant, so spreading the same normalized relief noise over a larger
physical footprint automatically flattens real-world slope in degrees. The radial falloff
band near the coastline still produces a natural steep/rocky rim, so `RockOrSteep`/
`Coast` invalid-placement testing remains possible. Re-running the same exhaustive scan
after this change found **169 non-overlapping valid 2×2 footprints** across the
archipelago (16,877 attempts, 92 ms) — comfortably above the sandbox's "50–100 mixed
buildings" target. Two tests document this: `RealArchipelago_PlacesAllAchievableBuildings_...`
asserts the real achievable count (≥100, was ≥25) on the actual scene, and
`PlacementSandboxCapacityTests.SandboxIsland_FitsAllThreeBuildingTypesMixedWithRotationAndRemoval`
places 5 houses + 5 production blocks + 3 public buildings + a rotated house
side-by-side on `TestIsland_Large`, asserts an overlapping placement is correctly
rejected, and confirms removal frees the cell for re-placement. A separate
`SyntheticLargeMap_Places150Buildings_...` stress-test additionally proves the underlying
occupancy/validation data structures themselves scale far past "100+" independent of any
one island's real buildable land (synthetic fully-buildable 200×200 island, 150
placements in ~1 ms).

No `GameObject`/`MonoBehaviour` per grid cell anywhere (placement grids and ghost
footprints are merged meshes); no `Instantiate`/`Destroy` per frame for the ghost;
`IslandSurfaceMap`/`IslandOccupancyMap` lookups are O(1) flat-array index math.

## 12. Tests

**EditMode — new (14 files):** `BuildingFootprintTests`, `PlacementValidatorTests`,
`IslandOccupancyMapTests`, `IslandSurfaceMapFromRawFlagsTests`,
`ArchipelagoPlacementStateTests`, `BuildingPlacementCommandHandlerTests`,
`PlaceBuildingCommandTests`/`RemoveBuildingCommandTests`,
`BuildingDefinitionXmlLoaderTests`, `ContentDocumentFileLoaderTests`,
`BuildingContentBootstrapTests` (end-to-end against the real `StreamingAssets` files).

**PlayMode — new (3 files):** `PlacementAcceptanceTests` (uses `InputTestFixture` —
**first real simulated-device-input test in this project**, requiring a new
`"Unity.InputSystem.TestFramework"` reference on `Varynth.Tests.PlayMode.asmdef` — real
`Keyboard`/`Mouse` events drive selection/rotation/place/cancel/multi-island/removal-
only-in-Idle), `PlacementGridVisibilityTests`, `PlacementPerformanceTests` (2 tests, see
§11). All prior PlayMode regressions (`WorldPrototypeSceneTests`,
`WorldPrototypeVisibilityTests`, `WorldPointerTests`, `GridCellHighlightTests`,
`GridDisplayTests`, `ResourceCandidateMarkersTests`) and `CameraRigMathTests` (EditMode)
re-verified green — G/F2/F3/hover/camera-framing/camera-scaling all unaffected.

**Results (real Unity 6000.5.7f1 batchmode runs):**
- EditMode: **328/328 passed**, 0 failed, 0 inconclusive, 0 skipped.
- PlayMode: **21/21 passed**, 0 failed, 0 inconclusive, 0 skipped.
- 0 compile errors, 0 shader errors.

## 13. Scope Confirmation

No roads (incl. no diagonal/curved — architecture does not preclude them, none built),
no production/goods/cost/upkeep/workforce, no warehouses/shared-island inventory
(architecture does not preclude it), no population/needs/upgrades/high-rises, no
power/water utilities, no ships/trade/pirates/war/AI/diplomacy/research/quests/
story/expeditions, no savegame, no Steamworks/multiplayer implementation (only
continued future-proofing via `PlayerId`/`GridCoordinate`/command shape), no final
UI/building art/vegetation/water, no Blender assets.

## 14. Varynth 0.2.0 Build

`PlayerSettings.bundleVersion = "0.2.0"`; `MilestoneBuild.BuildCurrentVersion()` used
unmodified (no `Version0_2_0Build.cs`). Real Linux x86_64 batchmode build:
`Varynth 0.2.0 built successfully at Builds/Varynth-0.2.0-linux-x64 (108736487 bytes)`.
Verified on disk: real ELF 64-bit executable, non-empty `Varynth_Data/`, ~105 MiB total.
Windows build remains a known, accepted gap (no Windows Build Support installed on the
Fedora dev machine) — not a blocker, per standing project decision.

## 15. Manual Visual Acceptance Checklist

1. Open `WorldPrototype.unity`, press Play.
2. `G`/`F2`/`F3` debug toggles still work exactly as in 0.1.1.
3. Press `1`/`2`/`3` (or click the on-screen build bar) — ghost preview (footprint +
   upright building shape) appears, following the cursor.
4. Move over open water / a different island — the Player Placement Grid follows,
   showing only the currently-hovered island's grid; no grid appears over water.
5. Press `R` — footprint and building preview rotate together.
6. Hover a buildable vs. an invalid (water/coast/rock/occupied) cell — clear
   valid/invalid visual difference on both footprint and building preview.
7. Left-click on a valid cell — a real building appears; occupancy is set (re-clicking
   the same cells is rejected).
8. Right-click / `Escape` — ghost and placement grid disappear, mode returns to Idle.
9. With no building selected (`Idle`), hover an existing building and press `Delete` —
   it is removed, the cell becomes buildable again. Pressing `Delete` while actively
   placing a new building does **not** remove anything.
10. Repeat placement on at least two different islands.
11. No magenta/missing-shader geometry anywhere; no console errors.

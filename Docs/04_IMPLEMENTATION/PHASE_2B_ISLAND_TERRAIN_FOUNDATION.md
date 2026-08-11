# Phase 2B — Island & Terrain Foundation (Varynth 0.1.1)

Extends the Phase 2A single-island prototype into a small archipelago: multiple
independent `Terrain`/`TerrainData` islands, a compact per-cell surface
classification (water/coast/land/rock/buildable/vegetation-candidate/
resource-candidate), a buildability debug overlay, and a resource/mine slot
candidate foundation. No economy, no buildings, no networking. All spatial
numbers in this document are **technical prototype values**, not final
design/balancing decisions (`SPEC_AUDIT.md`/`MEGA_PROMPT` confirmed: no binding
spec exists for island size classes, beach width, buildable-slope threshold, or
resource-slot spacing — only the narrative "~10–15 islands/region" and the
binding "0–15 mine slots/island" count range).

## 1. Multi-Island Architecture

One `Terrain`/`TerrainData` per island (not one mega-terrain), matching the
architecture's own stated bridge role for `Varynth.World` between simulation
and the Unity scene. Each island lives under `World/Islands/{Name}` in the
scene, with its own fixed-path `TerrainData` asset
(`Assets/Game/World/Art/Terrain/{Name}_TerrainData.asset`, Git-LFS-tracked).
The three prototype terrain layers/textures (sand/grass/rock) and the terrain
material remain **shared** across all islands — only `TerrainData` (heights,
alphamaps) and the `TerrainCollider` are per-island; the visual/material setup
lives in `TerrainData`, not the material itself, so one shared
`Universal Render Pipeline/Terrain/Lit` material works for every island.

`IslandPrototypeConfig` (`Assets/Game/Editor/WorldPrototype/`, Editor-only for
now — no XML/mod schema yet) is a small plain data type, deliberately decoupled
from the build logic: `Name` (debug label, **not** a savegame ID or Occidentia
lore name), `Center`, `TerrainWidth`/`TerrainLength`, `Seed`, `IslandRadius01`,
`CoastNoiseStrength`, `Octaves`, `Persistence`, `Lacunarity`,
`MaxResourceCandidates`. `WorldPrototypeIslands.GetDefaultConfigs()` returns the
four prototype islands used for 0.1.1:

| Name | Center | Footprint | Seed | Radius | Coast noise | Max resource candidates |
|---|---|---|---|---|---|---|
| TestIsland_Large | (0, 0) | 260×260 | base+1 | 0.62 | 0.14 | 5 |
| TestIsland_Medium | (520, 40) | 170×170 | base+2 | 0.55 | 0.20 | 3 |
| TestIsland_Small | (60, 480) | 110×110 | base+3 | 0.50 | 0.10 | 2 |
| TestIsland_Coastal | (560, 470) | 200×140 | base+4 | 0.58 | 0.26 | 3 |

(`base` = 20260809, same seed family as Phase 2A.) Centers are spaced with
enough margin that no two terrain footprints overlap — a documented prototype
layout, not a final map.

## 2. Terrain / Island Generation

`IslandHeightmapGenerator` (Phase 2A) is reused **unchanged** — its existing
seed/radius/coast-noise/octaves/persistence/lacunarity parameters already
produce meaningfully different islands per config. This was **verified, not
assumed**: a standalone `dotnet run` check (outside Unity, using the same pure
C# generator file) computed real statistics for the four configured islands at
129×129 resolution:

```
Large    min=0.000 max=0.756 mean=0.064 landFrac=0.068 flatLandFrac=0.237
Medium   min=0.000 max=0.763 mean=0.050 landFrac=0.056 flatLandFrac=0.235
Small    min=0.000 max=0.829 mean=0.047 landFrac=0.052 flatLandFrac=0.122
Coastal  min=0.000 max=0.850 mean=0.065 landFrac=0.074 flatLandFrac=0.124
```

Max-height and relief-roughness (`flatLandFrac`) differ by up to ~2× across
islands, combined with genuinely different absolute footprints (110–260 units).
No generator extension was needed (per the correction that explicitly permitted
one only if verification showed it necessary) — the manual visual acceptance
checklist below still asks to confirm the islands read as visually distinct.

Sea-level convention is **unchanged from Phase 2A** for every island:
`TerrainData.size.y = 40`, `Transform.position.y = -15`, sea level = world
Y 0 → normalized height 0.375. `TerrainData.SetHeights` still only ever
receives the required 0..1 range (the vertical placement, not negative
heightmap values, is what puts terrain below sea level).

## 2a. Bug found during idempotency testing: `heightmapResolution` resample drift

A second `Build()` run against an already-generated island produced a
byte-different `TerrainData` asset (same size, different LFS content hash),
even though `IslandHeightmapGenerator` is fully deterministic. Root cause:
`TerrainData.heightmapResolution`'s setter resamples the *current* heightmap
even when assigned its already-current value — a floating-point round-trip
that occurred on every rebuild. Fix: only assign the property when it actually
differs from the target resolution. Re-verified idempotent afterward (three
consecutive rebuilds against a staged baseline produced an empty
`git diff --stat`).

## 3. Surface Classification

`Varynth.World.Surface` (new namespace, `Assets/Game/World/Surface/`):

- `SurfaceCellFlags : byte [Flags]` — `Water`, `Coast`, `Land`, `RockOrSteep`,
  `Buildable`, `VegetationCandidate`, `ResourceCandidate` (7 flags, one byte).
- `IslandSurfaceMap` — dense `SurfaceCellFlags[]` over one island's cell-bounds
  rectangle (`OriginCell`, `Width`, `Height`), O(1) global-`GridCoordinate` →
  local-index lookup, no per-cell objects, no
  `Dictionary<GridCoordinate, HeavyObject>`.
- `SlopeEstimator` — pure, samples a cell's center + 4 direct neighbors,
  returns the steepest height-delta as degrees. **A missing neighbor sample is
  excluded from the calculation, never treated as height 0** — a cell near an
  island's edge (where a neighbor cell falls into open water with no
  registered terrain) must not read as an artificial cliff. If the center
  itself has no data, or none of the neighbors do, the result is a documented
  neutral 0°. Regression-tested explicitly (`SlopeEstimatorTests.
  MissingNeighborSamples_AreExcludedNotFabricatedAsZero`).
- `SurfaceClassificationConfig` — prototype thresholds: `SeaLevelWorldY=0`,
  `CoastBandHeight=3`, `SlopeThresholdDegrees=30` (shared threshold for both
  `RockOrSteep` and `Buildable`/`VegetationCandidate` — not a 50-field config).
- `SurfaceMapGenerator` — per cell: no height data or height ≤ sea level →
  `Water`; height ≤ sea level + coast band → `Coast`; else `Land`, plus
  `RockOrSteep` if slope ≥ threshold, else `Buildable` + `VegetationCandidate`.
  **Slope, not raw height, gates `RockOrSteep`/`Buildable`** — a high but flat
  plateau is buildable (tested explicitly:
  `SurfaceMapGeneratorTests.HighButFlatCell_IsBuildable`).

## 4. Buildability / Coast / Rock — what the overlay does and doesn't mean

The F2 overlay says *"terraintechnically this cell would in principle be
buildable"* — it does **not** mean a final building may be placed there. Later
systems (occupancy, roads, ownership, reach, harbor requirements, research,
mission locks, terrain-type-specific buildings) still apply on top; this
package establishes none of them.

## 5. Resource/Mine & Vegetation Candidate Foundation

`ResourceSlotCandidate` — `{ GridCoordinate Cell; float Suitability }`. No
resource type, amount, ownership, or production — a bare 0..1 prototype
eligibility score, nothing else. `ResourceCandidateGenerator` selects up to
`MaxResourceCandidates` cells per island (2/3/5 in this prototype),
deterministically via the **same hash function already used by
`IslandHeightmapGenerator`** (made `internal`, reused rather than
re-implemented a third time) seeded by `(config.Seed + fixed salt)` — never
`UnityEngine.Random`. Eligibility = `Land && !Coast`, preferring
`RockOrSteep` cells (mines belong in rocky/high terrain thematically),
falling back to any eligible Land cell if a small island lacks enough rocky
cells. A minimum spacing (6 cells, prototype) between picks is enforced.

`VegetationCandidate` — `{ GridCoordinate Cell }`. `VegetationCandidateGenerator`
is a pure function of the already-deterministic surface map: it returns every
cell already carrying the `VegetationCandidate` flag. No scatter/density/
thinning system yet (brief §26/§52 — placement foundation only, not a forestry
system).

**Per correction: `VegetationCandidate` is generated and fully tested, but not
separately visualized in 0.1.1.** Its current eligibility rule is identical to
`Buildable`'s — rendering a fourth overlay category on top of `Buildable` would
just overdraw the same geometry (Z-fighting risk, no additional information for
the user to see). A dedicated F4 layer is deferred until vegetation
classification actually diverges from buildability; the data/generator
foundation itself is complete and tested regardless.

## 6. Multi-Terrain Height Query

`CompositeWorldHeightSource : IWorldHeightSource`
(`Assets/Game/World/Terrain/`) wraps an explicitly-registered list of
`UnityTerrainHeightSource` (one per island, built once — never
`FindObjectsOfType`). A deterministic linear scan over the small island list is
acceptable per the brief's own explicit allowance for this island count.

**`TryGetHeight` is the authoritative query API** wherever "no terrain here"
(open water between islands) is a real possibility. **`GetHeightAt` throws
`InvalidOperationException`** outside every registered terrain's bounds rather
than silently returning `0f` — `0` is also sea level, so a silent fallback
there would make "no terrain" indistinguishable from "a legitimate sea-level
sample". Every call site that might legitimately query open water
(`SlopeEstimator`, `SurfaceMapGenerator`, `GridCellMeshBuilder`'s existing
`SamplePoint`) was audited to use `TryGetHeight` exclusively.

`GridCellHighlight.SetCell` and `GridMeshBuilder`/`GridCellMeshBuilder` needed
**zero changes** to work correctly with multiple islands — they already only
depended on the `IWorldHeightSource` interface, not on
`UnityTerrainHeightSource` directly, so a `CompositeWorldHeightSource` drops in
transparently.

## 7. Multi-Island Raycast

`WorldPointer` generalized from a single `Collider` to
`IReadOnlyList<Collider>`: `TryRaycast` calls `Collider.Raycast` on **every**
registered terrain collider (still not a generic `Physics.Raycast`/LayerMask —
keeps the "structurally cannot hit water" guarantee from the 0.1.0 magenta/
raycast bugfix) and keeps the closest valid hit by `hit.distance`. Deterministic,
no ambiguity, no per-frame scene search.

## 8. Debug Overlays

- **G — Debug Grid** (unchanged key/behavior): now one combined mesh built by
  merging each island's own `GridMeshBuilder.Build` output (each island's grid
  lines cover exactly that island's own terrain footprint, matching the 0.1.0
  single-island behavior — not the open ocean between islands, avoiding one
  huge, mostly-irrelevant mesh).
- **F2 — Surface/Buildability Overlay** (new): three aggregated meshes
  (`Buildable`, `Coast`, `RockOrSteep`), each spanning every island, each a flat
  `Unlit`-shaded color, each its own `GridDisplay` instance (the same, already-
  tested component reused, not a new display class). Hidden by default,
  toggled together via F2.
- **F3 — Resource Candidate Markers** (new): one small cube marker per
  candidate (≤13 total across all islands in this prototype), built once,
  `ResourceCandidateMarkers.SetVisible` toggles them all. Hidden by default.
- Shader verification before implementation (not a guess): the installed
  `Universal Render Pipeline/Unlit` shader source
  (`UnlitForwardPass.hlsl`) was checked and does **not** read a per-vertex
  `COLOR` attribute — so the overlay uses three separate flat-colored meshes/
  materials (the same proven pattern as `DebugGrid`/`CellHighlight`) rather than
  one vertex-colored mesh.

Input for all three toggles (`G`/`F2`/`F3`) stays entirely in
`WorldInteractionController` (`Varynth.Presentation`) — `Varynth.World`'s new
Surface types remain pure data/generation, no `MonoBehaviour`, no input read.

## 9. Scene Builder / GUID Stability

`WorldPrototypeSceneBuilder` (same file, extended — no second competing
builder) loops `WorldPrototypeIslands.GetDefaultConfigs()`, builds each island,
aggregates a `CompositeWorldHeightSource` and the combined grid/overlay meshes,
resizes `Water` to the whole archipelago's bounding box + margin, recomputes
`StrategyCameraController`'s `CameraRigConfig.BoundsMin/BoundsMax` from that
bounding box, and rewires the expanded `WorldInteractionController` fields
(`Terrain[]`, `TerrainCollider[]`, `GridDisplay[]` overlays,
`ResourceCandidateMarkers`). A pre-2B scene's single `World/TestIsland` child is
explicitly removed on first re-run against an old scene (no orphaned leftover).

Idempotency re-verified with the same method as Phase 2A: stage the generated
scene+assets, rebuild, confirm `git diff --stat` is empty. Confirmed empty
across two additional consecutive rebuilds after the `heightmapResolution` fix
above.

## 10. Tests

**EditMode — new (31):** `IslandSurfaceMapTests` (6), `SlopeEstimatorTests` (4),
`SurfaceMapGeneratorTests` (5), `ResourceCandidateGeneratorTests` (6),
`VegetationCandidateGeneratorTests` (3), `CompositeWorldHeightSourceTests` (5),
`WorldPrototypeSceneBuilderTests` (2, real `Build()` calls — island count +
idempotency via `AssetDatabase.FindAssets`).

**PlayMode — new/extended:** `WorldPointerTests` (4, was 2 — multi-island hit,
open-water miss, foreign-collider-ignored retained), `GridCellHighlightTests`
(2, was 1 — island-switch rebuild), `GridDisplayTests` (3, was 2 — surface
overlays hidden by default; existing tests fixed to target the "Grid"
GameObject by name since `GridDisplay` is now also used by 3 overlay
categories), `ResourceCandidateMarkersTests` (3, new), `WorldPrototypeSceneTests`
(extended assertions: multiple `Terrain`/`TerrainCollider`, ≥4 `GridDisplay`
instances, `ResourceCandidateMarkers` present).

**Results (real Unity 6000.5.7f1 batchmode runs, `-runTests` without `-quit`):**

- EditMode: **242 total, 242 passed, 0 failed, 0 inconclusive, 0 skipped**
  (211 pre-existing + 31 new).
- PlayMode: **14 total, 14 passed, 0 failed, 0 inconclusive, 0 skipped**
  (7 pre-existing + 7 net new/added).
- 0 compile errors, 0 shader errors in both runs.

## 11. Performance / Memory

- No `GameObject`/`MonoBehaviour`/heavy-`Dictionary` entry per surface cell —
  one `byte` (`SurfaceCellFlags`) per cell in a dense array.
- No per-frame terrain re-classification, mesh rebuild, or `FindObjectsOfType`
  in the hover/camera hot path — surface maps and overlay meshes are built once
  at scene-generation time.
- Multi-island raycast/height-query are small deterministic linear scans over
  an explicitly-registered list (≤15 islands per the brief's own stated
  ceiling for this approach), not a spatial-acceleration structure — deferred
  until actually needed.
- Resource markers capped (≤13 total in this prototype) — not a per-cell
  object flood.

## 12. Multiplayer-/Modding-Future-Proofing

`SurfaceMapGenerator`/`ResourceCandidateGenerator`/`VegetationCandidateGenerator`
are pure functions of (world/island data, seed) — no hidden mutable state, no
`UnityEngine.Random`, reproducible given identical inputs (matches
`MEGA_PROMPT` §401/§403's "same seed reproduces same world" mod/QA requirement).
`GridCoordinate` remains the one float-independent, engine-independent
positioning primitive — unchanged, still not carrying an island/owner ID since
none is needed yet (brief §29/§30 explicit). Camera/debug-overlay state stays
100% local `Varynth.Presentation` state. `IslandPrototypeConfig` living outside
the build method itself (not baked as inline private constants) is the first
step toward a later data-driven island loading path — no XML/mod schema
implemented now (explicit non-goal).

## 13. Scope Confirmation

Not implemented in this package: buildings, roads, production, residents,
economy, savegame, Steamworks, multiplayer/networking, Blender assets, real
resource types/amounts, a forestry system, a final map generator.

## 14. Varynth 0.1.1 Build

- `PlayerSettings.bundleVersion` = `0.1.1`.
- Build tool generalized: `Version0_1_0Build.cs` → `MilestoneBuild.cs` (file +
  `.meta` moved together, GUID preserved). **Fully version-neutral per
  correction**: `BuildCurrentVersion()` only *reads* `PlayerSettings.bundleVersion`
  and derives `Builds/Varynth-{version}-linux-x64/` from it — no version
  constant anywhere in the tool. The same unmodified class will build 0.1.2,
  0.2.0, 1.0.0, etc. without any code change; only `PlayerSettings.bundleVersion`
  needs to change per milestone (a one-line `ProjectSettings/ProjectSettings.asset`
  edit for this package, not a tool-code change).
- Output: `Builds/Varynth-0.1.1-linux-x64/` (gitignored, not committed) —
  `Varynth` (ELF executable, verified), `Varynth_Data/` (~64 MiB, non-empty),
  `UnityPlayer.so`. `WorldPrototype` is the only scene in the build.
- Windows build: still not a 0.1.1 blocker (no Windows Build Support installed
  on the Fedora dev machine) — same documented, accepted gap as 0.1.0.

## 15. Manual Visual Acceptance Checklist

1. Open `Assets/Game/Scenes/WorldPrototype.unity`.
2. Play.
3. Multiple islands visible in the sea (4 in this prototype)?
4. Clearly different sizes/shapes (not four resized copies of the same
   contour)?
5. Coastlines/beaches visible on each?
6. Rock/steep areas visible on at least the larger islands?
7. WASD/arrows pan across the whole archipelago?
8. Q/E rotates?
9. Mouse-wheel zoom (fast, multi-step, per the 0.1.0-round zoom fix)?
10. `G` toggles the debug grid?
11. `F2` toggles the Buildable/Coast/RockOrSteep overlay?
12. Mouse hover highlights the correct cell on every island (not just the
    first one)?
13. `F3` toggles small resource-candidate markers (not implying real mines)?
14. No magenta materials anywhere?
15. No white grid/highlight artifacts?
16. Console shows 0 errors?
17. Repeat the relevant checks (3–13, 16) against
    `Builds/Varynth-0.1.1-linux-x64/Varynth` (standalone build).

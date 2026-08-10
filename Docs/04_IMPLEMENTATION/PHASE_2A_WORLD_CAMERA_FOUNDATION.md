# Phase 2A — World & Camera Foundation (Varynth 0.1.0)

Erster sichtbarer Meilenstein: `WorldPrototype.unity` zeigt im Play Mode eine navigierbare
Testwelt (Wasser, Insel mit echtem Terrainrelief, Küstenlinie, Debug-Baugrid, Strategiekamera,
Maus-Hover-Highlight). Dieses Paket ist gleichzeitig **VARYNTH 0.1.0** (erstes Roadmap-Release).

Kein Gameplay, keine Wirtschaft, kein Savegame, kein Multiplayer, keine Blender-Assets.
Alle Zahlenwerte in diesem Dokument sind **technische Prototypwerte**, keine finalen
Design-/Balancing-Entscheidungen (siehe `SPEC_AUDIT.md`: Maßstab/Grid waren zu Phase-0-Ende
noch offen).

## 0. Bugfix-Runde 2 (Post-Review, drei Runtime-/UX-Bugs)

Die zweite manuelle visuelle Abnahme fand drei konkrete Bugs. Alle drei behoben, verifiziert
(EditMode 211/211, PlayMode 7/7), Linux-0.1.0-Build neu erzeugt.

**1. Mausrad-Zoom viel zu langsam.** Ursache: `StrategyCameraController.ReadZoomInput` dämpfte
das rohe Scroll-Delta zusätzlich mit einem fest verdrahteten Faktor `0.01f` **vor** der
eigentlichen `ZoomStepPerScrollUnit`-Anwendung — bei den unter dieser Linux/Input-System-
Konfiguration typischen kleinen Rohwerten (~1 pro Notch) ergab das faktisch unmerkliche
Zoomschritte. Fix: fester `0.01f`-Faktor entfernt, neues, direkt im Inspector konfigurierbares
Feld `CameraRigConfig.ZoomSensitivity` (Prototypwert **8**, ausdrücklich als nicht-final
dokumentiert) wird jetzt direkt mit dem rohen Scroll-Delta multipliziert:
`_zoomTarget -= scrollDelta * ZoomSensitivity`. Weiterhin kein `Time.deltaTime` im Delta-Pfad —
nur die anschließende Distanz-Interpolation bleibt frame-rate-basiert (`ZoomSmoothSpeed`, jetzt
10). Min/Max-Zoom unverändert. Schnelles Scrollen akkumuliert weiterhin natürlich, da
`Mouse.scroll` bereits alle Events seit dem letzten Frame aufsummiert zurückgibt.

**2. `G`-Toggle schaltete das Debug-Grid nicht um.** Ursache (per Codeanalyse diagnostiziert,
nicht geraten): `GridDisplay._meshFilter`/`_meshRenderer` waren reine private Felder, die
ausschließlich innerhalb von `Initialize(Mesh)` gesetzt wurden — und `Initialize` wurde nur
**einmal, zur Editor-Bauzeit** vom Scene Builder aufgerufen, nie erneut beim tatsächlichen
Play-Mode-Start. Beim echten Laden der Szene blieben beide Felder `null`, wodurch
`SetVisible(bool)` (`if (_meshRenderer != null) ...`) still ins Leere lief — der Toggle-Code
selbst (`WorldInteractionController.UpdateGridToggle`) war korrekt, nur seine Zielkomponente
war "leer". Fix: `GridDisplay` holt die Komponentenreferenzen jetzt zusätzlich in `Awake()`
(`EnsureComponents()`, idempotent), sodass `SetVisible` nach einem echten Szenen-Load
funktioniert. Kein Material-/Mesh-Rebuild pro Frame, `SetVisible(bool)` unverändert
wiederverwendet, Toggle-Zustand bleibt eindeutig in `WorldInteractionController._gridVisible`.
Initialzustand: sichtbar (`MeshRenderer.enabled` startet `true`). Automatisierter Test:
`GridDisplayTests.SetVisible_TogglesRendererEnabled_AfterRealSceneLoad` (PlayMode, gegen die
echte geladene Szene, nicht ein frisch konstruiertes GameObject — reproduziert exakt die
Bedingungen, unter denen der Bug auftrat). Die tatsächliche Tastenbetätigung bleibt manuell zu
prüfen.

**3. Grid-Hover/Highlight nicht perspektivisch/terrainkorrekt (wichtigster Bug).**
- **Raycast-Pipeline neu**: `WorldPointer` raycastet jetzt nicht mehr per `LayerMask` gegen
  "irgendeinen Collider auf diesem Layer", sondern **direkt gegen den konkreten
  `TerrainCollider`** (`_terrainCollider.Raycast(ray, out hit, ...)`), der vom Scene Builder
  explizit verdrahtet wird. Das ist strukturell eindeutig — selbst wenn künftig versehentlich
  wieder ein Collider auf der Wasserplane landet, kann der Pointer ihn nicht treffen (verifiziert
  durch `WorldPointerTests.Raycast_IgnoresOtherColliders_OnlyTargetsAssignedTerrainCollider`,
  die einen blockierenden Collider absichtlich näher am Ray-Ursprung platziert und prüft, dass
  trotzdem die Terrainoberfläche getroffen wird). `Camera.main` wird weiterhin nicht pro Frame
  gesucht — `WorldInteractionController._camera` war bereits ein gecachtes serialisiertes Feld.
- **Highlight-Geometrie neu**: Das Highlight war zuvor ein flaches, um 90° gekipptes Quad an
  einer einzigen Höhe (Zellzentrum) — auf geneigtem Terrain schnitt es sichtbar durch den Hang
  und stimmte nicht mit der realen 3D-Oberfläche überein. Neuer Ansatz:
  `Varynth.World.Grid.GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset)`
  (rein, in `Varynth.World`) bestimmt die vier Weltraum-Eckpunkte der Zelle, sampelt an jeder
  Ecke die echte Terrainhöhe über `IWorldHeightSource`, baut daraus ein 4-Vertex/2-Tri-Mesh und
  ruft `RecalculateNormals()`. `GridCellHighlight` (Presentation) besitzt jetzt selbst
  `MeshFilter`/`MeshRenderer` (kein separates Quad-Kind-Objekt mehr) und baut das Mesh nur bei
  tatsächlichem Zellwechsel neu (`SetCell` early-returns bei unveränderter Zelle). Kleiner
  konfigurierbarer `_heightOffset` (Default `0.06`) gegen Z-Fighting bleibt erhalten. Getestet
  in `GridCellMeshBuilderTests` (EditMode, u. a. `BuildCellQuad_FollowsSlopedTerrain_...` beweist
  unterschiedliche Eckhöhen auf geneigtem Terrain, `BuildCellQuad_NormalPointsUpward` beweist
  korrekte Sichtbarkeit von oben) und `GridCellHighlightTests` (PlayMode, gegen die echte Szene).
  Keine Screen-Space-Sonderkorrektur — die perspektivische Korrektheit ergibt sich allein aus
  echter World-Geometrie plus Standard-Kameraprojektion, wie gefordert.
- **Weiße Streifen/Flecken**: hingen tatsächlich mit dem Highlight (und dem Debug-Grid)
  zusammen. Beide nutzten zuvor `Universal Render Pipeline/Lit`; das Grid-Mesh hat als
  Linientopologie keine sinnvollen Normalen, und das Highlight-Quad lag nahezu flach unter dem
  Directional Light — beides führte zu Lit-Shading-Artefakten (sehr helle/weiße Aufhellung).
  Fix: beide Materialien laufen jetzt auf `Universal Render Pipeline/Unlit`
  (`WorldPrototypeSceneBuilder.UnlitShaderName`), was für Debug-Overlay-Geometrie ohnehin der
  konventionellere, lichtunabhängige Ansatz ist. Terrain/Wasser bleiben unverändert bei
  `Universal Render Pipeline/Lit`.
- **Scene-Builder-Aufräumarbeiten** (nötig, weil die alte Szene bereits mit der alten
  Highlight-Architektur gespeichert war): eine verwaiste `GridCellHighlight`-Komponente auf dem
  `Grid`-Root (alte Architektur) wird jetzt entfernt; das `Highlight`-Kind-GameObject wird auf
  Identity-Transform zurückgesetzt (das neue Mesh liefert bereits Weltraum-Koordinaten, jede
  Rotation/Skalierung hätte es doppelt transformiert) und explizit aktiv gehalten (Sichtbarkeit
  läuft jetzt ausschließlich über `MeshRenderer.enabled`, nicht mehr über
  `GameObject.SetActive`).

## 1. Assemblystruktur

Neu, additiv zu Phase 1A–1C, keine bestehende Datei aus `Varynth.Core.Definitions`,
`Varynth.Data` oder `Varynth.Core.Simulation` verändert (außer der additiven `GridCoordinate`
in Core):

```
Assets/Game/Core/Common/GridCoordinate.cs      (Varynth.Core.Definitions, additiv)

Assets/Game/World/Varynth.World.asmdef          -> Varynth.Core.Definitions
  Grid/WorldGrid.cs, Grid/GridMeshBuilder.cs
  Terrain/IWorldHeightSource.cs, UnityTerrainHeightSource.cs, IslandHeightmapGenerator.cs
  Interaction/WorldPointer.cs

Assets/Game/Presentation/Varynth.Presentation.asmdef  -> Varynth.Core.Definitions, Varynth.World, Unity.InputSystem
  Camera/CameraRigConfig.cs, CameraRigMath.cs, StrategyCameraController.cs
  Visualization/GridDisplay.cs, GridCellHighlight.cs
  Interaction/WorldInteractionController.cs

Assets/Game/Editor/Varynth.Tooling.Editor.asmdef (Editor-only) -> Varynth.World, Varynth.Presentation, Varynth.Core.Definitions, Unity.InputSystem, Unity.RenderPipelines.Core.Runtime
  WorldPrototype/WorldPrototypeSceneBuilder.cs
  WorldPrototype/Version0_1_0Build.cs

Assets/Game/Scenes/WorldPrototype.unity
```

Dependency-Richtung strikt bottom-up: `World → Core`, `Presentation → World + Core`,
`Editor → World + Presentation + Core`. Keine zyklischen Referenzen. `Varynth.Core.Simulation`
bleibt von diesem Paket komplett unberührt — keine neue Abhängigkeit in beide Richtungen.

**Trennung Input/Rendering von Spatial-Logik (Korrektur während der Planung):**
`Varynth.World` enthält ausschließlich Spatial-/Grid-/Terrainlogik (`WorldGrid`,
`GridMeshBuilder`, `IWorldHeightSource`, `WorldPointer`) — keine `MonoBehaviour`-Renderkomponente,
kein Input-Read. `WorldPointer.TryRaycast(Ray, ...)` nimmt den `Ray` als Parameter entgegen und
liest selbst nie `Mouse.current`/`Keyboard.current`. Alle sichtbaren/interaktiven Komponenten
(`GridDisplay`, `GridCellHighlight`, `WorldInteractionController`) sowie **jeglicher Tasten-/
Maus-Input inklusive des Debug-Grid-Toggles (`G`)** liegen in `Varynth.Presentation`.

## 2. World-/Grid-Konvention (Prototyp)

- X/Z = horizontale Weltebene, Y = Höhe. 1 Unity-Unit = 1 Meter (Engine-Konvention, keine
  kanonische Spielmaßstabsentscheidung).
- Grid-Zellgröße: **4.0 Units** (`WorldGrid.CellSize`), Grid-Ursprung: Welt-`(0,0)`.
- `WorldGrid.WorldToCell` nutzt explizites `Mathf.FloorToInt`, keinen naiven Cast — negative
  Koordinaten werden korrekt behandelt (kein Rundungsfehler an Zellgrenzen).
- `GridCoordinate` (`Varynth.Core.Common`, in der `noEngineReferences:true`-Assembly) ist ein
  reiner `readonly struct { int X; int Z; }` mit Value Equality — bewusst ohne `Vector3`, damit
  eine spätere Simulation/ein Command diesen Typ referenzieren kann, ohne eine Engine-Abhängigkeit
  zu erben. Keine Belegungslogik, kein `BuildBuildingCommand` in diesem Paket.

## 2a. Bugfix: magenta Terrain (Post-0.1.0-Review)

Die manuelle visuelle Abnahme zeigte `TestIsland` vollständig magenta/pink. Ursache: Ein per
`AddComponent<Terrain>()` skriptgeneriertes Terrain erhält — anders als über das
"GameObject > Terrain"-Menü — kein automatisches Material (`m_MaterialTemplate: {fileID: 0}`);
Unity fällt darauf unter URP auf `Hidden/InternalErrorShader` zurück. Fix in
`WorldPrototypeSceneBuilder.GetOrCreateTerrainMaterial`: explizite Erzeugung/Zuweisung eines
Materials mit Shader **`Universal Render Pipeline/Terrain/Lit`** (verifiziert gegen
`Library/PackageCache/com.unity.render-pipelines.universal@.../Shaders/Terrain/TerrainLit.shader`,
nicht geraten), fester Asset-Pfad `Assets/Game/World/Art/Materials/TestIslandTerrain.mat`,
zugewiesen an `terrain.materialTemplate`. Wasser-/Grid-/Highlight-Materialien (`Universal
Render Pipeline/Lit`) waren bereits korrekt und unverändert. Beide Shader-Erzeugungspfade
(`GetOrCreateTerrainMaterial`, `GetOrCreateMaterial`) werfen jetzt zusätzlich eine explizite
Exception, falls `Shader.Find` `null` liefert, statt still ein Material ohne Shader anzulegen.

## 3. Terrain / Sea-Level-Konvention (verbindliche technische Korrektur)

- Terrain-Weltgröße (X/Z): **300 × 300 Units**.
- `TerrainData.size` (vertikal): **40 Units**.
- Terrain-`Transform.position.y`: **−15**.
- Damit liegt Sea Level (Welt-Y = 0) bei normalisierter Terrainhöhe **15/40 = 0.375**
  (`TerrainData.SetHeights` erwartet 0..1, keine negativen Werte — die Absenkung passiert über
  die Transform-Position, nicht über negative Heightmap-Werte).
- Terrain reicht real von Welt-Y **−15** (tiefstes Heightmap-Sample) bis **+25**
  (höchstes Sample) — es existiert also echtes Terrain unterhalb UND oberhalb des Meeresspiegels.
  `IslandHeightmapGeneratorTests.Generate_ProducesValuesBothBelowAndAboveSeaLevel` prüft das
  explizit gegen die Schwelle 0.375.
- Heightmap-Auflösung: 257 (2ⁿ+1, Unity-Anforderung).

## 4. Insel-/Terrain-Generierung

`IslandHeightmapGenerator` (`Varynth.World.Terrain`, reines C#, keine `UnityEngine.Random`-
Verwendung) erzeugt deterministisch (fixer Seed `20260809`, dokumentiert in
`WorldPrototypeSceneBuilder`):
- Eigene Hash-basierte Value-Noise-Funktion mit FBM (mehrere Oktaven) für Elevationsvariation.
- Elliptische Falloff-Maske, deren Radius selbst durch eine zweite Noise-Oktave entlang des
  Winkels perturbiert wird — keine perfekte Kreisinsel, echte unregelmäßige Küstenlinie.
- Ergebnis ist garantiert nicht flach: äußerer Bereich fällt auf `raw = 0` (tiefes Wasser,
  weit unter Sea Level), Inland bleibt durch Konstruktion immer `raw ≥ 0.55` (über Sea Level).
- Determinismus getestet: gleicher Seed + gleiche Auflösung → bitidentisches Array
  (`IslandHeightmapGeneratorTests.Generate_SameSeedAndResolution_ProducesIdenticalHeights`).

Terrain-Materialien: drei `TerrainLayer`s (Sand/Gras/Fels), Alphamap-Painting nach
Höhen-Schwelle (Sand nahe Sea-Level-Band 0.375–0.425) und Steilheit (Fels ab ~25°–45°) —
deterministisch aus derselben Heightmap abgeleitet.

## 5. Wasser (Prototyp)

Einfache skalierte `Plane`-Primitive (Unity-Standardmesh, kein Custom-Asset), positioniert bei
Welt-Y = 0, Größe = Terrain + 60 Units Rand pro Seite. URP-Lit-Material mit Transparenz
(`Water.mat`, fixer Pfad). Kein Custom-Collider (entfernt, damit Weltraycasts nur das Terrain
treffen). Keine Wellen-/Brandungssimulation, kein Unterwasser-Rendering — bewusst nicht Teil
dieses Pakets.

## 6. Debug-Grid

`GridMeshBuilder` (rein, `Varynth.World.Grid`) erzeugt ein einzelnes `LineList`-Mesh (Vertikal-
und Horizontallinien über den Terrainbereich), pro Vertex höhensampled über
`IWorldHeightSource`, damit das Grid dem Terrainrelief folgt. **Kein GameObject pro Zelle/Linie.**
Das Mesh wird als eigenständiges Asset unter einem festen Pfad
(`Assets/Game/World/Art/Meshes/DebugGridMesh.asset`) gespeichert und bei Rebuilds per
`EditorUtility.CopySerialized` in-place aktualisiert (GUID-stabil, siehe Abschnitt 9).
`GridDisplay` (Presentation) zeigt das Mesh nur an; `SetVisible(bool)` wird ausschließlich von
`WorldInteractionController` über die Taste **G** gesteuert (Input bleibt vollständig in
Presentation).

## 7. Grid-Cell-Highlight

`GridCellHighlight` (Presentation) besitzt ein einzelnes Quad. `SetCell(...)` bewegt/aktiviert
es nur bei tatsächlichem Zellwechsel (kein Rebuild pro Frame), Höhe wird pro Zellzentrum über
`IWorldHeightSource` gesampled (kein Z-Fighting mit dem Terrain).

## 8. Strategiekamera

`StrategyCameraController` (Presentation, `Assets/Game/Presentation/Camera/`):
- Rig: `CameraRigRoot` → `Yaw`-Kind → `Main Camera`-Kind, feste Pitch-Offset-Distanz.
- **Pan**: WASD/Pfeiltasten, relativ zur aktuellen Blickrichtung; Shift = schneller
  (`FastPanMultiplier`).
- **Rotation**: Q/E, `RotationSpeedDegreesPerSecond`, über `CameraRigMath.WrapYaw` normalisiert.
- **Zoom**: Mausrad. **Korrektur**: Das Scroll-Delta verändert `_zoomTarget` direkt als
  Eingabedelta (nicht mit `Time.deltaTime` multipliziert); die tatsächliche Kameradistanz nähert
  sich `_zoomTarget` danach separat frame-rate-unabhängig über `Time.deltaTime`-Interpolation an.
- Alle Clamp-/Wrap-Berechnungen laufen über `CameraRigMath` (statisch, ohne `MonoBehaviour`,
  ohne `UnityEngine.Input`) — unit-testbar ohne Szene.
- Direktes Polling von `Keyboard.current`/`Mouse.current` (neues Input System) statt eines
  eigenen `.inputactions`-Assets — bewusste Prototyp-Vereinfachung für diese Handvoll Rohachsen,
  dokumentiert, später ohne Architekturänderung erweiterbar.
- Kein `Rigidbody`, keine `FixedUpdate`-Kopplung.

Prototyp-Werte (`CameraRigConfig`): `PanSpeed=40`, `FastPanMultiplier=2.2`,
`RotationSpeedDegreesPerSecond=90`, `PitchDegrees=55`, `ZoomMinDistance=15`,
`ZoomMaxDistance=120`, `ZoomSensitivity=8` (§0, nicht final — Bugfix-Runde 2, Inspector-
konfigurierbar), `ZoomSmoothSpeed=10`, `BoundsMin=(20,20)`, `BoundsMax=(280,280)`.

## 9. World Pointer / Raycast

`WorldPointer` (`Varynth.World.Interaction`) ist ein reiner Spatial-Helfer: `TryRaycast(Ray, out
Vector3)` nimmt den Ray entgegen, führt `Physics.Raycast` gegen die Terrain-`TerrainCollider`
aus, `ToCell(...)` delegiert an `WorldGrid`. Liest selbst keinen Input.

`WorldInteractionController` (Presentation, einziger Ort mit Maus-/Tastatur-Read für diesen
Bereich) verkettet pro Frame: `Mouse.current.position` → `Camera.ScreenPointToRay` →
`WorldPointer.TryRaycast` → `WorldGrid.WorldToCell` → `GridCellHighlight.SetCell`. Zusätzlich
der `G`-Toggle für `GridDisplay.SetVisible`.

## 10. Reproduzierbarer Scene Builder

`WorldPrototypeSceneBuilder` (`Varynth.Tooling.Editor`, Menüpunkt `Varynth/Build World
Prototype`, batchmode-fähig via `-executeMethod`) baut die komplette Szene reproduzierbar:
Hierarchie wird per Name gesucht/wiederverwendet statt gelöscht/neu erzeugt; alle generierten
Assets (TerrainData, TerrainLayers, Prototype-Texturen, Materialien, Debug-Grid-Mesh) liegen
unter **festen Pfaden** und werden bei erneutem Lauf per `AssetDatabase.LoadAssetAtPath` +
In-Place-Update (`EditorUtility.CopySerialized`/direkte Property-Zuweisung + `SetDirty`)
aktualisiert statt neu angelegt.

**Verifiziert**: Ein zweiter/dritter Build-Lauf gegen eine bereits committete (gestagte) Szene
erzeugt einen **leeren** `git diff` — keine GUID-Churn, kein spuriosum Diff.

## 11. Multiplayer-/Koop-Future-Proofing

- `GridCoordinate` (Core, engine-unabhängig) ist der Identitätstyp, den ein späteres
  `BuildCommand` transportieren könnte — noch nicht gebaut.
- Kamera- und Cursor-/Highlight-State sind vollständig lokaler `MonoBehaviour`-State in
  `Varynth.Presentation`, ohne jede Kopplung an `Varynth.Core.Simulation`.
- Kein `BuildCommand`, kein Ownership, kein Steamworks/Networking — nichts davon in diesem
  Paket ergänzt.

## 12. Performance

- Kein GameObject pro Grid-Zelle/-Linie (ein Mesh).
- Kein `FindObjectOfType` im Hot Path (Referenzen einmalig vom Scene Builder verdrahtet).
- Kein Mesh-Rebuild pro Frame; Grid-Highlight aktualisiert nur bei Zellwechsel.
- Keine LINQ-Nutzung in Kamera-/Interaction-Update-Pfaden.
- Terrain-Prototype-Texturen klein (32×32, prozedural).

## 13. Bekannte Grenzen / bewusste Prototyp-Entscheidungen

- Kamera nutzt direktes Input-System-Device-Polling statt `.inputactions`-Action-Map-Asset.
- Wasser ist eine einfache transparente Plane ohne Wellen-/Brandungssimulation.
- Grid-Deckungsbereich entspricht der vollen Terrainfläche (75×75 Zellen bei 4-Unit-Zellgröße) —
  kein Streaming/Culling, für diesen Prototypumfang ausreichend performant.
- **PlayMode-Testassembly-Konfiguration**: `Varynth.Tests.PlayMode.asmdef` verwendet
  `includePlatforms: []` (Standard-PlayMode-Konvention, damit die Assembly korrekt als
  PlayMode-testbar erkannt wird) **und** ein explizites `excludePlatforms` für die
  Standalone-Zielplattformen (`LinuxStandalone64`, `WindowsStandalone64`, `WindowsStandalone32`,
  `macOSStandalone`). Kein `defineConstraints: ["UNITY_INCLUDE_TESTS"]` (per Vorgabe explizit
  ausgeschlossen). Begründung: Ein regulärer `BuildPipeline.BuildPlayer`-Lauf ohne
  `BuildOptions.IncludeTestAssemblies` kompiliert `UnityEngine.TestRunner`-referenzierende
  Assemblies für das Standalone-Ziel nicht auflösbar (führte zu Compile-Fehlern im 0.1.0-Build);
  `BuildOptions.IncludeTestAssemblies` wurde probiert, löste das ursprüngliche Problem, brach
  aber an einem Bug im installierten `com.unity.inputsystem`-Paket (`InputSystemTestHooks.cs`,
  fehlendes `InputSystem.remoteConnection` unter `UNITY_INCLUDE_TESTS`) — ein Paketfehler
  außerhalb dieses Projekts. Die gewählte `excludePlatforms`-Lösung vermeidet beide Probleme,
  ohne die verbotene Define Constraint zu benötigen, und wurde verifiziert: Tests bleiben unter
  `-testPlatform PlayMode` sichtbar/lauffähig (4/4 bestanden) UND der reguläre 0.1.0-Standalone-
  Build kompiliert fehlerfrei.

## 14. Tests und reale Ergebnisse

Echter Unity-6000.5.7f1-Batchmode-Lauf (ohne `-quit` für die Testläufe), Stand nach
Bugfix-Runde 2 (§0):
- **EditMode**: 211/211 bestanden, 0 fehlgeschlagen, 0 inconclusive, 0 skipped
  (178 aus Phase 1A–1C + 29 aus der ersten 2A-Runde + 4 neu: `GridCellMeshBuilderTests`).
- **PlayMode**: 7/7 bestanden, 0 fehlgeschlagen (`WorldPrototypeSceneTests` 2,
  `WorldPointerTests` 2, `GridDisplayTests` 2, `GridCellHighlightTests` 1).
- 0 Compile-Errors, 0 Shader-Errors/-Warnings in beiden Läufen.

## 15. Varynth 0.1.0 Build

- `PlayerSettings.bundleVersion` = `0.1.0` (`ProjectSettings/ProjectSettings.asset`).
- Linux-x86_64-Standalone-Build real erzeugt über
  `Varynth.Tooling.Editor.WorldPrototype.Version0_1_0Build.Build`
  (Menüpunkt `Varynth/Build Varynth 0.1.0 (Linux x86_64)`, batchmode-fähig).
- Ausgabe: `Builds/Varynth-0.1.0-linux-x64/` (durch `.gitignore` `Builds/` ausgeschlossen,
  nicht committed) — enthält `Varynth` (ELF-Executable, verifiziert), `Varynth_Data/`
  (~58 MiB, nicht leer), `UnityPlayer.so`.
- `WorldPrototype` ist die einzige Szene im Build.
- Windows-Build: kein Blocker für 0.1.0 (kein Windows Build Support auf dem
  Fedora-Entwicklungsrechner installiert) — dokumentierte, akzeptierte Lücke.

## 16. Manuelle Visual-Checkliste

1. `Assets/Game/Scenes/WorldPrototype.unity` in Unity öffnen.
2. Play drücken.
3. Insel mit Höhenrelief und unregelmäßiger Küste sichtbar?
4. Wasser rund um die Insel sichtbar, Küstenlinie lesbar (Sand → Gras → Fels)?
5. WASD/Pfeiltasten bewegen die Kamera?
6. Mausrad zoomt (näher/weiter)?
7. Q/E dreht die Kamera?
8. Debug-Grid im Game View sichtbar? Taste **G** blendet es um/aus?
9. Maus über die Insel bewegen — folgt das gelbe Highlight-Quad der Zelle unter dem Cursor?
10. Console: 0 Errors?

Danach zusätzlich denselben Ablauf (Punkte 3–9, ohne Editor-Menüs) gegen
`Builds/Varynth-0.1.0-linux-x64/Varynth` (Standalone-Executable) wiederholen.

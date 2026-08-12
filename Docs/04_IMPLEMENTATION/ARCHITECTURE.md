# ARCHITECTURE.md — Varynth Technische Architektur (Arbeitsfassung v0.1, Phase 0)

Status: erste technische Architekturarbeitsfassung. Kein Code in diesem Paket. Grundlage: `MEGA_PROMPT_v1.0.md`, `CLAUDE_IMPLEMENTATION_GUIDE_v1.0.md`, `FINAL_AUDIT_PASS09.md` sowie die Fach-Bibles. Wird paketweise verfeinert; spätere Pakete dürfen diese Fassung präzisieren, aber nicht stillschweigend gegen die Grundprinzipien in diesem Dokument verstoßen.

## 0. Zentrale Architekturregel (verbindlich für alle späteren Pakete)

**Strikte Schichtentrennung: Definition/Data → Runtime State/Simulation → Presentation/View.**

- **Definition/Data-Ebene**: unveränderliche, aus XML (oder äquivalenten moddbaren Quellen) geladene Content-Definitionen. Reine Werte, keine Laufzeitlogik, kein Unity-`MonoBehaviour`.
- **Runtime State/Simulation-Ebene**: veränderlicher Spielzustand, referenziert Definitionen über stabile IDs, läuft Engine-unabhängig oder zumindest Rendering-unabhängig, eigener Tick, keine direkte Kenntnis von GameObjects/Renderern.
- **Presentation/View-Ebene**: liest Simulationszustand (lesend, über definierte Schnittstellen/Snapshots), erzeugt Visuals/Audio/UI. Besitzt selbst keinen kanonischen Spielzustand.

**Hartes Verbot:** Eine einzelne Unity-`MonoBehaviour`-Klasse darf nicht gleichzeitig Definition, Simulationszustand, Save-State, UI und Darstellung besitzen. Verstöße dagegen sind in jedem Code-Review-Gate (`QA_GATES.md`) ein Fail-Kriterium. Diese Trennung ist die Voraussetzung dafür, dass Simulation und visuelle Repräsentation getrennt bleiben (siehe §2) und dass Millionen von Bewohnern nicht als permanente Einzel-GameObjects existieren müssen.

## 1. Grundprinzipien aus der Spezifikation

- Tiefe entsteht aus Produktionsketten, Bevölkerung, regionaler Verflechtung, Handel, Forschung, Diplomatie, Exploration — **nicht** aus Mikromanagement (kein Pflicht-Verkehrsstau, keine Einzelfahrzeugsteuerung, keine Einzel-Pipeline-/Ventilsteuerung, keine Einzel-Crowd-Agentensimulation).
- Simulation läuft entkoppelt vom Render-FPS, mit eigener(n) Tickrate(n) für lokale Ökonomie; Hintergrundregionen werden aggregiert/statistisch behandelt, nicht vollständig simuliert.
- Architektur muss auf 2.000.000+ statistische Bewohner, 20.000+ reguläre Bauwerke, 50.000er Stresstest, mehrere parallel aktive Regionen skalieren können, ohne dass jeder Bewohner ein permanentes GameObject ist.
- Datengetriebenheit ist Pflicht: XML ist ein zentraler moddbarer Contentpfad; die Architektur darf nicht voraussetzen, dass Content nur hart in C# oder ScriptableObjects existiert.
- Keine feste Tierobergrenze (Regionen/Zivilisationsstufen/Automationsgrade etc.) darf hart im Code verankert werden.

## 2. Simulation vs. Präsentation — Trennungsprinzip

Simulation (Ökonomie, Bevölkerung, Produktion, Logistik, Forschung, Diplomatie, Quests-Zustand) besitzt den kanonischen Zustand. Präsentation (Meshes, Instancing, UI, Audio, VFX) liest daraus ab und darf niemals rückwirkend die Simulation über Seiteneffekte in Render-/MonoBehaviour-Code verändern; jede Rückwirkung (z. B. Spielerklick auf ein Gebäude) läuft über explizite Befehls-/Intent-Schnittstellen in die Simulationsebene, nie direkt auf internen Simulationszustand.

**Bewohnerdarstellung**: Bewohner existieren in der Simulation als aggregierte/statistische Datensätze pro Wohnhaus/Region (Anzahl, Klasse, Zufriedenheit, Bedarfsdeckung), nicht als Einzelagenten. Sichtbare Crowd-Repräsentation (falls vorhanden) ist ein rein präsentatives, gepooltes/instanziertes Sichtbarkeits-Sampling ohne eigenen kanonischen Zustand.

**Regionen/Weltwechsel**: Nicht aktive/nicht sichtbare Regionen laufen auf einer aggregierten/reduzierten Simulationsebene weiter (siehe §4), werden beim Betreten/Aktivieren auf volle Detailsimulation hochgefahren bzw. beim Verlassen in eine kompakte Zusammenfassung heruntergefahren.

## 3. Vorläufige Assembly-/Modulstruktur

Assembly-Definitionsdateien (`.asmdef`) trennen Domänen, um zyklische Abhängigkeiten strukturell zu verhindern und Editor-only-Code (Blender-Tooling, Validatoren) vom Runtime-Build fernzuhalten. Abhängigkeitsrichtung strikt von unten nach oben (Presentation/Narrative dürfen Core referenzieren, nie umgekehrt):

- `Varynth.Core.Definitions` — Definitionsdatentypen, ID-/Namespace-Primitiven (`ContentId`, `ContentSourceId`, `LocalizationKey`), generische `ContentRegistry<T>`, `ContentReference<T>`, keine Unity-Engine-Abhängigkeit (`noEngineReferences: true`).
- `Varynth.Data` — datengetriebene Lade-Pipeline: XML-Parsing (gehärtet, kein DTD/keine externen Entities), Content-Source-/Mod-Manifest-Abstraktion, deterministische Ladeorder, Definition-Loader, Validierungs-/Report-Mechanismus (Phase 1B). Referenziert `Varynth.Core.Definitions`, `noEngineReferences: true`. Wird nur beim Content-Laden gebraucht, nicht von `Varynth.Core.Simulation`/`Presentation` zur Laufzeit referenziert.
- `Varynth.Core.Simulation` — Wirtschaftssimulation, Bevölkerung, Produktion, Logistik, Forschung, Diplomatie/KI, Utilities, Disasters, Expeditionen; Engine-unabhängig, wo möglich reines C#. Fundament seit Phase 1C: `GameClock`/`GameTick` (deterministischer Tick-Zähler, keine Wall-Clock-/FPS-Kopplung), `SimulationScheduler`/`ISimulationSystem` (deterministische Systemreihenfolge), `SimulationContext`/`SimulationLevel`/`SimulationLevelMask` (ActiveNear/ActiveFar/Background), `PlayerId`/`ISimulationCommand` (Multiplayer-Future-Proofing, siehe unten). Referenziert nur `Varynth.Core.Definitions` (für Diagnostics), `noEngineReferences: true`.
- `Varynth.World` — **implementiert seit Phase 2A** (Grid-/Terrain-Fundament: `WorldGrid`, `GridMeshBuilder`, `IWorldHeightSource`/`UnityTerrainHeightSource`, `IslandHeightmapGenerator`, `WorldPointer`); **erweitert seit Phase 2B** um Multi-Insel-Unterstützung (`CompositeWorldHeightSource`, `WorldPointer` über mehrere registrierte Collider) und eine kompakte Surface-Classification-Grundlage (`Varynth.World.Surface`: `SurfaceCellFlags`, `IslandSurfaceMap`, `SlopeEstimator`, `SurfaceMapGenerator`, `ResourceCandidateGenerator`, `VegetationCandidateGenerator`); **erweitert seit Phase 2C** um `Varynth.World.Placement` (Footprint-/Rotations-Math, `IslandOccupancyMap`, `PlacementValidator`, `ArchipelagoPlacementState` — command-agnostisches Welt-Bau-State, aus laufzeitfähigen `IslandSurfaceRuntimeData`-Assets aufgebaut, nicht aus Editor-only-Typen — plus `BuildingPlacementCommandHandler`, dem einzigen Ort mit einer neuen, eng begrenzten Referenz auf `Varynth.Core.Simulation`); **erweitert seit Phase 2D** um `BuildingRepeatPlanner` (deterministischer Drag/Repeat-Origin-Generator) sowie `Varynth.World.Roads` (`RoadGraph`/`RoadNetworkState` — command-agnostisches, zu `ArchipelagoPlacementState` referenzfrei gehaltenes Straßen-Welt-State; `RoadPlacementValidator`, `RoadRouter` (deterministisches Integer-A\*), `RoadMeshBuilder`, `BuildingRoadConnectionQuery`, `RoadCommandHandler`) — Cross-Validierung zwischen Gebäude- und Straßen-State läuft über die neutralen `IBuildingOccupancyQuery`/`IRoadOccupancyQuery`-Interfaces, nie über eine direkte Referenz der beiden State-Typen aufeinander. `RoadCommandHandler` nutzt dieselbe bereits bestehende `Varynth.Core.Simulation`-Referenz wie `BuildingPlacementCommandHandler`, keine neue Assembly-Kante nötig. Region-/Weltwechsel/Streaming-Koordination weiterhin offen für spätere Pakete. Referenziert `Varynth.Core.Definitions` (+ `Varynth.Core.Simulation`, für `BuildingPlacementCommandHandler`/`RoadCommandHandler`).
- `Varynth.Presentation` — **implementiert seit Phase 2A** (Strategiekamera, Debug-Grid-Darstellung, Grid-Cell-Highlight, Welt-Interaktion/Input); **erweitert seit Phase 2C** um `Varynth.Presentation.Placement` (`PlacementController`, `PlacementGhostDisplay`, Prototype-Bauleiste) — erste echte Nutzung der Command-Boundary (`PlaceBuildingCommand`/`RemoveBuildingCommand`). Instancing/LOD/VFX weiterhin offen; **erweitert seit Phase 2D** um `Varynth.Presentation.Roads` (`RoadPlacementController`, `RoadPreviewDisplay`, `RoadNetworkDisplay` — genau ein zur Laufzeit geklonter, wiederverwendeter Mesh pro Insel, nie das gespeicherte Editor-Asset direkt mutiert) sowie `DragPreviewDisplay` (gepoolter Multi-Ghost, zwei Meshes statt einem GameObject pro geplantem Gebäude) und `ConstructionToolCoordinator`/`ConstructionToolCoordinatorHost` (zentrale Tool-Aktivierung/-Abbruch und Player-Placement-Grid-Sichtbarkeit; `PlacementController` und `RoadPlacementController` referenzieren sich dadurch nie direkt gegenseitig). Referenziert `Varynth.World`, `Varynth.Core.Definitions`, `Unity.InputSystem`, seit Phase 2C zusätzlich `Varynth.Core.Simulation` (Command-Konstruktion); liest Simulationszustand weiterhin nur read-only.
- `Varynth.UI` — UI Toolkit-basierte Oberfläche, eigene Assembly getrennt von `Presentation`, damit UI-Iteration nicht die 3D-Präsentationsschicht neu kompiliert.
- `Varynth.Audio` — Musik-/SFX-/Voice-System, adaptive Zustände, Streaming.
- `Varynth.Narrative` — Quest-/Story-Engine, liest Simulation über definierte Schnittstellen, besitzt keine eigene Parallelwirtschaft.
- `Varynth.Persistence` — Save/Load, Versionierung, Migration, Profile-Save getrennt von Spielstand-Saves (siehe §6).
- `Varynth.Modding` — Mod-Ladeorder, Namespace-Konfliktprüfung, Content-Registry-Integration.
- `Varynth.Tooling.Editor` (Editor-only) — **teilweise implementiert seit Phase 2A** (`WorldPrototypeSceneBuilder`, versionsneutrales `MilestoneBuild` unter `WorldPrototype/`); Blender-Pipeline-Integration folgt in einem späteren Paket.
- `Varynth.Tests.EditMode` / `Varynth.Tests.PlayMode` — Testassemblies je Zielebene; `Varynth.Tests.PlayMode` seit Phase 2A vorhanden.

Diese Aufteilung ist eine Arbeitsfassung; feinere Untergliederung (z. B. separate Assemblies je Systembereich innerhalb `Core.Simulation`) erfolgt bei Bedarf paketweise.

## 4. Simulationsebenen und Tickraten (Architekturprinzip, keine Werte)

Nicht jedes System und nicht jede Region wird pro Frame oder mit derselben Frequenz simuliert. Architektonisch vorzusehen sind mindestens folgende unterscheidbare Ebenen (Benennung vorläufig, konkrete Zahlen bewusst **nicht** Teil von Phase 0):

1. **Aktive Nahsimulation** — aktuell sichtbare/aktive Region(en) mit voller Detailtiefe (Produktion, Bedarf, Logistik pro Gebäude).
2. **Aktive Fernsimulation** — andere gleichzeitig "laufende" Regionen (z. B. während Expeditionen/Handelsrouten), reduzierte Aktualisierungsfrequenz, aggregierte statt Einzelgebäude-Buchhaltung wo möglich.
3. **Hintergrund-/Ruhesimulation** — nicht besuchte Regionen, rein statistisch fortgeschrieben (z. B. periodische Batch-Aktualisierung statt kontinuierlichem Tick).
4. **Präsentations-/Renderrate** — komplett von den obigen entkoppelt, läuft mit der tatsächlichen Frame-Rate und interpoliert/sampled aus dem letzten Simulationszustand.

Die konkrete Zuordnung von Tickraten, Batch-Intervallen und Aggregationsformeln ist Aufgabe eines späteren Implementierungspakets (nach erstem Vertical Slice/Profiling), nicht von Phase 0.

## 5. Systembereiche (Abdeckung laut Spezifikation)

Kurzverortung jedes vom Auftrag geforderten Bereichs innerhalb der Schichten/Assemblies oben; Details folgen in den jeweiligen Implementierungspaketen.

- **Grid/Bau** — `Varynth.World`: Rasterrepräsentation, Platzierungsvalidierung (liest `BuildingDefinition`/`RoadDefinition`/Footprint-Daten), keine Kollisions-/Pathfinding-Feinsteuerung in Phase 0.
- **Bewohner** — `Varynth.Core.Simulation`: aggregierte Bevölkerungsdatensätze pro Wohnhaus, Lebensqualitätsberechnung, Klassenübergänge (Upgrade sofort bei erfüllten Bedingungen, keine künstliche Bauzeit/Vertreibung).
- **Produktion** — `Varynth.Core.Simulation`: Rezept-/Produktionsketten-Ausführung, Auslastungsfaktoren, referenziert `RecipeDefinition`/`ProductionBuildingDefinition`.
- **Logistik** — `Varynth.Core.Simulation` + `Varynth.World`: Lagerhäuser (inkl. kleines Lagerhaus = gleiche Kapazität/weniger Rampen), Transportrouten, keine Pflicht-Stausimulation.
- **Handel** — `Varynth.Core.Simulation`: Händler-/Marktpreislogik, referenziert `TraderDefinition`.
- **Forschung** — `Varynth.Core.Simulation`: Knotenfortschritt, referenziert `ResearchDefinition`, RQ-Gate-Logik (Ultima) als Aggregation mehrerer Knoten.
- **Diplomatie/KI** — `Varynth.Core.Simulation`: Fraktionszustand, referenziert `FactionDefinition`/`PirateFactionDefinition`.
- **Utilities/Netzwerke** — `Varynth.Core.Simulation`: Ver-/Entsorgungsnetze als eigener Teilbereich, referenziert `UtilityDefinition`/`UtilityNetworkDefinition`, getrennt von reiner Straßenlogik.
- **Disasters/Resilience** — `Varynth.Core.Simulation`: Ereignis-/Krisensystem, referenziert `DisasterDefinition`, wirkt auf Produktions-/Bevölkerungszustand über dieselben Schnittstellen wie reguläre Simulation (keine Sonderpfade, die die Schichtentrennung umgehen).
- **Expeditionen** — `Varynth.Core.Simulation`: eigener Teilzustand für Erkundung/Freischaltung neuer Regionen im Aufbaumodus, referenziert `ExpeditionDefinition`.
- **Visitors/Tourismus** — `Varynth.Core.Simulation`: Attraktivitäts-/Besucherzustand als eigener, von der regulären Bewohnerlogik unterscheidbarer Teilbereich (unterschiedliche Bedarfs-/Belohnungsmechanik).
- **Quests** — `Varynth.Narrative`: generische Quest-/Objective-Engine zuerst (Phase 9 laut Implementation Guide), Story-Content danach; liest Simulationszustand über read-only Schnittstellen, setzt Storyflags über definierte Commands.
- **Game Modes & profilweite Unlocks** — `Varynth.Core.Simulation`/`Varynth.Persistence`: `GameModeDefinition`-getriebene Unterscheidung Story/Aufbau/Sandbox/Szenario; `buildModeUnlocked` und `ultima_discovered` sind laut Spezifikation getrennte Flags und werden entsprechend getrennt persistiert (siehe §6).
- **Achievements/Profile Progression** — `Varynth.Persistence` + `Varynth.Core.Simulation`: profilweiter Fortschritt (nicht savegame-lokal), referenziert `AchievementDefinition`.
- **Save/Load** — `Varynth.Persistence`: siehe §6.
- **Modding** — `Varynth.Modding`: Namespace-Registry, Ladeorder, Konfliktprüfung, Validierung gegen Content Catalog-Konventionen.
- **UI** — `Varynth.UI`: UI Toolkit, liest Simulationszustand/Definitionen über View-Models, keine Simulationslogik in UI-Code.
- **Audio** — `Varynth.Audio`: adaptive Musikzustände, VO, SFX-Skalierung nach Kameradistanz/Simulationsgröße.
- **Asset Streaming** — `Varynth.World`/Editor-Tooling: Addressables (oder äquivalent) je Region/Content-Gruppe, SourceAssets strikt außerhalb `Assets/`.
- **Regionen/Weltwechsel** — `Varynth.World`: siehe §4, Regionsaktivierung/-deaktivierung als expliziter State-Übergang.
- **Localization** — Definitionsebene referenziert ausschließlich Loc-Keys, nie Literal-Text (siehe `DATA_SCHEMA.md` §Loc-Strategie); Auflösung erfolgt erst in der Presentation-Ebene.
- **Content Registry** — `Varynth.Core.Definitions`: zentrale, namespace-bewusste Registry aller geladenen Definitionen inkl. Herkunfts-Mod, Grundlage für Validierung und Speichereffizienz (Referenzen statt Kopien).
- **Tests** — je Assembly gespiegelte Test-Assemblies (EditMode für Definitionsladen/Validierung/reine Simulationslogik, PlayMode für Szenen-/Integrationsverhalten).

## 6. Save/Load und Profile-Trennung

Zwei klar getrennte Persistenzarten:

- **Profile Save** (`Varynth.Persistence`): profilweiter, spielstand-übergreifender Fortschritt — u. a. Story-Abschlussstatus, `buildModeUnlocked`, Achievements, ggf. Meta-Progression. Unabhängig von einzelnen Spielständen, überlebt das Löschen/Neuanlegen einer Partie.
- **Savegame** (`Varynth.Persistence`): Zustand einer einzelnen Partie/Region-Welt. Speichert primär Definition-ID + veränderlichen Zustand, keine vollständigen statischen Definitionen. Versioniert, mit Migrationspfad für ID-Retirement/Schemaänderungen.

Beide Persistenzarten referenzieren Content ausschließlich über stabile IDs aus der Content Registry, nie über eingebettete Kopien der Definitionsdaten.

## 7. Bezug zur Implementierungsreihenfolge

Diese Architektur ist auf die Phasenfolge aus `CLAUDE_IMPLEMENTATION_GUIDE_v1.0.md` gemappt: Phase 0 (dieses Dokument, kein Code) → Phase 1 (technisches Fundament: Logging, stabile IDs, GameClock, Save-Format, XML-Loader/Validator, Mod-Ladeorder) → Phase 2 (Datenmodell vor Content) → Phase 3 (Grid/Bau/Save) → Phase 4 (Bevölkerung/Wohnen, Occidentia-Vertical-Slice) → Phase 5 (Produktion/Arbeitskraft) → Phase 6 (Logistik/Straßen/Häfen/Schiffe) → Phase 7 (Utility-Netzwerke) → Phase 8 (UI/UX) → Phase 9 (generische Quest-/Objective-Engine vor Story-Content) → Phase 10 (Story-Vertical-Slice) → Phase 11 (bekannte-Welt-Regionen) → Phase 12 (Ultima) → Phase 13 (Art-Pipeline) → Phase 14 (Audio/Voice) → Phase 15 (Performance, kontinuierlich) → Phase 16 (QA/Content-Lock), mit dem "Perfektionspass" (Lagerhaus-Rampen, neutrale Händler, KI-Konkurrent, Diplomatie, Piraten, Spezialisten, Prestige/Kultur/Besucher, Monumente/Welthandelszentrum) zwischen Phase 5 und den Regionalpaketen.

## 8. Future Multiplayer / Optional Co-op DLC

Seit Phase 1C verbindlich festgelegt:

- **Das Basisspiel bleibt vollständig offline spielbar.** Kein Always-online-Zwang, kein eigener Account-Zwang, keine Serverpflicht, keine Internetpflicht für Story, Aufbauspiel, Sandbox oder Szenarien.
- Ein **optionales** Multiplayer-/Koop-DLC ist für später vorgesehen, zunächst mit einer Zielrichtung von **ca. 2–4 Spielern**.
- Bevorzugte Architektur: **Host-/Client-Modell mit host-autoritativer Simulation** — ein Spieler hostet die Partie. Steam-Lobbys/Steam-Networking/P2P bzw. Relay sind spätere, bevorzugte Transportoptionen; **keine kostenpflichtigen Dedicated Server als Voraussetzung**.
- **Simulation/Input-Trennung**: die Simulationsebene hängt nicht direkt von Maus/Tastatur/UI/lokalen Unity-Input-Events ab (in `Varynth.Core.Simulation` seit Phase 1C durch `noEngineReferences: true` compilerseitig erzwungen, nicht nur Konvention). Spieleraktionen laufen konzeptionell über `Local Input → Command → Simulation`, später `Remote/Local Input → Command → Host-authoritative Simulation`. Phase 1C liefert dafür die minimale `ISimulationCommand`-Schnittstelle (`Varynth.Core.Simulation.Common`) als Sitz für zukünftige konkrete Commands — noch keine Netzwerkimplementierung, keine konkreten Gameplay-Commands.
- **Keine Annahme "es gibt für immer genau einen Spieler".** Spielerspezifische Identität läuft über `PlayerId` (`Varynth.Core.Simulation.Common`), einen plattformunabhängigen, auf `System.Guid` basierenden Identitätstyp — **keine SteamID/Epic-Account-ID als interne Spieler-Identität**, funktioniert vollständig offline. `PlayerId.NewId()` ist ausschließlich für Profil-/Spielerinitialisierung bestimmt und darf niemals innerhalb eines deterministischen Simulationsticks zur Erzeugung von Gameplayzustand aufgerufen werden (zufällig generierte GUIDs dürfen die Simulation nicht heimlich beeinflussen).
- **Späterer Besitz muss explizit modellierbar sein** (Gebäude, Schiffe, Inseln, Städte, Expeditionen tragen künftig eine `PlayerId`/`OwnerId`) — Phase 1C implementiert kein Ownership-Gameplay, aber `PlayerId` existiert bereits als wiederverwendbarer Baustein dafür.
- **Keine Networking-Implementierung jetzt**: keine Steamworks-Abhängigkeit, keine Lobby, keine Netzwerkpakete, kein P2P/Relay/RPC, kein Netcode for GameObjects/Mirror/FishNet/Photon, keine eigenen Server, keine Synchronisationslogik. Dies ist ausschließlich Future-Proofing der Architektur, kein Multiplayer-Feature.

## 9. Kamera-/Navigations-Skalierung (verbindlich seit Phase 2B, siehe `.claude/rules/05-camera-navigation-scaling.md`)

Grund: derselbe Geschwindigkeitsfehler (Pan/Zoom fühlen sich bei größeren Karten wieder
zu langsam an) wurde bei Varynth 0.1.1 zweimal manuell gemeldet, weil Pan-Geschwindigkeit
und Zoom-Schrittweite ursprünglich als feste absolute Konstanten (Units/Sekunde,
Units/Scroll-Notch) implementiert waren — das funktioniert nur für die eine Kartengröße,
gegen die die Konstante zuletzt getunt wurde.

- Pan-Geschwindigkeit und Zoom-Schrittweite sind **verbindlich als Funktion der aktuellen
  Kameradistanz/des aktuellen Zoomlevels** auszudrücken, nicht als feste Konstante — siehe
  `Varynth.Presentation.Camera.CameraRigMath.ComputePanSpeed`/`ComputeZoomTarget` (pure,
  testbare Funktionen) als Referenzimplementierung seit Phase 2B.
- Zoom ist multiplikativ/prozentual (`distance *= (1 - percent)^scrollDelta`), nicht additiv
  mit fixer Schrittweite, damit derselbe Scroll-Input weit draußen einen großen und nah am
  Terrain einen kleinen, präzisen Distanzsprung erzeugt — ohne Kenntnis der konkreten
  Kartengröße.
- Scroll-Rohinput wird nicht mit `Time.deltaTime` multipliziert (Input-Delta, keine Rate);
  visuelles Smoothing bleibt frame-rate-unabhängig.
- Min-/Max-Zoom- und Pan-Bounds-Clamping bleiben in jeder Implementierung erhalten.
- Regressionstests für diese Skalierung dürfen nicht an der konkreten aktuellen
  Kartengröße hängen, sondern müssen die Skalierungsgesetzmäßigkeit selbst prüfen (z. B.
  "gleicher Scroll-Input bei größerer aktueller Distanz erzeugt größere absolute
  Distanzänderung"), damit spätere, um Größenordnungen größere Karten nicht denselben
  Fehler erneut manuell aufdecken müssen.

## 10. Nicht Teil dieser Fassung

Keine konkreten Klassennamen/APIs, keine Tickraten-Werte, keine Engine-Detailimplementierung (z. B. konkrete Addressables-Gruppen-Konfiguration), keine Balancing-Werte. Diese folgen in den jeweiligen späteren Implementierungspaketen unter Bezug auf dieses Dokument.

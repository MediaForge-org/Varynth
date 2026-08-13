# Simulation Boundary Rule

Verbindlich seit Phase 2E (Varynth 0.2.3), für jedes Paket, das autoritativen
Gameplay-State liest oder mutiert — nicht nur Placement/Roads.

## Regel

**Authoritative simulation state must remain independent from Unity Presentation.**

- Der autoritative Gameplay-State (aktuell: `ArchipelagoPlacementState`,
  `RoadNetworkState`, künftig weitere Simulationssysteme) sitzt hinter `ISimulation`
  (`Varynth.Core.Simulation`, `noEngineReferences: true`). Unity/Presentation darf
  diesen State **nie** direkt konstruieren, halten oder mutieren.
- Bestätigte Mutation läuft ausschließlich über `ISimulation.Submit(ISimulationCommand)`
  → nächster Tick → `GetSnapshot()`. Kein Presentation-Code ruft
  `ArchipelagoPlacementState.TryPlace`/`RoadNetworkState.TryBuildPath` o. Ä. direkt auf.
- Presentation-seitiges read-only Feedback (Ghost-/Drag-Preview, Hover-Validierung) darf
  lokal über die schmalen `ISimulationPlacementQueries`/`ISimulationRoadQueries`-
  Interfaces laufen — diese Interfaces sind selbst engine-frei und mutieren nie.
- Referenzimplementierung: `ManagedSimulation` (`Assets/Game/Simulation/Boundary/`),
  angetrieben von `UnitySimulationDriver` (`Assets/Game/Presentation/`) — der einzige
  Unity-spezifische Baustein (`Time.unscaledDeltaTime` → `FixedTickAccumulator` →
  `AdvanceTicks`). `PlacementController`/`RoadPlacementController` finden den Driver
  über `FindFirstObjectByType` in `Start()` und hängen nur an den Interfaces, nie an
  der konkreten `ManagedSimulation`-Klasse.
- Kein Live-Zugriff auf interne Graph-/Map-Strukturen (z. B. `RoadGraph`) aus
  Presentation heraus — Presentation liest ausschließlich `SimulationSnapshot`
  (`BuildingRenderSnapshot[]`/`RoadRenderSnapshot[]`) und rekonstruiert bei Bedarf
  eine Wegwerf-Replika aus Snapshot-Daten, statt die autoritative Instanz zu berühren.
- Neue autoritative Simulationslogik lebt in `Varynth.Core.Simulation` (oder einer
  gleichwertigen `noEngineReferences: true`-Assembly) — nicht in `Varynth.World`/
  `Varynth.Presentation`, auch wenn das kurzfristig weniger Umbau bedeuten würde.

## Tests

Headless-Tests müssen `ManagedSimulation` (bzw. jedes neue Simulationssystem) ohne
Unity Scene/Terrain/GameObject/ScriptableObject konstruieren können — siehe
`Assets/Game/Tests/EditMode/Simulation/Boundary/ManagedSimulationTests.cs` als Vorlage.
Ein PlayMode-Test muss strukturell beweisen, dass Presentation nicht parallel direkt
mutiert (siehe `UnitySimulationDriverBridgeTests.cs`).

## Verboten

- Ein neues Presentation-Feature, das "der Einfachheit halber" direkt auf
  `ArchipelagoPlacementState`/`RoadNetworkState`/ein zukünftiges Simulationssystem
  zugreift, statt über `ISimulation`/die Query-Interfaces zu gehen.
- Neue autoritative Zustandsklassen, die von vornherein `UnityEngine`-Typen in ihrer
  öffentlichen API führen, ohne das im Plan explizit zu begründen (siehe
  `PHASE_2E_SIMULATION_BOUNDARY_FOUNDATION.md` für die Methode: reales
  Unity-Abhängigkeits-Audit vor jeder Annahme).

# PHASE_1C_RUNTIME_FOUNDATION.md — Varynth

Status: drittes Code-Paket, abgeschlossen im Scope von Phase 1C. Baut auf Phase 1A (`Varynth.Core.Definitions`) und Phase 1B (`Varynth.Data`) auf, ohne deren Typen zu duplizieren. Grundlage: `ARCHITECTURE.md`, `QA_GATES.md`.

## 1. Neue Assembly

`Varynth.Core.Simulation` (`Assets/Game/Simulation/`), referenziert ausschließlich `Varynth.Core.Definitions` (nur für `Varynth.Core.Diagnostics` — `IVarynthLogger`/`NullLogger`; nichts von `ContentId` wird in Phase 1C benötigt), `"noEngineReferences": true`. Diese Einstellung ist ein echter, compilerseitig erzwungener Beweis für "keine `Time.deltaTime`-/Input-/Wall-Clock-Kopplung im Simulationskern" — nicht nur eine Konvention.

```
Assets/Game/Simulation/
├── Varynth.Core.Simulation.asmdef
├── Clock/       GameTick, GameClock
├── Context/      SimulationLevel, SimulationLevelMask (+Extensions), SimulationContext
├── Scheduling/   SimulationSystemId, ISimulationSystem, DuplicateSimulationSystemException,
│                 SimulationSystemException, SimulationScheduler
└── Common/       PlayerId, ISimulationCommand
```

## 2. GameClock / GameTick

`GameTick` ist ein kleiner Value-Type (gleiches Idiom wie `ContentId`/`ContentSourceId`), der einen `ulong`-Zähler kapselt. Negative Ticks sind durch die Wahl von `ulong` konstruktionsbedingt unmöglich. `Add(ulong)` läuft in einem `checked`-Block — ein Überlauf wirft `OverflowException` statt still auf 0 zurückzuspringen (kein undefiniertes Wraparound).

`GameClock` besitzt `CurrentTick` (Start `GameTick.Zero`) und `IsPaused` (Start `false`).
- `Advance(ulong deltaTicks = 1)` ist der Normalpfad: **gibt `bool` zurück und wirft nicht**, wenn pausiert. Begründung: Pause ist ein normaler, kontrollierbarer Laufzeitzustand, kein Programmierfehler — passend zur eigenen Fehlerphilosophie des Auftrags (§22), die genau diese Unterscheidung verlangt.
- `Step(ulong deltaTicks = 1)` ist der explizit benannte, pause-ignorierende Bypass für gezieltes manuelles/Test-Stepping — weiterhin überlaufgeprüft.
- `Pause()`/`Resume()` sind idempotent.
- Keine `DateTime`/`Stopwatch`/FPS-Abhängigkeit irgendwo im Typ.

Ein zukünftiger World Save muss `GameClock.CurrentTick` speichern können — `GameTick` ist ein einzelner serialisierbarer `ulong`-Wert; kein Serializer wird in Phase 1C implementiert.

## 3. Simulation Context / Levels

`SimulationLevel` (`ActiveNear`, `ActiveFar`, `Background`) — Presentation ist ausdrücklich **kein** vierter Wert, sondern eine eigene, getrennte Ebene.

`SimulationLevelMask` (`[Flags]`, `None = 0`, `ActiveNear = 1`, `ActiveFar = 2`, `Background = 4`, `All = ActiveNear | ActiveFar | Background`) ist die Grundlage, über die ein `ISimulationSystem` erklärt, welche Levels es unterstützt — bewusst eine Bitmaske statt einer Collection, damit die Unterstützungsprüfung im Tick-Hotpath eine einzelne bitweise Und-Verknüpfung ist, ohne Enumerator-/Allokations-Overhead. `SimulationLevelExtensions.ToMask(SimulationLevel)` bildet den aktuellen Kontext-Level auf sein Einzelbit ab. `SimulationLevelMaskExtensions.IsValid()` lehnt sowohl `None` (unterstützt gar nichts — praktisch immer ein Konfigurationsfehler) als auch Bits außerhalb von `All` (undefinierte/kaputte Maske, z. B. durch einen ungültigen Cast) ab; `SimulationScheduler.Register` prüft dies und wirft `ArgumentException` bei Verstoß.

`SimulationContext` ist ein `readonly struct` mit genau zwei Feldern: `GameTick Tick`, `SimulationLevel Level`. Bewusst kein Region-/Weltfeld — das wäre eine spekulative, ungenutzte Erweiterung; ein zukünftiger regionsbewusster Kontext kann additiv ergänzt werden (gleiches Muster wie Phase 1B, das Phase 1A additiv erweitert hat, statt zu ersetzen), ohne diesen Typ zu brechen.

## 4. Simulation System / Scheduler

`SimulationSystemId` ist ein eigener Value-Type mit eigener, kleiner Validierung — bewusst **nicht** `ContentId` und **nicht** über den `internal`en `DottedIdentifier`-Helfer aus `Varynth.Core.Definitions` implementiert (andere Assembly, andere Domäne: eine System-ID ist kein moddbarer Content-Identifier, auch wenn die Zeichenregeln ähnlich aussehen). Regel: punktgetrennte Segmente (mindestens **eines** — anders als `ContentId`s Minimum von zwei, da z. B. `"population"` allein gültig sein muss), jedes Segment `[a-z0-9]+(?:[_-][a-z0-9]+)*`, ausschließlich Kleinschreibung, kein Leerzeichen, kein führender/abschließender/doppelter Punkt. Gültig: `population`, `production`, `simulation.trade`. Ungültig: `Production`, `trade..ships`, `production@core`. Keine automatische Normalisierung — `Parse` wirft `ArgumentException`, `TryParse` liefert `false`.

`ISimulationSystem` (`Id`, `Order`, `SupportedLevels : SimulationLevelMask`, `Tick(SimulationContext)`) macht keinerlei Annahme, dass eine Implementierung ein `MonoBehaviour` ist. Konkrete Systeme (Produktion, Bevölkerung, Logistik, Handel, ...) werden in Phase 1C **nicht** implementiert — nur die Infrastruktur.

`SimulationScheduler`:
- `Register` lehnt `null` (`ArgumentNullException`), doppelte `SimulationSystemId` (`DuplicateSimulationSystemException`, keine stille Überschreibung) und eine ungültige `SupportedLevels`-Maske (`ArgumentException`, siehe oben) ab. Die interne Liste wird bei jeder Registrierung nach `(Order, Id ordinal)` neu sortiert — derselbe deterministische Tie-Break wie bereits in Phase 1Bs `LoadOrderResolver`, aus Konsistenzgründen wiederverwendet statt einer dritten Sortierkonvention.
- `RunTick(SimulationContext)` iteriert die sortierte Liste per `foreach` (keine LINQ, keine Allokation im Hotpath jenseits dessen, was Systeme selbst tun), überspringt Systeme, deren Maske den angeforderten Level nicht enthält (reine bitweise Prüfung, kein `.Contains()`).
- **Fehlerverhalten (korrigiert gegenüber einem ersten Entwurf mit "loggen und weiterlaufen"):** Ein fehlgeschlagenes System kann den Runtime-State bereits teilweise verändert haben; mit den restlichen Systemen weiterzulaufen würde einen inkonsistenten bzw. für späteren Multiplayer divergierenden Zustand riskieren. Ein Fehler wird daher zuerst über den injizierten `IVarynthLogger` geloggt (`Error`, mit SystemId und Tick — niemals still verschluckt, unabhängig davon, ob ein echter Logger oder der Default-`NullLogger` konfiguriert ist), **dann** in eine kleine `SimulationSystemException` (SystemId/Tick/InnerException) verpackt und erneut geworfen — der aktuelle `RunTick`-Aufruf bricht sofort ab, später folgende Systeme laufen in diesem Tick nicht mehr.
- Kein Job System/ECS/Threads/async — einfacher, einzelsträngiger `foreach`, wie vom Auftrag verlangt. Parallelisierung ist eine spätere, profilingbasierte Entscheidung.

## 5. Multiplayer-/Koop-DLC-Future-Proofing

Siehe `ARCHITECTURE.md` §8 ("Future Multiplayer / Optional Co-op DLC") für die vollständige, verbindliche Festlegung. Zusammengefasst, was Phase 1C konkret liefert bzw. bewusst nicht liefert:

**Vorbereitet:**
- `noEngineReferences: true` macht "Simulation hängt nicht von Maus/Tastatur/UI/lokalen Unity-Input-Events ab" strukturell unmöglich zu verletzen, nicht nur konventionell.
- `ISimulationCommand` (`Varynth.Core.Simulation.Common`) — minimale Schnittstelle (`PlayerId IssuedBy`, `GameTick IssuedAtTick`) als Sitz für spätere reproduzierbare Commands (`Input → Command → Simulation`, später `Remote/Local Input → Command → Host-authoritative Simulation`).
- `PlayerId` (`Varynth.Core.Simulation.Common`) — plattformunabhängiger, `System.Guid`-basierter Identitätstyp, **keine** SteamID/Epic-Account-ID, funktioniert vollständig offline. `None`-Sentinel für Singleplayer-implizite/systemseitig ausgelöste Aktionen. Keine Annahme "es gibt für immer genau einen Spieler" — `PlayerId` ist ein echter Mehrwert-Identitätstyp, kein globales Singleton.
- Dokumentierte Nutzungsgrenze: `PlayerId.NewId()` ist ausschließlich für Profil-/Spielerinitialisierung bestimmt und darf **niemals** innerhalb eines deterministischen Simulationsticks zur Erzeugung von Gameplayzustand aufgerufen werden — eine frisch-zufällige GUID wäre ein verstecktes, unkontrolliertes, pro Maschine divergierendes Eingangssignal in die Simulation und würde sowohl die angestrebte Determinismus-Grundlage als auch eine spätere Host/Client-Übereinstimmung im Koop untergraben. Im XML-Doc-Kommentar von `PlayerId.NewId()` sowie hier dokumentiert; nicht code-technisch erzwingbar (kein Sandboxing-Mechanismus verhindert den Aufruf aus einer `Tick`-Methode heraus).

**Bewusst NICHT implementiert:**
- Keine konkreten Gameplay-Commands (`BuildBuildingCommand` usw.).
- `ISimulationCommand` ist **nicht** in `ISimulationSystem`/`SimulationScheduler` verdrahtet — `Tick` nimmt ausschließlich `SimulationContext` entgegen.
- Kein Ownership-Gameplay (keine `OwnerId`-Felder auf irgendeinem Zustand) — nur die Voraussetzung dafür (`PlayerId`) existiert.
- Keine `SimulationRunner`/Treiber-Klasse, die `GameClock` und `SimulationScheduler` verbindet: bewusst nicht gebaut, weil deren tatsächliche Form von der noch offenen Pro-Level-Frequenz-/Akkumulationslogik abhängt (§13/§14 des Auftrags), die ausdrücklich noch nicht festgelegt werden soll. Der relevante Test komponiert `GameClock` und `SimulationScheduler` stattdessen direkt (siehe Tests unten), ohne dass Produktionscode eine bestimmte Kompositionsform vorwegnimmt.
- Keine Steamworks-Abhängigkeit, keine Lobby, keine Netzwerkpakete, kein P2P/Relay/RPC, kein Netcode for GameObjects/Mirror/FishNet/Photon, keine eigenen Server, keine Synchronisationslogik.

## 6. Determinismus

Kein Versprechen auf vollständige bitgenaue Lockstep-Deterministik über jede Plattform — aber unnötige Nichtdeterminismen werden vermieden: stabile Systemreihenfolge (`(Order, Id)`, nie Dictionary-/HashSet-/Registrierungsreihenfolge), integerbasierter Tick, kein FPS-gekoppelter Simulationskern, kein systemzeitabhängiges Verhalten. Kein RNG-System in Phase 1C (nicht zwingend nötig — nichts in diesem Paket braucht Zufall); zukünftige Zufallsmechaniken müssen über kontrollierte Seeds laufen, nicht über z. B. `PlayerId.NewId()` oder `System.Random`.

## 7. Performance

Keine Allokation pro Tick jenseits dessen, was einzelne Systeme selbst tun; keine LINQ-Kette im Scheduler-Hotpath (`RunTick`/Level-Prüfung sind reine `foreach`/Bitmasken-Operationen); keine Reflection, keine `FindObjectOfType`-Aufrufe, keine Scene-Scans, keine globale Service-Locator-God-Class.

## 8. Tests

EditMode-Tests (Erweiterung von `Varynth.Tests.EditMode`, jetzt zusätzlich mit Referenz auf `Varynth.Core.Simulation`; die bestehende `defineConstraints: []`-Korrektur aus Phase 1B bleibt unverändert):
- `GameTickTests`, `GameClockTests` (inkl. Pause/Resume/Step, Overflow-Verhalten)
- `SimulationSystemIdTests` (gültige/ungültige IDs, Gleichheit)
- `SimulationContextTests` (Level-Werte, `SimulationLevelMask`-Konvertierung/Validierung inkl. `None` und undefinierter Bits)
- `SimulationSchedulerTests` (Registrierung, Duplicate-Ablehnung, Maskenvalidierung, deterministische Reihenfolge, Level-Filterung, Fehlerabbruch inkl. "nachfolgendes System läuft nicht mehr" und "Fehler wird auch mit Default-`NullLogger` nicht verschluckt", Determinismus bei unterschiedlicher Registrierreihenfolge und über mehrere Ticks) — nutzt eine testeigene `TestSimulationSystem`-Fixture
- `SimulationCommandTests` (`ISimulationCommand`-Metadaten, `PlayerId`-Gleichheit/Eindeutigkeit/`None`)

Tatsächliche Testausführung/-ergebnis: siehe Abschlussbericht dieses Pakets, nicht hier dupliziert. Alle 119 bestehenden Tests aus Phase 1A/1B müssen weiterhin bestehen.

## 9. Bewusst verschobene Punkte

Kein Gameplay, keine Gebäude/Straßen/Bewohner/Bedürfnisse/Produktion/Warenwirtschaft/Lagerhäuser/Schiffe/Händler/Piraten/KI-Gegner/Diplomatie/Forschung/Expeditionen/Quests/Achievements/Story/Karte/Terrain/Inseln/Kamera/UI, keine Blender-Assets, keine Audioimplementation, kein produktives Savegame-System, keine Steamworks-Integration, kein Multiplayer/Networking/Lobby/P2P/Dedicated Server, keine echten Produktionsdaten.

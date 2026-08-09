# PHASE_1A_CORE_FOUNDATION.md — Varynth

Status: erstes echtes Code-Paket, abgeschlossen im Scope von Phase 1A. Grundlage: `ARCHITECTURE.md`, `DATA_SCHEMA.md`, `QA_GATES.md`.

## 1. Implementierte Struktur

Ein Assembly (`Varynth.Core.Definitions`), das intern nach Verantwortlichkeit gegliedert ist, plus ein zugehöriges EditMode-Testassembly:

```
Assets/Game/Core/
├── Varynth.Core.Definitions.asmdef   ("noEngineReferences": true)
├── Common/
│   ├── ContentId.cs
│   ├── DottedIdentifier.cs           (internal, geteilte Validierung)
│   └── ContentIdFormatException.cs
├── Definitions/
│   ├── LocalizationKey.cs
│   └── IContentDefinition.cs
├── Registry/
│   ├── ContentRegistry.cs
│   └── DuplicateContentIdException.cs
└── Diagnostics/
    ├── IVarynthLogger.cs
    ├── NullLogger.cs
    └── CollectingLogger.cs

Assets/Game/Tests/EditMode/
├── Varynth.Tests.EditMode.asmdef
├── ContentIdTests.cs
├── LocalizationKeyTests.cs
├── ContentRegistryTests.cs
└── DiagnosticsTests.cs
```

Nur ein Produktions-Assembly für dieses Paket — die vier Unterordner (`Common`, `Definitions`, `Registry`, `Diagnostics`) sind Quellcode-Organisation innerhalb desselben Assemblys, keine vier separaten leeren Assemblies. Name/Rolle folgen `ARCHITECTURE.md` §3 (`Varynth.Core.Definitions`, `Varynth.Tests.EditMode`).

## 2. Implementierte Typen und Verantwortlichkeiten

- **`ContentId`** (`Varynth.Core.Common`) — unveränderlicher Value-Type für stabile technische IDs (`res.occidentia.t1.f1`, `good.meridia.coffee`, `mygreatmod.good.blueberry`). `Parse`/`TryParse`, Value Equality, `GetHashCode`, `ToString`. Validiert ausschließlich Struktur, keine feste Prefix-Whitelist.
- **`DottedIdentifier`** (internal, `Varynth.Core.Common`) — geteilte Strukturvalidierung, von `ContentId` und `LocalizationKey` verwendet, um dieselbe Regel nicht zweimal zu implementieren.
- **`ContentIdFormatException`** (`Varynth.Core.Common`) — geworfen von `Parse` beider Value-Types bei ungültigem Rohstring; trägt Rohwert und Grund.
- **`LocalizationKey`** (`Varynth.Core.Definitions`) — analoger Value-Type für Lokalisierungsschlüssel (`ui.population.title`, `quest.story.occidentia.001.title`), gleiche Strukturregel wie `ContentId`.
- **`IContentDefinition`** (`Varynth.Core.Definitions`) — minimale gemeinsame Schnittstelle (`Id`, `NameKey`). Keine der 20 Fachdefinitionstypen aus `DATA_SCHEMA.md` ist hier bereits implementiert.
- **`ContentRegistry<T>`** (`Varynth.Core.Registry`) — generische ID→Definition-Registry, Dictionary-basiert (Hash-Lookup, keine lineare Suche). `Register`, `TryGet`, `Get`, `All` (read-only), `Count`. Lehnt Duplikate und `null` ab, überschreibt nie still.
- **`DuplicateContentIdException`** (`Varynth.Core.Registry`) — geworfen bei doppelter Registrierung, trägt die betroffene `ContentId`.
- **`IVarynthLogger`** (`Varynth.Core.Diagnostics`) — Logging-Abstraktion (`Info`/`Warning`/`Error`), damit Core-Code nicht direkt `UnityEngine.Debug.Log` aufruft.
- **`NullLogger`** — No-Op-Implementierung als sicherer Default.
- **`CollectingLogger`** — In-Memory-Implementierung, sammelt `LogEntry`-Werte (Severity/Message/Context); dient Tests und als Vorlage für spätere Adapter.

## 3. Architekturentscheidungen

- **`"noEngineReferences": true`** auf `Varynth.Core.Definitions.asmdef` — erzwingt compilerseitig, dass diese Assembly `UnityEngine`/`UnityEditor` nicht referenzieren kann. Direkte Umsetzung der Definition/Simulation/Presentation-Trennungsregel aus `ARCHITECTURE.md` §0. Konsequenz: Ein Unity-Console-Adapter für `IVarynthLogger` existiert hier bewusst noch nicht (siehe §5, verschobene Punkte).
- **Keine separate Mod-Namespace-Abstraktion.** `ContentId` validiert Struktur, nicht fachliche Kategorie — ein Mod-Präfix (`mygreatmod.good.blueberry`) ist strukturell nicht von einer Core-ID (`bld.global.market`) unterscheidbar. Eine zusätzliche `ModNamespace`-Klasse hätte keinen Mehrwert für Phase 1A und würde nur Komplexität ohne Funktion hinzufügen; das Mod-Ladesystem selbst folgt in einem späteren Paket.
- **Kein `ValidationIssue`/`ValidationSeverity`-Framework.** Zwei gezielte Exception-Typen (`ContentIdFormatException`, `DuplicateContentIdException`) reichen für zwei Value-Types und eine Registry aus und bleiben nachvollziehbar/testbar, ohne ein generisches Fehler-Framework vorzuziehen.
- **Geteilte Validierungslogik** (`DottedIdentifier`) statt Duplikation zwischen `ContentId` und `LocalizationKey` — beide folgen identischer Strukturregel (punktgetrennte Kleinschreibungs-Segmente).

## 4. Teststrategie

EditMode-Tests (`Varynth.Tests.EditMode`, referenziert `Varynth.Core.Definitions` + Unity Test Framework):
- `ContentIdTests` — valide Core-/Mod-IDs, alle in Abschnitt 13 des Auftrags genannten Ungültigkeitsfälle (leer, Whitespace, Großbuchstaben, doppelter Punkt, führender/abschließender Punkt, ungültige Sonderzeichen, Einzelsegment), Equality, Hashing, `TryParse`, Dictionary-Key-Tauglichkeit.
- `LocalizationKeyTests` — analoge Struktur-/Equality-Tests.
- `ContentRegistryTests` — Registrierung/Lookup, `TryGet`/`Get` (Treffer und Fehlschlag), Duplicate-Ablehnung ohne stille Überschreibung, mehrere Einträge, read-only `All`, `null`-Ablehnung.
- `DiagnosticsTests` — `CollectingLogger` empfängt und speichert Info/Warning/Error nachvollziehbar; `NullLogger` wirft nie; Logger-Abstraktion ist austauschbar (Beweis, dass Core nicht hart an eine Implementierung gekoppelt ist).

Tatsächliche Ausführung und Ergebnis: siehe Abschlussbericht dieses Pakets (git-Historie/Session), nicht in diesem Dokument dupliziert.

## 5. Bewusst verschobene Punkte (nicht Teil von Phase 1A)

- XML-Loader/-Validator für Content-Definitionen.
- Savegame-System/-Serialisierung (siehe §6 unten für die reine Formatnotiz).
- GameClock/Simulation-Scheduler, konkrete Tickraten.
- Mod-Ladeorder/Mod-Manager (nur die ID-Struktur verhindert Mods nicht — mehr nicht).
- Unity-Console-/Datei-/Mod-Diagnostik-Adapter für `IVarynthLogger`.
- Die 20 konkreten Fachdefinitionstypen aus `DATA_SCHEMA.md`.
- Jegliche Gameplaylogik, Blender-Assets, fachliche Spieldaten.

## 6. Savegame-Referenznotiz (Dokumentation, kein Code)

Künftige Savegames referenzieren Content ausschließlich über die stabile String-Form von `ContentId` (z. B. `"res.occidentia.t1.f1"`) plus veränderlichen Zustand — nie über eine Kopie der vollständigen Definition. `ContentId.ToString()`/`ContentId.Parse` bilden das symmetrische Serialisierungspaar dafür; ein tatsächliches Save-Format folgt in einem späteren Paket.

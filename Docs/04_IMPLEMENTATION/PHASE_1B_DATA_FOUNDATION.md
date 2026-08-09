# PHASE_1B_DATA_FOUNDATION.md — Varynth

Status: zweites Code-Paket, abgeschlossen im Scope von Phase 1B. Baut auf Phase 1A (`Varynth.Core.Definitions`, siehe `PHASE_1A_CORE_FOUNDATION.md`) auf, ohne dessen Typen zu duplizieren. Grundlage: `ARCHITECTURE.md`, `DATA_SCHEMA.md`, `QA_GATES.md`.

## 1. Neue Assembly

`Varynth.Data` (`Assets/Game/Data/`), referenziert `Varynth.Core.Definitions`, `"noEngineReferences": true` (System.Xml.Linq/System.IO/System.Xml sind Teil der Standardbibliothek, keine externe XML-Bibliothek nötig).

```
Assets/Game/Data/
├── Varynth.Data.asmdef
├── AssemblyInfo.cs                 ([InternalsVisibleTo("Varynth.Tests.EditMode")])
├── Sources/    ContentSourceType, ContentSource
├── Mods/       ModDependency, ModManifest, ModManifestXmlReader
├── Xml/        ContentDocument, XmlDocumentReader
├── Loading/    ContentFileDiscovery, IContentDefinitionLoader<T>, DefinitionLoadPipeline<T>,
│               LoadOrderResolver, LoadOrderResult (LoadOrderExclusionReason, LoadOrderExclusion)
└── Validation/ ContentLoadIssue, ContentLoadReport
```

Drei additive, nicht-brechende Erweiterungen der bestehenden Phase-1A-Assembly `Varynth.Core.Definitions`:
- `DottedIdentifier` (`Core/Common/`): neuer `IsValid(string, int minSegments, out string reason)`-Overload; bleibt `internal`. Der bestehende zweistellige Overload ruft ihn mit `minSegments: 2` auf — Phase-1A-Verhalten unverändert.
- `ContentSourceId` (`Core/Common/`, neu): gleiche Form wie `ContentId`, aber `minSegments: 1` (Source-IDs wie `"core"` sind einsegmentig legitim). Nutzt die bestehende `ContentIdFormatException` mit.
- `ContentReference<T>` (`Core/Definitions/`, neu): minimaler ID-Referenz-Wrapper (`ContentId Id`, `TryResolve(ContentRegistry<T>, out T)`) für künftige Cross-References (Recipe→Good usw.), ohne Objektkopien.

## 2. Content-Source-Abstraktion

`ContentSourceType`: `Core`, `OfficialAddon`, `Mod`, `Test`. `ContentSource` ist ein reiner Datenhalter (`Id`, `Type`, `RootPath`, `Priority`, `RequiredDependencies`, `OptionalDependencies`, `LoadAfter`) — keine XML-/Dateisystem-Logik. `ContentSource.FromModManifest(ModManifest, rootPath, priority)` baut eine Mod-Source aus einem geparsten Manifest; Core-/Test-Sources werden direkt im Code konstruiert.

## 3. Mod-Manifest-Schema (minimal)

```xml
<mod id="author.modname" version="1.0.0" nameKey="mod.author.modname.name">
    <dependencies>
        <dependency id="someother.mod" />
        <dependency id="anothermod" optional="true" />
    </dependencies>
    <loadAfter>
        <source id="core" />
    </loadAfter>
</mod>
```
Pflicht: `id`, `version` (reiner nicht-leerer String, kein Semver-Parsing in 1B), `nameKey`. `dependencies`/`loadAfter` optional. `dependency@optional` Default `false` (harte Abhängigkeit). Ein ungültiger `optional`-Wert wird kontrolliert behandelt (Warning, Default `false`), nicht als Fehler.

**Einheitlicher gehärteter Einstiegspunkt:** `ModManifestXmlReader.TryRead(XDocument...)` ist `internal` (Testzugriff über `InternalsVisibleTo`) und parst ausschließlich ein bereits geladenes `XDocument` — kein eigener Dateizugriff. Der einzige öffentliche Einstiegspunkt ist `TryReadFromFile(string manifestPath, ...)`, der zuerst den gehärteten `XmlDocumentReader.TryLoad(...)` aufruft und erst bei Erfolg an `TryRead` weiterreicht. Es gibt keinen zweiten, ungehärteten `XDocument.Load`-Pfad in `Data/Mods/`.

## 4. Gehärtetes XML-Parsing

Mod-XML ist untrusted declarative input. `XmlDocumentReader.TryLoad` baut explizit einen `XmlReader` mit `XmlReaderSettings { DtdProcessing = Prohibit, XmlResolver = null, MaxCharactersInDocument = <konfigurierbar, Default 10.000.000> }` statt sich auf `XDocument.Load`-Defaults zu verlassen. `DtdProcessing.Prohibit` lehnt jedes `<!DOCTYPE ...>` bereits ab — das ist zugleich die primäre XXE-Abwehr, da ohne DTD keine Entities deklariert werden können; `XmlResolver = null` ist zusätzliche Tiefenverteidigung. `LoadOptions.SetLineInfo` bleibt für Zeilen-/Positionskontext in Fehlermeldungen erhalten. Die Root-Element-Prüfung (`expectedRootName`) deckt sowohl "kaputtes XML" als auch "falsches/fehlendes Root-Element" in einem Schritt ab. Dies ist der einzige Weg, wie in `Varynth.Data` eine Datei zu einem `XDocument` wird — sowohl reguläre Content-Dateien als auch Mod-Manifeste laufen darüber.

Sicherheitstests (siehe `XmlDocumentReaderTests`, `ModManifestXmlReaderTests`): DOCTYPE wird abgelehnt, ein XXE-Payload mit externem Entity-Verweis auf eine echte lokale Datei wird abgelehnt und lässt deren Inhalt nachweislich nicht in eine Fehlermeldung durchsickern, normales DOCTYPE-freies XML funktioniert unverändert.

## 5. Deterministische Ladeorder (`LoadOrderResolver`)

Kahn's-Algorithmus in sechs Phasen. Kernkorrektur gegenüber einem naiven einstufigen Ansatz: **harte Abhängigkeiten allein bestimmen Ladbarkeit**; `loadAfter` und *vorhandene* optionale Abhängigkeiten sind rein weiche Ordnungsbeziehungen, die niemals eine ansonsten gültige Source ausschließen.

- **Phase A — Duplicate IDs:** Sources werden nach `ContentSourceId` gruppiert; jede Gruppe mit mehr als einem Mitglied schließt **alle** Mitglieder aus (`DuplicateSourceId`) — eingabereihenfolge-unabhängig, da rein gruppenbasiert.
- **Phase B — direkte fehlende harte Abhängigkeit:** existiert die harte Abhängigkeit gar nicht als Source → Ausschluss `MissingDependency`.
- **Phase C — transitive Propagation:** Fixpunktschleife ausschließlich über harte Abhängigkeiten; hängt eine Source (direkt oder über beliebig lange Ketten) von einer bereits ausgeschlossenen Source ab → `DependencyExcluded`.
- **Phase D — harter Abhängigkeitsgraph + Zyklusklassifikation:** Kahn's-Lauf nur über harte Kanten; alles, was übrig bleibt, wird per Zyklenerkennung (DFS-Erreichbarkeit im Restgraphen) in echte `CycleMember` und nur transitiv blockierte `BlockedByCycle` unterschieden — eine Source, die lediglich von einem zyklischen Knoten abhängt, ist selbst kein Zyklusmitglied.
- **Phase E — weiche Kanten (optionale Abhängigkeiten + `loadAfter`):** werden erst auf den bereits zyklenfreien harten Graphen angewandt, in deterministischer Reihenfolge (`(Dependent, Target, Kantenart)`); ein Kandidat, der einen Zyklus schließen würde (inkl. Widerspruch zu einer harten Kante), wird verworfen und als `Warning` gemeldet — **nie** als Ausschluss.
- **Phase F — finale Ordnung:** Kahn's-Lauf über harte + akzeptierte weiche Kanten, deterministischer Tie-Breaker `(Priority, Id ordinal)` unter allen bereiten Knoten je Schritt.

`LoadOrderResult` liefert `OrderedSourceIds` sowie `Exclusions` (volles `ContentSource`-Objekt inkl. `.Type`, `Reason`, `Detail`) — jede Exclusion wird zusätzlich als `Error` in den `ContentLoadReport` geschrieben. Ob ein ausgeschlossener Core-Source zu einem fatalen Abbruch führt oder ein ausgeschlossener Mod nur übersprungen wird, entscheidet bewusst ein späteres Bootstrap-Paket anhand von `.Type` — der Resolver selbst bleibt dafür einheitlich/einfach.

## 6. Definition-Loader-Pipeline

`IContentDefinitionLoader<T>` (keine Reflection, kein dynamisches Type-Lookup) parst genau einen XML-Elementnamen in genau einen Definitionstyp. `DefinitionLoadPipeline<T>` läuft über eine geordnete Liste von `ContentDocument` (Source + Datei + bereits geparstes `XDocument`), verarbeitet `<content>`-Kindelemente, die zum Loader passen, und registriert Erfolge in einer `ContentRegistry<T>`. Duplicate-IDs werden abgefangen (`DuplicateContentIdException` → `Error`, erste Registrierung bleibt), strukturell ungültige Elemente erreichen die Registry nie.

**Mod-Namespace-Ownership:** Für `ContentSourceType.Mod` muss jede neu registrierte ID dem eigenen Namensraum der Source gehören (`source.Id` oder `source.Id + "."`-Präfix). Ein Mod, der `good.meridia.something` (Core-Namespace) oder eine fremde Mod-ID definiert, wird abgelehnt (`Error`, landet nicht in der Registry). `Core`/`OfficialAddon`/`Test`-Sources sind davon in Phase 1B nicht betroffen. Gezielte Overrides/Patches bleiben ausdrücklich einem späteren Paket vorbehalten.

**Bekannte, dokumentierte Vereinfachung:** Da Phase 1B genau einen konkreten (Test-)Definitionstyp hat, wird "unbekannter Definitionstyp" als "jedes `<content>`-Kind, dessen Tag nicht `RootElementName` dieses Loaders entspricht" gemeldet — für einen Einzel-Loader-Lauf korrekt, aber ein künftiges Paket mit mehreren echten Loadern braucht eine kleine, weiterhin reflection-freie Registrierungstabelle, um "nicht meins" von "niemandem bekannt" zu unterscheiden.

## 7. Reporting (`ContentLoadReport`)

Nutzt Phase 1As `LogSeverity` (`Info`/`Warning`/`Error`) direkt weiter statt ein zweites Severity-System einzuführen. `ContentLoadIssue` ergänzt strukturierten Kontext (`Source`, `FilePath`, `ContentId`, jeweils optional), den `IVarynthLogger`s einzelner Freitext-Context nicht sauber tragen kann. `ContentLoadReport` sammelt Issues **und** leitet jedes an einen injizierten `IVarynthLogger` weiter (Default `NullLogger`) — eine Schicht über Phase-1A-Diagnostics, kein Konkurrenzsystem.

## 8. Sicherheits-/Safety-Checkliste (Auftrag §20)

Kein `Type.GetType`/`Activator.CreateInstance`, keine Shell-Ausführung, keine Schreibzugriffe (nur `File.OpenRead`/`Directory.EnumerateFiles`), gehärtetes XML ohne DTD/externe Entities, konfigurierbares Größenlimit. Content-Roots sind in Phase 1B kein Netzwerk-Input; darüber hinausgehendes Sandboxing ist nicht Teil dieses Pakets.

## 9. Tests

EditMode-Tests (Erweiterung von `Varynth.Tests.EditMode`, jetzt mit Referenz auf `Varynth.Data`):
- `ContentSourceIdTests`, `ContentReferenceTests` (Core-Erweiterungen)
- `Data/ContentSourceTests`, `Data/ModManifestXmlReaderTests`, `Data/LoadOrderResolverTests`, `Data/XmlDocumentReaderTests`, `Data/ContentLoadReportTests`, `Data/DefinitionLoadPipelineTests` (+ testeigene `TestDefinitionXmlLoader`, `OtherTestDefinition`)

`LoadOrderResolverTests` deckt gezielt die korrigierten Diagnosefälle ab: fehlende harte Abhängigkeit, transitive Exklusion über Ketten, echte Zyklusmitglieder vs. nur blockierte Sources, Duplicate-IDs unabhängig von der Eingabereihenfolge, sowie dass weiche Kanten (`loadAfter`/optionale Abhängigkeit) niemals ausschließen — inkl. reiner Soft-Zyklen (2- und 3-Knoten) und eines gemischten Hard/Soft-Konflikts.

Tatsächliche Testausführung/-ergebnis: siehe Abschlussbericht dieses Pakets, nicht hier dupliziert.

## 10. Bewusst verschobene Punkte

Kein Gameplay, kein GameClock/Simulation-Scheduler, kein Savegame-System, keine Bewohner/Häuser/Produktion/Warenwirtschaft/Straßen/Schiffe/KI/Händler/Piraten/Quests/Achievements/Story/Karten/UI, keine Blender-Assets, keine Migration der echten 350 Wohnformen/174 Waren/54 Gebäude/etc. in XML — nur das Ladefundament dafür.

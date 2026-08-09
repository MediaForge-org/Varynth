# QA_GATES.md — Varynth Allgemeine Qualitäts-Gates (Arbeitsfassung v0.1, Phase 0)

Konsolidierte Gate-Liste. Jedes zukünftige Arbeitspaket muss die für seinen Scope zutreffende Teilmenge erfüllen, bevor es als abgeschlossen gilt (siehe `.claude/rules/02-tests-and-gates.md`). Diese Datei wird um paketspezifische Ergänzungen erweitert, nicht ersetzt.

## Reihenfolge pro Paket (Minimum, aus `.claude/rules/02-tests-and-gates.md`)

1. Compile-/Importfehler = 0.
2. EditMode/Unit-Tests ausführen.
3. PlayMode/Integrationstests ausführen, wenn Systemverhalten betroffen ist.
4. Daten-/XML-Validator ausführen, wenn Contentdaten betroffen sind.
5. Save/Load testen, wenn persistenter Zustand betroffen ist.
6. Build-/Größen-/Performance-Gates prüfen, wenn Assets/Simulation betroffen sind.
7. Fehler vor Abschluss beheben.

## Gate-Katalog

### Compile
- 0 Compiler-/Importfehler in allen Assemblies (siehe `ARCHITECTURE.md` §3).
- Keine neuen Warnings-als-Fehler-Verstöße ohne Begründung.

### Unit/EditMode Tests
- Reine Definitions-/Simulationslogik (Assembly `Varynth.Tests.EditMode`) hat Tests für neue/geänderte Logik.
- Kein Test wird deaktiviert, um ein Paket "grün" zu bekommen, ohne dokumentierte Begründung.

### PlayMode/Integrationstests
- Pflicht, wenn Systemverhalten (nicht nur reine Datenstruktur) betroffen ist — z. B. Szenen-/Prefab-Interaktion, Grid/Bau, Save/Load-Zyklus in einer laufenden Szene.

### Datenvalidierung
- Pflicht bei jeder Änderung an Content-/XML-Daten.
- Prüft mindestens: eindeutige IDs je Namensraum, aufgelöste Referenzen, keine verwaisten Abhängigkeiten, Namespace-Konflikte zwischen Mods und `core.*` (siehe `DATA_SCHEMA.md` §7).
- Werkzeuge: `Tools/Validation/` (siehe dortige Skripte).

### Save/Load
- Pflicht bei jeder Änderung an persistentem Zustand.
- Prüft: Laden eines Spielstands vor der Änderung bleibt kompatibel oder hat eine dokumentierte Migration; Profile Save und Savegame bleiben getrennt (siehe `ARCHITECTURE.md` §6); keine vollständigen Definitionskopien im Save.

### Modding
- Neue Content-Typen/Felder sind über den XML-Pfad moddbar, nicht nur hart in C#.
- Mod-Namespace-Konflikte werden erkannt und gemeldet, nicht stillschweigend überschrieben.

### Performance
- Regressions-Gate (aus `PERFORMANCE_VALIDATION_PLAN_v1.0.md`): ein Paket darf ohne dokumentierte Begründung nicht zu einer typischen Frame-Time-Verschlechterung > 10 % führen, RAM/VRAM-Peak nicht deutlich erhöhen, Save-Größe nicht unverhältnismäßig vergrößern, Build-Größe nicht spürbar erhöhen, keine neuen dauerhaften GC-Spikes einführen. Ausnahmen erfordern Profiling-Daten plus Folgeaufgabe.
- Zielkorridor (Neukalibrierung nach Vertical Slice vorbehalten): Minimum 1080p/30, Empfohlen 1440p/60, High-End 4K/60.
- Architektur muss auf die in `PERFORMANCE_VALIDATION_PLAN_v1.0.md` genannten Stresswerte skalierbar bleiben (2.000.000+ statistische Bewohner, 20.000+ reguläre Bauwerke, 50.000er Stresstest) — kein Paket darf eine Architekturentscheidung treffen, die diese Skalierung strukturell ausschließt (z. B. ein Bewohner = ein permanentes GameObject).

### Memory
- Keine unnötigen Assetduplikate; Materialinstanzen statt Materialkopien; Referenzen statt Datenkopien in Simulation und Save (siehe `DATA_SCHEMA.md` §1).

### Buildgröße
- Autoritativ (siehe `SPEC_AUDIT.md` Punkt 1, `DECISIONS.md`): Installgröße Soft Target ≤ 40 GiB pro Desktopplattform, Review Gate > 50 GiB, Hard Review > 60 GiB.
- Keine SourceAssets im Release-Build (siehe `.claude/rules/03-unity-assets.md`, `.claude/rules/04-blender-asset-production.md`).

### Savegröße
- Autoritativ: Late-Game-Save-Ziel < 75 MiB komprimiert, Review-Schwelle > 150 MiB.

### Story/Quest
- Automatischer Dialog-/Template-Lint vor Story-Content-Lock (siehe `MISSION_AND_QUEST_BIBLE_v1.0.md` §1.4): technische Regeln dürfen templiert sein, Dramaturgie/Dialog/Ereignisablauf müssen missionsspezifisch bleiben.
- Jede Story-Mission setzt genau ein stabiles Abschluss-Flag; Mehrphasenziele speichern Phase getrennt (siehe `DATA_SCHEMA.md` §3 QuestDefinition, `ARCHITECTURE.md` §5).

### Plattformen
- Zielplattformen Windows/Linux/macOS bleiben im Blick; kein Paket darf plattformspezifischen Code ohne Abstraktion einführen, der eine der drei Zielplattformen strukturell ausschließt.
- Fehlender lokaler Windows/macOS-Build-Support auf dem Entwicklungsrechner ist kein Gate-Blocker (siehe `DECISIONS.md`).

### IP/Asset License
- Keine geschützten Assets/Texte/UI/Modelle anderer Spiele; Anno 1800 nur als abstrakte Fallback-Referenz.
- Jedes neue externe Asset wird vor `FINAL`-Markierung in `THIRD_PARTY_LICENSES.md`/`ASSET_LICENSES.md` mit vollständigen Lizenzfeldern eingetragen (siehe `.claude/rules` und `BRAND_AND_RELEASE_READINESS_v1.0.md`).

## Nicht Teil dieser Fassung

Keine konkreten CI-Pipeline-Konfiguration, keine Tool-spezifischen Kommandozeilenaufrufe außer den in Phase 0 tatsächlich erstellten Validierungsskripten (siehe `Tools/Validation/`). Diese werden paketweise ergänzt, sobald die jeweilige Toolchain (Test-Runner, Build-Pipeline, Performance-Profiler) eingerichtet ist.

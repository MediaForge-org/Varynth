# SPEC_AUDIT.md — Varynth

Lebendes Dokument für Spezifikationskonflikte, veraltete Angaben und offene Klärungspunkte, gemäß `.claude/rules/01-spec-authority.md`. Jeder Eintrag erhält Severity (BLOCKER/HIGH/MEDIUM/LOW), Status (OPEN / RESOLVED BY AUTHORITY RULE / RESOLVED BY USER DECISION) und, falls resolved, die anwendbare Begründung.

Priorität laut Regel: aktuelle explizite Nutzerentscheidung > neuere ausdrücklich verbindliche Override-Entscheidung > aktuelle spezifische Fach-Bible > aktueller Mega-Prompt > ältere generische Zusammenfassung. Alte Backup-Dateien sind keine Quelle.

---

## Audit-Pass: Phase 0 (2026-08-09)

### 1. Build-/Savegrößenbudget — zwei konkurrierende Zahlensets

**Severity:** BLOCKER (bei Auffinden) → **Status: RESOLVED BY AUTHORITY RULE + Nutzerentscheidung**

**Befund:**
- `Docs/01_DESIGN/MEGA_PROMPT_v1.0.md`, §480 ("Perfektionspass"-Block): Installgröße Soft Target ≤ 50 GB, Review > 60 GB, Hard-Warnung 80 GB; Saves < 50 MB (Normalspiel) / < 200 MB (Stresstest).
- `Docs/05_QA/BUILD_SIZE_BUDGETS_v1.0.md` ("PASS 10", inhaltlich auch am Ende des Mega-Prompts noch einmal eingebettet): Installgröße Soft Target ≤ 40 GiB, Review > 50 GiB, Hard Review > 60 GiB; Saves < 25 MiB (Midgame) / < 75 MiB (Lategame-Ziel), Review > 150 MiB, Hard Review > 250 MiB.

**Auflösung:** `BUILD_SIZE_BUDGETS_v1.0.md` (PASS 10) ist die neuere, eigenständig versionierte, spezifische Fach-Bible und schlägt damit den älteren generischen §480-Block im Mega-Prompt. Vom Nutzer am 2026-08-09 ausdrücklich bestätigt: **PASS 10 ist autoritativ.** Die §480-Werte gelten als überschrieben/veraltet und sind nicht länger anzuwenden.

**Bindender Wert ab sofort:** Installgröße Soft Target ≤ 40 GiB / Review > 50 GiB / Hard Review > 60 GiB pro Desktopplattform; Late-Game-Save-Ziel < 75 MiB komprimiert, Review > 150 MiB. Siehe `DECISIONS.md` und `QA_GATES.md`.

---

### 2. Quest-XML-Beispiel-IDs vs. Content-Catalog-Namensraum

**Severity:** MEDIUM → **Status: RESOLVED BY AUTHORITY RULE + Nutzerentscheidung**

**Befund:**
Das illustrative Quest-XML-Beispiel in `Docs/02_STORY/MISSION_AND_QUEST_BIBLE_v1.0.md` §6 verwendet IDs wie `story.occidentia.001`, `occidentia.harbor.office.t1`, `occidentia.residence.farmer.t1`. Diese folgen nicht dem im `Docs/01_DESIGN/CONTENT_CATALOG_v1.0.md` definierten, tatsächlich verwendeten Namensraum (`res.<region>.t<tier>.f<form>`, `bld.<region-or-global>.<name>`, `good.<region-or-global>.<name>`, `ship.<region-or-scope>.<class>`, `veh.<region-or-scope>.<class>`; Missions-Header selbst verwenden `ST-OCC-001` etc.).

**Auflösung:** `CONTENT_CATALOG_v1.0.md` ist die dedizierte, spezifische Fach-Bible für technische IDs. Die Mission Bible verweist in ihrem eigenen §9 ("Pass-09-Synchronisierung") bereits explizit auf den Content Catalog als ID-Quelle. Vom Nutzer bestätigt: **Content Catalog ist für technische IDs autoritativ**; das XML-Beispiel in der Mission Bible ist ein illustratives/veraltetes Artefakt und darf nicht wörtlich implementiert werden.

**Wichtig — Scope-Grenze:** Phase 0 ändert die Mission Bible oder den Mega-Prompt **nicht**. Kein Massen-Rewrite des XML-Beispiels oder anderer großer Spezifikationsdateien in diesem Paket. Diese Auflösung ist ausschließlich eine Implementierungsrichtlinie für `DATA_SCHEMA.md` und zukünftige Content-/Quest-Implementierung.

---

## Als bewusste Arbeitsbasis bestätigt — keine Konflikte

Diese Punkte enthalten explizite "vorläufige Baseline"-Hinweise in ihren Quelldokumenten und sind laut Projektregel **keine** Spezifikationskonflikte, sondern bewusst offen gelassene, spätere Balancing-/Profiling-Arbeit:

- `Docs/01_DESIGN/BALANCING_BIBLE_v1.0.md` §1: explizit "implementierbare v1.0-Balancingbaselines, aber noch kein finaler Release-Lock".
- `Docs/03_PRESENTATION/ART_BIBLE_v1.0.md` §19: exakte Triangle-/Texelbudgets werden bewusst erst nach dem ersten Vertical Slice per GPU-/CPU-Profiling eingefroren.
- `Docs/03_PRESENTATION/AUDIO_BIBLE_v1.0.md` §11: keine endgültige Mastering-Norm vor Plattformtests.

## Offene Punkte ohne Blockercharakter (zur Kenntnis, keine Phase-0-Aktion nötig)

- Der frühere Arbeitstitel `ORBIS — Beyond Horizons` ist nicht freigegeben; VARYNTH ist der verbindliche Produktname (siehe `DECISIONS.md`). Eine formale Marken-/IP-Prüfung bleibt ein späteres Release-Gate, kein Phase-0-Blocker.
- `Docs/07_LEGAL/THIRD_PARTY_LICENSES.md` und `ASSET_LICENSES.md` sind aktuell reine Platzhaltertabellen — konsistent mit dem Projektstand (noch keine externen Assets), keine Aktion nötig, bis reale Drittassets hinzukommen.

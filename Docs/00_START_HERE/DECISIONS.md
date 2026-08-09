# DECISIONS.md — Varynth Phase 0

Verbindliche Projekt-Grundentscheidungen, Stand des Phase-0-Audits. Dieses Dokument wird bei zukünftigen Overrides ergänzt, nicht rückwirkend umgeschrieben (siehe `.claude/rules/01-spec-authority.md`).

**Audit-Datum:** 2026-08-09

## Projektname

- **VARYNTH** ist der verbindliche, aktuelle Spiel-/Produktname — kein Codename.
- `AnnoLikeGame` war ein früherer interner Arbeitscodename und ist nicht mehr in Verwendung.
- `ORBIS — Beyond Horizons` ist ein diskutierter Titel und **ausdrücklich nicht freigegeben** (`Docs/07_LEGAL/BRAND_AND_RELEASE_READINESS_v1.0.md`: Vorab-Screening fand potenziell kollidierende bestehende Marken/Spiele mit "Orbis/Orbi" bzw. "Beyond Horizon").
- Eine abschließende Marken-/Rechtsprüfung (EUIPO/WIPO, Store-Namenssuche, IP-Rechtsberatung) bleibt vor jedem öffentlichen Store-Listing, Marketing oder Domainkauf verpflichtend — das ist kein Phase-0-Blocker, sondern ein späteres Release-Gate.

## Engine & Rendering

- **Unity 6000.5.7f1** (entspricht der projektinternen Bezeichnung "Unity 6.5"), eingefroren als Startversion in `ProjectSettings/ProjectVersion.txt`.
- Renderingpipeline: **Universal Render Pipeline (URP) 17.5.0**, bestätigt in `Packages/manifest.json`, `ProjectSettings/GraphicsSettings.asset`, `ProjectSettings/URPProjectSettings.asset`. PC- und Mobile-RP-Asset/Renderer-Paare bereits unter `Assets/Settings/` vorbereitet.
- Weitere relevante Packages: AI Navigation 2.0.14, Input System 1.20.0, Timeline 1.8.12, UGUI 2.5.0, Visual Scripting 1.9.12, Test Framework 1.7.0, Linux-Build-Support.

## Plattformziele

- Desktop: **Windows, Linux, macOS**.
- Aktueller Entwicklungsrechner: **Fedora Linux**. Fedora ist keine von Unity offiziell unterstützte Editor-Plattform (Ubuntu 22.04/24.04 sind es), das blockiert die Entwicklung laut Spezifikation aber nicht.
- Ein aktuell fehlender lokaler Windows/macOS-Build-Support auf dem Entwicklungsrechner ist ausdrücklich **kein** Designproblem und blockiert nicht (bestätigt in `UNITY_PROJECT_SETTINGS.md`, `FINAL_AUDIT_PASS09.md`, Mega-Prompt §485/§510).

## Git / Repository

- Branch: `main`.
- Remote: `origin` → `https://github.com/MediaForge-org/Varynth.git`.
- Arbeitsverzeichnis zu Audit-Beginn: sauber, ein Commit (`ac67a49 chore: initialize Varynth Unity project`).
- Git LFS 3.7.1 aktiv; `.gitattributes` trackt `.blend .psd .kra .fbx .wav .flac .tif .tiff .exr .mp4 .mov .7z .zip` über LFS und normalisiert Text-/Codedateien (`eol=lf`) für `.cs .md .xml .json .uxml .uss .shader .hlsl .yml .yaml`.
- Generierte Unity-Ordner (`Library/ Temp/ Logs/ UserSettings/ Builds/ obj/`) sind nicht getrackt — verifiziert per `git ls-files`.
- Unity-Serialisierung: **Force Text** (`m_SerializationMode: 2`), **Visible Meta Files** (`m_ExternalVersionControlSupport: Visible Meta Files`) — beide bereits korrekt konfiguriert, keine Korrektur nötig.

## Aktuell verbindliche fachliche Overrides (Kurzreferenz, Details in den jeweiligen Bibles)

- Storymodus ist beim ersten Start verfügbar; Story beginnt bereits beim ersten Haus.
- Aufbauspielmodus (storyfrei) wird profilweit erst nach vollständigem Storyabschluss freigeschaltet, startet standardmäßig in Aurelia/Ultima.
- Sandbox ist ein separater dritter Modus.
- Bekannte Welt: Occidentia 8, Meridia 4, Orientia 6, Aferia 5, Australis 3 (Australis: 3 Personalgruppen, kein klassisches Tier-Modell).
- Ultima: Aurelia 8, Viridia 6, Titania 5, Ignaria 6, Pelagia 5 = 30 Grundspiel-Zivilisationsstufen. Keine technische Obergrenze für Mods/DLCs.
- Normalspiel-Gesamtbevölkerung = reguläre Bewohner + Lebensqualitäts-Bonusbewohner. Optionale Zusatzwaren geben im Normalspiel **0** Bonusbewohner (nur über optionale Sandbox-/Cheat-Regel reaktivierbar).
- Wohnhaus-Upgrades erfolgen mechanisch sofort bei erfüllten Voraussetzungen, keine künstliche Bauzeit, keine künstliche Bewohnervertreibung.
- Kleine Lagerhäuser haben auf derselben Technologiestufe dieselbe Lagerkapazität wie Standardlagerhäuser derselben Stufe — Unterschied liegt in Footprint und (aufrüstbarer) Rampenzahl, niemals in der Kapazität.
- Keine verpflichtende Straßenauslastungs-/Stau-/Kreuzungssimulation.
- Anno 1800 ist ausschließlich abstrakte Fallback-Referenz für noch undefinierte Genremechaniken — keine Assets/Modelle/UI/Texte/Quests/Icons/Musik/Trade-Dress-Übernahme.

## Speicher-/Buildgrößenbudget (siehe auch SPEC_AUDIT.md, Punkt 1)

- Autoritativ: `Docs/05_QA/BUILD_SIZE_BUDGETS_v1.0.md` ("PASS 10"). Installgröße Soft Target ≤ 40 GiB pro Desktopplattform, Review Gate > 50 GiB, Hard Review > 60 GiB. Late-Game-Save Ziel < 75 MiB komprimiert, Review > 150 MiB.

## Bekannte Setup-Besonderheiten

- `Assets/Game/` existiert noch nicht (erwartet vor Phase 1/Content-Arbeit).
- `SourceAssets/Art/Blender/` enthält bislang nur den `Shared`-Unterordner (Platzhalter/`.gitkeep`); regionsspezifische Unterordner fehlen noch.
- `Tools/Blender/{generators,exporters,previews,validators}` existieren als leere Scaffolding-Ordner.
- Keine `.blend`-Dateien im Repository — 3D-Produktion hat noch nicht begonnen (planmäßig für Phase 0).
- `Docs/07_LEGAL/THIRD_PARTY_LICENSES.md` und `ASSET_LICENSES.md` enthalten nur Platzhalter-/Skeletteinträge — kein externes Drittasset aktuell in Verwendung.

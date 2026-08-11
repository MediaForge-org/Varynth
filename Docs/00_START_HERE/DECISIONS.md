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

## Phase 2A Prototypwerte (Varynth 0.1.0, nicht kanonisch)

Zu Phase-0-Ende waren Maßstab und Grid ausdrücklich offen (siehe oben, §26 Mega-Prompt). Für
den ersten sichtbaren Meilenstein (`WorldPrototype`-Szene) wurden folgende **technische
Prototypwerte** verwendet — keine finalen Design-/Balancing-Entscheidungen, jederzeit änderbar,
dokumentiert gemäß `.claude/rules/01-spec-authority.md`:

- 1 Unity-Unit = 1 Meter (Engine-Konvention).
- Grid-Zellgröße: 4.0 Units, Ursprung Welt-`(0,0)`.
- Terrain-Weltgröße: 300×300 Units, `TerrainData.size.y` = 40, Terrain-`Transform.position.y` = −15.
- Sea Level = Welt-Y 0 → normalisierte Terrainhöhe 15/40 = 0.375.
- Testinsel-Heightmap: deterministischer Seed `20260809` (eigener Hash-/Value-Noise-Generator,
  kein `UnityEngine.Random`).

Details siehe `Docs/04_IMPLEMENTATION/PHASE_2A_WORLD_CAMERA_FOUNDATION.md`.

## Phase 2B Prototypwerte (Varynth 0.1.1, nicht kanonisch)

Erweitert die einzelne 0.1.0-Testinsel um ein kleines Testarchipel. Weiterhin keine
verbindliche Spezifikation für Inselgrößenklassen, Strandbreite, bebaubaren Steigungs-
Schwellwert oder Ressourcenplatz-Abstand gefunden (`MEGA_PROMPT` liefert nur die narrative
"~10–15 Inseln/Region" und die verbindliche, aber nicht-räumliche "0–15 Minenplätze/Insel"-
Zahl) — folgende Werte bleiben technische Prototypwerte:

- 4 Prototype-Inseln (`TestIsland_Large/Medium/Small/Coastal`), Footprints 110×110 bis
  260×260 Units, Seed-Familie `20260809 + Index`. Namen sind Debug-Labels, keine
  Savegame-IDs/Occidentia-Lore.
- Sea-Level-Konvention aus Phase 2A unverändert für jede Insel: `TerrainData.size.y = 40`,
  Terrain-`Transform.position.y = -15`, Sea Level = normalisierte Höhe 0.375.
- `SurfaceClassificationConfig`: `CoastBandHeight = 3` Units über Sea Level, gemeinsamer
  `SlopeThresholdDegrees = 30°` für `RockOrSteep`/`Buildable`.
- Ressourcenplatz-Kandidaten: 2–5 pro Insel (abhängig von Inselgröße), Mindestabstand
  6 Zellen, deterministisch aus Insel-Seed + festem Salt (kein `UnityEngine.Random`).

Details siehe `Docs/04_IMPLEMENTATION/PHASE_2B_ISLAND_TERRAIN_FOUNDATION.md`.

## Phase 2B Bugfix-Runde: Kamera-Framing, Underwater-Terrain, Pan-/Zoom-Skalierung (Varynth 0.1.1)

Technische Prototype-UX-Schwellwerte, keine finalen Balancing-Werte:

- Initiales Kamera-Framing (`ComputeCameraFraming` in `WorldPrototypeSceneBuilder`) wird
  aus der Bounding-Box der tatsächlich über Sea Level liegenden Surface-Zellen berechnet
  (nicht aus der vollen, das Unterwasser-"Skirt" jeder Terrain-Kachel einschließenden
  Footprint-Bounding-Box) — `ArchipelagoFramingMargin = 1.15`.
- Terrain-Alphamap: 4. Layer "Seabed" (dunkles Blaugrau) für Zellen unterhalb Sea Level,
  verhindert helle Sand-Rechtecke unter der halbtransparenten Wasserfläche.
- Wasserfläche-Rand: dynamische Marge `CameraBoundsMargin + ZoomMaxDistance + WaterMargin(60)`,
  garantiert, dass die Wasserkante bei erlaubtem Pan+Zoom nie sichtbar wird.
- Pan-Geschwindigkeit skaliert mit der aktuellen Zoomdistanz (`PanSpeedPerZoomDistance = 0.6`,
  `MinPanSpeed = 25`) statt eines festen Wert/Sekunde-Konstanten — verhindert, dass Panning
  bei großen Welten (großer Zoom-Range) gefühlt wirkungslos wird.
- Zoom ist prozentual/multiplikativ (`ZoomPercentPerNotch = 0.2`), nicht additiv mit fixer
  Schrittweite — ein Mausrad-Notch ändert die Distanz um ~20%, unabhängig vom absoluten
  Zoom-Bereich. Kalibriert unter der Annahme `Mouse.scroll.y ≈ 1 Einheit/Notch` (konsistent
  mit dem zuvor gemeldeten "dutzende Notches nötig"-Symptom beim alten
  `ZoomSensitivity = 8`/Notch-Wert über einen ~105-Einheiten-Bereich); finale Gefühlskontrolle
  bleibt beim Nutzer.

## Phase 2C Prototypwerte (Varynth 0.2.0, nicht kanonisch)

Erste echte Bau-Sandbox (Gebäude auswählen, Ghost, Rotation, Platzieren, Entfernen).
Keine verbindliche Spezifikation für Footprints außerhalb der 3×3-Wohnhausparzelle
(`MEGA_PROMPT` §35.1, irrelevant hier — die 3 Prototype-Gebäude sind ausdrücklich keine
echten Wohnhausketten), für den Rotations-Snap-Schritt oder für eine generische
Bauflächen-Formel über `SurfaceCellFlags.Buildable` hinaus gefunden — folgende Werte
bleiben technische Prototypwerte:

- 3 Prototype-Gebäude: House 2×2, Production Block 3×2, Public Building 4×3.
  Rotations-Snap: 4 orthogonale Schritte (0°/90°/180°/270°).
- `PlacementConfig.MaxFootprintHeightVariation = 4` Weltunits (max. Höhendifferenz über
  die Footprint-Zellen hinweg) — empirisch gegen echte generierte Terrains kalibriert,
  nicht die dominante Ablehnungsursache (die realen 4 Prototype-Inseln sind primär durch
  Wasser-/Nicht-Buildable-Fläche begrenzt, nicht durch diese Regel).
- Neue Content-Root-Konvention (bisher nicht festgelegt): reale XML-Content-Dateien
  liegen unter `Assets/StreamingAssets/Content/<Kategorie>/*.xml` (Player-Build-sicher
  über `Application.streamingAssetsPath`, im Gegensatz zu `Assets/` oder `Resources/`).
- `BuildingInstanceId`: sequentieller, world-state-eigener `ulong`-Zähler (nicht `Guid`)
  — Determinismus-Anforderung für künftiges Host-authoritatives Koop/Replay/Save.
- **Follow-up "Bau-Sandbox-Fläche vergrößern"**: `TestIsland_Large` wurde von 260×260 auf
  440×440 vergrößert und abgeflacht (`IslandRadius01` 0.62→0.70, `Octaves` 5→3,
  `Persistence` 0.45→0.30, `CoastNoiseStrength` 0.14→0.12), ausschließlich über bestehende
  `IslandPrototypeConfig`-Parameter — keine neue Terrainarchitektur, `HeightmapResolution`
  bleibt bei 257. Reale Archipel-Kapazität stieg dadurch von ~38 auf 169 nicht
  überlappende 2×2-Footprints. Ausdrücklich weiterhin ein technischer Sandbox-Prototypwert,
  keine finale Varynth-Inselgröße.

Details siehe `Docs/04_IMPLEMENTATION/PHASE_2B_ISLAND_TERRAIN_FOUNDATION.md`.

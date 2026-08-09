# CLAUDE IMPLEMENTATION GUIDE v1.0
## Schritt-für-Schritt-Entwicklungsleitfaden für das AnnoLike-Game-Projekt

> Dieser Guide erklärt, **wie Claude/Claude Code das Projekt umsetzen soll**, welche Programme der Nutzer benötigt, welche Reihenfolge einzuhalten ist, welche Dateien zuerst entstehen und welche Tests/Gates nach jedem Paket zwingend laufen. Der Mega-Prompt und die Mission & Quest Bible sind die fachliche Quelle; dieser Guide ist die operative Umsetzungsanweisung.

# 1. Grundsatz: Nicht das ganze Spiel in einem einzigen Coding-Prompt bauen

Claude darf niemals versuchen, 100.000+ Wörter Spezifikation in einem einzigen Implementierungsschritt „fertig zu programmieren“. Das Projekt wird paketweise umgesetzt. Jedes Paket muss klein genug sein, dass Claude:

1. vorhandenen Code vollständig lesen kann;
2. einen konkreten Plan schreibt;
3. nur den vereinbarten Scope verändert;
4. Tests ergänzt;
5. Tests/Gates ausführt;
6. Fehler behebt;
7. Dokumentation aktualisiert;
8. einen klaren Abschlussbericht liefert.

**Verbindlich:** Jede zukünftige Claude-Aufgabe enthält ausdrücklich „Tests/Gates prüfen und ausführen“. Ein Paket gilt nicht als fertig, nur weil der Code kompiliert.

**Narrative Pflicht:** Bei Story-/Quest-Paketen gehören zusätzlich Dialog-/Template-Lint, Storyflag-Prüfung, Reveal-Pacing-Prüfung und Save/Load-Tests zu den Gates. Wiederholte technische Boilerplate ist erlaubt; wiederholte austauschbare Dialoge gelten vor Content Lock als Fehler.


# 2. Programme und Werkzeuge

## 2.1 Pflichtprogramme

### Unity Hub + Unity 6
- Projekt-Engine: **Unity 6**.
- Für eine stabile Produktionsbasis soll eine konkrete Unity-Version im Repository/Projekt festgehalten und nicht bei jedem Update spontan gewechselt werden.
- **Verbindliche Projektstart-Version: Unity 6.5.** Die konkrete 6.5-Patchversion wird beim ersten Projekt-/Repository-Setup festgehalten; ein Wechsel erfolgt nur über den definierten Upgrade-Prozess.
- Über Unity Hub werden Editor und Build-Support verwaltet.

### Visual Studio Code
- Haupteditor für C#, XML, Markdown, JSON/Config und Git-Arbeit.
- Auf Fedora die offizielle RPM-/Repository-Variante verwenden.
- Empfohlene Erweiterungen: C#-Unterstützung, Unity-Unterstützung, XML-Unterstützung, Git-Integration sowie Claude Code.

### Claude Code / Claude-Code-Erweiterung für VS Code
Claude ist der primäre Coding-Agent. Er soll:
- Repository und Spezifikation lesen;
- Code erstellen/ändern;
- Tests schreiben;
- Unity-Projektdateien und XML-Schemas pflegen;
- technische Dokumentation aktualisieren;
- keine fachlichen Entscheidungen stillschweigend erfinden, wenn der Mega-Prompt einen Wert verbindlich vorgibt.

Claude-Checkpoints sind **kein Ersatz für Git**. Vor großen Agentenläufen muss der Git-Arbeitsbaum sauber oder bewusst gesichert sein.

### Git
Pflicht für Versionskontrolle. Keine Entwicklung ohne Versionshistorie.

### Git LFS
Pflicht, sobald Binär-/Großdateien wie `.blend`, hochauflösende Texturen, Audio, Video, große PSD/KRA-Dateien oder andere schwere Assets im Repository liegen. Quellcode, XML und Markdown bleiben normales Git.

### Blender
Pflichtwerkzeug für 3D-Modelle, Gebäude, Fahrzeuge, Schiffe, Props, einfache Animationen, LOD-Modelle und Exportvorbereitung.

### Krita
Empfohlenes freies Hauptwerkzeug für Concept Art, UI-Mockups, Texturmalerei, Karten-/Icon-Entwürfe und 2D-Artwork.

### Audacity
Freies Werkzeug für Sprachschnitt, einfache Soundbearbeitung und schnelle Audioaufgaben.

## 2.2 Optional, aber sinnvoll
- **REAPER:** umfangreicher DAW-Workflow für Musik, Sprachschnitt und komplexes Sounddesign.
- **Inkscape:** Vektor-Icons, Logos, UI-Symbole, Diagramme.
- **OBS Studio:** Videoaufnahmen von Bugs, Performancevergleichen und Trailer-/Devlog-Material.
- **RenderDoc:** spätere GPU-/Renderinganalyse, wenn Unity Profiler allein nicht genügt.
- **GitHub:** Remote-Repository, Issues, Releases, CI und Backup der Git-Historie.

# 3. Wichtiger Hinweis für Fedora

Der Nutzer entwickelt auf Fedora. VS Code, Blender, Krita, Git und die übrigen Werkzeuge sind dort grundsätzlich gut nutzbar. Für den **Unity Editor** ist jedoch die offiziell unterstützte Linux-Referenz entscheidend. Falls eine Unity-Version Fedora nicht offiziell unterstützt, darf Claude keine „garantiert stabil“-Behauptung machen. Der praktische Workflow ist:

1. Fedora zunächst verwenden, wenn Unity Hub/Editor sauber funktionieren.
2. Projekt selbst distributionsneutral halten: keine Fedora-spezifischen Runtime-Abhängigkeiten im Spielcode.
3. Reproduzierbare Builds/CI auf einer offiziell unterstützten Linux-Umgebung vorsehen.
4. Bei Editorproblemen nicht tagelang Workarounds in den Gamecode einbauen; stattdessen eine offiziell unterstützte Entwicklungs-/Buildumgebung (z. B. Ubuntu) als Referenz verwenden.

# 4. Unity-Paketbasis

Zu Projektbeginn nur benötigte, stabile Pakete installieren. Kernbasis:
- **UI Toolkit** für skalierbare Runtime-/Editor-UI;
- **Input System** statt Legacy Input Manager;
- **Addressables** für große, regionsweise ladbare Assetmengen;
- Unity Test Framework für EditMode-/PlayMode-Tests;
- Localization-Paket für alle sichtbaren Texte/Dialoge;
- Cinemachine nur dort, wo geführte Kamera-/Storyinszenierung davon profitiert.

Jedes zusätzliche Package muss begründet werden. Keine Sammlung von Plugins „für später“.

# 5. Repository-Struktur

Empfohlene Top-Level-Struktur:

```text
Varynth/
├── Assets/
│   ├── _Game/
│   │   ├── Art/
│   │   ├── Audio/
│   │   ├── Data/
│   │   ├── Prefabs/
│   │   ├── Scenes/
│   │   ├── Scripts/
│   │   ├── UI/
│   │   └── Tests/
│   └── ThirdParty/
├── ArtSource/
│   ├── Blender/
│   ├── Krita/
│   └── AudioSource/
├── Docs/
│   ├── MEGA_PROMPT_v1.0.md
│   ├── MISSION_AND_QUEST_BIBLE_v1.0.md
│   ├── ARCHITECTURE.md
│   ├── DATA_SCHEMA.md
│   ├── MODDING.md
│   ├── QA_GATES.md
│   └── DECISIONS.md
├── Packages/
├── ProjectSettings/
├── Tools/
│   ├── Validators/
│   ├── Importers/
│   └── Build/
├── .gitattributes
├── .gitignore
├── README.md
└── CHANGELOG.md
```

# 6. Git-/Branch-Workflow

- `main`: nur getestete, lauffähige Integrationsstände.
- Feature-/Paketbranches: `feature/grid-core`, `feature/population`, `feature/quest-system` usw.
- Vor einem großen Claude-Lauf: `git status` prüfen.
- Nach einem abgeschlossenen Paket: Tests, Review, Commit/Tag nach Nutzerworkflow.
- Große Binärdateien ausschließlich über LFS-regulierte Muster.
- Niemals generierte Unity-Cacheordner committen (`Library/`, `Temp/`, `Logs/`, lokale IDE-Caches usw.).

# 7. Phase 0 — Projekt einfrieren und Spezifikationen importieren

Claude soll zuerst **noch keinen Gameplaycode** schreiben.

Aufgaben:
1. Mega-Prompt nach `Docs/MEGA_PROMPT_v1.0.md` übernehmen.
2. Mission Bible nach `Docs/MISSION_AND_QUEST_BIBLE_v1.0.md` übernehmen.
3. `Docs/DECISIONS.md` erzeugen: nur verbindliche Entscheidungen und Overrides.
4. Widerspruchsscan: doppelte IDs, alte Zusatzversorgungs-Bonusregeln, falsche Modusregeln, alte Storyfassungen.
5. `Docs/OPEN_QUESTIONS.md` nur für echte noch offene Punkte. Keine erfundenen „TBD“, wenn die Spezifikation bereits einen Wert festlegt.
6. Projektversion und Paketversionen dokumentieren.

**Gate P0:** Keine widersprüchliche Kernregel, Spezifikationen im Repo, Git sauber, Projekt öffnet.

# 8. Phase 1 — Technisches Fundament

Claude implementiert ausschließlich Basisschichten:
- Logging;
- Service-/Systemregistrierung;
- stabile IDs;
- GameClock/SimulationTick;
- Eventbus oder klar begrenzte Event-Schnittstelle;
- Savegame-Grundformat mit Versionierung;
- XML-Lader;
- XML-Validierung;
- Mod-Load-Order-Grundlage;
- Debug-Konsole nur für Development Builds;
- Test-Utilities.

Noch **keine** komplette Wirtschaft und keine Story.

**Gate P1:** EditMode-Tests grün, kaputte XML wird verständlich abgewiesen, unbekannte Mod-ID crasht nicht den gesamten Editor, leeres Save/Load funktioniert.

# 9. Phase 2 — Datenmodell zuerst, Content danach

Datenobjekte definieren für:
- Region;
- Zivilisationsstufe;
- Bevölkerungsklasse;
- Wohnform;
- Ware;
- Rezept;
- Produktionsgebäude;
- Dienst;
- Straße/Transport;
- Schiff;
- Forschung;
- Quest;
- Dialog;
- Fraktion;
- Katastrophe.

Jedes Objekt besitzt stabile String-ID, Schema-Version und Validierungsregeln. Gameplaycode referenziert IDs/Definitionen, nicht harte Enum-Maximalwerte.

**Gate P2:** Beispiel-Mod kann mindestens eine neue Ware, ein Rezept und eine Wohnform hinzufügen, ohne C# zu ändern.

# 10. Phase 3 — Karten-/Grid-/Bausystem

Reihenfolge:
1. Welt-/Regionscontainer;
2. Insel-/Landmassenkarte;
3. Baubereich;
4. Raster;
5. Gebäudeflächen;
6. Drehen/Platzieren;
7. Straßen;
8. Abriss;
9. Verschieben, falls vorgesehen;
10. Blaupausen;
11. Copy/Paste.

Noch keine hübschen finalen Assets: farbige Blockout-Prefabs genügen.

**Gate P3:** Tausende Blockout-Gebäude platzierbar, Save/Load identisch, keine Grid-Desynchronisierung, Copy/Paste respektiert Daten-IDs.

# 11. Phase 4 — Bevölkerung und Wohnhäuser

Zuerst Bauern in Occidentia als vertikaler Slice:
- Bauernhaus;
- 10 reguläre Bewohner;
- Pflichtbedürfnisse;
- Markt;
- Lebensqualität;
- Arbeitskräfte;
- sofortiges Upgrade ohne Bauzeit;
- alle sechs Hausformen derselben Klasse;
- danach horizontaler Aufstieg zu Arbeitern.

Erst wenn das generische System funktioniert, werden die restlichen 48 Occidentia-Hausformen und danach andere Regionen per Daten ergänzt.

**Kritischer Test:** Optionale Waren geben im Normalspiel **0 Bonusbewohner**. Sandboxregel kann die alte Mechanik gezielt aktivieren.

# 12. Phase 5 — Produktion und Arbeitskräfte

Implementationsreihenfolge:
1. Lager;
2. Input/Output-Rezepte;
3. Produktionszeit;
4. Arbeitskraftfaktor 0–1;
5. Produktionsgebäude;
6. Warenverbrauch;
7. Transportabholung/-lieferung;
8. Statistiken Produktion/Minute;
9. alternative Rezepte;
10. Nebenprodukte;
11. Ultima-Automation U0–U5 erst viel später.

Produktionssimulation muss deterministisch genug für Save/Load- und Regressionstests sein.

# 13. Phase 6 — Logistik, Straßen, Häfen, Schiffe

- 5 bekannte-Welt-Straßenstufen;
- Reichweite/Geschwindigkeit statt Straßenauslastung;
- Lagertransport;
- Hafen;
- automatisierte Handelsroute;
- Mindestreserve/Zielbestand;
- mehrere Stopps;
- mehrere Schiffe je Route;
- Expeditionstransport als eigener Zustand.

**Nicht implementieren:** Kreuzungssimulation, Stau-Heatmap, Verkehrslicht-Mikro.

# 14. Phase 7 — Netze und Dienste

Strom, Wasser, Wärme/Kühlung, Abwasser, Abfall, Gesundheit, Feuerwehr, Sicherheit, Bildung, Post usw. werden als Kapazitäts-/Reichweiten-/Qualitätssysteme umgesetzt. Keine Haus-zu-Haus-Rohrsimulation.

Hohe Stufen steigern oft Kapazität/Redundanz statt endlos neue Qualitätsnummern.

# 15. Phase 8 — UI/UX

UI Toolkit verwenden, klare Trennung:
- HUD;
- Bauleiste;
- Gebäudeinspektor;
- Bedürfnisse;
- Lebensqualität;
- Produktion;
- Statistik;
- Handelsrouten;
- Forschung;
- Questtracker;
- Storylog;
- Welt-/Regionsauswahl;
- Modverwaltung.

Alle Kernanzeigen mit 200 % UI-Skalierung testen.

# 16. Phase 9 — Quest-/Story-Engine vor Storycontent

**Sehr wichtig:** Claude implementiert nicht zuerst 100+ Missionen mit Spezialcode.

Zuerst generische Objective-Typen:
- Build;
- Upgrade;
- Population;
- NeedCoverage;
- ProductionRate;
- Stockpile;
- TradeRoute;
- Import/Export;
- Research;
- ServiceQuality;
- Expedition;
- Diplomacy;
- DisasterRecovery;
- WorldNexusPhase;
- Set/Check Flag;
- Choice.

Dann Dialogsystem, Storylog, Kamera-Cue, Save/Load und Questvalidator.

**Gate P9:** Eine Testmission kann komplett aus XML erstellt werden, ohne neuen C#-Questcode.

# 16.1 Story-Content-Lock vor Implementierung

**VERBINDLICH:** Für feste Storymissionen gilt ausschließlich der narrative **Content Pass 06** der `MISSION_AND_QUEST_BIBLE_v1.0.md` als aktuelle Textbasis. Ältere Generator-/Template-Dialoge dürfen nicht aus Backups, Git-Historie oder früheren Promptteilen wieder eingeführt werden.

Vor Implementierung eines Missionspakets prüft Claude zusätzlich:

- Mission besitzt `Missionsdrehbuch — individueller Ablauf`;
- keine pauschalen 25/50/75-Prozent-Dialogtrigger;
- Figurenstimme stimmt mit der Character-Bible überein;
- Wendepunkt ist an ein konkretes Missionsobjekt/Ereignis gebunden;
- Dialog-/Template-Lint des Pakets ist sauber;
- technische Boilerplate darf gemeinsam bleiben, dramatische Zeilen nicht.

Der spätere Voice-Lock darf Formulierungen redaktionell verbessern, aber nicht wieder zu austauschbaren Standarddialogen zurückfallen.

# 17. Phase 10 — Story vertikaler Slice

Erst die Missionen `ST-OCC-001` bis `ST-OCC-005` vollständig implementieren:
- echte Questdaten;
- deutsche Texte;
- Platzhalter-Sprachausgabe optional;
- Kamera-Cues;
- Storylog;
- Save/Load an jedem Schritt;
- bereits-erfüllte Ziele;
- Abbruch-/Recovery.

Wenn diese fünf Missionen robust funktionieren, wird der Rest datengetrieben importiert.

# 18. Phase 11 — Bekannte Welt Content

Reihenfolge nach Systemreife:
1. Occidentia komplett;
2. Meridia;
3. Orientia;
4. Aferia;
5. Australis;
6. globale bekannte-Welt-Story;
7. Expedition zur Eiswand.

Nicht jede Region benötigt sofort Final Art. Blockout → Gameplay komplett → Art Pass.

# 19. Phase 12 — Ultima

Ultima niemals früh halb implementieren. Voraussetzung:
- bekannte-Welt-Kernsysteme stabil;
- Regionstreaming stabil;
- Questengine stabil;
- Forschung stabil;
- Netzsysteme stabil;
- Moddaten skalieren.

Dann:
1. Aurelia Slice;
2. Ätherenergie AQ/Q-System;
3. U0–U5 Produktion;
4. RQ-Forschung;
5. UT-Transport;
6. Viridia;
7. Titania + Riesen;
8. Ignaria + Drachen;
9. Pelagia + Tiefsee/Leviathane;
10. Caelari;
11. Portale;
12. World Nexus;
13. 60 Ultima-Hauptmissionen;
14. Ultima-Postgame.

# 20. Phase 13 — Art Pipeline

Für jedes Asset:
1. Konzept/Referenzbriefing aus Art Direction;
2. Blockout in Blender;
3. Maßstab/Pivot prüfen;
4. High-/Lowpoly nur wenn nötig;
5. UVs;
6. Texturen;
7. LODs;
8. Collider;
9. Export;
10. Unity-Importpreset;
11. Prefab;
12. Performancebudget;
13. Varianten.

Source-Dateien (`.blend`, `.kra`) bleiben in `ArtSource/` und laufen über Git LFS. Spielassets werden sauber getrennt importiert.

# 21. Phase 14 — Audio und Sprache

- Dialoge aus Mission Bible → Lokalisierungsschlüssel → Recording-Sheets.
- Erst Platzhalter-/Temp-VO für Timing.
- Danach Casting/Finalaufnahme.
- Audacity oder REAPER für Schnitt/Normalisierung/Organisation.
- Regionale Musikstems und dynamische Zustände getrennt vom Dialog.
- Untertitel sind Pflicht und dürfen nicht von vorhandener Sprachausgabe abhängen.

# 22. Phase 15 — Performance

Nicht bis zum Ende warten. Nach jedem großen System messen:
- CPU/GPU Frametime;
- Simulations-Tick;
- GC Allocations;
- RAM/VRAM;
- Save-/Loadzeit;
- Regionswechsel;
- 20k/50k-Strukturen-Stressszenarien.

Erst optimieren, nachdem der Profiler einen echten Engpass zeigt. Keine vorschnelle ECS-Komplettmigration aus Vermutung.

# 23. Phase 16 — QA und Content Lock

Vor Alpha:
- Kernloop vollständig;
- keine Save-Corruption;
- keine Hauptstory-Hardlocks;
- Modvalidator stabil;
- bekannte-Welt-Story durchspielbar;
- Ultima-Basissysteme erreichbar.

Vor Beta:
- komplette Hauptstory vom ersten Haus bis World Nexus;
- alle Hauptmissionen und festen Nebenquests implementiert;
- Aufbauspielmodus freischaltbar und storyfrei;
- große Performance-/Langzeittests;
- Controller/Accessibility;
- Lokalisierung vollständig.

Vor Release:
- alle Gates;
- Store-/Rechts-/Lizenzprüfung;
- Crashreport-/Supportplan;
- Save-Migrations-Test;
- Clean-install-Test;
- Offline-Test;
- Modded/Unmodded-Regression.

# 24. Standardprompt, den der Nutzer Claude für jedes Paket geben kann

```text
Lies zuerst vollständig die für diesen Scope relevanten Teile aus:
- Docs/MEGA_PROMPT_v1.0.md
- Docs/MISSION_AND_QUEST_BIBLE_v1.0.md
- Docs/ARCHITECTURE.md
- Docs/DATA_SCHEMA.md
- Docs/QA_GATES.md

Arbeite ausschließlich am folgenden Paket: <PAKETNAME>.

Bevor du Code änderst:
1. Prüfe den aktuellen Repositoryzustand und relevante bestehende Implementierungen.
2. Liste kurz auf, welche verbindlichen Regeln aus den Projektdokumenten für dieses Paket gelten.
3. Nenne die Dateien/Systeme, die du ändern willst.
4. Erfinde keine neuen Gameplaywerte, wenn der Mega-Prompt bereits Werte festlegt.
5. Bei einem echten Widerspruch stoppe und benenne ihn, statt stillschweigend eine Variante zu wählen.

Implementiere das Paket vollständig, aber erweitere den Scope nicht ungefragt.
- datengetrieben;
- moddbar;
- keine künstlichen Maximalwerte;
- Savegame-Kompatibilität beachten;
- keine unnötige Simulationskomplexität;
- Tests für neue Logik hinzufügen.

Danach zwingend:
1. Build/Compile prüfen.
2. Alle relevanten EditMode-/Unit-Tests ausführen.
3. Relevante PlayMode-/Integrationstests ausführen.
4. Daten-/XML-Validator ausführen.
5. QA-Gates für dieses Paket prüfen.
6. Fehler beheben und Tests erneut ausführen.
7. Dokumentation aktualisieren.
8. Eine Abschlussübersicht geben: geändert, getestet, offene echte Probleme, empfohlener nächster Schritt.

Das Paket ist NICHT fertig, solange Tests/Gates nicht geprüft wurden.
```

# 25. Was der Nutzer selbst bereitstellen/entscheiden muss

Claude kann sehr viel Code und Tooling erzeugen, aber folgende Punkte brauchen reale Nutzer-/Teamentscheidungen oder Assets:
- endgültiger Spielname und Markenprüfung;
- finale Art Direction-Abnahmen;
- Auswahl/Erwerb lizenzierter Drittassets;
- Sprecher/Casting und Nutzungsrechte;
- Musik/Soundrechte;
- Storekonten und Veröffentlichungsverträge;
- Alterskennzeichnung/Storeformulare;
- tatsächliche Hardwaretests auf mehreren Geräten;
- finale Balancing-Abnahme;
- Priorität, falls Scope/Budget reduziert werden muss.

# 26. Die ersten konkreten Schritte für den Nutzer

1. Git, Git LFS, VS Code, Claude Code, Unity Hub/Unity, Blender und Krita installieren.
2. Leeres Git-Repository anlegen.
3. Unity-Projekt mit festgelegter Unity-Version erzeugen.
4. `.gitignore` und `.gitattributes` **vor** dem ersten großen Assetimport einrichten.
5. Mega-Prompt, Mission Bible und diesen Guide unter `Docs/` legen.
6. Claude zunächst **nur Phase 0** ausführen lassen.
7. Ergebnis prüfen.
8. Erst dann Phase 1 starten.
9. Nach jedem Paket Tests/Gates verlangen.
10. Kein Final Art produzieren, bevor Maßstab, Grid und Kernbauformen stabil sind.

# 27. Definition of Done für Claude-Arbeit

Claude darf „fertig“ nur schreiben, wenn:
- der vereinbarte Scope implementiert ist;
- Code kompiliert;
- neue/angepasste Tests bestehen;
- relevante Gates geprüft sind;
- XML-/Datenvalidierung sauber ist;
- keine bekannten kritischen Save-/Hardlock-Probleme bestehen;
- Dokumentation aktualisiert ist;
- keine verbindliche Spezifikation stillschweigend geändert wurde.


# 28. Konkrete Software-Baseline (Stand August 2026)

Diese Versionsangaben sind **Installationsarbeitsbasis**, keine dauerhaft fest verdrahteten Gameplayregeln. Vor dem tatsächlichen Produktionsstart wird einmal geprüft, ob neuere stabile/LTS-Versionen sinnvoll sind; danach wird die gewählte Toolchain für einen Produktionsabschnitt eingefroren.

## 28.1 Unity

**VERBINDLICHER AKTUELLER PROJEKTSTART:** Unity **6.5** ist die gewählte Editor-/Produktionsbasis. Unity 6.5 ist ein Supported Release der Unity-6-Linie. Die konkrete Patchversion wird beim Anlegen des Repositorys in `ProjectVersion.txt` und `Docs/TOOLCHAIN_LOCK.md` festgehalten. Innerhalb eines laufenden Entwicklungspakets wird nicht automatisch auf eine andere Unity-Version gewechselt. Spätere Upgrades erfolgen nur nach eigenem Upgrade-Branch, Backup, vollständigem Testlauf und erfolgreicher Save-/Build-/Performance-Prüfung.

**Wichtig unter Linux:** Die Unity-6.5-Systemanforderungen nennen für den Editor offiziell Ubuntu 22.04 und Ubuntu 24.04. Fedora ist damit **keine offiziell zugesicherte Editorplattform**. Das bedeutet nicht automatisch, dass Unity auf Fedora nicht funktioniert; es bedeutet, dass ein Fedora-spezifischer Fehler nicht durch Gamecode „weggepatcht“ werden darf.

Für dieses Projekt gilt deshalb:
- Entwicklung auf Fedora ist erlaubt, solange Editor/Hub stabil laufen;
- CI/Referenzbuilds sollen mindestens auf einer offiziell unterstützten Umgebung reproduzierbar sein;
- wenn ein Unity-Editorproblem eindeutig distributionsspezifisch ist, wird eine Ubuntu-24.04-Referenzumgebung verwendet, bevor Architekturcode verändert wird;
- Projektpfade, Dateinamen und Tools werden case-sensitive/Linux-tauglich gehalten.

## 28.2 VS Code

VS Code besitzt offizielle RPM-Unterstützung für Fedora/RHEL-basierte Systeme. Für dieses Projekt ist VS Code der Standardeditor neben Unity.

Empfohlene Editorfunktionen:
- C#;
- Unity-Unterstützung;
- XML;
- Markdown;
- Git-Diffs;
- Claude Code;
- EditorConfig-Unterstützung;
- optional Spellchecker für Dokumentation/Localization.

## 28.3 Claude Code

Anthropic bietet Claude Code im Terminal und als native VS-Code-Integration an. Checkpoints/Rewind sind nützlich, ersetzen aber **niemals** Git. Claude soll größere Aufgaben bevorzugt in einem sauberen Branch durchführen und Änderungen anhand echter Diffs prüfen.

Für dieses Projekt wird Claude nicht nur als Autocomplete benutzt, sondern als **Engineering-Agent unter festen Grenzen**:
- Spezifikation lesen;
- Plan machen;
- Implementieren;
- Tests ergänzen;
- Tools/Tests ausführen;
- dokumentieren;
- keine versteckten Produktentscheidungen treffen.

## 28.4 Git + Git LFS

Git verwaltet Quellcode, XML, UXML, USS, Markdown und kleine textbasierte Daten. Git LFS verwaltet große Binärdateien. GitHub dokumentiert LFS ausdrücklich für große Grafik-, Audio-, Video- und andere Binärdateien über Pointerdateien.

Mindestens LFS-kandidaten:
```text
*.blend
*.psd
*.kra
*.tga
*.exr
*.wav
*.flac
*.mp4
*.mov
*.fbx   # nur falls große Binärexporte tatsächlich im Repo liegen
```

Nicht automatisch jede PNG in LFS legen. Kleine UI-Icons können normales Git bleiben. Die LFS-Liste wird nach realer Dateigröße und Repositoryverhalten angepasst.

## 28.5 Blender

Blender ist das primäre 3D-DCC. Die aktuelle Blender-Dokumentation bietet Linux-Binaries und weist darauf hin, dass Distributionspakete gegenüber der offiziellen Version älter oder anders gebaut sein können. Für reproduzierbare Art-Pipelines bevorzugen wir eine bewusst festgelegte Blender-Version und dokumentierte Exportpresets.

Blender-Aufgaben:
- Gebäude;
- Schiffe;
- Fahrzeuge;
- Props;
- Terrain-/Felsmodule;
- einfache Skelette/Animationen;
- LODs;
- Collision-Proxies;
- UVs;
- Asset-Maßstab;
- Render-/Turntable-Preview für Reviews.

## 28.6 Krita

Krita ist das freie Standardwerkzeug für:
- Concept Art;
- Paintovers;
- UI-Mockups;
- 2D-Illustrationen;
- Textur-/Maskenarbeit;
- Karten- und Symbolentwürfe.

Aktuelle offizielle Linux-Builds sind als AppImage verfügbar. Die Projekt-Source-Dateien (`.kra`) werden versioniert/LFS-verwaltet, exportierte Runtime-Assets getrennt abgelegt.

## 28.7 Audio

**Audacity** genügt für schnellen Sprachschnitt, Cleaning, einfache SFX-Bearbeitung und Temp-VO. Die offizielle Linux-Seite bietet AppImages und weist auf schnelle lokale Datenträger für zuverlässige Audiobearbeitung hin.

**REAPER** ist optional, wenn Musik, umfangreiches Voice-Editing, Batchrendering und komplexes Routing wichtig werden. Es besitzt offizielle Linux-Builds und eine Evaluierungsphase. Ein kostenpflichtiger DAW-Kauf ist keine Voraussetzung für den ersten Prototyp.

# 29. Fedora-Installationscheckliste

Die exakten Installationsbefehle können sich durch Repositorynamen ändern. Claude soll vor Ausführung systemverändernder Befehle den aktuellen Fedora-Zustand prüfen. Sichere Basis:

```bash
sudo dnf install git git-lfs

git --version
git lfs version
git lfs install
```

VS Code wird bevorzugt aus dem offiziellen Microsoft-RPM/Repository installiert. Blender/Krita können über offizielle Downloads oder eine bewusst gewählte Paketquelle installiert werden. Unity Hub/Editor werden anhand der aktuellen Unity-Downloadanleitung eingerichtet.

Nach Installation prüfen:

```bash
git --version
git lfs version
code --version
blender --version   # falls im PATH
```

Zusätzlich manuell:
- Unity Hub startet;
- Unity-Lizenz/Account funktioniert;
- Unity 6.5 kann ein leeres Projekt öffnen;
- ein leeres Linux-Standalone-Build lässt sich erstellen;
- VS Code öffnet die Unity-C#-Solution;
- Claude Code kann im Testrepository Dateien lesen und Diffs anzeigen.

# 30. Erstes Repository — exakte Reihenfolge

## Schritt 1 — Ordner und Git

```bash
mkdir -p ~/Projekte/Varynth
cd ~/Projekte/Varynth
git init
```

Noch keine großen Assets kopieren.

## Schritt 2 — `.gitignore`

Unity-spezifische generierte Verzeichnisse ausschließen. Claude soll eine aktuelle Unity-`.gitignore` erzeugen und mindestens kontrollieren, dass `Library/`, `Temp/`, `Logs/`, `obj/`, lokale IDE-Caches und Buildausgaben nicht versehentlich committed werden.

## Schritt 3 — Git LFS

Vor dem ersten großen Artimport `.gitattributes` einrichten. Danach Test:

```bash
git lfs track
git status
```

## Schritt 4 — Unity-Projekt

Im Unity Hub ein neues Unity-6.5-3D-Projekt anlegen. Der endgültige Projekttemplate-Typ wird durch die festgelegte Renderpipeline bestimmt. Für den aktuellen Prompt gilt URP als Arbeitsbasis; Claude prüft nach Projekterzeugung die tatsächlich installierte Pipeline und verändert sie nicht nebenbei.

## Schritt 5 — Dokumentation

Unter `Docs/` sofort kopieren:
- Mega-Prompt;
- Mission & Quest Bible;
- Claude Implementation Guide.

Danach soll Claude aus diesen Quellen `ARCHITECTURE.md`, `DATA_SCHEMA.md`, `QA_GATES.md` und `DECISIONS.md` ableiten.

## Schritt 6 — erster Commit

Der erste Commit enthält nur:
- sauberes Unity-Projekt;
- Gitignore/LFS-Konfiguration;
- Projektdokumentation;
- noch keinen halb fertigen Gameplay-Prototyp.

# 31. Wie Claude den Mega-Prompt lesen soll

Claude muss nicht bei jeder kleinen Aufgabe 200.000+ Wörter vollständig neu in den aktiven Kontext laden, wenn die benötigten Regeln bereits als strukturierte Projektdokumente extrahiert wurden. Stattdessen entsteht eine **Dokumentationshierarchie**:

1. `MEGA_PROMPT_v1.0.md` — ultimative fachliche Quelle;
2. `MISSION_AND_QUEST_BIBLE_v1.0.md` — autoritative Missiondetails;
3. `DECISIONS.md` — kompakte, aktuelle Overrides;
4. `ARCHITECTURE.md` — technische Architektur;
5. `DATA_SCHEMA.md` — Datenverträge;
6. `QA_GATES.md` — prüfbare Gates;
7. systemspezifische Docs, z. B. `POPULATION.md`, `PRODUCTION.md`, `QUESTS.md`.

Vor jeder Arbeit liest Claude mindestens die für den Scope relevanten Abschnitte. Ein automatisierter Doc-Index darf Kapitel/IDs referenzieren, aber niemals Werte erfinden.

# 32. Paketgrößen für Claude

Gute Pakete:
- „Implementiere generisches Wohngebäude-Datenmodell + Tests“;
- „Implementiere NeedCoverageObjective im Quest-System“;
- „Implementiere Bauernhaus T1–T6 aus XML“;
- „Implementiere Handelsroute mit Mindestreserve und Zielbestand“;
- „Implementiere ST-OCC-001 bis ST-OCC-005 auf vorhandener Questengine“.

Zu große Pakete:
- „Baue Occidentia komplett“;
- „Programmiere alle 30 Ultima-Stufen“;
- „Mach das ganze UI“;
- „Implementiere die komplette Story“.

Wenn ein Paket mehrere unabhängige Kernsysteme gleichzeitig neu erfordert, muss Claude es vor Coding in kleinere Schritte zerlegen.

# 33. Pflichtformat für jeden Claude-Arbeitsbericht

Nach einem Paket muss Claude liefern:

```text
PAKET: <Name>
STATUS: fertig / teilweise / blockiert

GEÄNDERT
- Datei A: ..
- Datei B: ..

DATEN/SCHEMAS
- ..

TESTS AUSGEFÜHRT
- Testname: PASS/FAIL
- Build: PASS/FAIL
- Validator: PASS/FAIL

QA-GATES
- Gate X: PASS
- Gate Y: PASS

BEKANNTE PROBLEME
- nur reale bekannte Probleme

SPEZIFIKATIONSABWEICHUNGEN
- keine
oder
- exakte, vom Nutzer genehmigte Abweichung

NÄCHSTER SINNVOLLER SCHRITT
- ..
```

Keine Abschlussmeldung „sieht gut aus“ ohne Tests.

# 34. Technische Kernarchitektur — empfohlene Schichten

## 34.1 Definition Layer
Nur Daten: XML -> validierte immutable Definitionen.

## 34.2 Simulation Layer
Reine oder weitgehend Unity-unabhängige C#-Logik für Bevölkerung, Produktion, Waren, Forschung und ökonomische Zustände. Je weniger MonoBehaviour-Abhängigkeit hier nötig ist, desto besser testbar.

## 34.3 World Layer
Unity-Szenen, Karten, Inseln, Gebäudeinstanzen, visuelle Repräsentation, Spatial Queries.

## 34.4 Presentation Layer
UI Toolkit, Audio, Animation, VFX, Kamera.

## 34.5 Narrative Layer
Questgraph, Dialoge, Storyflags, Cutscene-Cues; greift über definierte Interfaces auf Simulation/World zu und schreibt keine Sonderwirtschaft neben dem Kernsystem.

## 34.6 Persistence Layer
Versionierte Saves, Profil-Unlocks, Mod-Metadaten, Migration.

Diese Trennung ist wichtig, damit 50.000 sichtbare/gespeicherte Strukturen nicht bedeuten, dass 50.000 schwere MonoBehaviour-Skripte jeden Frame komplexe Wirtschaftslogik ausführen.

# 35. XML-Modding konkret

Claude soll früh ein Schema definieren und Beispielmods mitführen.

Ordnerbeispiel:

```text
Mods/
└── ExampleMoreFarmers/
    ├── manifest.xml
    ├── patches/
    │   └── residences.xml
    └── localization/
        ├── de.xml
        └── en.xml
```

Manifestfelder:
- stabile Mod-ID;
- Version;
- Game-Version-Range;
- Dependencies;
- LoadAfter/LoadBefore nur wenn nötig;
- Autor/Name;
- optionale Homepage/License.

Validatoren prüfen:
- doppelte IDs;
- fehlende Referenzen;
- zyklische Upgradepfade, wenn verboten;
- unmögliche Produktionsrezepte;
- negative Kapazitäten;
- fehlende Localization Keys;
- Questflags ohne Definition;
- ungültige Regionsreferenzen.

# 36. Savegame-Strategie konkret

Kein `BinaryFormatter`, keine fragile direkte Serialisierung kompletter Unity-Objektgraphen.

Save enthält IDs + Zustände, z. B.:
- Weltseed;
- Regionszustände;
- Gebäudeinstanz-ID + Definition-ID + Position + Upgradezustand;
- Lager;
- Bevölkerung;
- aktive Routen;
- Forschungszustand;
- Quest-/Storyflags;
- Fraktionsbeziehungen;
- Modliste + Versionen;
- Profilreferenzen getrennt vom Spielstand.

Jede Saveversion erhält Migrationstests. Vor Migration wird Backup erzeugt.

# 37. Quest-Implementierung konkret

Die Mission Bible darf nicht zu 174 individuell hardcodierten C#-Klassen führen.

Zielstruktur:

```text
QuestDefinition
  -> Prerequisites
  -> ObjectiveGraph
  -> DialogueRefs
  -> ChoiceNodes
  -> OnCompleteActions
  -> OnFailSafeActions
  -> CameraCues
  -> Localization
```

Objective-Arten arbeiten gegen Interfaces wie:
- `IBuildingQuery`;
- `IPopulationQuery`;
- `IProductionQuery`;
- `ITradeQuery`;
- `IResearchQuery`;
- `IExpeditionQuery`;
- `IDiplomacyQuery`.

Damit kann ein XML-Questziel „Erreiche 500 reguläre Bauern“ denselben PopulationQuery benutzen wie UI/Statistik und besitzt keine eigene Zählung.

# 38. Dialog- und Voice-Pipeline

Drehbuchtext wird nicht direkt in Quest-XML als langer Freitext dupliziert.

Empfehlung:
```text
Quest XML -> dialogue ID -> Localization Database -> Text/Subtitle
                                   |
                                   -> Voice asset reference
```

Für jede Zeile:
- stabile Line-ID;
- Sprecher-ID;
- deutscher Mastertext;
- englische Übersetzung;
- optional Voice-Datei;
- Timing/Subtitle-Hints;
- Kontextkommentar für Übersetzer/Sprecher.

Recording Sheets werden aus Daten exportiert, nicht manuell separat gepflegt.

# 39. Art-Produktionsregeln für Claude

Claude darf Placeholder-Prefabs selbst erzeugen/konfigurieren, aber finale 3D-Art soll nicht aus zufälligen Primitive-Kombinationen als „fertig“ erklärt werden.

Jede Gebäudeart erhält:
- Gameplay-Footprint;
- physische Maße;
- maximale visuelle Höhe;
- Ein-/Ausgangs-/Straßenanker;
- LOD-Budget;
- Collider-Regel;
- Materialslots;
- Variantenslots;
- Nachtbeleuchtungspunkte;
- VFX-Anker;
- Upgrade-Bezug.

Damit kann Blender-Art später Prefabs ersetzen, ohne Gameplay neu zu programmieren.

# 40. Asset-Namenskonvention

Beispiel:

```text
BLD_OCC_Farmer_T01_V01
BLD_OCC_Farmer_T06_V03
VEH_OCC_Cart_T01
SHP_AUS_Icebreaker_Heavy
BLD_ULT_AUR_AetherRelay_Q04
CHR_ULT_Caelari_Iriath
UI_Icon_Good_Coffee
SFX_BLD_Sawmill_Loop_01
MUS_ULT_Aurelia_Explore_A
```

Dateinamen ASCII-freundlich, keine zufälligen `final_final2`-Namen.

# 41. Performance-Budgets bereits beim Blockout

Claude/Team definiert pro Assetklasse Budgets, bevor Final Art massenhaft produziert wird:
- Triangle-/Vertex-Budget nach LOD;
- Materialanzahl;
- Texturauflösung;
- Animator-Kosten;
- VFX-Partikelbudget;
- sichtbare Crowd-/Fahrzeugrepräsentation;
- Updatefrequenz der Simulation.

Ein Bauernhaus, das tausendfach vorkommt, benötigt strengere Budgets als ein einmaliger World Nexus.

# 42. Automatisierte Tests, die früh existieren müssen

Mindestens:
- alle XML-Dateien laden;
- IDs eindeutig;
- alle Referenzen gültig;
- jede Produktionsware besitzt erreichbare Quelle oder absichtliche Importdefinition;
- jede Pflichtversorgung kann in ihrer Region hergestellt/erreicht werden;
- jeder gesellschaftliche Aufstieg besitzt gültigen Vorgänger/Nachfolger;
- keine Zusatzversorgung gibt im Normalspiel Einwohner;
- Sandboxregel schaltet Bonusmechanik reproduzierbar um;
- jede Hauptmission hat erreichbare Objectives;
- jede Mission speichert/lädt;
- Storyflags führen zu keinem zyklischen Hardlock;
- Aufbau-Spielmodus enthält keine Hauptstoryquests;
- Storyabschluss setzt Profilunlock;
- Mods können Stufe > 10 und zusätzliche Hausformen definieren.

# 43. Manuelle Testsavegames

Claude soll Development-Saves/Fixtures unterstützen:
- `Test_EarlyOccidentia`;
- `Test_WorkerUnlock`;
- `Test_AllKnownWorldRegions`;
- `Test_AustralisPreWall`;
- `Test_UltimaAurelia`;
- `Test_AllUltimaRegions`;
- `Test_WorldNexusPhase8`;
- `Test_BuildModeUltimaStart`;
- `Stress_20kStructures`;
- `Stress_50kStructures`.

Diese Saves sind Testfixtures, nicht normale User-Saves, und werden bei Schemaänderungen kontrolliert migriert oder neu generiert.

# 44. Roadmap für einen Solo-/AI-unterstützten Start

Das vollständige Spiel ist sehr groß. Für einen einzelnen Nutzer mit Claude ist die realistische Strategie ein **Vertical Slice**, der beweist, dass Architektur und Workflow funktionieren.

## Meilenstein A — technische Demo
- Insel/Grid;
- Straße;
- Bauernhaus;
- Markt;
- Haferkette;
- Bewohner;
- Save/Load;
- einfache UI.

## Meilenstein B — Occidentia Slice
- Bauern T1–T6;
- Arbeiter T1;
- erste horizontale/vertikale Upgrades;
- Handelsschiff;
- erste fünf Storymissionen;
- XML-Mod fügt Ware hinzu.

## Meilenstein C — Region Slice
- zweite Insel/Region;
- Expedition;
- Handelsroute;
- Wettersystem;
- Meridia-Minipaket.

## Meilenstein D — öffentliche Demoqualität
- eine spielbare längere Occidentia-Kette;
- gute UI;
- erste Final-Art-Gebäude;
- Audio;
- Performanceprofil;
- Tutorial/Story sauber.

Erst dann Umfang massiv hochskalieren.

# 45. Was Claude NICHT tun darf

- nicht alle Systeme in `GameManager.cs` packen;
- keine Zivilisationsstufen als festes 0–9-Enum-Maximum modellieren;
- keine Haus-Endstufe hardcoden;
- keine 174 Storymissionen als 174 individuelle MonoBehaviour-Skripte anlegen;
- keine Produktionswerte erfinden, wenn Spezifikation sie vorgibt;
- keine optionalen Waren wieder zu normalen Bonusbewohnern machen;
- keine Stausimulation einschleusen;
- keine „temporäre“ Save-Struktur veröffentlichen, die später ohne Migration gebrochen wird;
- keine fremden Anno-Assets/UI/Logos kopieren;
- keine riesigen Fremdplugins ohne Lizenz-/Abhängigkeitsprüfung hinzufügen;
- keine Sicherheitsabfragen pauschal umgehen;
- keine Tests überspringen, nur um ein Paket „fertig“ nennen zu können.

# 46. Empfohlene Claude-Aufteilung nach Spezialrollen

Bei großen Paketen kann Claude mit klaren Subagent-/Rollenaufträgen arbeiten:
- **Architecture:** Interfaces, Abhängigkeiten, Datenfluss;
- **Gameplay:** Simulation;
- **Data:** XML/Schemas/Validatoren;
- **UI:** UI Toolkit;
- **Narrative:** Questdaten/Dialogintegration;
- **QA:** Tests, Regression, Hardlockanalyse;
- **Performance:** Profiler/Benchmarks.

Die Rollen dürfen nicht parallel dieselben Dateien unkoordiniert verändern. Der Hauptagent integriert und prüft.

# 47. Release-/Rechtsworkflow

Vor externem Assetimport:
- Lizenz speichern;
- Quelle dokumentieren;
- kommerzielle Nutzung prüfen;
- Änderungs-/Attributionspflicht prüfen.

Vor Marketing/Release:
- Name/Logo/Key Art prüfen;
- typische UI-Screens und Gebäudedesigns auf Eigenständigkeit kontrollieren;
- `THIRD_PARTY_LICENSES` und `ASSET_LICENSES` vollständig;
- Datenschutz/Telemetrie nur falls tatsächlich genutzt;
- Offline-Einzelspieler ohne Pflichtkonto testen.

# 48. Offizielle Referenzen für die Toolchain (Stand der Guide-Erstellung)

Diese URLs dienen nur der Tool-/Installationsverifikation und sind keine Gameplayquellen:

- Unity 6 Releases: https://unity.com/releases/unity-6
- Unity 6 Support/LTS: https://unity.com/releases/unity-6/support
- Unity 6.5 System Requirements: https://docs.unity3d.com/6000.5/Documentation/Manual/system-requirements.html
- Unity UI Toolkit: https://docs.unity3d.com/6000.6/Documentation/Manual/UIElements.html
- Unity Input: https://docs.unity3d.com/6000.6/Documentation/Manual/Input.html
- Unity Addressables: https://docs.unity3d.com/6000.3/Documentation/Manual/com.unity.addressables.html
- VS Code Linux: https://code.visualstudio.com/docs/setup/linux
- Anthropic Claude Code product information: https://www.anthropic.com/news/enabling-claude-code-to-work-more-autonomously
- Git LFS: https://git-lfs.com/
- GitHub Git LFS docs: https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-git-large-file-storage
- Blender Linux Manual: https://docs.blender.org/manual/de/latest/getting_started/installing/linux.html
- Krita Download: https://krita.org/en/download/
- Audacity Linux: https://www.audacityteam.org/download/linux/
- REAPER Download: https://www.reaper.fm/download.php

# 49. Der erste Prompt an Claude nach Installation

```text
Wir beginnen noch NICHT mit Gameplaycode.

Lies:
- Docs/MEGA_PROMPT_v1.0.md
- Docs/MISSION_AND_QUEST_BIBLE_v1.0.md
- Docs/CLAUDE_IMPLEMENTATION_GUIDE_v1.0.md

Arbeite ausschließlich an PHASE 0 des Implementation Guides.

Ziele:
1. Repositoryzustand prüfen.
2. Projektdokumente indexieren.
3. Verbindliche Projektentscheidungen in Docs/DECISIONS.md konsolidieren.
4. Echte Widersprüche/alte Overrides in Docs/SPEC_AUDIT.md dokumentieren.
5. Docs/ARCHITECTURE.md, DATA_SCHEMA.md und QA_GATES.md als erste technische Arbeitsfassungen anlegen.
6. Noch keine Gameplayfeatures implementieren.
7. Prüfen, ob Unity-Projekt, Git und LFS sauber eingerichtet sind.
8. Tests/Gates für Phase 0 ausführen bzw. sinnvolle Validierungsskripte dafür anlegen.

Keine fachlichen Werte ändern. Keine neuen Features erfinden.
Am Ende: geänderte Dateien, Prüfungen, echte Probleme und nächster Schritt.
```

# 50. Zweiter Prompt erst nach erfolgreicher Phase 0

```text
Lies die aktuellen Projektdokumente und den Bericht von Phase 0.
Arbeite ausschließlich an PHASE 1: Technisches Fundament.

Implementiere:
- stabile IDs;
- XML-Lader + Schema-/Referenzvalidierung;
- versioniertes Save-Grundformat;
- SimulationClock;
- minimale Service-/Systemgrenzen;
- Testutilities.

Noch KEIN Wohn-, Produktions-, Quest- oder Regionscontent.

Pflicht:
- Unit/EditMode-Tests;
- ungültige XML-Testfälle;
- Save/Load-Roundtrip;
- Tests/Gates ausführen und alle Fehler vor Abschluss beheben.
```

# 51. Langfristige Regel für neue Ideen während der Entwicklung

Wenn der Nutzer später eine neue Idee nennt, geht Claude so vor:
1. Idee klassifizieren: neue verbindliche Regel, Ergänzung, Override oder nur Option;
2. betroffene Mega-Prompt-Kapitel identifizieren;
3. Widersprüche nennen;
4. erst Dokumentation aktualisieren;
5. dann technische Auswirkung planen;
6. Datenmigration/Save/Mods prüfen;
7. erst danach implementieren.

So verhindert das Projekt, dass spontane gute Ideen die Architektur unkontrolliert zerreißen.


# 52. Speicher-/Buildgrößen-Gates für Claude

Claude behandelt Dateigröße und Laufzeitspeicher als kontinuierliche Engineering-Anforderung. Nach jedem größeren Art-/Content-Paket werden Buildgröße und Duplikate geprüft.

Pflichtberichte nennen mindestens:
- neue/entfernte Assets und Größen;
- größte neue Dateien;
- doppelte Hashes beziehungsweise bewusste Duplikate;
- Änderung der Gesamt-Buildgröße;
- Änderung typischer Savegamegröße, falls Save-Daten betroffen sind;
- Addressables-/Streaming-Gruppen, falls verändert.

Claude darf keine Rohdateien, Testvideos, unkomprimierte Audioquellen oder doppelte Exportvarianten in einen Release-Assetpfad legen.

# 53. Neue Pflichtpakete nach dem Perfektionspass

Vor großflächigem Regionscontent werden zusätzlich als eigene Engineering-Pakete umgesetzt und getestet:

1. `Warehouse Core + Small Warehouse + Ramp Upgrades`;
2. `Neutral Trader Core`;
3. `AI Competitor Core`;
4. `Diplomacy Core`;
5. `Pirate Core`;
6. `Specialists/Modules`;
7. `City Prestige/Culture/Visitors`;
8. `Monuments/Exhibitions`;
9. `World Trade Centre`;
10. `Runtime/Install Size Audit Pipeline`.

Keines dieser Pakete darf gleichzeitig mit unverbundenen Story- oder Regionsfeatures vermischt werden.


## Story-/Quest-Implementierung — Voice-Lock Pass 07

Für alle festen Story- und Nebenmissionen gilt die **MISSION_AND_QUEST_BIBLE_v1.0.md in Voice-Lock Pass 07** als autoritative Dialog-/Inszenierungsfassung. Claude darf ältere Pass-05/06-Template-Dialoge nicht wiederherstellen.

Beim Implementieren von Dialogen gilt zwingend:

- Figuren sprechen niemals Missions-ID oder Missionstitel als Meta-Begriff aus.
- Questtracker-Ziele werden nicht wörtlich als Dialog vorgelesen.
- Kein 25/50/75-%-Dialogtrigger als Standardmuster.
- Figurenstimmen aus `Voice-Lock v1.0` müssen erhalten bleiben.
- Technische Standardregeln (Save/Load, bereits erfüllte Ziele, Wiederaufnahme) bleiben datengetrieben und dürfen zentral wiederverwendet werden.
- Gesprochener Text wird über Lokalisierungsschlüssel referenziert und bleibt von Questlogik getrennt.
- Vor Merge eines Missionspakets muss `STORY_DIALOGUE_LINT_PASS07.md` bzw. dessen automatisierte Entsprechung ohne harte Treffer bestehen.
- Claude darf Dialog nicht „vereinheitlichen“, nur weil mehrere Missionen dieselbe Mechanik benutzen.



# 54. Pflichtpakete aus Content-Lock Pass 08

Vor Content-Implementierung sind zusätzlich folgende generische Systeme als getrennte Claude-Pakete umzusetzen:

1. `ProfileAchievementCore`: interne stabile Achievement-IDs, Progresscounter, Hidden/Spoiler-Metadaten, Offline-Queue, Plattformadapter; keine direkte Steam-Logik im Gameplay.
2. `StoryKnowledgeState`: Storyflags/Wissenszustände, damit Figuren nur Informationen sprechen, die sie zu diesem Zeitpunkt kennen dürfen.
3. `CinematicAnchorFramework`: In-Engine-Sequenzen, Checkpoints, Skip/Replay, Untertitel, Accessibility-Camera, Rückkehr ins Gameplay.
4. `ScenarioDefinition`: datengetriebene Szenarioregeln/Startzustände/Siegbedingungen statt Szenario-Hardcode.

**Tests/Gates zwingend:** Achievement-Doppeltrigger, Profilpersistenz, Spoiler-Hidden-Test, Cheat-Eligibility, Cinematic-Save/Load, Skip/Resume, Scenario-Validation, StoryKnowledge-State-Tests.

Claude darf Lore- oder Dialogwahrheiten nicht aus technischen Implementierungsdetails erfinden. Mega-Prompt Pass 08 + Mission Bible sind dafür autoritativ.

---


# 54. Content-Lock Pass 09 — neue autoritative Produktionsdokumente

Vor Implementierung von Content-Systemen muss Claude zusätzlich die relevanten Pass-09-Dokumente laden:

- `CONTENT_CATALOG_v1.0.md`
- `RESEARCH_TREE_v1.0.md`
- `BALANCING_BIBLE_v1.0.md`
- `ART_BIBLE_v1.0.md`
- `AUDIO_BIBLE_v1.0.md`
- `FINAL_AUDIT_PASS09.md`

## 54.1 Paketzuordnung

### Daten/Content
Laden:
- Master
- Content Catalog
- Data Schema
- betreffende Region
- QA

### Forschung
Laden:
- Master
- Research Tree
- betreffende Region
- Balancing Bible
- QA

### Economy/Balance
Laden:
- Master
- Balancing Bible
- betreffende System-/Regionskapitel
- Testsavegames

### Art
Laden:
- Master
- Art Bible
- betreffende Region
- Performance
- IP/Licensing

### Audio/Voice
Laden:
- Master
- Audio Bible
- Mission Bible Pass 07+
- Character Bible
- QA

## 54.2 Wichtige Verbote

Claude darf nicht:

- einen zweiten parallelen Waren-/Gebäude-ID-Namespace erfinden;
- Forschung als beliebige Punktwährung vereinfachen;
- optionale Waren wieder zu normalen Bonusbewohnern machen;
- beim kleinen Lagerhaus die Lagerkapazität reduzieren, um es zu balancen;
- Stausimulation als Pflichtsystem einführen;
- Artassets anderer Spiele nachbauen;
- Source-WAV/PSD/BLEND-Dateien ungefiltert in Releasebuilds packen.

# 55. Reihenfolge nach Design-Lock

1. Phase 0: Repository/Unity 6.5/Docs einfrieren.
2. Daten-ID-/Schema-Grundlage.
3. Content Registry importer/validator.
4. Grid/Bau/Save-Basis.
5. Wohn-/Bevölkerungssystem.
6. Waren/Produktion/Lager.
7. Logistik/Handel/Schiffe.
8. Netze/Dienste.
9. Forschung.
10. KI/Händler/Piraten/Diplomatie.
11. Questengine.
12. Story Vertical Slice.
13. Regionen.
14. Ultima.
15. Art-/Audio-Produktion.
16. Performance/QA/Release.

Jedes Paket endet mit Tests/Gates und einem rückkehrbaren Git-Stand.

---


# 56. PASS 10 — Projekt-Root und Produktionsreife

Vor Phase 0 müssen im Repository-Root vorhanden sein:
- `CLAUDE.md`
- `.gitignore`
- `.gitattributes`
- `.editorconfig`
- `.claude/rules/`

Die autoritativen Dokumente liegen ausschließlich in den im `PROJECT_MANIFEST.md` genannten Pfaden.
Alte Backup-Prompts gehören nicht in das aktive Repository.

Zusätzliche Release-/Produktions-Gates:
- Brand/Name bleibt Codename bis rechtlicher Lock.
- Build-/Save-Größenbudgets werden ab dem ersten Vertical Slice gemessen.
- SourceAssets und Runtime-Assets bleiben strikt getrennt.
- Lokalisierungsschlüssel werden ab dem ersten sichtbaren UI-Text verwendet.
- Finale Voice-Produktion erst nach Voice-Lock.
- Performance-Regressionen werden mit festen Testsavegames gemessen.


---


# 57. Blender-Asset-Pipeline — verbindliche Claude-Verantwortung

Claude soll die Blender-Modelle von Varynth im Projektverlauf **selbst erzeugen und pflegen**.

## 57.1 Wann 3D-Produktion beginnt
Nicht in Phase 0. Zuerst Daten-/Bau-/Save-/Prefab-Grundlagen. Sobald ein Implementierungspaket ein echtes visuelles Asset benötigt, wird ein zugehöriges Blender-Unterpaket eingeplant.

## 57.2 Vorgehen
1. Content-ID und Art-Bible lesen.
2. Asset-Brief erzeugen.
3. Blender-Python-Generator/Script erstellen.
4. `blender --background --python ...` ausführen.
5. `.blend` Source speichern.
6. FBX/glTF oder vereinbartes Runtimeformat exportieren.
7. Unity-Import/Prefab konfigurieren.
8. LOD/Collider/Material testen.
9. Preview/Turntable rendern.
10. QA + Git/LFS.

## 57.3 Assetklassen
Claude erstellt im Rahmen dieser Pipeline insbesondere:
- Wohnhäuser und ihre Upgradeformen;
- Produktions- und Dienstgebäude;
- Lagerhäuser;
- Häfen;
- Schiffe;
- Land-/Schienen-/Luftfahrzeuge;
- Props und Dekoration;
- modulare Terrain-/Fels-/Küstenobjekte;
- Story-/Ruinen-/Ultima-Strukturen;
- technische Basismodelle für Riesen, Drachen, Leviathane, Caelari und andere Figuren/Fauna.

## 57.4 Qualität
Finale Assets müssen Art Bible, technische Maße und Performancebudgets erfüllen. Organische Hero-Assets dürfen einen späteren manuellen Polish benötigen, aber Claude soll trotzdem eine funktionale, technisch saubere Basis liefern.

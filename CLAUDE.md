# Varynth — Claude Code Projektregeln

## Start
Lies zuerst `Docs/00_START_HERE/README_START_HERE.md`.

## Harte Regeln
- Arbeite paketweise nach `Docs/04_IMPLEMENTATION/CLAUDE_IMPLEMENTATION_GUIDE_v1.0.md`.
- Tests/Gates sind nach jedem Paket Pflicht.
- Keine Gameplayimplementierung vor erfolgreichem Phase-0/Phase-1-Fundament.
- Keine fachlichen Werte erfinden, wenn eine Spezifikation existiert.
- Bei Widerspruch: dokumentieren, nicht raten.
- Neuere konkrete Overrides schlagen ältere generische Zusammenfassungen.
- Keine geschützten Assets/Texte/UI/Modelle anderer Spiele kopieren.
- `Anno 1800` ist nur abstrakte Fallback-Referenz für noch undefinierte Genremechaniken.
- Kleine Lagerhäuser: gleiche Lagerkapazität wie Standard derselben Technologie, weniger/aufrüstbare Rampen.
- Normalspiel-Bewohner = regulär + Lebensqualität; optionale Waren geben keine Extraeinwohner.
- Keine Pflicht-Stau-/Kreuzungssimulation.
- Wohnhausupgrades erfolgen mechanisch sofort bei erfüllten Bedingungen.
- Speichereffizienz ist ein Release-Gate: keine SourceAssets im Build, keine unnötigen Duplikate, referenzbasierte Saves.

## Dokumente auf Bedarf laden
Der 2-MB-Master muss nicht bei jeder kleinen Aufgabe komplett analysiert werden.
Lade zusätzlich nur die Fachdateien des aktuellen Pakets.

## Git
- Keine Secrets committen.
- Keine generierten Unity-Ordner committen.
- Große Binärquellen über Git LFS.
- Nach abgeschlossenem Paket rückkehrbaren Git-Stand erzeugen/vorschlagen.

## Scope
Wenn eine Aufgabe mehr als ein klar abgegrenztes Systempaket gleichzeitig verändert, zuerst in kleinere Pakete aufteilen.


## 3D-Asset-Verantwortung / Blender

Claude ist für die Erstellung der 3D-Assets von **Varynth** verantwortlich, soweit dies technisch sinnvoll automatisierbar ist.

Claude soll Blender aktiv über die vorhandene Installation verwenden und darf nicht nur Platzhalter beschreiben. Für 3D-Arbeit soll Claude bevorzugt:
- Blender-Python-Skripte unter `Tools/Blender/` erzeugen;
- Blender headless/CLI mit `blender --background --python <script.py>` ausführen;
- `.blend`-Quelldateien unter `SourceAssets/Art/Blender/` erzeugen und pflegen;
- Runtime-Meshes/Exporte nach `Assets/Game/Art/` exportieren;
- Gebäude, Wohnhäuser, Produktionsgebäude, Schiffe, Fahrzeuge, Props, Felsen/Terrainmodule, Riesen-/Drachen-/Fauna-Basismodelle und andere spezifizierte 3D-Inhalte erstellen;
- UVs, Materialslots, LODs, Collision-Proxies, Pivots, Maßstab und technische Anker mitliefern;
- Turntable-/Preview-Renderings für Reviews erzeugen;
- Assets anschließend in Unity als Prefabs integrieren und technisch testen.

Claude darf für Final Art keine fremden Modelle oder geschützten Assets kopieren. Die Art Bible und die regionsspezifischen Vorgaben sind verbindlich.

Bei komplexen organischen Figuren, Gesichtern, hochwertigen Character-Rigs oder Hero-Assets darf Claude zunächst eine technisch saubere Produktionsbasis/Blockout/Prozeduralbasis erstellen und im Abschlussbericht klar markieren, wenn ein menschlicher Art-Polish sinnvoll wäre. Das ist kein Grund, das Asset komplett auszulassen.

Kein 3D-Arbeitspaket gilt als fertig, wenn nur ein Primitive-/Placeholder-Cube vorhanden ist, obwohl das Paket ausdrücklich ein echtes Asset verlangt.

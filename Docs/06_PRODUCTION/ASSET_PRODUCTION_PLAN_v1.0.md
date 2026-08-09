# ASSET PRODUCTION PLAN v1.0 — PASS 10B


## 0. Verantwortlichkeit: Claude + Blender

Für Varynth ist Blender nicht nur ein manuelles Hilfsprogramm. **Claude Code soll die 3D-Produktion aktiv durchführen**, indem es Blender über Python/CLI steuert und die resultierenden Source- und Runtime-Assets in die Projektpipeline integriert.

Ziel: Claude erstellt im Verlauf der Entwicklung die tatsächlich benötigten 3D-Modelle selbst, soweit die Qualität und Art der Assets dies technisch zulassen. Der Workflow umfasst Blockout, Modellierung, prozedurale/modulare Varianten, UVs, Materialslots, LODs, Collision-Proxies, Export, Unity-Prefab-Integration und Preview-Renderings.

Empfohlene Verzeichnisstruktur:

```text
SourceAssets/Art/Blender/
├── Shared/
├── Occidentia/
├── Meridia/
├── Orientia/
├── Aferia/
├── Australis/
└── Ultima/
    ├── Shared/
    ├── Aurelia/
    ├── Viridia/
    ├── Titania/
    ├── Ignaria/
    └── Pelagia/

Tools/Blender/
├── generators/
├── exporters/
├── validators/
└── previews/

Assets/Game/Art/
├── Shared/
├── Occidentia/
├── Meridia/
├── Orientia/
├── Aferia/
├── Australis/
└── Ultima/
```

Für wiederkehrende Gebäudefamilien soll Claude bevorzugt modulare/prozedurale Blender-Generatoren bauen, damit Varynth viele optische Varianten erhält, ohne hunderte redundante Vollmodelle zu speichern.

## 1. Trennung Source / Runtime

**SourceAssets/** liegt außerhalb von Unity `Assets/` und enthält bearbeitbare Quellen:
- Blender `.blend`
- Krita `.kra`
- hochauflösende Texturquellen
- Audio-Master
- Recording Sessions
- Konzeptbilder

**Assets/Game/** enthält nur Runtime-/Unity-Importdaten:
- exportierte Meshes
- Runtime-Texturen
- Prefabs
- Animationen
- Runtime-Audio
- UI
- VFX

Keine bearbeitbaren Multi-GB-Quellen werden versehentlich in Releasebuilds gepackt.

## 2. 3D-Pipeline

Concept/Blockout → Blender Source → UV/Materials → LODs → Export → Unity Import → Prefab → QA

Jedes Gameplaygebäude erhält vor Final Art:
- stabile technische ID;
- Footprint;
- Straßen-/Transportanker;
- Ein-/Ausgänge;
- Colliderregeln;
- LOD-Slots;
- VFX-/Nachtlichtanker;
- Materialslots;
- Variantenslots.

## 3. Texturen

- Trim-Sheets und Atlanten, wenn mehrere Assets dieselbe Materialfamilie nutzen.
- Materialinstanzen statt Kopien.
- Mask Maps zusammenfassen, wenn technisch sinnvoll.
- Runtime-Importkompression pro Plattform.
- Source-Texturen dürfen hochauflösend sein; Runtimeauflösung richtet sich nach tatsächlicher Bildschirmgröße.

## 4. Regionen

Addressable-/Streaming-Gruppen mindestens:
- Shared Core
- Occidentia
- Meridia
- Orientia
- Aferia
- Australis
- Ultima Shared
- Aurelia
- Viridia
- Titania
- Ignaria
- Pelagia
- Story/Cinematics
- Voice Packs

## 5. Varianten

Optische Vielfalt bevorzugt durch:
- modulare Fassaden;
- Dachmodule;
- Propsets;
- Materialvarianten;
- deterministische Seed-Varianten.

Nicht durch dutzende vollständige Mesh-/Texturkopien.

## 6. Quellenverwaltung

Große Binärquellen laufen über Git LFS.
Finale Runtime-Assets dürfen ebenfalls LFS nutzen, wenn Größe/Binärformat es sinnvoll macht.

## 7. Asset QA

Jedes Finalasset:
- korrekte ID/Namenskonvention;
- korrekte Skalierung;
- Pivot/Anker;
- Collider;
- LODs;
- Materialanzahl;
- keine fehlenden Texturen;
- keine fremden/unlizenzierten Inhalte;
- Build-/VRAM-Auswirkung dokumentiert.

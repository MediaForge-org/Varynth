# Blender Asset Production Rule

Claude soll für Varynth 3D-Assets selbst mit Blender erzeugen, nicht nur Assetlisten oder Beschreibungen schreiben.

## Standardworkflow
1. Relevante Art-Bible/Region/Content-ID lesen.
2. Technische Maße, Footprint, Pivot, Anker und LOD-Ziele ableiten.
3. Blender-Python-Skript in `Tools/Blender/` anlegen.
4. Blender per CLI/headless ausführen.
5. `.blend` unter `SourceAssets/Art/Blender/<Region>/<Kategorie>/` speichern.
6. Runtime-Export unter `Assets/Game/Art/<Region>/<Kategorie>/` erzeugen.
7. In Unity importieren und Prefab/Material/Collider/LOD konfigurieren.
8. Turntable/Preview für Review erzeugen.
9. Tests/Gates durchführen.
10. Quellen und Exporte committen; große Binärdateien über Git LFS.

## Pflichtinhalte je Asset, soweit anwendbar
- korrekter Maßstab;
- Pivot;
- UVs;
- Materialslots;
- LOD0/LOD1/LOD2, bei großen Assets ggf. LOD3/Impostor;
- Collision Proxy;
- Nachtlicht-/VFX-Anker;
- Transport-/Straßen-/Hafenanker;
- Variantenslots;
- deterministische Benennung über Content-ID.

## Verboten
- fremde Spielmodelle rippen/übernehmen;
- Anno-Modelle nachbauen;
- Final Asset durch primitiven Cube ersetzen;
- `.blend` direkt als einziges Runtimeformat verwenden;
- SourceAssets in Releasebuilds aufnehmen.

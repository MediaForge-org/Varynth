# Unity Asset Rule

- Runtime-Assets unter `Assets/Game/`.
- Bearbeitbare große Quellen unter `SourceAssets/`, nicht unter `Assets/`.
- Keine `Resources/`-Ablage als bequeme Standardlösung für große dynamische Inhalte.
- Addressables/Streaming nach Architekturpaket nutzen.
- Prefabs referenzieren stabile Content-IDs.
- `.meta`-Dateien werden committed.
- Scene/Prefab/Asset-Serialization bleibt textbasiert.

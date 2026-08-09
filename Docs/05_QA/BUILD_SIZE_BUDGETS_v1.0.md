# BUILD & SAVE SIZE BUDGETS v1.0 — PASS 10

## 1. Ziel

Das fertige Spiel soll trotz seines Umfangs speichereffizient bleiben. Qualität wird nicht unnötig geopfert,
aber Duplikate, Rohquellen und ungenutzter Content dürfen nicht im Release landen.

## 2. Installationsgröße — Arbeitsbudget

**Soft Target Basisspiel:** ≤ 40 GiB pro Desktopplattform  
**Review Gate:** > 50 GiB  
**Hard Review:** > 60 GiB

Das ist kein Versprechen an Stores, sondern ein Engineeringbudget.

Richtwerte:
- Runtime Texturen/Materialdaten: ≤ 18 GiB
- Meshes/Animationen: ≤ 7 GiB
- Musik/SFX: ≤ 4 GiB
- ein vollständiges Voice-Pack: ≤ 6 GiB
- UI/VFX/sonstige Shared Assets: ≤ 3 GiB
- Code/Data/Localization/Configs: möglichst < 2 GiB

Zusätzliche Voice-Sprachen sollen als separate Pakete möglich sein.

## 3. Savegames

Ziel:
- normales Midgame: < 25 MiB komprimiert
- großes Late Game: < 75 MiB komprimiert
- Review Gate: > 150 MiB
- Stresssave Hard Review: > 250 MiB

Savegames speichern statische Definitionen nie vollständig mit.

## 4. Autosaves

Standard 5 Rotationen. Alte Autosaves dürfen optional automatisch komprimiert/aufgeräumt werden.

## 5. Build-Size-Gate

CI/Buildbericht zeigt:
- Gesamtgröße;
- Änderung zum letzten Baselinebuild;
- Top-50 größte Dateien;
- doppelte Hashes;
- unreferenzierte/unerwartete Assets;
- Größe je Addressable-Gruppe;
- Voice-Pack-Größe;
- Debug-/Source-Asset-Verstöße.

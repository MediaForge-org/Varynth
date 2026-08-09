# PERFORMANCE VALIDATION PLAN v1.0 — PASS 10

## 1. Zielklassen

- Minimum: 1080p / 30 FPS
- Empfohlen: 1440p / 60 FPS
- High-End: 4K / 60 FPS

Diese Ziele sind Performance-Arbeitsziele und werden nach dem Vertical Slice mit realer Zielhardware
neu kalibriert.

## 2. Simulationsziele

Architektur muss mindestens auf folgende Stressfälle vorbereitet sein:
- 2.000.000+ statistische Bewohner;
- 20.000+ reguläre Strukturen;
- 50.000-Strukturen-Stresstest;
- mehrere gleichzeitig aktive Regionen;
- große Handels-/Schiffsnetze;
- Ultima-Cross-Region-Endgame.

Keine Annahme, dass jeder Bewohner als permanenter GameObject-Agent existiert.

## 3. Messpunkte

Pro Performance-Build erfassen:
- CPU Frame Time;
- GPU Frame Time;
- Main Thread;
- Render Thread;
- GC Allocations;
- Managed Heap;
- Native Memory;
- VRAM;
- Draw Calls/Batches;
- sichtbare Crowd-/Fahrzeugrepräsentation;
- Simulations-Tickdauer;
- Save-/Load-Dauer;
- Regionswechsel;
- Addressables Load/Unload;
- Buildgröße.

## 4. Testsavegames

Mindestens:
1. Early Occidentia
2. große Occidentia-Metropole
3. bekannte Welt Multi-Region
4. Australis Sturm
5. Aurelia Midgame
6. Ultima U4 Cross-Region
7. World Nexus
8. 50k-Building Stress
9. Piraten-/Flottenstress
10. Modded Tier > 10

## 5. Regression Gate

Ein Paket darf nicht gemergt werden, wenn es ohne dokumentierte Begründung:
- typische Frame Time >10 % verschlechtert;
- Peak RAM/VRAM stark erhöht;
- Savegröße unverhältnismäßig erhöht;
- Buildgröße deutlich erhöht;
- neue dauerhafte GC-Spikes einführt.

Ausnahmen benötigen Profilingdaten und Folgeaufgabe.

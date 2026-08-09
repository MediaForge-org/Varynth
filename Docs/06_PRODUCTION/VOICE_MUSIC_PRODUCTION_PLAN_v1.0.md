# VOICE & MUSIC PRODUCTION PLAN v1.0 — PASS 10

## 1. Story Voice

Keine finale Aufnahme vor Voice-Lock.

Pipeline:
1. Mission Bible finaler Line-Edit.
2. Character Voice Profiles locken.
3. Recording Sheet automatisch aus Line-IDs erzeugen.
4. Casting.
5. Testaufnahme.
6. Aufnahme.
7. Schnitt/Cleanup.
8. Integration über Voice-Asset-ID.
9. Untertitel-Timing.
10. In-Game-QA.

Masteraufnahme: 48 kHz / 24-bit WAV.
Diese WAV-Dateien sind SourceAssets und nicht direkt die Releaseform.

## 2. Voice-Packs

Voice wird sprachweise paketiert, sodass zusätzliche Sprachpakete nicht zwangsläufig Teil jedes Downloads
sein müssen.

## 3. Musik

Jede Region folgt der Audio Bible. Produktion erfolgt in Cues/Layern:
- Intro
- Base
- Development
- Tension
- Resolution

Keine Musikdatei wird nur deshalb mehrfach exportiert, weil sie in mehreren Regionen referenziert wird.

## 4. Runtime

- lange Musikdateien streamen;
- Voice passend komprimieren;
- kurze UI-SFX resident nur wenn sinnvoll;
- Distanz-/Cluster-Audio für große Städte;
- simultane Quellen limitieren;
- keine Source-WAVs im Releasebuild.

## 5. Abnahmekriterien

- Dialog verständlich bei Standardmix;
- Skip/Save/Load hinterlässt keine Audiofehler;
- Untertitel vollständig;
- keine doppelten Line-IDs;
- Regionen haben korrekte Ambience;
- Musikzustände flackern nicht;
- Buildgrößenbudget eingehalten.

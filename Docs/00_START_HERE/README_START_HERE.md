# START HERE — Varynth PASS 10B

Dieses Paket wird in den **Root des Unity-Projekts** entpackt.

## Reihenfolge

1. In Unity Hub mit Unity 6.5 ein neues **Universal 3D / URP** Projekt erstellen.
2. Unity nach dem ersten erfolgreichen Öffnen schließen.
3. Dieses Paket in den Projekt-Root entpacken.
4. Git initialisieren und Git LFS aktivieren.
5. Unity erneut öffnen und `UNITY_PROJECT_SETTINGS.md` prüfen.
6. Noch keinen Gameplaycode schreiben.
7. Terminal im Projekt-Root öffnen und `claude` starten.
8. Den Inhalt von `FIRST_PROMPT_TO_CLAUDE.md` senden.
9. Nur Phase 0 durchführen.
10. Erst nach sauberem Phase-0-Abschluss Phase 1 starten.

## Autorität

Bei Konflikten:
1. aktuelle explizite Nutzerentscheidung;
2. neuere verbindliche Overrides;
3. Mega Prompt PASS 10B;
4. spezifische Bible/Fachdatei;
5. Implementation Guide für Arbeitsreihenfolge.

Claude darf Konflikte nicht stillschweigend raten.

## Blender

Blender-Modelle werden später nicht nur manuell erwartet: Claude soll sie gemäß
`.claude/rules/04-blender-asset-production.md` und dem Asset Production Plan selbst
über Blender/Python/CLI erzeugen. Phase 0 prüft nur die Blender-Installation.

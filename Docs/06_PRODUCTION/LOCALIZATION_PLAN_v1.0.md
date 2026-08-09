# LOCALIZATION PLAN v1.0 — PASS 10

## 1. Sprachen

Pflicht-Textbasis für v1.0:

- Deutsch
- Englisch

Weitere Sprachen werden über dieselbe Datenpipeline ergänzt und dürfen keine Codeänderungen benötigen.

## 2. Autorität

- Deutscher Mastertext ist die narrative Ausgangsfassung.
- Jeder sichtbare String besitzt einen stabilen Localization Key.
- Kein Gameplaycode enthält fest verdrahtete sichtbare Sätze.
- Questdaten referenzieren Dialog-/Lokalisierungs-IDs.
- Zahlen, Einheiten, Datum, Dezimaltrennzeichen und Pluralformen werden lokalisiert.

## 3. Key-Schema

Beispiele:

`ui.population.title`
`good.coffee.name`
`building.occ.market.name`
`quest.story.occidentia.001.title`
`dialogue.story.occidentia.001.open.helena.001`
`achievement.world_nexus.title`

## 4. Übersetzungsworkflow

1. String im Master anlegen.
2. Kontextkommentar hinzufügen.
3. Platzhalter/Variablen deklarieren.
4. Export in Übersetzungstabelle.
5. Übersetzung.
6. automatischer Placeholder-Lint.
7. In-Game-Screenshot-/Layoutprüfung.
8. linguistische QA.
9. Content Lock.

## 5. Voice

Text und Voice sind getrennt. Architektur unterstützt unabhängige Voice-Packs.

Produktionsreihenfolge:
- Masterdialoge finalisieren;
- Voice-Lock;
- mindestens eine vollständige Hauptstory-Voice-Sprache produzieren;
- weitere Voice-Sprachen abhängig von Budget/Produktion;
- Untertitel bleiben für alle unterstützten Textsprachen verfügbar.

## 6. UI-Gates

- +30 % Textlängenreserve bei Standardbuttons;
- keine kritischen festen Pixelbreiten;
- CJK/andere Schriftsysteme später ohne Systemumbau möglich;
- Font-Fallbacks;
- UI-Skalierung;
- RTL wird als eigene Erweiterung behandelt, nicht stillschweigend behauptet.

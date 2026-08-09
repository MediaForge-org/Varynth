# DATA_SCHEMA.md — Varynth Datengetriebenes Contentmodell (Arbeitsfassung v0.1, Phase 0)

Status: erste technische Schema-Arbeitsfassung, kein Code, keine konkreten Werte. Grundlage: `CONTENT_CATALOG_v1.0.md`, `RESEARCH_TREE_v1.0.md`, `MISSION_AND_QUEST_BIBLE_v1.0.md` §6, `LOCALIZATION_PLAN_v1.0.md`, `SPEC_AUDIT.md` Punkt 2.

## 1. Grundprinzipien

- **XML als moddbarer Contentpfad.** Definitionen werden aus XML (oder modseitig äquivalenten datengetriebenen Quellen) geladen; die Architektur setzt nicht voraus, dass Content nur in C#/ScriptableObjects existieren kann.
- **Stabile IDs.** Jede Definition hat eine stabile, unveränderliche ID. Anzeigenamen sind lokalisierbar und dürfen sich ändern; IDs nicht (Migration nötig, falls doch).
- **Mod-Namespaces.** Core-Content liegt im reservierten `core.*`-artigen Namensraum (exakte Präfix-Konvention siehe §2); Mods erhalten eigene Namespaces und dürfen nicht mit Core oder anderen Mods kollidieren.
- **Keine festen Obergrenzen.** Kein `maxCivilizationTier`, `ultimaMaxTier`, `maxAutomationGrade` o. ä. im Schema oder Code.
- **Referenzen statt Kopien.** Savegames und Laufzeitzustand referenzieren Definitionen über ID, kopieren nie vollständige Definitionsdaten.
- **Validierung.** Jede Definition muss gegen ein Schema/Regelwerk prüfbar sein (eindeutige IDs, aufgelöste Referenzen, keine verwaisten Abhängigkeiten) — siehe `QA_GATES.md` und `Tools/Validation/`.
- **Versionierung/Migration.** Definitionen und Savegames tragen eine Versionsnummer; ID-Retirement erfordert eine dokumentierte Migrationsregel, nie stillschweigendes Verwerfen.

## 2. ID-/Namespace-Konvention (autoritativ: Content Catalog, siehe SPEC_AUDIT.md Punkt 2)

Format: punktgetrennt, durchgehend Kleinschreibung, `<domain>.<region-oder-scope>.<...>`. Bestätigte Beispiele aus dem Content Catalog:

| Domäne | Muster | Beispiel |
|---|---|---|
| Wohngebäude | `res.<region>.t<tier>.f<form>` | `res.occidentia.t1.f1` |
| Waren | `good.<region-or-global>.<name>` | `good.global.holz`, `good.occidentia.brot` |
| Gebäude | `bld.<region-or-global>.<name>` | `bld.global.kleines.lagerhaus`, `bld.occidentia.foerster` |
| Schiffe | `ship.<region-or-scope>.<class>` | `ship.bekannte.welt.kuestensegler` |
| Fahrzeuge | `veh.<region-or-scope>.<class>` | `veh.bekannte.welt.handkarren` |
| Forschung | `resrch.<zone>.<branch>.<nn>` | `resrch.kw.agr.01`, `resrch.ultima.aer.01` |
| Missionen (Story) | `ST-<REGION>-<nnn>` (Header-Konvention der Mission Bible) | `ST-OCC-001` |
| Missionen (Nebenquest) | `SQ-<CHAR>-<nn>` | `SQ-HEL-01` |
| Storyflags | `st_<region>_<nnn>_complete`, `sq_<char>_<nn>_complete` | `st_occ_001_complete` |

**Bekannter Altkonflikt (siehe SPEC_AUDIT.md Punkt 2):** Das illustrative Quest-XML in der Mission Bible §6 nutzt ein abweichendes Schema (`story.occidentia.001`, `occidentia.harbor.office.t1`). Dieses Muster ist **nicht** zu implementieren; jede tatsächliche Quest-/Objective-Definition referenziert Gebäude/Waren/Wohnungen über die Content-Catalog-Konvention aus obiger Tabelle. Mission-eigene IDs (`ST-OCC-001` etc.) bleiben wie in der Mission Bible verwendet.

## 3. Kern-Definitionstypen (laut Auftrag, Mindestumfang)

Für jeden Typ: Zweck, Kernfelder (konzeptionell, keine finalen Typnamen/Sprachbindung), zentrale Referenzen. Konkrete numerische Werte kommen ausschließlich aus den jeweiligen Fach-Bibles (Balancing Bible, regionale Kapitel) und werden hier nicht dupliziert.

- **RegionDefinition** — Region-ID, Zugehörigkeit (bekannte Welt/Ultima), verfügbare Zivilisationsstufen-Referenzliste, klimatische/visuelle Referenz (Art Bible), Freischaltbedingungen.
- **CivilizationTierDefinition** — Tier-Index innerhalb einer Region, zugehörige Klasse/Bezeichnung (lokalisiert), Voraussetzungen für Erreichen, referenzierte `ResidenceDefinition`-Formen.
- **ResidenceDefinition** — ID nach `res.*`-Schema, Region, Tier, Form (1–7), Footprint, Upgrade-Achsen (vertikal/horizontal), Verweis auf Wertequelle (regionales Kapitel), Bedarfsreferenzen (`GoodDefinition`-Liste, mandatory vs. optional — optional liefert laut Regel 0 Bonusbewohner).
- **BuildingDefinition** — ID nach `bld.*`-Schema, Kategorie (Basis für Produktions-/Service-/Lager-/Sonderbauten, siehe Subtypen unten), Footprint, Anker (Straße/Hafen/VFX/Nachtlicht), Freischaltbedingung.
- **GoodDefinition** — ID nach `good.*`-Schema, Region-oder-global, Familie, Primärrolle (Rohstoff/Zwischenprodukt/Endprodukt/optionale Ware), Bewohnerregel-Flag (mandatory/optional — optional = kein Bevölkerungsbonus im Normalspiel).
- **RecipeDefinition** — Input-Waren (Menge/Zyklus), Output-Ware, Referenz auf produzierendes `ProductionBuildingDefinition`, Verweis auf Balancing-Bible-Formel (nicht Wert-Duplikat).
- **ProductionBuildingDefinition** — Subtyp von `BuildingDefinition`, Referenzliste zulässiger `RecipeDefinition`, Personalbedarf-Referenz, Powerfactor-/Servicefactor-Abhängigkeiten (konzeptionell, Werte extern).
- **ServiceDefinition** — Subtyp von `BuildingDefinition`, abgedeckter Bedarfstyp, Reichweite/Wirkradius-Referenz.
- **RoadDefinition** — Straßentier, Transportgeschwindigkeits-/Reichweiten-Multiplikator-Referenz (Werte: Balancing Bible), Baukosten-Referenz, kein Stau-/Kreuzungsmodell.
- **WarehouseDefinition** — Subtyp von `BuildingDefinition`; Pflichtfeld-Invariante: `storageCapacity` identisch zur Standard-Lagerhausvariante derselben Technologiestufe; unterscheidbare Felder sind ausschließlich Footprint und Rampenzahl (inkl. Aufrüstbarkeit).
- **ShipDefinition** — ID nach `ship.*`-Schema, Klasse, Region-oder-scope, Kapazität/Rollen-Referenz, Freischaltbedingung.
- **VehicleDefinition** — ID nach `veh.*`-Schema, analog zu Ships für Landtransport/Dienste.
- **ResearchDefinition** — ID nach `resrch.*`-Schema, `baseResearchWork`-Referenz (Balancing Bible), `requiredGoods`, `requiredBuildingIds`, `requiredPopulationConditions`, `prerequisiteResearchIds`, optionale `diplomacyConditions`, Unlock-Ziel (Gebäude/Rezept/Schiff/Fahrzeug/Service/Qualitätsstufe/Expeditions-Trait/Automations-Funktion). Ultima-RQ-Gates als aggregierende Definition über mehrere `ResearchDefinition`-Referenzen.
- **FactionDefinition** — Fraktions-ID, diplomatischer Grundzustand, Referenzliste verfügbarer Interaktionen.
- **TraderDefinition** — neutrale Händler, Warenangebot-Referenz, Preisbildungs-Verweis (Balancing Bible Formel).
- **PirateFactionDefinition** — Subtyp/Verwandter von `FactionDefinition`, Aggressions-/Bedrohungsreferenz.
- **QuestDefinition** — Mission-ID (`ST-*`/`SQ-*`-Konvention), Region, Leitfigur-Referenz, Trigger/Voraussetzungen, Zielstruktur (Build/Deliver/Reach-Population/etc., referenziert Content-Catalog-IDs, **nicht** das veraltete Mission-Bible-XML-Muster), Belohnungs-/Freischaltungsreferenz, Storyflag-ID.
- **AchievementDefinition** — Achievement-ID, Bedingungsreferenz, profilweiter Persistenzort (siehe ARCHITECTURE.md §6, nicht savegame-lokal).

## 4. Zusätzliche Definitionstypen (Nutzer-Ergänzung, verbindlich für dieses Schema)

- **GameModeDefinition** — Modus-ID (Story/Aufbau/Sandbox/Szenario), Startregion-Referenz, aktivierte/deaktivierte Sonderregeln (z. B. Sandbox-Cheat "optionale Ware gibt Bonusbewohner"), Abhängigkeit von Profile-Flags (`buildModeUnlocked`, `ultima_discovered` — getrennt geführt, siehe Mega-Prompt).
- **ExpeditionDefinition** — Ziel-Region-Referenz, Voraussetzungen (Schiffe/Fahrzeuge/Forschung), Ergebnis (Regionsfreischaltung im Aufbaumodus), Risikoreferenz.
- **DisasterDefinition** — Ereignistyp, Auslösebedingung-Referenz, betroffene Systeme (Produktion/Bevölkerung/Gebäude), Wirkmechanik-Referenz (keine Werte hier).
- **UtilityDefinition / UtilityNetworkDefinition** — Ver-/Entsorgungsart, Netzwerktopologie-Referenz (Knoten/Reichweite), betroffene Bedarfstypen, getrennt von reiner Straßenlogik.
- **ScenarioDefinition** — feste Startbedingungen/-ziele für die 12 Kern-Szenarien, referenziert Region/Startzustand/Siegbedingung.
- **SpecialistDefinition** — benannte/besondere Arbeitskraft-Einheiten (Perfektionspass), Wirkungsreferenz auf Produktions-/Serviceboni.
- **ModuleDefinition** — modulare Gebäude-Erweiterungs-/Anbauslots (z. B. Lagerhaus-Rampen-Module), referenziert Basis-`BuildingDefinition`.
- **Tourism/Attractiveness-Definitionen** — z. B. `AttractionDefinition`/`VisitorProfileDefinition`: Attraktivitätsquelle, Besucherbedarfs-/Belohnungsreferenz, klar getrennt von regulärer Bewohner-Bedarfslogik.

## 5. Localization-Key-Referenzstrategie

- Jede Definition mit sichtbarem Text (Name, Beschreibung, Flavor) trägt ausschließlich Loc-Keys, nie Literal-Text — konsistent mit `LOCALIZATION_PLAN_v1.0.md`.
- Key-Schema folgt dem dort gezeigten Muster: `<domäne>.<scope>.<bezeichner>.<feld>`, z. B. `good.coffee.name`, `building.occ.market.name`, `quest.story.occidentia.001.title`, `dialogue.story.occidentia.001.open.helena.001`, `achievement.world_nexus.title`.
- Definitionsebene speichert nur den Key; die Auflösung in tatsächlichen Text erfolgt ausschließlich in der Presentation-Ebene (siehe ARCHITECTURE.md §2/§5), nie in der Simulationsebene.
- Deutsch ist Master-Sprache für Autoren; Englisch ist Pflicht-Zielsprache zum Release; weitere Sprachen werden über denselben Pipeline-Mechanismus ergänzt, ohne Codeänderung.
- Zahlen/Einheiten/Datumsformate/Pluralformen werden über die Localization-Pipeline aufgelöst, nicht hart in Definitionen kodiert.

## 6. Quest-Datenmodell — Klarstellung zum Content-Catalog-Bezug

Das technische Quest-Datenmodell aus `MISSION_AND_QUEST_BIBLE_v1.0.md` §6 (Felder: `Title`, `Region`, `Prerequisites`, `Objectives`, `Dialogue`, `OnComplete`) bleibt strukturell gültig als Vorlage für Feldtypen. Nur die darin verwendeten Beispiel-IDs sind veraltet (siehe §2 oben und `SPEC_AUDIT.md` Punkt 2) und werden bei tatsächlicher Implementierung durch Content-Catalog-konforme IDs ersetzt.

## 7. Validierungspflichten (Mindestumfang, siehe auch `Tools/Validation/` und `QA_GATES.md`)

- Eindeutigkeit aller IDs je Namensraum.
- Jede Ware hat entweder eine Produktionsquelle oder ist bewusst als Import/Quest-Ware markiert.
- Jeder Pflichtbedarf hat einen gültigen Versorgungspfad.
- Jedes Produktionsgebäude hat mindestens ein gültiges Rezept.
- Jedes Schiff/Fahrzeug hat gültige Rolle + Freischaltbedingung.
- Keine Referenzen auf gelöschte/nicht existierende IDs.
- Mod-Namespaces kollidieren nicht mit `core.*` oder untereinander.
- Save-Migrationsregeln existieren für jedes zurückgezogene ID-Schema.

## 8. Nicht Teil dieser Fassung

Keine konkreten C#-Typdefinitionen/Interfaces, kein XML-Schema (XSD) final, keine numerischen Balancing-Werte, keine Entscheidung über exakte Serialisierungsbibliothek. Diese folgen in Phase 1/2 der Implementierung.

# BALANCING BIBLE v1.0 — Baseline Pass 09


## 1. Status

Diese Zahlen sind **implementierbare v1.0-Balancingbaselines**, aber noch kein finaler Release-Lock. Sie verhindern, dass Claude beim Implementieren beliebige Werte erfindet. Spätere Telemetrie-/Playtest-Pässe dürfen Werte ändern, solange die Designrelationen erhalten bleiben.

## 2. Grundphilosophie

- Herausforderung entsteht aus Planung und Abhängigkeiten, nicht aus künstlichem Grind.
- Ein einzelner Produktionsfehler darf reparierbar sein.
- Höhere Stufen sind komplexer, aber nicht einfach nur langsamer.
- Ultima hat längere Ketten, dafür deutlich schnellere/automatisiertere Anlagen.
- Niedrigere Bevölkerungsklassen bleiben wirtschaftlich relevant.
- Optionale Luxusketten sind profitabel/attraktiv, aber kein Zwang für Grundbevölkerung.

## 3. Produktionsrate

```text
effectiveOutputPerMinute =
    (60 / cycleSeconds)
    × batchSize
    × staffingFactor
    × powerFactor
    × serviceFactor
    × regionalFactor
    × technologyFactor
```

`staffingFactor` liegt im Normalfall zwischen 0 und 1. Über 1 darf nur durch klar definierte Module/Events entstehen.

Unterbesetzung:

- 100 % Personal → 100 % personalabhängige Leistung;
- 75 % → ungefähr 75–85 % je Gebäudeart;
- 50 % → ungefähr 50–65 %;
- 25 % → ungefähr 25–40 %;
- 0 % → keine personalabhängige Produktion.

Nicht jede Anlage muss exakt linear skalieren; hochautomatisierte Ultima-Anlagen besitzen einen höheren automatischen Sockelanteil.

## 4. Verbrauch

Wohnbedürfnisse werden pro **1.000 Bewohner pro Minute** definiert. Arbeitsbaseline für Pflichtwaren:

- frühe Grundnahrung: 1,0–2,5 Einheiten / 1.000 / min;
- zweite Grundnahrung: 0,6–1,5;
- Kleidung/Hygiene: 0,25–0,9;
- höherwertige Pflichtware: 0,15–0,6;
- sehr späte technische Pflichtware: 0,05–0,25.

Optionale/Luxuswaren dürfen deutlich geringere Verbrauchsraten besitzen.

Die tatsächliche Rate wird je Klasse und Ware im Contentdatensatz kalibriert, nicht über eine einzige globale Multiplikation.

## 5. Bewohner und Lebensqualität

Normalspiel:

`housePopulation = regularResidents + lifeQualityBonus`

- reguläre Plätze werden primär durch Pflichtbedürfnisse gehalten;
- Lebensqualitätsplätze werden anteilig aus ihren Diensten gefüllt;
- optionale Waren: **0 Einwohnerwirkung**;
- Lebensqualitätsbewohner liefern Arbeitskräfte und Steuern;
- nur reguläre Bewohner zählen für Klassenfreischaltungen.

Dienstdeckung:

`effectiveLifeQualityContribution = capContribution × coverage × qualityFactor`

Kurze Versorgungsunterbrechungen verwenden einen Gnadenpuffer von typischerweise **60–180 Sekunden**, bevor sichtbare Abwanderung beginnt. Kritische Story-/Challengepresets dürfen diesen Wert verändern.

## 6. Baukostenrelationen

Arbeitsbasis je Wohnform innerhalb einer Klasse:

- Form 1: 1,00× Referenz;
- Form 2: 1,6–2,0×;
- Form 3: 2,4–3,0×;
- Form 4: 3,4–4,4×;
- Form 5: 4,8–6,5×;
- Form 6: 6,5–9,0×;
- Form 7 Ultima/Apex: 9,0–13,0×.

Kosten wachsen stärker in **Materialvielfalt und Qualitätsanforderungen** als nur in Geld.

Wohnupgrades bleiben mechanisch sofort, sobald alle Bedingungen erfüllt sind.

## 7. Straßen

Bekannte Welt besitzt 5 Kernstufen. Baseline relativ zur ersten Straße:

| Stufe | Transportgeschwindigkeit | Dienst-/Transportreichweite | Baukostenrelation |
|---|---:|---:|---:|
| 1 | 1,00× | 1,00× | 1,00× |
| 2 | 1,20× | 1,10× | 1,7× |
| 3 | 1,45× | 1,22× | 2,8× |
| 4 | 1,75× | 1,36× | 4,5× |
| 5 | 2,10× | 1,52× | 7,0× |

Keine Stau-/Kreuzungssimulation wird für diese Werte vorausgesetzt.

Ultima UT1–UT6 darf deutlich über Stufe 5 hinausgehen und arbeitet mit eigenen Netzen.

## 8. Lagerhäuser

Auf derselben Technologiestufe:

- kleines Lagerhaus: **100 % Lagerkapazität** der Standardform, ca. 40–60 % der Rampenslots;
- Standardlager: 100 % Kapazität, 100 % Rampenslots;
- Groß-/Industrieform: Kapazität kann je Technologie 125–200 % betragen, Rampenslots 150–300 %, benötigt aber mehr Fläche/Unterhalt.

Kleine Lagerhäuser werden über Rampenupgrades verbessert. Das Ziel ist Layoutfreiheit, nicht ein objektiv bestes Gebäude.

## 9. Geld und Unterhalt

Einnahmen:

`income = residentBaseTax + optionalConsumptionIncome + tradeProfit + service/contractIncome`

Zielrelation:

- niedrige Klassen: geringe Steuerbasis, hohe Arbeitskraftrelevanz;
- mittlere Klassen: stabiler Kerngewinn;
- hohe Klassen: hohe Steuern, aber starke Dienst-/Infrastrukturkosten;
- Luxus: profitabel, aber volatil und optional;
- Militär/Schutz: Kostenstelle, deren Wert in Risikoreduktion liegt.

Normal-Schwierigkeit soll bei sauberer Grundversorgung einen positiven, aber nicht explosiven Cashflow ermöglichen.

## 10. Handelspreise

```text
marketPrice =
    baseValue
    × scarcityFactor
    × relationshipFactor
    × contractFactor
```

Baseline `scarcityFactor`: 0,75–1,50.

Normale Händler dürfen keine extremen 10×-Preisschwankungen erzeugen. Spezialaufträge dürfen gezielt höhere Prämien bieten.

## 11. Expeditionen

Eine Expedition besitzt einen `preparednessScore` aus Navigation, Versorgung, Technik, Forschung, Diplomatie, Klima und optional Spezialtags.

- klar unter Empfehlung: hohes Risiko/zusätzliche Kosten;
- knapp darunter: möglich mit Entscheidungen;
- erfüllt: normale Erfolgswahrscheinlichkeit;
- deutlich übererfüllt: zusätzliche positive Optionen, aber keine automatische Maximalbelohnung.

Storykritische Expeditionen dürfen nie durch einen einzelnen RNG-Wurf dauerhaft scheitern.

## 12. Forschung

Zeitbaselines stehen im Research Tree. Materialkosten sollen:

- frühe Forschung: lokal verfügbar;
- mittlere: 1–2 verarbeitete Waren;
- späte: 2–4 Hightechwaren;
- Ultima: häufiger Cross-Region-Komponenten.

Forschung ist kein Geldsink ohne konkrete Freischaltung.

## 13. KI

Normal:

- gleiche Grundwirtschaftsregeln;
- keine unbegrenzten Ressourcen;
- Reaktionslatenz und Planungsqualität menschlich nachvollziehbar.

Schwer:

- bessere Planung, höhere Zielgenauigkeit, geringere Leerlaufzeiten;
- maximal kleine transparente Start-/Effizienzvorteile.

KI darf keine Schiffe „aus dem Nichts“ spawnen, wenn ihre Ökonomie diese nicht tragen könnte, außer explizite Story-/Szenarioeinheiten.

## 14. Piraten

Piratenbedrohung skaliert über:

- Sichtbarkeit/Attraktivität von Handelsrouten;
- Ruf/Feindschaft;
- regionale Präsenz;
- Schwierigkeitsgrad;
- Eskort-/Schutzkapazität.

Der Spieler erhält immer mindestens zwei Lösungsrichtungen: militärisch, diplomatisch/ökonomisch oder Routen-/Schutzplanung.

## 15. Ultima-Produktion

Verbindliche effektive Zielmultiplikatoren gegenüber funktional vergleichbarer bekannte-Welt-Baseline:

| U | Durchsatz | Arbeitskraft je Output | Rückgewinnung |
|---|---:|---:|---:|
| U0 | 1,35× | 100 % | 5 % |
| U1 | 1,80× | 82 % | 15 % |
| U2 | 2,50× | 65 % | 30 % |
| U3 | 3,40× | 48 % | 50 % |
| U4 | 4,60× | 34 % | 70 % |
| U5 | 6,00× | 24 % | 85 % |

Diese Werte werden nicht blind auf jedes Rezept multipliziert; sie sind Zielwerte für vollständig versorgte, vergleichbare Produktionssysteme.

## 16. Zeitziele eines normalen Storydurchgangs

Nur Arbeitsbasis für Playtests:

- erste stabile Siedlung: 15–30 min;
- erste höhere Klasse Occidentias: 45–90 min;
- erste große interregionale Expedition: 3–6 h;
- mehrere bekannte Regionen aktiv: 10–25 h;
- Australis erreicht: 25–45 h;
- Eiswand/Ultima-Enthüllung: 40–70 h;
- Ultima-Hauptbogen: zusätzlich 35–70 h;
- World Nexus / Hauptstoryabschluss: grob **80–140 h** je Spielstil.

Diese Werte dürfen durch Spielerfahrung und Schwierigkeitsgrad stark variieren. Das Spiel darf Schnellspieler nicht künstlich ausbremsen.

## 17. Aufbauspielmodus

Keine Storytimer. Fortschritt wird ausschließlich über Wirtschaft, Forschung, Expeditionen, Diplomatie und Ressourcen gesteuert.

Start Aurelia → freie Ultima-Erschließung → bekannte Welt als große Expeditionsfreischaltung.

## 18. Balance-Testmatrix

Pflicht-Testsavegames:

1. frühes Occidentia;
2. dichte Occidentia-Metropole;
3. Meridia-Exportwirtschaft;
4. Orientia-Hochverdichtung;
5. Aferia-Trockenzeit;
6. Australis-Wärme-/Importkrise;
7. Aurelia U2;
8. Cross-Region-Ultima U4;
9. World-Nexus-Bau;
10. KI/Piraten-Handelskrieg;
11. kleines-Lagerhaus-Stresstest;
12. Modded Tier > 10.

Jeder Test erfasst: Geldsaldo, Produktionssaldo, Arbeitskräfte, Lieferreserve, Forschung, Abfertigung, Frametimes und Savegröße.

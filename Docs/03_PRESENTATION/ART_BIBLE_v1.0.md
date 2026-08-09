# ART BIBLE v1.0 — Visual Content Lock Pass 09


## 1. Art Pillars

1. **Lesbar aus Strategiesicht:** Silhouette, Material und Dach-/Turmform zeigen Funktion und Entwicklungsstufe.
2. **Glaubwürdig statt fotorealistisch um jeden Preis:** realistische Materialreaktion und Licht, leicht stilisierte Proportionen.
3. **Regionale Eigenständigkeit:** keine Region ist nur ein Texture-Swap.
4. **Vertikaler Fortschritt sichtbar:** Wohnupgrades verdichten und modernisieren nachvollziehbar.
5. **Keine fremde IP kopieren:** Anno und andere Spiele sind nur Genrereferenz.
6. **Speichereffiziente Vielfalt:** modulare Kits, Materialinstanzen, Trim-Sheets, Atlanten, prozedurale/parametrische Varianten statt unnötiger Vollkopien.

## 2. Regionale visuelle Matrix

| Region | Materialkern | Farbe | Architektur | Vegetation | VFX/Licht |
|---|---|---|---|---|---|
| Occidentia | Holz, Ziegel, Kalkstein, Stahl, Glas | warm gedeckt → industriell → urbane neutrale Hochhauspalette | europäisch-industriell, klarer Übergang von Dorf zu Metropole | Laubwald, Felder, Alleen, Stadtparks | Dampf, Rauch, warmes Fensterlicht, später elektrische Leuchtreklame sparsam |
| Meridia | Putz, Holz, Ziegel, Naturstein, farbige Keramik, später Beton/Glas | sonnenwarme Erdtöne, gebrochene kräftige Fassaden, üppiges Grün | Veranden, Innenhöfe, luftige Fassaden, Fluss-/Küstenbezug | Tropenwald, Palmen, Flussvegetation, Plantagen | feuchte Luft, Regenfronten, Wasserreflexe |
| Orientia | Holz, Stein, Keramik, Putz, Metall, Beton, Glas | ruhige Naturtöne + gezielte Lack-/Keramikakzente | Hofstrukturen, Dächer, Terrassen, dichte Handelsquartiere, spätere moderne Vertikalität | Bambus, Tee-/Reisterrassen, saisonale Bäume | Monsunregen, Papier-/Lichtlaternen nur passend zur Stufe, keine stereotype Überzeichnung |
| Aferia | Lehm/Putz, Stein, Holz, Textilien, Metall, Beton | sonnenhelle Mineral-/Erdpalette, regionale Stoff-/Keramikakzente | offene Höfe, Verschattung, klimaangepasste Fassaden, später moderne eigenständige Metropolen | Savanne, Trockenland, Tropenwald, Hochland, Delta | Staub, Hitzeflimmern dezent, starke Sonnen-/Schattenkontraste |
| Australis | isolierte Paneele, Metall, Verbundstoffe, Glas | weiß/grau/blau mit warmen Sicherheits-/Innenlichtakzenten | Expeditionsmodule → verbundene Polarsiedlung → technische Polarstadt | Tundra, Eis, Fels, Schnee | Schneetreiben, Atemdampf nah, Polarlicht, Warnleuchten |
| Aurelia | heller Stein, dunkles Metall, milchiges Glas, Resonanzmaterial | hell neutral + tiefblau/kupfer/warme Ätherakzente | präzise Achsen, Plätze, Ringknoten, vertikale Gärten, geometrische Resonanzbauten | gepflegte Hochtechnologie-Gärten | saubere Resonanzlinien, schwebende Elemente, keine Neon-Sci-Fi-Überladung |
| Viridia | Biokeramik, Harz, lebende Faser, transluzente Biopolymere | tiefe Grüntöne, Creme, Bernstein, Violett als Bioleuchten | organische Terrassen, lebende Stützen, Wasserläufe, Biobrücken | Weltbäume, Myzel, Urwald, Saatgutgärten | Biolumineszenz, Sporen, Regenerationsimpulse |
| Titania | Basalt, Granit, Titanlegierung, Bronze | dunkler Stein, kaltes Metall, warme Schmiedepunkte | Felsenterrassen, massive Brücken, riesige Tore, Berginnenräume | Hochland, Gletscher, karge Täler | Staub, Funken, Gravkran-Feldlinien sehr subtil |
| Ignaria | Obsidian, Basalt, hitzefeste Keramik, dunkle Metalle | schwarz/dunkel + glühendes Orange + weißgoldene Hitzeschilde | thermische Terrassen, Lufttürme, ringförmige Plattformen | Ascheflora, hitzetolerante Pflanzen | Hitzeverzerrung, Lava, Ätherflugspuren, Drachen nur glaubwürdig skaliert |
| Pelagia | Druckkeramik, Metall, Korallenverbünde, Glas | Meeresblau, Grün, Perlmutt, dunkle Tiefseeakzente | Inselstädte, Plattformen, Tiefseehäfen, druckfeste Unterwasserstrukturen | Kelp, Küstenflora, Korallen | Wasserkaustik, Nebel, Biolumineszenz, Tiefendruckeffekte |

## 3. Zivilisations- und Wohnfortschritt

Für jede Bevölkerungsklasse wird eine **Kit-Familie** erstellt. Innerhalb der Kit-Familie teilen Varianten:

- Tragwerkslogik;
- Trim-Sheet;
- Fenster-/Türatlas;
- Dach-/Fassadenmodule;
- Props;
- LOD-Regeln.

Die sechs beziehungsweise sieben Wohnformen dürfen **nicht** nur höher skaliert werden. Jede Form braucht eine neue Silhouette:

1. Basis: freistehend/geringe Dichte;
2. erweitert: Anbau/zusätzlicher Flügel;
3. groß: mehrgeschossig/größerer Innenhof;
4. verdichtet: nahezu volle Parzellennutzung;
5. Wohnkomplex: klar urban;
6. Hochhaus: vertikale Dominante;
7. Apex (nur passende Ultima-Ketten): ikonische, aber regional glaubwürdige Endform.

## 4. Variantenanzahl

Ziel je normale Wohnform:

- 5–10 sichtbare Fassaden-/Dachvarianten;
- 3–6 Garten-/Hofsets;
- 4–12 kleine Propsets;
- Materialvarianten aus gemeinsamen Textursets;
- Hochhäuser: weniger Basismodelle erlaubt, dafür austauschbare Kronen/Fassadenmodule.

Randomisierung muss deterministisch aus Seed + Building-ID möglich sein, damit Savegames keine vollständigen Variantendaten speichern müssen.

## 5. Produktionsgebäude

Jede Produktionsstätte zeigt ihren Prozess aus der Nähe durch mindestens zwei der folgenden Signale:

- Materialein-/ausgang;
- bewegte Maschine;
- Dampf/Rauch/Staub/Wasser;
- Arbeiter-/Fahrzeugaktivität;
- Felder/Tiere;
- Licht-/Hitzezustand;
- Lagerbestand als sichtbare Props;
- Ultima: Resonanz-, Bio-, Grav- oder Druckeffekt.

Aus mittlerer Entfernung reicht Silhouette + 1–2 Hauptanimationen.

## 6. Straßen und Infrastruktur

Fünf bekannte-Welt-Straßenstufen müssen aus der Kamera unterscheidbar sein durch:

- Oberfläche;
- Rand-/Gehwegbehandlung;
- Beleuchtung;
- Bäume/Props;
- technische Einbauten.

Keine breiteren Fahrspuren nur zur Simulation von Staus nötig.

Ultima UT1–UT6 besitzt klar fremdartigere, aber funktional lesbare Transitflächen.

## 7. Lagerhäuser

Kleines, normales und großes Lagerhaus teilen eine erkennbare Familien-DNA.

Kleines Lagerhaus:

- deutlich kleinerer Footprint;
- **keine visuelle Behauptung geringerer Lagerkapazität**;
- kompakte vertikale/unterirdische Lageridee ist erlaubt;
- weniger sichtbare Rampen;
- Rampenupgrades verändern die Außenfassade sichtbar.

## 8. Häfen und Schiffe

Schiffe werden über Silhouette gelesen:

- Fracht: Volumen/Deckfläche;
- Passagier: Fenster/Aufbauten;
- Marine: Schutz/Bewaffnung;
- Expedition: Sensoren/Versorgung;
- Polar: verstärkter Bug;
- Ultima: regionale Hochtechnologie.

Keine realen historischen Schiffe 1:1 nachbauen; historische Funktionslogik darf inspirieren.

## 9. Bewohner

Crowd-Kits je Region und Klasse:

- Körpervarianten;
- Altersgruppen;
- Berufs-/Freizeitkleidung;
- Kopfbedeckungen nur kulturell plausibel;
- Farbvarianten;
- kleine Accessoires.

Keine Klasse wird durch karikierte Ethnie dargestellt. Gesellschaftsstufe zeigt sich eher in Materialqualität, Schnitt, Berufsausrüstung und Kontext.

## 10. Hauptfiguren

Jede Hauptfigur benötigt:

- Turnaround;
- Gesichts-/Haarsheet;
- Outfit A/B/C je Storyphase;
- Materialpalette;
- 8–12 Kernexpressionen;
- Gestikprofil;
- Silhouettenregel;
- Voice-Lock-Verknüpfung.

Outfits dürfen sich über die 80–140h-Kampagne sichtbar entwickeln.

## 11. Riesen

Riesen sind intelligente Personen, keine Monster-Masse.

Design:

- monumental, aber anatomisch glaubwürdig;
- Kleidung/Schmuck zeigt Clan/Handwerk;
- Werkzeug und Architektur sind mit ihrer Größe konsistent;
- Gesichter individuell;
- keine primitive „Höhlenmensch“-Karikatur.

## 12. Drachen

Mehrere Arten erhalten unterschiedliche Flugsilhouette, Lebensraum und Äther-/Thermalbezug.

Regeln:

- keine direkte Übernahme ikonischer Drachen anderer Franchises;
- Flügel-/Körperphysik stilisiert glaubwürdig;
- Narben/Alter/Vertrauen sichtbar;
- Drachen sind keine Ressourcenvieh-Einheiten;
- nicht-destruktive Materialgewinnung ist visuell erkennbar.

## 13. Caelari

Caelari müssen gleichzeitig fremd und würdevoll wirken.

- humanoide Erscheinung möglich;
- „Flügel“ als ätherische/resonante Strukturen, nicht zwingend biologische Federschwingen;
- sechs Orden mit klarer, aber nicht kitschiger Formsprache;
- wahre Natur bleibt visuell mehrdeutig;
- kein direktes Kopieren christlicher/TV-spezifischer Engelikonografie.

## 14. Leviathane und Großfauna

Pelagia-/Viridia-Großfauna folgt Ökosystemlogik.

- klare Größenreferenzen;
- Bewegungen langsam/massereich bei großen Tieren;
- Beobachtung statt permanente Bekämpfung;
- Habitat-Markierungen und Verhalten geben Gameplayfeedback.

## 15. UI-Visuals

UI besitzt eine eigenständige Material-/Formensprache:

- klare Hierarchie;
- sparsame Rahmen;
- hohe Kontraste;
- Icons als eigene Vektorfamilie;
- Region darf Akzentdetails verändern, nicht Bedienlogik;
- Spoilerinhalte verwenden neutrale Platzhalter bis Freischaltung.

Iconregeln:

- 24 px muss erkennbar sein;
- ein Hauptsymbol pro Icon;
- keine Fotominiaturen als Standard;
- Farbinformation nie alleinige Bedeutung.

## 16. VFX

VFX-Prioritäten:

1. Gameplayfeedback;
2. Lesbarkeit;
3. Atmosphäre;
4. Spektakel.

Äther:

- keine permanente Partikelwolke;
- Energie fließt geordnet entlang Resonanz-/Feldstrukturen;
- höhere AQ-Stufen wirken stabiler, nicht einfach heller.

Katastrophen:

- Vorwarnung visuell klar;
- Intensität skaliert;
- wichtige Flucht-/Schutzzonen erkennbar.

## 17. Beleuchtung

- physikalisch plausible Tageslichtbasis;
- regionalspezifische Atmosphäre;
- Nacht bleibt spielbar;
- Stadtlichter erzeugen Orientierung;
- HDR optional;
- keine kritische Information nur über Bloom.

## 18. Asset-Speichereffizienz

Pflichtregeln:

- Trim-Sheets/Atlanten, wo sinnvoll;
- Materialinstanzen statt Texturkopien;
- gemeinsame Normal-/Mask-Maps;
- Mesh-Instancing;
- LOD0–LOD3/Impostor je Relevanz;
- regionale Addressable-Gruppen bzw. äquivalentes Streaming;
- Texturen in plattformgeeigneten komprimierten Formaten;
- nicht sichtbare High-Res-Quellen nicht im Releasebuild;
- Source Assets getrennt von Runtime Assets;
- Varianten bevorzugen modulare Kombinationen.

## 19. Produktionsbudgets — Arbeitsbasis

Typische Gebäude:

- LOD0 nur im Nahbereich;
- LOD1 bei normaler Spielkamera;
- LOD2 weit;
- LOD3/Impostor strategischer Zoom.

Priorität liegt auf Framerate und Lesbarkeit, nicht maximalen Polygonzahlen. Exakte Triangle-/Texelbudgets werden nach erstem Vertical Slice durch GPU-/CPU-Profiling eingefroren.

## 20. Art QA

- Silhouette-Test in Graustufen;
- 24px-Icon-Test;
- Nachtlesbarkeit;
- Farbenblindheitscheck;
- LOD-Popping-Test;
- 100-identische-Häuser-Test auf sichtbare Wiederholung;
- Speicher-/VRAM-Test je Region;
- keine geschützten Assets/Trade-Dress-Kopien.

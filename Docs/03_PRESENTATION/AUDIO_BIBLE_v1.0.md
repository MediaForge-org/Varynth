# AUDIO BIBLE v1.0 — Audio Content Lock Pass 09


## 1. Audio-Pillars

- Musik begleitet, sie kommandiert nicht permanent.
- Regionen besitzen eigene musikalische Identität ohne stereotype Ein-Instrument-Klischees.
- Soundeffekte erklären Systemzustände.
- Sprache trägt Figuren und Story, Questtrackertexte werden nicht wörtlich vorgelesen.
- Audio skaliert aggressiv mit Kameraentfernung und Simulationsgröße.
- Alle wichtigen Informationen bleiben mit Untertiteln/visuellen Hinweisen zugänglich.
## 2. Musikidentitäten

| Bereich | Instrument-/Klangfamilien | Dynamik | Leitmotiv |
|---|---|---|---|
| Occidentia | akustische Streicher/Holzbläser, Klavier, später mechanisch-orchestrale Layer | Aufbruch → Industrie → Metropole | prägnantes 4–6-Noten-Weltmotiv |
| Meridia | Gitarren-/Zupftexturen, Holz-/Rahmenperkussion, Streicher, warme Ensemblefarben | Fluss, Regen, Export, Stadtwachstum | fließendes synkopiertes Motiv ohne folkloristische Kopie |
| Orientia | Zupf-/Bogenfarben, Holzbläser, Percussion, moderne Orchester-/Elektroniklayer | Terrassen, Handel, Gelehrsamkeit, Metropole | präzises aufsteigendes Intervallmotiv |
| Aferia | Perkussionsensemble, Stimmen als Textur, Saiten/Bläser, moderne orchestrale Layer | Weite, Trockenzeit, Handel, urbane Entwicklung | rhythmisch flexibles Antwortmotiv |
| Australis | reduzierte Streicher, Klavier, tiefe Drones, Wind-/Eistexturen | Isolation, Forschung, Sturm, Eiswand | langes Intervall + kalter Resonanzton |
| Aurelia | Chorflächen, Glas-/Metallresonanz, tiefe Streicher, kontrollierte Elektronik | Ordnung, Archive, Weltgitter | geordnetes Nexus-Motiv |
| Viridia | organische Percussion, Atem-/Holztexturen, warme Stimmen, granular-biologische Klangfarben | Leben, Regeneration, Gefahr | wachsendes zyklisches Motiv |
| Titania | tiefe Trommeln, Metallresonanz, tiefe Blech-/Streicherfarben | Masse, Riesen, Megabau | schweres dreiteiliges Motiv |
| Ignaria | tiefe Pulse, Metall, aggressive Streicher, luftige Höhenlayer | Hitze, Flug, Drachen, Sturm | aufsteigendes Thermal-/Flugmotiv |
| Pelagia | tiefe Drones, Wasserresonanz, helle Obertöne, langsame Pulse | Weite, Tiefsee, versunkene Stadt | abfallendes Echo-Motiv |
| Caelari | stimmähnliche Resonanzen, reine Obertöne, bewegliche Harmonien | Erkenntnis, Unsicherheit, Transzendenz | mehrdeutige Intervallfolge |
| Piraten | perkussiv, rau, mobil, je Fraktion eigenes Material | Bedrohung, Schmuggel, Verhandlung | kein generisches Seemannslied |

## 3. Dynamisches Musiksystem

Musikzustände:

- Exploration/ruhiger Aufbau;
- produktiver Aufschwung;
- große Stadt;
- Forschung/Entdeckung;
- Diplomatie;
- Piraten-/Konfliktspannung;
- Katastrophe;
- Story-Cinematic;
- Mystery;
- Ultima-Erstkontakt;
- World-Nexus-Bau;
- Finale/Postgame.

Layer statt harter Trackwechsel. Ein Cue besitzt `intro`, `base`, optionale `developmentLayers`, `tensionLayer`, `resolution`.

Cooldown verhindert, dass derselbe Track zu oft wiederholt wird.

## 4. Tageszeit

Nachtarrangements dürfen dünner/intimer werden. Produktion stoppt nicht; Musik behauptet daher keine künstliche Nachtruhe.

## 5. Adaptive Stadtgröße

Musik reagiert nicht auf jede einzelne Einwohnerzahl. Es gibt grobe Entwicklungszustände, z. B.:

`settlement`, `town`, `city`, `metropolis`, `apex`.

Übergang mit Hysterese, damit Musik nicht bei kleinen Schwankungen ständig umschaltet.

## 6. Gebäude-SFX

Produktionsgebäude besitzen:

- Baseloop;
- Aktivitätsloop;
- Start/Stop;
- Warn-/Störzustand;
- einzelne One-Shots.

Nur nahe relevante Gebäude erhalten volle Quellen. Mittlere Entfernung verwendet Cluster-/Bus-Audio. Weite Kamera nutzt Stadtteppiche.

## 7. Logistik-SFX

- Karren/Fahrzeuge: Roll-/Antriebsfamilien;
- Bahn: Motor, Rad/Schiene, Bremsen, Horn sparsam;
- Häfen: Wasser, Tauwerk, Kräne, Schiffsmotoren;
- Luftfahrt: Entfernung-/Höhenfilter;
- Ultima: Ätherantrieb klingt stabil/resonant, nicht wie konventioneller Jet mit Sci-Fi-Filter.

## 8. UI-Sound

UI-Soundfamilien:

- Hover sehr dezent;
- Bestätigung;
- Ablehnung/fehlende Voraussetzung;
- Bau;
- Upgrade;
- Forschung abgeschlossen;
- Achievement;
- kritische Warnung;
- Questupdate.

Ein Sound darf nicht bei massenhaften Aktionen hunderte Male stapeln; Batch-/Cooldownregeln sind Pflicht.

## 9. Sprache

Hauptstory:

- vollständig vertonte Schlüsseldialoge;
- Mission Bible Pass 07+ ist Textquelle;
- Sprecher lesen keine IDs, Prozenttracker oder technischen Zieltexte;
- Untertitel immer vorhanden;
- Skip respektiert Storyflags und setzt Sequenz sauber fort.

Neben-/Systemaufträge:

- kurze Barks optional vertont;
- Detailtext bleibt lesbar;
- Wiederholungs-Cooldown.

## 10. Voice Profile Pipeline

Jede Hauptfigur besitzt:

- `voiceProfileId`;
- Sprachtempo;
- Tonhöhe als Bereich, nicht starre Tonmanipulation;
- bevorzugte Satzlänge;
- emotionale Grenzen;
- Humor-/Ironiegrad;
- Aussprachelexikon;
- Namen-/Regionsaussprache;
- erlaubte wiederkehrende Signaturen.

Vor Aufnahme: **Voice-Lock-Line-Edit**.

## 11. Lautheits-/Mix-Arbeitsbasis

Keine endgültige Mastering-Norm vor Plattformtests, aber Ausgangsbasis:

- Sprache klar vor Musik;
- kritische UI-Warnungen dürfen Musik sidechainen;
- Musik besitzt genügend Headroom für Katastrophen-/Cinematic-Layer;
- keine dauerhafte Loudness-Wall.

Optionales Dynamic-Range-Preset:

- Nacht/Leise;
- Standard;
- Kino/Weit.

## 12. Untertitel

Optionen:

- Sprechername;
- Größe;
- Hintergrund;
- maximale Zeilenbreite;
- Sound-Captions;
- Richtung wichtiger Offscreen-Geräusche optional.

Storyrelevante nichtsprachliche Geräusche erhalten Captions.

## 13. Regionale Ambience

Jede Region besitzt mindestens:

- Tag;
- Nacht;
- Regen/Sturm/Saison;
- Küste/Wasser;
- Wald/Vegetation;
- Stadt;
- Industrie;
- Spezialbiom.

Ultima zusätzlich Äther-/Fauna-/Ruinenlayer.

## 14. Drachen

Drachenstimmen sind Tier-/Resonanzdesign, keine menschliche Sprache, außer Lore definiert explizit andere Kommunikationsform.

- Artgröße beeinflusst Frequenz/Decay;
- Nähe erzeugt physische Wirkung;
- kein dauerndes Brüllen als Ambientspam;
- Vertrauen erkennt man auch an ruhigerem Verhalten.

## 15. Riesen

Riesen sprechen verständlich und individuell, nicht automatisch mit billigem Pitch-Down-Effekt. Körpergröße zeigt sich über Raumresonanz, Schritt-/Kleidungs-/Umgebungsreaktion.

## 16. Leviathane

Unterwasserkommunikation wird durch Druck, Distanz und Wasserfilter inszeniert. Oberfläche und Tiefsee besitzen getrennte Mixzustände.

## 17. Caelari

Caelari-Sound soll Mehrdeutigkeit erhalten:

- reale Stimme + resonante Obertöne;
- keine generischen Engelschöre bei jeder Begegnung;
- Orden besitzen subtile Motive;
- Iriath und andere Individuen bleiben als Person erkennbar.

## 18. Piraten

Jede Hauptpiratenfraktion erhält:

- eigenes Musikmotiv;
- eigene Funk-/Rufsignale;
- Schiffssound-Variation;
- Anführer-Voiceprofil.

Spieler soll anhand Audio grob erkennen können, welche Piratenfraktion in der Nähe ist.

## 19. Achievement-Audio

Achievement-Pop:

- kurz;
- befriedigend;
- nicht lauter als kritische Warnungen;
- Geheim-/Storyachievement darf eigenes dezentes Motiv besitzen;
- bei mehreren simultanen Achievements Queue statt Soundstack.

## 20. Speichereffizienz

- Musikstreaming statt vollständigem PCM im RAM;
- Voice nach Region/Akt paketierbar;
- geeignete verlustbehaftete Kompression für Runtime;
- kurze UI-SFX ggf. komprimiert im Speicher;
- Duplikate per Shared Source vermeiden;
- Sprachpakete technisch separat installierbar;
- Source-WAVs nicht im Releasebuild.

## 21. Audio QA

- Story verständlich bei Standardmix;
- Untertitel decken alle Pflichtinformationen;
- keine Audioquellenexplosion in Millionenstadt;
- Regionswechsel ohne falsche Ambience;
- Save/Load setzt Musikzustand sauber fort;
- Skip hinterlässt keine Loops;
- keine fremden Soundtrack-/SFX-Kopien.

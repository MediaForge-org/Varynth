# MISSION & QUEST BIBLE v1.0
## Vollständiges Produktionsdokument für die Hauptstory, feste Nebenmissionen und Quest-Implementierung

> **AUTORITATIV:** Dieses Dokument ersetzt alle früheren Kurzlisten einzelner Storymissionen. Die Storyübersicht im Mega-Prompt bleibt für Makrostruktur gültig; bei Details zu Missionsablauf, Dialogtriggern, Flags, Fail-Safes und Questlogik gilt diese Mission Bible.

# 1. Narrative Produktionsregeln

Die Story beginnt mit dem ersten Haus und bleibt bis zum World Nexus mit normalen Aufbausystemen verflochten. Missionen sind keine losgelösten Minispiele. Sie verwenden echte Gebäude, Waren, Bewohnerzahlen, Dienstqualitäten, Produktionsraten, Handelsrouten, Forschung, Diplomatie, Expeditionen und Resilienzwerte. Der Spieler darf frei bauen; die Mission definiert einen gewünschten **Zustand**, nicht einen vorgeschriebenen Stadtplan.

**Storymodus:** vollständige Hauptstory, geführte Expeditionen, Figuren, Dialoge, Enthüllungen und Konsequenzen.

**Aufbauspielmodus:** nach Storyabschluss profilweit freigeschaltet; keine Hauptstory und keine Storydialogkette; Expeditionen dienen frei der Erschließung. Start standardmäßig Aurelia/Ultima, bekannte Welt wird später per Expedition freigeschaltet.

**Sandbox:** freie Regeln/Cheats; Story optional deaktiviert; keine Storyfreischaltungen für das Profil, sofern nicht ausdrücklich als Debugoption markiert.

## 1.1 Was „Drehbuch“ hier bedeutet
Jede feste Mission besitzt mindestens: Trigger, Voraussetzungen, Primärziele, optionale Ziele, Auftaktinszenierung, konkrete Dialogzeilen, dynamische Zwischenreaktionen, Abschlussdialog, Belohnungen, Flags, Fail-Safe, Save/Load-Verhalten und QA-Gates. Voll vertonte Hauptdialoge verwenden Lokalisierungsschlüssel; das deutsche Drehbuch ist die inhaltliche Masterfassung, bis eine andere Sprachmaster-Entscheidung getroffen wird.

## 1.2 Wiederkehrende Hauptfiguren
- **Helena Voss:** pragmatische Siedlungsverwalterin; kurze, klare Sätze; denkt in Menschen, Vorräten und Konsequenzen; trockener Humor.
- **Marek Vale:** Kapitän und Händler; gelassen, neugierig, seefahrerische Bilder; risikofreudig, aber nicht leichtsinnig.
- **Dr. Mira Halden:** Kartographin und Naturwissenschaftlerin; präzise, skeptisch, wird bei Widersprüchen sichtbar fasziniert; vermeidet voreilige Schlussfolgerungen.
- **Elias Renn:** Ingenieur; praktisch, technisch, lösungsorientiert; misstraut Mystik, bis Messwerte ihn zwingen umzudenken.
- **Inés Araya:** Meridia-Sprecherin und Logistikerin; warm, direkt, regional verwurzelt; bewertet Projekte danach, ob sie lokalen Gemeinschaften helfen.
- **Lian Sora:** Orientia-Archivarin und Gelehrte; ruhig, aufmerksam, historisch denkend; spricht selten ohne Grund.
- **Amara Selu:** Aferia-Ingenieurin und Diplomatin; strategisch, selbstbewusst, gemeinschaftsorientiert; verbindet Wasser, Handel und Politik.
- **Dr. Edda Vey:** Australis-Expeditionsleiterin; nüchtern, zäh, risikoavers; Respekt vor Natur und Logistik.
- **Serin Ael:** Aurelia-Koordinator; höflich, reserviert, hochgebildet; hält Informationen zurück, wenn er Verantwortung ungeklärt sieht.
- **Ilyra Venn:** Viridia-Lebensgelehrte; empathisch und wissenschaftlich; betrachtet Ökosysteme als Netz, nicht als Rohstofflager.
- **Kharum Tor:** Sprecher eines Titania-Riesenclans; langsam, gewichtig, bildhaft; misst Vertrauen an gemeinsam getragenen Lasten.
- **Vaelis:** drakonische Vermittlerfigur; scharf beobachtend, stolz, wenig geduldig mit Besitzansprüchen; unterscheidet Respekt von Unterwerfung.
- **Neris Pell:** Pelagia-Navigatorin und Tiefenforscherin; trocken, furchtlos, technisch; behandelt das Meer als dreidimensionalen Raum.
- **Iriath:** Caelari des Ordens des Gitters; fremdartig ruhig, präzise und mehrdeutig; spricht von Resonanz, Verantwortung und Grenzen.
- **Sareen:** Caelari des Ordens des Lichts; sanft, aber nicht sentimental; Heilung ist für sie Wiederherstellung von Ordnung.
- **Orun:** Caelari des Ordens der Wacht; streng, misstrauisch, pflichtorientiert; akzeptiert Stärke nur zusammen mit Selbstbegrenzung.

## 1.3 Dialogregeln
- Figuren erklären niemals Systeme, die der Spieler im UI bereits eindeutig sieht, in langen Infodumps.
- Tutorialinformationen werden auf kurze Sätze verteilt und mit Codexlinks ergänzt.
- Mysteryfiguren dürfen irren, lügen, unvollständige Quellen besitzen oder unterschiedliche Interpretationen vertreten; technische Kernregeln des Gameplays dürfen dadurch jedoch nicht unklar werden.
- Bei großen Enthüllungen wird die Kamera kurz geführt; der Spieler erhält danach sofort wieder Kontrolle.
- Keine Hauptmission verlangt einen Dialogentscheid innerhalb eines engen Echtzeitfensters.
- Alle gesprochenen Zeilen werden untertitelt und im Storylog gespeichert.


## 1.4 Verbindlicher Individualitätsstandard für Missionsdrehbücher

**OVERRIDE gegenüber älteren Generator-/Templateformulierungen:** Keine Haupt- oder feste Nebenmission darf im finalen Spiel wie eine umbenannte Kopie einer anderen Mission klingen. Wiederkehrende technische Regeln wie Save/Load, bereits erfüllte Ziele und Fail-Safes dürfen identisch sein; **Dramaturgie, Dialog, Ereignisfolge und emotionale Funktion müssen missionsspezifisch sein**.

- Fortschrittsdialoge werden nicht pauschal an 25/50/75 Prozent gehängt. Sie werden an echte Zustandswechsel der jeweiligen Mission gebunden: erste funktionsfähige Teilkette, erste Lieferung, erste stabile Versorgung, Fund eines konkreten Objekts, Reaktion einer Fraktion, Beginn einer Expedition, Eintritt eines Wettersystems, Abschluss eines Forschungsversuchs oder vergleichbare echte Ereignisse.
- Jede Leitfigur spricht in der in 1.2 definierten Stimme. Austauschbare Sätze, die ohne Bedeutungsverlust einer anderen Figur gegeben werden könnten, werden beim Narrative-QA markiert.
- Eine Mystery-Enthüllung darf nicht in einem Abschlussdialog bloß wiederholt werden. Der Abschluss muss zeigen, **was sich für Figuren, Weltverständnis oder nächste Entscheidung verändert hat**.
- Tutorialmissionen dürfen klein sein, müssen aber einen glaubwürdigen Anlass besitzen. Ein Gebäude wird nicht gebaut, „weil das Tutorial es verlangt“, sondern weil eine konkrete Versorgung, Person, Krise, Reise oder politische Entscheidung es notwendig macht.
- Große Enthüllungen brauchen Setup, Bestätigung und Konsequenz. Kein Drachen-, Riesen-, Caelari-, Äther- oder Ultima-Reveal wird allein durch einen Codex-Popup abgehandelt.
- Der Storymodus verwendet echte Spielergebäude und echte Wirtschaftszustände. Vorgefertigte Filmkulissen dürfen den eigenen Spielstand nur ergänzen, niemals ersetzen.
- Vor Voice-Lock wird jede Mission auf Wiederholungen mit den zehn unmittelbar benachbarten Missionen geprüft. Wiederholte Satzmuster, identische Beatfolge und identische Schlussformeln werden überarbeitet.


### Automatischer Dialog-/Template-Lint vor Story-Content-Lock

Für alle 174 festen Missionen gilt zusätzlich:

- Figurspezifische Dialogzeilen dürfen nicht unverändert in mehreren unabhängigen Missionen wiederverwendet werden, außer sie sind ausdrücklich als wiederkehrende Catchphrase markiert.
- Generische Produktions-/Abschlusszeilen dürfen nicht serienweise individuellen Dialog ersetzen.
- Technische Standardregeln zu Save/Load, bereits erfüllten Zielen, Fail-Safes und Questtracker dürfen zentral wiederverwendet werden; Dramaturgie, Mystery-Beats, Konflikte, Wendepunkte und Abschlussdialoge müssen missionsspezifisch sein.
- Vor Story-Content-Lock läuft ein automatischer Text-Lint über alle Missionsdialoge. Identische/nahezu identische Zeilen, übermäßig ähnliche Beatfolgen und ungewollt gleich klingende Sprecher werden als Fehlerliste ausgegeben.
- Jede Hauptmission braucht einen eigenen narrativen Kern: neue Information, Figurenentwicklung, Konflikt, Entscheidung, Konsequenz oder einzigartige Inszenierung.
- Ein Missionsdrehbuch gilt nicht als final, wenn lediglich Ziele, Ortsnamen oder Sprecher in einem gemeinsamen Template ausgetauscht wurden.

# 1.5 Voice-Lock & Natural Dialogue — Content Pass 07

**VERBINDLICH:** Alle 174 festen Missionen wurden in diesem Pass von den früheren wiederholten Auftakt-, Prozent- und Abschlussmustern gelöst.

- Jeder Szenenauftakt ist an Region, Missionsort, Primärziel und tatsächlichen Spielstand gebunden.
- Dialoge verwenden die festgelegte Figurenstimme und nennen den konkreten Konflikt beziehungsweise Prüfpunkt der Mission.
- Storybeats hängen an echten Zustandswechseln statt an pauschalen 25/50/75-Prozent-Marken.
- Der Wendepunkt wird am konkreten Missionsobjekt, Fund, Netz, Vertrag oder Ereignis sichtbar.
- Abschlussdialoge beziehen sich auf das tatsächliche Ergebnis und die Haltung der beteiligten Figur.
- Wiederverwendet werden nur technische Standardregeln zu Save/Load, Fail-Safes und Questtracker.

**Voice-Lock-Regel:** Vor finaler Sprachaufnahme erfolgt zusätzlich ein autorischer Line-Edit pro Mission für Timing, Schauspielbarkeit, Humor, regionale Wortwahl, Subtext und direkte Übergänge zwischen benachbarten Missionen. Dieser Line-Edit darf nicht wieder zu gemeinsamen Generator-Dialogen zurückkehren.


# 2. Hauptstory — Bekannte Welt und Australis

Die folgenden Missionen sind vollständig verpflichtende Hauptstorymissionen. Die Reihenfolge innerhalb Meridia/Orientia/Aferia kann nach `ST-OCC-024` teilweise frei sein; der globale Konvergenzbogen startet erst, wenn alle drei Beweisfragmente vorliegen.


## 1.5 Voice-Lock v1.0 — Figurenstimmen vor Sprachaufnahme

**VERBINDLICH ab Content Pass 07:** Gesprochener Missionsdialog darf nicht wie Questtracker-Prosa klingen. Figuren nennen Missions-IDs oder Missionstitel nicht im Dialog, lesen keine Primärziele wörtlich vor und verwenden keine Serienformeln wie „Prüfpunkt“, „der belastbare Zustand betrifft konkret“ oder „als Bezugspunkt bleibt“. Technische Zieltexte bleiben im Questtracker/Codex.

Leitstimmen:

- **Helena Voss:** sachlich, menschlich, verantwortungsorientiert. Denkt zuerst an Bewohner und dauerhafte Zustände; keine bürokratische Floskelmaschine.
- **Elias Renn:** knapp, technisch, trocken. Spricht über Last, Reserve, Fehlerwege und Reparierbarkeit; Humor entsteht aus Ingenieursnüchternheit.
- **Marek Vale:** seemännisch-pragmatisch, trocken, erfahren. Denkt in Kurs, Reserve, Rückweg, Hafen und zweiter Fahrt; verachtet unnötiges Heldentum.
- **Dr. Mira Halden:** wissenschaftlich neugierig und skeptisch. Trennt Befund, Hypothese und Gegenprobe; freut sich über Widersprüche mehr als über bequeme Bestätigung.
- **Inés Araya:** gemeinschaftlich und flussorientiert. Denkt an Ober-/Unterlauf, Verteilung und langfristige Folgen für andere.
- **Lian Sora:** ruhig, archivisch präzise. Achtet auf Herkunft, Lücken, Widersprüche und das Recht späterer Leser auf eine unverfälschte Quelle.
- **Amara Selu:** direkt, gerechtigkeitsorientiert. Fragt, wer Zugang erhält, wer die Kosten trägt und was Randbezirke tatsächlich bekommen.
- **Dr. Edda Vey:** polar-erfahren, trocken, kompromisslos bei Sicherheit. Reserve und Rückweg sind für sie keine Boni, sondern Grundfunktion.
- **Serin Ael:** höflich, kontrolliert, philosophisch-pragmatisch. Trennt Zugang, Besitz, Zustimmung und Verantwortung im Äthernetz.
- **Ilyra Venn:** bioätherisch und regenerationsorientiert. Behandelt lebende Materialien als Beziehung statt Vorrat.
- **Kharum Tor:** knapp, gewichtsbewusst, gemeinschaftlich. Metaphern stammen aus Last, Stein, Fundament und geteiltem Tragen.
- **Vaelis:** wach, hitze- und flugbewusst. Betont Grenzen, freiwillige drakonische Zusammenarbeit und kontrollierten Abstand.
- **Neris Pell:** ruhig, tiefseepraktisch. Denkt in Druck, Schleusen, Reserve und Rückkehr.
- **Iriath:** präzise, ethisch und institutionell. Trennt technische Möglichkeit von Zustimmung, Macht von Recht und Versiegelung von Kontrolle.

### Voice-Lock-Regeln

- Direkter Dialog ist im Regelfall kürzer als der erklärende Designtest darunter.
- Kein Charakter erklärt UI, XML, Prozentwerte, Savegames oder Questlogik in der Weltfiktion.
- Wiederkehrende Gedanken sind erlaubt, aber nicht als identische Satzschablone in jeder Mission.
- Figuren dürfen sich widersprechen; ihre Stimmen sollen nicht zu einer einzigen „vernünftigen Designerstimme“ verschmelzen.
- Humor entsteht aus Charakter und Situation, nicht aus Meta-Witzen über Quests oder Spielerhandlungen.
- Große Enthüllungen erhalten bewusst mehr Stille, Reaktion und Subtext; nicht jede Information wird sofort erklärt.
- Vor finaler Sprachaufnahme folgt weiterhin ein **Performance/Voice Recording Pass**, der Atemlängen, Unterbrechungen, emotionale Betonung und Synchronlänge prüft, aber keine Lore mehr umschreibt.


## ST-OCC-001 — Ein Dach vor Einbruch der Nacht

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Die ersten Familien erreichen den Gründungsort früher als erwartet. Helena macht klar, dass die Siedlung nicht aus einem Symbol auf der Karte, sondern aus Menschen besteht, die heute Abend ein Dach brauchen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte das Kontor, eine Straße und das erste Bauernhaus.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Die ersten Familien erreichen den Gründungsort früher als erwartet. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Eine Stadt entsteht nicht im Planungsraum. Sie entsteht dort, wo Menschen sich morgen noch auf unsere Entscheidung verlassen müssen.«

**Elias Renn:** »Kontor, Weg, Dach: erst die Anschlüsse, dann die Höhe. Sonst suchen wir morgen Fehler im Dunkeln.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: noch kein offenes Mystery; beim Entladen wird eine alte Messingplatte mit unbekanntem Sternsymbol zwischen gebrauchten Werkzeugkisten gefunden. **Helena Voss:** »Das wird dokumentiert und getrennt von allen Vermutungen gelagert. Mira bekommt zuerst die Fakten.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Bauernhaus, Grundstraßenbau, Storylogbuch**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Bauernhaus, Grundstraßenbau, Storylogbuch.
- Storyflag: `st_occ_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-002 — Der erste Markt

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Ungeordnete Vorräte führen zu Streit und Verlusten. Der Markt wird als soziale Mitte eingeführt, nicht nur als Reichweitengebäude.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte einen Markt und verbinde mindestens drei Bauernhäuser mit ihm.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Ungeordnete Vorräte führen zu Streit und Verlusten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Marek Vale:** »Wenn drei Häuser sauber erreicht werden, sehen wir sofort, ob die Verteilung funktioniert.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Zeit und Reserve liegen im Plan. Wenn die zweite Fahrt genauso aussieht, haben wir eine Route.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: eine ältere Siedlerin erkennt das Sternsymbol der Messingplatte aus einer Familienerzählung, will aber nichts Sicheres behaupten. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »So wird aus Entfernung Handel: nicht durch Mut, sondern durch Wiederholbarkeit.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Marktsystem, Bedürfnisanzeige**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Marktsystem, Bedürfnisanzeige.
- Storyflag: `st_occ_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-003 — Hafer für hundert Mägen

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Die Startvorräte reichen nicht. Der Spieler lernt Landwirtschaft und Produktionsketten, während die Siedlung erstmals von der eigenen Wirtschaft leben muss.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue die frühe Hafergrütze-Produktionskette und halte sie stabil.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Die Startvorräte reichen nicht. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Elias Renn:** »Technisch heißt das für mich: Ich rechne mit dem Verbrauch, nicht mit dem besten Ertrag. Danach dimensionieren wir Reserve.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: beim Pflügen taucht ein schwarzer, ungewöhnlich glatter Stein auf; Elias hält ihn zunächst für Schlacke, obwohl es hier nie einen Ofen gab. **Helena Voss:** »Sichert den Fund und schreibt auf, wer ihn wo gesehen hat. Keine Gerüchte, bevor wir wissen, womit wir es zu tun haben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Helena Voss:** »Für die Stadt ist dabei klar: Die Versorgung trägt aus eigener Produktion. Das ist der Punkt, an dem eine Siedlung unabhängiger wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hafergrütze, frühe Landwirtschaft**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hafergrütze, frühe Landwirtschaft.
- Storyflag: `st_occ_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-004 — Holz, Ziegel, Zukunft

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Siedlung wächst über improvisierte Hütten hinaus. Elias führt das Prinzip ein, dass jede Entwicklungsstufe neue Baumaterialien verlangt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte Förster/Holzverarbeitung und die erste Ziegelkette; lege Materialreserven an.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Unsere Siedlung wächst über improvisierte Hütten hinaus. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Gib mir klare Anschlüsse, Reserve und einen Weg zum Reparieren. Dann kann ich mit fast allem arbeiten.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir testen den Engpass, nicht den Prospektwert. Dort entscheidet sich die Kette.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der schwarze Stein lässt sich mit normalem Werkzeug kaum ritzen; Mira fordert eine spätere Analyse. **Elias Renn:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Einmal unter voller Last. Wenn nichts aus dem Takt fällt, ist es bereit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Elias Renn:** »Technisch heißt das für mich: Die Kette läuft vom Rohstoff bis zum Ergebnis ohne versteckten Engpass.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ziegel, erweitertes Bauen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ziegel, erweitertes Bauen.
- Storyflag: `st_occ_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-005 — Hundert Stimmen

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die Siedlung wird zu einer Gemeinschaft. Mira trifft ein, um die Küste neu zu vermessen und wird als langfristige Hauptfigur eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche 100 reguläre Bauern bei stabiler Pflichtversorgung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Unsere Siedlung wird zu einer Gemeinschaft. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Dr. Mira Halden:** »Der Aufstieg zählt nur, wenn die bestehende Stadt ihn mitträgt.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: miras Kompass zeigt in derselben Nacht mehrere Sekunden nach Süden, obwohl die Nadel mechanisch intakt ist. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Helena Voss:** »Der nächste gesellschaftliche Schritt steht, ohne die vorherige Versorgung zu opfern.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Kartographie-Aufträge, erste Storyarchive**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Kartographie-Aufträge, erste Storyarchive.
- Storyflag: `st_occ_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-006 — Das Schiff ohne Flagge

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Marek Vale · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Ein beschädigtes Handelsschiff sucht Schutz. Der Spieler hilft, ohne das Schiff besitzen zu müssen, und lernt Hafenlogik.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue einen funktionsfähigen Hafenpier und lagere Reparaturmaterial für ein havariertes Handelsschiff ein.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Ein beschädigtes Handelsschiff sucht Schutz. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Routen scheitern selten am Horizont. Meist scheitern sie an dem, was jemand am Kai vergessen hat.«

**Helena Voss:** »Für die Stadt ist dabei klar: Laden, Reserve, Zielbestand, Rückfahrt. Wenn das automatisch funktioniert, haben wir Handel statt Botendienst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal in Logbuchdaten, die nicht mit der bekannten Karte übereinstimmen konkret: die Mannschaft berichtet von einer kalten Nacht, in der am südlichen Horizont ein unbewegliches Licht stand. **Marek Vale:** »Kompass, Zeit, Wetter, Kurs. Alles notieren. Wenn es wieder passiert, will ich denselben Fehler zweimal sehen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Lasst den Hafen einmal unter voller Abfertigung arbeiten. Dann sehen wir, ob die Zahlen ehrlich waren.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Marek Vale:** »Die Route arbeitet wiederholbar. Ab jetzt ist Entfernung eine Kostenfrage, keine Handarbeit.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hafen, erste externe Handelskontakte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hafen, erste externe Handelskontakte.
- Storyflag: `st_occ_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-007 — Ein Kompass lügt nicht

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Mira weigert sich, die Kompassabweichung als Aberglauben abzutun. Der Spieler lernt Forschungs-/Messaufträge.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte einen kleinen Messposten und führe drei Messungen an unterschiedlichen Punkten der Insel durch.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Mira weigert sich, die Kompassabweichung als Aberglauben abzutun. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Nichts wäre mir lieber, als mich hier zu irren. Also bauen wir den Versuch so, dass wir das auch merken würden.«

**Elias Renn:** »Technisch heißt das für mich: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Keine auffälligen Spitzen. Gut. Ich prüfe noch den schwächsten Anschluss, dann glaube ich dem Ganzen.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer reproduzierbaren Abweichung mehrerer Instrumente konkret: alle Abweichungen zeigen nicht zum magnetischen Norden, sondern leicht versetzt nach Süden. **Dr. Mira Halden:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Messposten, Forschungsprojekte Stufe 1**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Messposten, Forschungsprojekte Stufe 1.
- Storyflag: `st_occ_007_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_007_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-008 — Die Straße zum Hafen

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Eine dringende Lieferung zeigt, dass bessere Straßen Reichweite und Geschwindigkeit erhöhen, ohne Verkehrsstaus zu simulieren.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Upgrade eine zusammenhängende Hauptverbindung auf Straßenstufe 2.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Eine dringende Lieferung zeigt, dass bessere Straßen Reichweite und Geschwindigkeit erhöhen, ohne Verkehrsstaus zu simulieren. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Wenn wir schon bauen, bauen wir so, dass man später noch drankommt. Provisorien werden sonst sehr schnell dauerhaft.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Laden, Reserve, Zielbestand, Rückfahrt. Wenn das automatisch funktioniert, haben wir Handel statt Botendienst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Keine unnötige Wartezeit. Gut. Einmal noch mit voller Ladung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: in einer gelieferten alten Seekiste befindet sich ein Kartenfragment mit absichtlich weggeschnittenem Südrand. **Elias Renn:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Ich will einen Test ohne unsere Hände am Schalter. Wenn es dann läuft, bin ich zufrieden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Straßenupgrade 2**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Straßenupgrade 2.
- Storyflag: `st_occ_008_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_008_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-009 — Erste Handelsroute

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Marek Vale · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Der Spieler lernt die globale Handelslogik anhand eines praktischen Versorgungsproblems.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Richte eine automatische Handelsroute mit Mindestreserve und Zielbestand ein.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Wir lernt die globale Handelslogik anhand eines praktischen Versorgungsproblems. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Auf einer Karte ist jede Strecke kurz. Auf See zählt, ob man mit Fracht, Wetter und Reserve auch wieder zurückkommt.«

**Helena Voss:** »Für die Stadt ist dabei klar: Laden, Reserve, Zielbestand, Rückfahrt. Wenn das automatisch funktioniert, haben wir Handel statt Botendienst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der Händler, der die Route anbietet, kennt das Symbol auf dem Kartenfragment und nennt es „das geschlossene Tor“, behauptet aber, nur Seemannsgarn zu zitieren. **Marek Vale:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Noch eine Fahrt mit echter Last. Danach weiß ich, ob das eine Route oder nur ein glücklicher Versuch war.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Automatische Handelsrouten**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Automatische Handelsrouten.
- Storyflag: `st_occ_009_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_009_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-010 — Fünfhundert

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Der gesellschaftliche Aufstieg wird als echte Veränderung inszeniert: Werkstätten, Lohnarbeit und neue Ansprüche entstehen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche 500 reguläre Bauern und erfülle die Freischaltbedingungen der Arbeiterstufe.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Der gesellschaftliche Aufstieg wird als echte Veränderung inszeniert: Werkstätten, Lohnarbeit und neue Ansprüche entstehen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Elias Renn:** »Technisch heißt das für mich: Mehr Bewohner bedeuten mehr als eine Zahl. Versorgung und Arbeitskräfte müssen denselben Schritt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: mira findet in einem alten Hafenregister dieselbe südliche Abweichung über Jahrzehnte hinweg. **Helena Voss:** »Sichert den Fund und schreibt auf, wer ihn wo gesehen hat. Keine Gerüchte, bevor wir wissen, womit wir es zu tun haben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Arbeiterstufe**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Arbeiterstufe.
- Storyflag: `st_occ_010_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_010_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-011 — Schichtwechsel

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Der Spieler erlebt den horizontalen Hausaufstieg und den Unterschied zwischen Klasse und Verdichtung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte die ersten Arbeiterhäuser und eine arbeitergebundene Produktionskette.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Wir erlebt den horizontalen Hausaufstieg und den Unterschied zwischen Klasse und Verdichtung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Ich traue keinem System, das nur im Leerlauf gut aussieht. Wir bauen es so, dass der erste Fehler nicht gleich alles mitreißt.«

**Helena Voss:** »Für die Stadt ist dabei klar: Mehr Bewohner bedeuten mehr als eine Zahl. Versorgung und Arbeitskräfte müssen denselben Schritt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »So kann man damit arbeiten. Lasst es laufen; ich will sehen, was nach der ersten Belastung übrig bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Arbeiter bringt Mira ein Metallfragment aus einer importierten Maschinenkiste; es passt chemisch zu keinem gelisteten Lieferanten. **Elias Renn:** »Das Material gehört nicht hierher. Ich will eine Probe, aber niemand schleift oder erhitzt es, bevor Mira draufgeschaut hat.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Ich will einen Test ohne unsere Hände am Schalter. Wenn es dann läuft, bin ich zufrieden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Arbeiterproduktion**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Arbeiterproduktion.
- Storyflag: `st_occ_011_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_011_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-012 — Rauch über den Dächern

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Eine kontrollierbare Industriekrise führt Dienstqualität und Resilienz ein. Kein willkürlicher Totalverlust.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue Brandschutz und stabilisiere eine wachsende Industriezone.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Eine kontrollierbare Industriekrise führt Dienstqualität und Resilienz ein. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Material ist selten das Problem. Schlechte Übergänge sind es. Genau die suche ich zuerst.«

**Helena Voss:** »Notbetrieb zuerst, Optimierung danach.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Hitze des Brandes verändert das fremde Metallfragment nicht messbar. **Elias Renn:** »Ich kann euch sagen, was es nicht ist. Für den Rest brauche ich Messwerte.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Feuerwehr / frühe Katastrophenlogik**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Feuerwehr / frühe Katastrophenlogik.
- Storyflag: `st_occ_012_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_012_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-013 — Bürger einer Stadt

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die Siedlung wird zur Stadt; Verwaltung, Post und Bildung werden erstmals politisch relevant.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erfülle die Bürgerfreischaltung und entwickle einen Kernbezirk mit Bürgerhäusern.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Unsere Siedlung wird zur Stadt; Verwaltung, Post und Bildung werden erstmals politisch relevant. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Ich will keine schöne Momentaufnahme. Ich will eine Lösung, die morgen früh noch funktioniert.«

**Dr. Mira Halden:** »Der Aufstieg zählt nur, wenn die bestehende Stadt ihn mitträgt.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein neu eingerichtetes Archiv entdeckt einen Reisebericht mit einer Passage über „Licht, das hinter dem Eis nicht unterging“. **Helena Voss:** »Sichert den Fund und schreibt auf, wer ihn wo gesehen hat. Keine Gerüchte, bevor wir wissen, womit wir es zu tun haben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Bürgerstufe, Stadtarchiv**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Bürgerstufe, Stadtarchiv.
- Storyflag: `st_occ_013_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_013_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-014 — Die Karte mit der Naht

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Mira legt Karten verschiedener Jahrgänge übereinander. Eine identische Naht verdeckt bei allen denselben südlichen Bereich.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Sammle drei historische Karten aus Handel/Archiv und errichte eine Kartographische Werkstatt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Mira legt Karten verschiedener Jahrgänge übereinander. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal in den wiederhergestellten Schichten des Archivs konkret: erstmals wird klar, dass die Auslassung systematisch sein könnte, ohne Ultima zu benennen. **Dr. Mira Halden:** »Wenn diese Spur echt ist, wird sie auch morgen noch gegen eine Gegenprobe bestehen. Sichert alles.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Kartographische Werkstatt**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Kartographische Werkstatt.
- Storyflag: `st_occ_014_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_014_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-015 — Hände und Meisterschaft

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Handwerkliche Spezialisierung und Qualitätsproduktion werden Teil des Stadtwachstums.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schalte Handwerker frei und errichte eine komplexere 2–3-stufige Produktionskette.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Handwerkliche Spezialisierung und Qualitätsproduktion werden Teil des Stadtwachstums. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Gib mir klare Anschlüsse, Reserve und einen Weg zum Reparieren. Dann kann ich mit fast allem arbeiten.«

**Helena Voss:** »Für die Stadt ist dabei klar: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Meister erkennt am fremden Fragment Bearbeitungsspuren, die mit bekannten Werkzeugen kaum herstellbar wären. **Elias Renn:** »Ich kann euch sagen, was es nicht ist. Für den Rest brauche ich Messwerte.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Noch ein Abschalten und sauberer Wiederanlauf. Danach nenne ich es betriebsfähig.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Elias Renn:** »Technisch heißt das für mich: Die Kette läuft vom Rohstoff bis zum Ergebnis ohne versteckten Engpass.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Handwerkerstufe**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Handwerkerstufe.
- Storyflag: `st_occ_015_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_015_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-016 — Das Haus der Fragen

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Der Spieler erhält die erste echte Forschungsantwort: Das Material ist real und künstlich, aber Herkunft und Verfahren bleiben unbekannt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte ein Forschungs-/Bildungsgebäude und schließe das Projekt „Unbekannte Legierung“ ab.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Wir erhält die erste echte Forschungsantwort: Das Material ist real und künstlich, aber Herkunft und Verfahren bleiben unbekannt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Nichts wäre mir lieber, als mich hier zu irren. Also bauen wir den Versuch so, dass wir das auch merken würden.«

**Elias Renn:** »Technisch heißt das für mich: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Keine auffälligen Spitzen. Gut. Ich prüfe noch den schwächsten Anschluss, dann glaube ich dem Ganzen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: im Material liegen regelmäßige mikroskopische Strukturen, die wie Resonanzkanäle wirken. **Dr. Mira Halden:** »Nicht anfassen, nicht reinigen, nicht „verbessern“. Der Zustand des Fundes ist selbst eine Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Forschungspfad Anomalien**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Forschungspfad Anomalien.
- Storyflag: `st_occ_016_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_016_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-017 — Kaufleute der Ferne

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Marek Vale · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Wirtschaftlicher Wohlstand öffnet Kontakte zu fernen Regionen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schalte Kaufleute frei und erreiche ein festgelegtes interregionales Handelsvolumen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Wirtschaftlicher Wohlstand öffnet Kontakte zu fernen Regionen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Ein Schiff ist nur dann schnell, wenn es nicht wegen schlechter Planung zweimal fahren muss.«

**Helena Voss:** »Für die Stadt ist dabei klar: Laden, Reserve, Zielbestand, Rückfahrt. Wenn das automatisch funktioniert, haben wir Handel statt Botendienst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Der Anfang steht. Jetzt bitte nichts schönreden, falls die Versorgung an einer anderen Stelle nachgibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: mehrere Kapitäne kennen unterschiedliche Legenden über einen verbotenen Süden. Die Details widersprechen sich, das Motiv nicht. **Marek Vale:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Der Rückweg fehlt mir noch. Eine Strecke ist erst fertig, wenn beide Richtungen funktionieren.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Marek Vale:** »So wird aus Entfernung Handel: nicht durch Mut, sondern durch Wiederholbarkeit.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Kaufleutestufe, Expeditionsplanung**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Kaufleutestufe, Expeditionsplanung.
- Storyflag: `st_occ_017_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_017_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-018 — Ein Rat, zwei Wahrheiten

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die erste größere Storyentscheidung verändert Vertrauen und spätere Dialoge, darf aber keine Inhalte dauerhaft sperren.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte höhere Verwaltung und entscheide, ob die Anomalien öffentlich diskutiert oder zunächst wissenschaftlich geprüft werden.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Die erste größere Storyentscheidung verändert Vertrauen und spätere Dialoge, darf aber keine Inhalte dauerhaft sperren. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Eine Stadt entsteht nicht im Planungsraum. Sie entsteht dort, wo Menschen sich morgen noch auf unsere Entscheidung verlassen müssen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: unabhängig von der Wahl erhält Mira eine anonyme Kopie eines alten Navigationsprotokolls mit Koordinaten weit südlich normaler Routen. **Helena Voss:** »Niemand baut daraus heute eine Legende. Wir sichern es, prüfen es und reden dann weiter.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Verwaltungsentscheidung, Reparaturpfade**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Verwaltungsentscheidung, Reparaturpfade.
- Storyflag: `st_occ_018_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_018_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-019 — Patrizier und Archive

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die wachsende Gesellschaft kann nun große historische Bestände erschließen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schalte Patrizier frei und stelle hochwertige Verwaltung, Recht, Kultur und Archivkapazität bereit.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Die wachsende Gesellschaft kann nun große historische Bestände erschließen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein versiegelter Bestand nennt eine Expedition, die offiziell nie stattgefunden haben soll. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Patrizierstufe, Hocharchiv**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Patrizierstufe, Hocharchiv.
- Storyflag: `st_occ_019_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_019_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-020 — Die verschwundene Expedition

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Der Spieler muss Informationen aus mehreren normalen Systemen kombinieren, nicht nur Dialoge anklicken.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Rekonstruiere über Handel, Archive und Forschung die Route einer verschollenen Expedition.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Wir muss Informationen aus mehreren normalen Systemen kombinieren, nicht nur Dialoge anklicken. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wenn das wirklich ungewöhnlich ist, muss es eine Gegenprobe aushalten. Sonst war es nur ein interessanter Nachmittag.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Ich plane die Reserve so, als würde das Wetter gegen uns stimmen. Meistens tut es das irgendwann.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Zeit und Reserve liegen im Plan. Wenn die zweite Fahrt genauso aussieht, haben wir eine Route.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die letzte bestätigte Position liegt südlich der bekannten Welt; danach endet das Log nicht mit Untergang, sondern mit „Land voraus“. **Dr. Mira Halden:** »Das passt zu keinem Datensatz, den ich erwartet habe. Genau deshalb dokumentieren wir jeden Schritt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »So wird aus Entfernung Handel: nicht durch Mut, sondern durch Wiederholbarkeit.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Langstreckenexpeditionen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Langstreckenexpeditionen.
- Storyflag: `st_occ_020_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_020_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-021 — Magnaten des Fortschritts

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Kapital und Großindustrie ermöglichen erstmals die technische Vorbereitung globaler Expeditionen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schalte Magnaten frei und errichte moderne Industrie-/Energieinfrastruktur.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Kapital und Großindustrie ermöglichen erstmals die technische Vorbereitung globaler Expeditionen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Ich will wissen, was passiert, wenn ein Teil ausfällt. Wenn die Antwort „alles“, sind wir noch nicht fertig.«

**Helena Voss:** »Für die Stadt ist dabei klar: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Magnatenlabor reproduziert Teile des fremden Materials nicht, misst aber eine ungewöhnliche Reaktion auf elektromagnetische Felder. **Elias Renn:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Einmal unter voller Last. Wenn nichts aus dem Takt fällt, ist es bereit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Magnatenstufe, Hochindustrie**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Magnatenstufe, Hochindustrie.
- Storyflag: `st_occ_021_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_021_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-022 — Der südliche Impuls

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Mehrere Stationen registrieren gleichzeitig einen kurzen, nicht natürlichen Impuls aus dem Süden.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue ein Langstreckenmessnetz und halte Energie sowie Kommunikation stabil.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Mehrere Stationen registrieren gleichzeitig einen kurzen, nicht natürlichen Impuls aus dem Süden. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Elias Renn:** »Technisch heißt das für mich: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Leistungsbilanz ohne sichtbaren Brennstoff konkret: das Signal besitzt eine wiederkehrende mathematische Struktur; es ist nicht bloß Wetter. **Dr. Mira Halden:** »Wenn diese Spur echt ist, wird sie auch morgen noch gegen eine Gegenprobe bestehen. Sichert alles.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Langstreckenkommunikation**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Langstreckenkommunikation.
- Storyflag: `st_occ_022_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_022_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-023 — Elite der Verantwortung

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die höchsten gesellschaftlichen Systeme werden nicht als Luxus, sondern als Verantwortung für komplexe Weltprojekte eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schalte Elite frei und errichte die erste internationale Koordinationsstruktur.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand der jungen Siedlung zwischen Kontor, Materialstapeln und den ersten bewohnten Häusern. Die höchsten gesellschaftlichen Systeme werden nicht als Luxus, sondern als Verantwortung für komplexe Weltprojekte eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Mehr Bewohner bedeuten mehr als eine Zahl. Versorgung und Arbeitskräfte müssen denselben Schritt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: mira legt dem Rat erstmals die Hypothese vor, dass die bekannte Welt kartographisch unvollständig sein könnte. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Helena Voss:** »Der nächste gesellschaftliche Schritt steht, ohne die vorherige Versorgung zu opfern.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Elitestufe, Weltkoordinationsrat**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Elitestufe, Weltkoordinationsrat.
- Storyflag: `st_occ_023_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_023_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-OCC-024 — Charta der Expeditionen

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Der Spieler erhält echte Wahlfreiheit bei der Reihenfolge Meridia/Orientia/Aferia; alle drei werden benötigt, aber keine ist bloß Nebenstation.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stelle eine globale Expeditionscharta auf, baue Vorräte und entscheide die erste Zielregion.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Wir erhält echte Wahlfreiheit bei der Reihenfolge Meridia/Orientia/Aferia; alle drei werden benötigt, aber keine ist bloß Nebenstation. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Ich plane die Reserve so, als würde das Wetter gegen uns stimmen. Meistens tut es das irgendwann.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der Kurs hält. Jetzt prüfe ich, ob der Hafen beim Rücklauf genauso schnell ist wie beim Auslaufen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Charta enthält einen gesperrten späteren Abschnitt „Australis“, der erst nach genügend Beweisen geöffnet wird. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Freie Reihenfolge Meridia/Orientia/Aferia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Freie Reihenfolge Meridia/Orientia/Aferia.
- Storyflag: `st_occ_024_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_occ_024_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-001 — Der breite Fluss

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Der Erstkontakt basiert auf Handel und lokaler Zustimmung, nicht auf Besitznahme.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte einen Hafenposten und versorge eine erste lokale Siedlung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einem bewirtschafteten Flussabschnitt, bevor die Kamera auf den konkreten Missionsort absinkt. Der Erstkontakt basiert auf Handel und lokaler Zustimmung, nicht auf Besitznahme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der schnellste Ausbau ist wertlos, wenn er beim nächsten Hochwasser gegen uns arbeitet.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Wir messen am schlechtesten versorgten Punkt; der Durchschnitt kann uns später trösten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der Kurs hält. Jetzt prüfe ich, ob der Hafen beim Rücklauf genauso schnell ist wie beim Auslaufen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein alter Flussstein trägt dasselbe Grundsymbol wie der Fund aus Occidentia, aber in anderer Ausführung. **Inés Araya:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Ich will die Werte vom Unterlauf noch einmal sehen. Danach können wir abschließen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Inés Araya:** »Dann können wir wachsen. Aber wir behalten im Kopf, wohin das Wasser nach uns weiterzieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Meridia-Basisbau**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Meridia-Basisbau.
- Storyflag: `st_mer_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-002 — Wasser für die Felder

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Meridias eigene Landwirtschaft wird über ein konkretes Versorgungsproblem gelernt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue Bewässerung und stabilisiere eine lokale Nahrungskette durch Regen-/Trockenzeit.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einem bewirtschafteten Flussabschnitt, bevor die Kamera auf den konkreten Missionsort absinkt. Meridias eigene Landwirtschaft wird über ein konkretes Versorgungsproblem gelernt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Wasser ist hier Straße, Vorrat und Grenze zugleich. Wer nur eine davon sieht, plant zu kurz.«

**Elias Renn:** »Technisch heißt das für mich: Ich rechne mit dem Verbrauch, nicht mit dem besten Ertrag. Danach dimensionieren wir Reserve.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: inés erzählt von Bauern, deren Großeltern Lichter über dem südlichen Meer sahen; sie behandelt es als Folklore. **Inés Araya:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Fast fertig. Jetzt prüfen wir, ob niemand außerhalb unseres Blickfelds die Rechnung bezahlt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Inés Araya:** »So kann der Fluss für uns arbeiten, ohne dass wir so tun, als gehörte er uns.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Bewässerung**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Bewässerung.
- Storyflag: `st_mer_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-003 — Die Last des Stroms

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Flusslogistik wird als regionale Stärke eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Nutze Flusspiers und eine Flussroute, um Waren zwischen zwei Siedlungen zu bewegen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Bewässerungsgräben, Marktständen und dem braunen Wasser des großen Stroms. Flusslogistik wird als regionale Stärke eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der Fluss gehört nicht dem ersten, der einen Pfahl hineinschlägt. Wir bauen so, dass auch flussabwärts noch jemand leben kann.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Zeit und Reserve liegen im Plan. Wenn die zweite Fahrt genauso aussieht, haben wir eine Route.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein geborgenes Kanu besitzt einen alten Metallbeschlag aus derselben unbekannten Legierungsfamilie. **Inés Araya:** »Wenn das hier schon vor uns eine Funktion hatte, will ich wissen, welche — nicht nur, wie wir es verwenden können.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Noch ein kompletter Wasserzyklus. Wenn Ober- und Unterlauf stabil bleiben, gebe ich mein Ja.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Inés Araya:** »So kann der Fluss für uns arbeiten, ohne dass wir so tun, als gehörte er uns.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Flusspiers**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Flusspiers.
- Storyflag: `st_mer_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-004 — Kakao und Kredit

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Mission verhindert koloniale Einbahnlogik: Export darf lokale Bedürfnisse nicht zerstören.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte eine Exportkette für Kakao/Kaffee und halte lokale Versorgung gleichzeitig stabil.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einem bewirtschafteten Flussabschnitt, bevor die Kamera auf den konkreten Missionsort absinkt. Die Mission verhindert koloniale Einbahnlogik: Export darf lokale Bedürfnisse nicht zerstören. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der schnellste Ausbau ist wertlos, wenn er beim nächsten Hochwasser gegen uns arbeitet.«

**Helena Voss:** »Ich will die zweite Fahrt genauso sauber wie die erste. Dann dürfen wir sie Route nennen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Händlerarchiv erwähnt eine südliche Route, die „zu schnell“ gewesen sei, um mit damaligen Schiffen möglich zu sein. **Inés Araya:** »Keine Souvenirs. Alles bleibt zusammen, bis wir verstehen, warum es genau hier liegt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Ich will die Werte vom Unterlauf noch einmal sehen. Danach können wir abschließen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Inés Araya:** »Gut. Der Nutzen bleibt hier, ohne dass wir ihn jemand anderem wegnehmen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Meridia-Exportverträge**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Meridia-Exportverträge.
- Storyflag: `st_mer_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-005 — Die versunkene Treppe

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Inés Araya

### Dramatischer Zweck
Eine überflutete Ruinenstufe wird untersucht, ohne dass der Spieler selbst eine Figur steuern muss.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Finanziere und versorge eine archäologische Flussexpedition.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Bewässerungsgräben, Marktständen und dem braunen Wasser des großen Stroms. Eine überflutete Ruinenstufe wird untersucht, ohne dass der Spieler selbst eine Figur steuern muss. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wenn das wirklich ungewöhnlich ist, muss es eine Gegenprobe aushalten. Sonst war es nur ein interessanter Nachmittag.«

**Inés Araya:** »Am Fluss heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Inés Araya:** »Der Durchfluss bleibt stabil. Jetzt sehen wir, ob die Felder profitieren, ohne dass der Unterlauf den Preis zahlt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: wandreliefs zeigen eine geometrische Form, die später als vereinfachtes Weltgitter erkannt wird. **Dr. Mira Halden:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Letzte Gegenprobe. Danach bekommt die Hypothese einen Namen — vorher nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Inés Araya:** »Das ist eine Lösung, die auch flussabwärts noch vernünftig aussieht.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Archäologie-Missionen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Archäologie-Missionen.
- Storyflag: `st_mer_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-006 — Hochwasser

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Katastrophensystem als planbare Herausforderung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Überstehe ein angekündigtes Hochwasser durch Lagerreserven, alternative Piers und Reparaturkapazität.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Flussufer zwischen Anlegern, Feldern und den ersten dicht bebauten Uferstraßen. Katastrophensystem als planbare Herausforderung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Wasser ist hier Straße, Vorrat und Grenze zugleich. Wer nur eine davon sieht, plant zu kurz.«

**Elias Renn:** »Wir stabilisieren zuerst Menschen und Grundversorgung. Ursachenforschung kommt, sobald nichts mehr kippt.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: nach dem Hochwasser wird ein versiegelter Steinbehälter mit einer Karte geborgen. **Inés Araya:** »Das ist älter als unsere heutige Nutzung dieses Ufers. Wir dokumentieren, bevor wir weitergraben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Noch ein kompletter Wasserzyklus. Wenn Ober- und Unterlauf stabil bleiben, gebe ich mein Ja.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Inés Araya:** »Am Fluss heißt das: Der Notzustand ist vorbei. Was wir daraus lernen, muss länger halten als die Reparatur.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Resilienz-Bonus Meridia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Resilienz-Bonus Meridia.
- Storyflag: `st_mer_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-007 — Die Karte ohne Maßstab

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Inés Araya

### Dramatischer Zweck
Die Karte zeigt Küstenformen, die nirgendwo in der bekannten Welt passen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Analysiere das Kartenfragment in einem Forschungsgebäude und beschaffe historische Vergleichsdaten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Flussufer zwischen Anlegern, Feldern und den ersten dicht bebauten Uferstraßen. Die Karte zeigt Küstenformen, die nirgendwo in der bekannten Welt passen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wir sammeln erst Daten. Eine gute Geschichte kann warten; ein sauberer Messpunkt nicht.«

**Inés Araya:** »Am Fluss heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Inés Araya:** »Die Versorgung kommt an. Gut. Jetzt prüfen wir die Verteilung, nicht nur die Gesamtmenge.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer überlagerten Kartenschicht konkret: ein Sternmuster stimmt mit Miras südlichem Impulszeitpunkt überein. **Dr. Mira Halden:** »Ich sage noch nicht, was es ist. Ich kann aber inzwischen ziemlich gut sagen, was es nicht ist.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Inés Araya:** »Das ist eine Lösung, die auch flussabwärts noch vernünftig aussieht.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Globales Kartenarchiv +1**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Globales Kartenarchiv +1.
- Storyflag: `st_mer_007_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_007_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-008 — Ein Vertrag unter Gleichen

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Diplomatie wird an messbare lokale Versorgung gekoppelt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche einen fairen Handelsvertrag über mehrere Waren und Dienste.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Bewässerungsgräben, Marktständen und dem braunen Wasser des großen Stroms. Diplomatie wird an messbare lokale Versorgung gekoppelt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der schnellste Ausbau ist wertlos, wenn er beim nächsten Hochwasser gegen uns arbeitet.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir schreiben nicht nur Rechte auf, sondern auch Grenzen und Ausstiegsmöglichkeiten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: inés gibt Mira Zugang zu Familienchroniken, die sie zuvor zurückhielt. **Inés Araya:** »Das ist älter als unsere heutige Nutzung dieses Ufers. Wir dokumentieren, bevor wir weitergraben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Fast fertig. Jetzt prüfen wir, ob niemand außerhalb unseres Blickfelds die Rechnung bezahlt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Inés Araya:** »Das ist eine Lösung, die auch flussabwärts noch vernünftig aussieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Meridia-Bündnis**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Meridia-Bündnis.
- Storyflag: `st_mer_008_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_008_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-009 — Das Licht auf dem Wasser

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Die Expedition beobachtet für wenige Minuten ein ungewöhnliches, stationäres Leuchten weit außerhalb üblicher Schifffahrtsrouten.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Sende eine Forschungsexpedition in ein südliches Seegebiet und halte Langstreckenkommunikation.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Flussufer zwischen Anlegern, Feldern und den ersten dicht bebauten Uferstraßen. Die Expedition beobachtet für wenige Minuten ein ungewöhnliches, stationäres Leuchten weit außerhalb üblicher Schifffahrtsrouten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Auf einer Karte ist jede Strecke kurz. Auf See zählt, ob man mit Fracht, Wetter und Reserve auch wieder zurückkommt.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir messen am schlechtesten versorgten Punkt; der Durchschnitt kann uns später trösten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: das Licht verschwindet exakt mit einem zweiten gemessenen Impuls. **Marek Vale:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Der Rückweg fehlt mir noch. Eine Strecke ist erst fertig, wenn beide Richtungen funktionieren.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Südsee-Messdaten**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Südsee-Messdaten.
- Storyflag: `st_mer_009_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_009_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-MER-010 — Meridias Beweis

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Inés Araya

### Dramatischer Zweck
Meridias Storybogen endet mit einem eigenständigen wissenschaftlichen Erfolg.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bringe Karten, Chroniken und Messdaten in einem regionalen Forschungsrat zusammen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Flussufer zwischen Anlegern, Feldern und den ersten dicht bebauten Uferstraßen. Meridias Storybogen endet mit einem eigenständigen wissenschaftlichen Erfolg. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Inés Araya:** »Am Fluss heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Inés Araya:** »Die Versorgung kommt an. Gut. Jetzt prüfen wir die Verteilung, nicht nur die Gesamtmenge.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der Rat bestätigt: Die südlichen Anomalien sind historisch und modern dokumentiert. **Dr. Mira Halden:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Noch eine unabhängige Messung. Wenn die dasselbe sagt, dürfen wir anfangen, Konsequenzen zu ziehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Inés Araya:** »Gut. Der Nutzen bleibt hier, ohne dass wir ihn jemand anderem wegnehmen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beweisfragment Meridia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beweisfragment Meridia.
- Storyflag: `st_mer_010_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_mer_010_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-001 — Terrassen im Regen

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Orientia wird über eigenes Terrain und Wissen eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte eine lokale Siedlung mit Terrassenlandwirtschaft und Monsunresilienz.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Orientia wird über eigenes Terrain und Wissen eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Bevor wir etwas verändern, halten wir fest, was schon da ist. Ein fehlender Randvermerk kann wichtiger sein als eine ganze Chronik.«

**Elias Renn:** »Technisch heißt das für mich: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Tempelarchiv besitzt eine Sternkarte mit einer auffälligen ausgelassenen südlichen Zone. **Lian Sora:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Noch eine Quelle. Wenn sie unabhängig ist und dasselbe Problem zeigt, können wir den Befund veröffentlichen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Lian Sora:** »Das Archiv ist offener geworden, nicht einfacher. Das ist meistens ein gutes Zeichen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Orientia-Basisbau**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Orientia-Basisbau.
- Storyflag: `st_ori_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-002 — Papier bewahrt mehr als Stein

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Wissen wird als Produktions- und Storyressource verbunden.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue Papier-/Archivkette und stelle Archivkapazität her.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Wissen wird als Produktions- und Storyressource verbunden. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Archive lügen selten absichtlich. Menschen tun es — und manchmal tun sie es, indem sie etwas nicht ablegen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal in Randnotizen, die älter sind als der katalogisierte Bestand konkret: ein beschädigter Bericht beschreibt eine „zweite Morgendämmerung“ im Süden. **Lian Sora:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Ich will die Gegenposition im Archiv finden, bevor wir unsere eigene Version festschreiben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Lian Sora:** »Für die Dokumentation heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Archivsystem Orientia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Archivsystem Orientia.
- Storyflag: `st_ori_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-003 — Die Monsunnacht

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Spieler nutzt Reserve und regionale Infrastruktur statt Klick-Minispiel.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Sichere Versorgung während eines angekündigten Monsunereignisses.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet oberhalb der Terrassenfelder; von dort folgt die Kamera dem Wasser bis zum Missionsort. Spieler nutzt Reserve und regionale Infrastruktur statt Klick-Minispiel. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Was versiegelt wurde, wurde aus einem Grund versiegelt. Der Grund kann falsch sein — aber wir sollten ihn kennen, bevor wir das Siegel brechen.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: während des Sturms reagieren mehrere alte Messinstrumente gleichzeitig. **Lian Sora:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Ich will die Gegenposition im Archiv finden, bevor wir unsere eigene Version festschreiben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Lian Sora:** »Jetzt kann jeder sehen, was belegt ist, was fehlt und wo unsere Interpretation beginnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Monsunresilienz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Monsunresilienz.
- Storyflag: `st_ori_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-004 — Seide und Präzision

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Elias Renn · **Unterstützung:** Lian Sora

### Dramatischer Zweck
Wirtschaftlicher Fortschritt eröffnet Forschung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte zwei regionale Produktionsketten und liefere Präzisionswaren an das Observatorium.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Wirtschaftlicher Fortschritt eröffnet Forschung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Ich traue keinem System, das nur im Leerlauf gut aussieht. Wir bauen es so, dass der erste Fehler nicht gleich alles mitreißt.«

**Lian Sora:** »Für die Dokumentation heißt das: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Lian Sora:** »Die erste Quelle ist sauber erfasst. Jetzt legen wir die zweite daneben und suchen nicht nach Bestätigung, sondern nach Abweichungen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein altes Instrument enthält eine Resonanzkammer, deren Zweck unbekannt ist. **Elias Renn:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Ich will einen Test ohne unsere Hände am Schalter. Wenn es dann läuft, bin ich zufrieden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Lian Sora:** »Damit bleibt die Frage erhalten, ohne dass wir sie mit einer bequemen Antwort zuschütten.«

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Präzisionsproduktion**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Präzisionsproduktion.
- Storyflag: `st_ori_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-005 — Das Observatorium

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Lian Sora

### Dramatischer Zweck
Astronomie und Kartographie verbinden sich.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte/reaktiviere ein Observatorium und führe eine koordinierte Nachtmessung durch.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Astronomie und Kartographie verbinden sich. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wenn das wirklich ungewöhnlich ist, muss es eine Gegenprobe aushalten. Sonst war es nur ein interessanter Nachmittag.«

**Lian Sora:** »Für die Dokumentation heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Lian Sora:** »Die erste Quelle ist sauber erfasst. Jetzt legen wir die zweite daneben und suchen nicht nach Bestätigung, sondern nach Abweichungen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sterne stimmen; die historischen Karten sind absichtlich verändert, nicht astronomisch fehlerhaft. **Dr. Mira Halden:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Lian Sora:** »Jetzt kann jeder sehen, was belegt ist, was fehlt und wo unsere Interpretation beginnt.«

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Observatorium**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Observatorium.
- Storyflag: `st_ori_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-006 — Drei Chroniken

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Der Spieler erschließt Wissen durch mehrere Systeme.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Beschaffe drei unabhängige Chroniken über Handel, Diplomatie und Forschung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet oberhalb der Terrassenfelder; von dort folgt die Kamera dem Wasser bis zum Missionsort. Wir erschließt Wissen durch mehrere Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Was versiegelt wurde, wurde aus einem Grund versiegelt. Der Grund kann falsch sein — aber wir sollten ihn kennen, bevor wir das Siegel brechen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: alle drei nennen denselben südlichen Himmelsvorfall in unterschiedlicher Sprache. **Lian Sora:** »Nicht nur den Text sichern. Papier, Tinte, Bindung, Fundort — alles kann Teil der Aussage sein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Noch eine Quelle. Wenn sie unabhängig ist und dasselbe Problem zeigt, können wir den Befund veröffentlichen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Lian Sora:** »Damit bleibt die Frage erhalten, ohne dass wir sie mit einer bequemen Antwort zuschütten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Chronik-Set**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Chronik-Set.
- Storyflag: `st_ori_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-007 — Das Siegel der Stille

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Mission belohnt langfristige regionale Beziehungen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche genügend Vertrauen, um ein versiegeltes Archiv zu öffnen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem regenfeuchten Platz, an dem Handwerk, Archiv und Verkehr sichtbar ineinandergreifen. Die Mission belohnt langfristige regionale Beziehungen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Wir lesen zuerst die Unterschiede zwischen den Quellen. Übereinstimmung ist bequem; Widerspruch ist oft informativer.«

**Helena Voss:** »Für die Stadt ist dabei klar: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal in den wiederhergestellten Schichten des Archivs konkret: das Siegel trägt eine geometrische Struktur, die dem Meridia-Relief entspricht. **Lian Sora:** »Diese Lücke ist zu sauber. Jemand wollte, dass spätere Leser genau hier nichts finden.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Ich will die Gegenposition im Archiv finden, bevor wir unsere eigene Version festschreiben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Lian Sora:** »Jetzt kann jeder sehen, was belegt ist, was fehlt und wo unsere Interpretation beginnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Versiegeltes Archiv**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Versiegeltes Archiv.
- Storyflag: `st_ori_007_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_007_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-008 — Eine Maschine ohne Zweck

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Elias Renn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Forschung verlangt Sicherheit und Dokumentation.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Rekonstruiere ein historisches Gerät aus Fragmenten, ohne es zu aktivieren.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Forschung verlangt Sicherheit und Dokumentation. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Ich traue keinem System, das nur im Leerlauf gut aussieht. Wir bauen es so, dass der erste Fehler nicht gleich alles mitreißt.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir testen den Engpass, nicht den Prospektwert. Dort entscheidet sich die Kette.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: das Gerät koppelt schwach an unbekannte Umgebungsenergie, liefert aber keine nutzbare Leistung. **Elias Renn:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Einmal unter voller Last. Wenn nichts aus dem Takt fällt, ist es bereit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Resonanzforschung Vorstufe**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Resonanzforschung Vorstufe.
- Storyflag: `st_ori_008_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_008_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-009 — Der südliche Himmel

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Lian Sora

### Dramatischer Zweck
Mira kann nun Zeitpunkte mehrerer Anomalien korrelieren.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Vergleiche moderne Langzeitmessungen mit historischen Sternaufzeichnungen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem regenfeuchten Platz, an dem Handwerk, Archiv und Verkehr sichtbar ineinandergreifen. Mira kann nun Zeitpunkte mehrerer Anomalien korrelieren. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wir sammeln erst Daten. Eine gute Geschichte kann warten; ein sauberer Messpunkt nicht.«

**Lian Sora:** »Für die Dokumentation heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Lian Sora:** »Die Ordnung stimmt. Jetzt fällt auf, was absichtlich nicht in diese Ordnung passt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Ereignisse folgen keinem natürlichen Zyklus, wirken aber absichtlich synchronisiert. **Dr. Mira Halden:** »Ich sage noch nicht, was es ist. Ich kann aber inzwischen ziemlich gut sagen, was es nicht ist.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Letzte Gegenprobe. Danach bekommt die Hypothese einen Namen — vorher nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Lian Sora:** »Damit bleibt die Frage erhalten, ohne dass wir sie mit einer bequemen Antwort zuschütten.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beweisfragment Orientia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beweisfragment Orientia.
- Storyflag: `st_ori_009_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_009_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ORI-010 — Archiv der Reisenden

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Orientia liefert den stärksten historischen Hinweis vor Australis.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Öffne ein Handelsarchiv und rekonstruiere Routen alter Fernfahrer.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem regenfeuchten Platz, an dem Handwerk, Archiv und Verkehr sichtbar ineinandergreifen. Orientia liefert den stärksten historischen Hinweis vor Australis. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Bevor wir etwas verändern, halten wir fest, was schon da ist. Ein fehlender Randvermerk kann wichtiger sein als eine ganze Chronik.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der Kurs hält. Jetzt prüfe ich, ob der Hafen beim Rücklauf genauso schnell ist wie beim Auslaufen.«
- **Wendepunkt:** Der Hinweis wird diesmal in den wiederhergestellten Schichten des Archivs konkret: mehrere Routen enden an einem Punkt, der auf modernen Karten als unbefahrbares südliches Meer gilt. **Lian Sora:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Wir schließen erst, wenn Herkunft und Lücke gleichermaßen dokumentiert sind.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Lian Sora:** »Gut. Wir haben nichts geglättet, nur damit die Geschichte ordentlicher aussieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Australis-Hinweis +1**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Australis-Hinweis +1.
- Storyflag: `st_ori_010_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ori_010_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-001 — Wasser vor Gold

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Aferia wird über Wasserplanung und lokale Prioritäten eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte Reservoir, Wasserverteilung und eine stabile Grundsiedlung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen rotem Boden, Wasserstellen und den Gebäuden des aktuellen Siedlungskerns. Aferia wird über Wasserplanung und lokale Prioritäten eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Eine Versorgung ist nur dann gut, wenn sie auch die erreicht, die am wenigsten Einfluss haben.«

**Helena Voss:** »Für die Stadt ist dabei klar: Menge allein reicht nicht. Druck, Verteilung und Rückweg müssen zusammenpassen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein alter Reservoirstein enthält dasselbe Sternsymbol wie Occidentias Messingplatte. **Amara Selu:** »Bevor jemand Ansprüche anmeldet, brauchen wir Herkunft und Kontext. Alles andere kommt danach.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Amara Selu:** »Das Ergebnis hält auch am Rand. Dann kann ich es vertreten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aferia-Basisbau**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aferia-Basisbau.
- Storyflag: `st_afe_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-002 — Karawane der Quellen

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Binnenlogistik wird regional gelernt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Richte einen Binnenposten und eine Karawanen-/Transportverbindung ein.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet bei einer Wasser- oder Handelsstation, von der die Kamera ohne Schnitt zum Missionsort fährt. Binnenlogistik wird regional gelernt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Wir können viel bauen. Entscheidend ist, wer danach Zugang hat und wer nur die Rechnung bekommt.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Laden, Reserve, Zielbestand, Rückfahrt. Wenn das automatisch funktioniert, haben wir Handel statt Botendienst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Keine unnötige Wartezeit. Gut. Einmal noch mit voller Ladung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Karawanenführer besitzt ein geerbtes Fragment einer Karte mit südlicher Markierung. **Amara Selu:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Amara Selu:** »Gut. Diesmal verteilt die Infrastruktur nicht nur Leistung, sondern auch Zugang.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Binnenhandel**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Binnenhandel.
- Storyflag: `st_afe_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-003 — Kupfer unter rotem Stein

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Elias Renn · **Unterstützung:** Amara Selu

### Dramatischer Zweck
Rohstoffentwicklung hat lokale Kosten, aber kein Mikromanagement.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erschließe eine lokale Metallkette, ohne die Wasserversorgung zu überlasten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet bei einer Wasser- oder Handelsstation, von der die Kamera ohne Schnitt zum Missionsort fährt. Rohstoffentwicklung hat lokale Kosten, aber kein Mikromanagement. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Wenn wir schon bauen, bauen wir so, dass man später noch drankommt. Provisorien werden sonst sehr schnell dauerhaft.«

**Amara Selu:** »Für die Versorgung heißt das: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Amara Selu:** »Die Menge reicht. Die Frage ist, ob sie gerecht verteilt ankommt.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Legierungsspur, die mit keiner lokalen Technik übereinstimmt konkret: im Erzgang liegt ein nicht geologischer, glasartig geschmolzener Kanal. **Elias Renn:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Amara Selu:** »So kann Wachstum aussehen, ohne dass jemand dafür unsichtbar gemacht wird.«

**Elias Renn:** »Technisch heißt das für mich: Die Kette läuft vom Rohstoff bis zum Ergebnis ohne versteckten Engpass.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Geologie-Forschung**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Geologie-Forschung.
- Storyflag: `st_afe_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-004 — Die alte Plattform

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Amara Selu

### Dramatischer Zweck
Großarchitektur wird als historisches Rätsel eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Versorge eine archäologische Außenstation und kartiere eine monumentale Plattform.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Versorgungshof, während Wind Staub über Straße und Dächer zieht. Großarchitektur wird als historisches Rätsel eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wenn das wirklich ungewöhnlich ist, muss es eine Gegenprobe aushalten. Sonst war es nur ein interessanter Nachmittag.«

**Amara Selu:** »Für die Versorgung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Amara Selu:** »Das Netz trägt unter Normalbetrieb. Jetzt nehme ich die Reserve aus der Rechnung und sehe, wer zuerst verliert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Plattform ist exakt auf dieselbe südliche Richtung wie Miras Anomalien ausgerichtet. **Dr. Mira Halden:** »Wenn diese Spur echt ist, wird sie auch morgen noch gegen eine Gegenprobe bestehen. Sichert alles.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Amara Selu:** »Gut. Diesmal verteilt die Infrastruktur nicht nur Leistung, sondern auch Zugang.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Archäologische Plattform**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Archäologische Plattform.
- Storyflag: `st_afe_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-005 — Dürrelinie

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Resilienz und regionale Autonomie.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Überstehe eine angekündigte Dürre durch Reservoire, Handel und alternative Nahrung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Versorgungshof, während Wind Staub über Straße und Dächer zieht. Resilienz und regionale Autonomie. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Eine Versorgung ist nur dann gut, wenn sie auch die erreicht, die am wenigsten Einfluss haben.«

**Helena Voss:** »Notbetrieb zuerst, Optimierung danach.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein ausgetrocknetes Flussbett legt eine Steinreihe mit geometrischer Resonanzanordnung frei. **Amara Selu:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Amara Selu:** »Für die Versorgung heißt das: Der Notzustand ist vorbei. Was wir daraus lernen, muss länger halten als die Reparatur.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Wasserreserve-Upgrades**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Wasserreserve-Upgrades.
- Storyflag: `st_afe_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-006 — Die Kammer unter der Plattform

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Keine Laufmission: Bau, Energie, Sicherheit und Forschung sind nötig.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stabilisiere den Zugang und betreibe Forschung an einer unterirdischen Kammer.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Versorgungshof, während Wind Staub über Straße und Dächer zieht. Keine Laufmission: Bau, Energie, Sicherheit und Forschung sind nötig. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Elias Renn:** »Technisch heißt das für mich: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: im Inneren befindet sich eine Karte, die bekannte Kontinente stark vereinfacht und südlich davon weitere Landmassen zeigt. **Dr. Mira Halden:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Weltkartenfragment Aferia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Weltkartenfragment Aferia.
- Storyflag: `st_afe_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-007 — Niemand besitzt die Vergangenheit

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Story behandelt historische Funde nicht als automatisch spielereigen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheide gemeinsam mit lokalen Vertretern über Forschung, Ausstellung oder Schutz der Kammer.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet bei einer Wasser- oder Handelsstation, von der die Kamera ohne Schnitt zum Missionsort fährt. Die Story behandelt historische Funde nicht als automatisch spielereigen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Eine Versorgung ist nur dann gut, wenn sie auch die erreicht, die am wenigsten Einfluss haben.«

**Helena Voss:** »Für die Stadt ist dabei klar: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: unabhängig von der Wahl darf Mira eine wissenschaftliche Kopie anfertigen. **Amara Selu:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Amara Selu:** »Gut. Diesmal verteilt die Infrastruktur nicht nur Leistung, sondern auch Zugang.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aferia-Vertrauen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aferia-Vertrauen.
- Storyflag: `st_afe_007_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_007_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-008 — Sterne im Stein

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Lian Sora

### Dramatischer Zweck
Erstmals werden regionale Hinweise bewusst zusammengeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Vergleiche Aferia-Reliefs mit Orientia-Sternkarten und Meridia-Kartenfragmenten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen rotem Boden, Wasserstellen und den Gebäuden des aktuellen Siedlungskerns. Erstmals werden regionale Hinweise bewusst zusammengeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Lian Sora:** »Für die Dokumentation heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Lian Sora:** »Das Muster wiederholt sich in zwei unabhängigen Beständen. Damit wird das Schweigen dazwischen interessant.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die drei Quellen passen aufeinander, wenn man einen bisher fehlenden südlichen Bereich ergänzt. **Dr. Mira Halden:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Lian Sora:** »Das Archiv ist offener geworden, nicht einfacher. Das ist meistens ein gutes Zeichen.«

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Globales Beweisniveau 2**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Globales Beweisniveau 2.
- Storyflag: `st_afe_008_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_008_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-009 — Der lange Funk

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Elias Renn · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Technik der bekannten Welt versucht aktiv, das Rätsel zu prüfen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue eine Langstreckenfunk-/Kommunikationsanlage und sende einen koordinierten Test nach Süden.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet bei einer Wasser- oder Handelsstation, von der die Kamera ohne Schnitt zum Missionsort fährt. Technik der bekannten Welt versucht aktiv, das Rätsel zu prüfen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Ich traue keinem System, das nur im Leerlauf gut aussieht. Wir bauen es so, dass der erste Fehler nicht gleich alles mitreißt.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: nach langer Verzögerung kommt ein schwacher, strukturiert wirkender Echoimpuls zurück. **Elias Renn:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Südfunkdaten**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Südfunkdaten.
- Storyflag: `st_afe_009_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_009_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AFE-010 — Aferias Zustimmung

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Aferia endet mit politischer Eigenständigkeit statt bloßer Ressourcenlieferung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schließe ein Forschungs- und Expeditionsabkommen zwischen lokalen Partnern und dem Weltkoordinationsrat.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen rotem Boden, Wasserstellen und den Gebäuden des aktuellen Siedlungskerns. Aferia endet mit politischer Eigenständigkeit statt bloßer Ressourcenlieferung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Eine Versorgung ist nur dann gut, wenn sie auch die erreicht, die am wenigsten Einfluss haben.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir schreiben nicht nur Rechte auf, sondern auch Grenzen und Ausstiegsmöglichkeiten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die südliche Expedition erhält nun ausreichende internationale Legitimation. **Amara Selu:** »Das wird nicht still in irgendein Lager verschwinden. Erst dokumentieren, dann gemeinsam entscheiden.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Amara Selu:** »Die Vereinbarung hält, weil beide Seiten Grenzen und Nutzen kennen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beweisfragment Aferia**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beweisfragment Aferia.
- Storyflag: `st_afe_010_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_afe_010_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-001 — Vier Karten, ein Loch

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Hauptfiguren treffen erstmals als internationales Team zusammen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bringe die Beweisfragmente aus Occidentia, Meridia, Orientia und Aferia in einem Weltarchiv zusammen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Kartenraum mit realen Kartenständen aus allen erschlossenen Regionen; anschließend springt die Kamera in die Weltansicht. Die Hauptfiguren treffen erstmals als internationales Team zusammen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wenn das wirklich ungewöhnlich ist, muss es eine Gegenprobe aushalten. Sonst war es nur ein interessanter Nachmittag.«

**Helena Voss:** »Für die Stadt ist dabei klar: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: alle Quellen lassen denselben Bereich aus oder verschlüsseln ihn. **Dr. Mira Halden:** »Wenn diese Spur echt ist, wird sie auch morgen noch gegen eine Gegenprobe bestehen. Sichert alles.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Noch eine unabhängige Messung. Wenn die dasselbe sagt, dürfen wir anfangen, Konsequenzen zu ziehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Globales Beweisniveau 3**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Globales Beweisniveau 3.
- Storyflag: `st_glb_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-002 — Der Südimpuls

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Die bekannte Welt bestätigt das Phänomen mit moderner Technik.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Synchronisiere Messstationen in mindestens drei Regionen und beobachte einen neuen Impuls.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im internationalen Hafen, wo Schiffe aus mehreren erschlossenen Regionen gleichzeitig abgefertigt werden. Die bekannte Welt bestätigt das Phänomen mit moderner Technik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Elias Renn:** »Technisch heißt das für mich: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Keine auffälligen Spitzen. Gut. Ich prüfe noch den schwächsten Anschluss, dann glaube ich dem Ganzen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der Impuls kommt hinter den bekannten südlichen Routen hervor und enthält wiederholte geometrische Sequenzen. **Dr. Mira Halden:** »Wenn diese Spur echt ist, wird sie auch morgen noch gegen eine Gegenprobe bestehen. Sichert alles.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Australis-Forschungsprogramm**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Australis-Forschungsprogramm.
- Storyflag: `st_glb_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-003 — Rat der Zweifel

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Helena Voss · **Unterstützung:** Amara Selu

### Dramatischer Zweck
Nicht alle Fraktionen glauben an eine verborgene Welt; die Expedition wird als überprüfbare Forschung begründet.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erfülle wirtschaftliche/technische Voraussetzungen für eine globale Expedition und gewinne politische Zustimmung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im internationalen Hafen, wo Schiffe aus mehreren erschlossenen Regionen gleichzeitig abgefertigt werden. Nicht alle Fraktionen glauben an eine verborgene Welt; die Expedition wird als überprüfbare Forschung begründet. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Eine Stadt entsteht nicht im Planungsraum. Sie entsteht dort, wo Menschen sich morgen noch auf unsere Entscheidung verlassen müssen.«

**Amara Selu:** »Eine Vereinbarung ist nur dann belastbar, wenn beide Seiten auch morgen noch einen Grund haben, sie einzuhalten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Amara Selu:** »Die Versorgung erreicht den ersten Bezirk. Jetzt prüfen wir den entferntesten, nicht den bequemsten.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Gegner der Expedition legt ein geheimes altes Dossier vor, das ihre Bedeutung ungewollt bestätigt. **Helena Voss:** »Alle vier Quellen getrennt halten. Wenn sie dasselbe Loch zeigen, ist gerade ihre Unabhängigkeit der Beweis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Amara Selu:** »Damit haben wir nicht alles gelöst, aber wenigstens niemanden aus der Rechnung gestrichen.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Australis-Charta**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Australis-Charta.
- Storyflag: `st_glb_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-004 — Schiff für das Ende der Karte

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Marek Vale · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Der Spieler muss mehrere Regionen wirtschaftlich verbinden.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Produziere Materialien und baue/chartere ein polartaugliches Expeditionsschiff.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem großen Planungstisch, auf dem nur tatsächlich entdeckte Regionen und Routen eingezeichnet sind. Wir muss mehrere Regionen wirtschaftlich verbinden. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Wenn wir fahren, fahren wir mit einem Grund und mit genug Reserve, um unsere Meinung ändern zu können.«

**Elias Renn:** »Technisch heißt das für mich: Ich plane die Reserve so, als würde das Wetter gegen uns stimmen. Meistens tut es das irgendwann.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer überlagerten Kartenschicht konkret: im Bauplan findet Elias eine unerwartete Anforderung an magnetische Abschirmung, die aus alten Expeditionserfahrungen stammt. **Marek Vale:** »Das gehört ins Kartenlog. Position genau markieren; auf See ist eine falsche Erinnerung schlimmer als gar keine.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Letzte Prüfung: Reserve bleibt unangetastet. Wenn wir sie schon im Normalbetrieb brauchen, ist der Plan zu knapp.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Polare Expeditionstechnik**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Polare Expeditionstechnik.
- Storyflag: `st_glb_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-005 — Vorräte für Monate

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Edda Vey

### Dramatischer Zweck
Edda wird eingeführt und macht klar, dass Polarentdeckung Logistik ist, nicht Abenteuerromantik.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stelle Vorräte, medizinische Kapazität, Ersatzteile und wissenschaftliche Ausrüstung bereit.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Kartenraum mit realen Kartenständen aus allen erschlossenen Regionen; anschließend springt die Kamera in die Weltansicht. Edda wird eingeführt und macht klar, dass Polarentdeckung Logistik ist, nicht Abenteuerromantik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Dr. Edda Vey:** »Reichweite, Vorrat und Rückweg zuerst. Entdeckung ist nur dann Fortschritt, wenn wir wiederkommen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Edda Vey:** »Das hält. Gut. Jetzt nehmen wir ihm Wärme, Zeit oder Sicht — eins davon wird uns draußen immer fehlen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sie besitzt eine Kopie eines alten Funkprotokolls mit derselben Signalstruktur wie der Südimpuls. **Helena Voss:** »Alle vier Quellen getrennt halten. Wenn sie dasselbe Loch zeigen, ist gerade ihre Unabhängigkeit der Beweis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Edda Vey:** »Sauber. Heute hat die Kälte nichts gewonnen.«

**Helena Voss:** »Für die Stadt ist dabei klar: Vorrat, Reichweite und Rückweg stehen. Jetzt ist Aufbruch eine Entscheidung und kein Glücksspiel.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Australis-Expedition startklar**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Australis-Expedition startklar.
- Storyflag: `st_glb_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-GLB-006 — Kurs Australis

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Edda Vey

### Dramatischer Zweck
Der bekannte-Welt-Bogen kippt in den Polar-Mysterybogen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Starte die Expedition und halte Kommunikation bis zum Übergang in die Australis-Session.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Kartenraum mit realen Kartenständen aus allen erschlossenen Regionen; anschließend springt die Kamera in die Weltansicht. Der bekannte-Welt-Bogen kippt in den Polar-Mysterybogen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Wenn wir fahren, fahren wir mit einem Grund und mit genug Reserve, um unsere Meinung ändern zu können.«

**Dr. Edda Vey:** »Reichweite, Vorrat und Rückweg zuerst. Entdeckung ist nur dann Fortschritt, wenn wir wiederkommen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Edda Vey:** »Normalbetrieb zählt hier wenig. Ich will sehen, was nach zehn Minuten Reservebetrieb noch funktioniert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: kurz vor dem Kartenrand fällt der Kompass aus; Sterne und Navigation bleiben jedoch korrekt. **Marek Vale:** »Alle vier Quellen getrennt halten. Wenn sie dasselbe Loch zeigen, ist gerade ihre Unabhängigkeit der Beweis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Noch eine Fahrt mit echter Last. Danach weiß ich, ob das eine Route oder nur ein glücklicher Versuch war.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Edda Vey:** »Jetzt haben wir nicht nur Fortschritt, sondern einen Rückweg.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Vorrat, Reichweite und Rückweg stehen. Jetzt ist Aufbruch eine Entscheidung und kein Glücksspiel.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Australis freigeschaltet**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Australis freigeschaltet.
- Storyflag: `st_glb_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_glb_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-001 — Weiße Küste

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Die Expedition überlebt nur mit echter Polarlogistik.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte Expeditionsquartiere, Versorgungspier, Wärme 1 und Grundstrom.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Windschutz des Polarstützpunkts; Schnee treibt quer über die Beleuchtung, während die Siedlung im Hintergrund weiterarbeitet. Die Expedition überlebt nur mit echter Polarlogistik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Wir beweisen heute nicht, dass wir mutig sind. Wir beweisen, dass wir wieder nach Hause kommen.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der Kurs hält. Jetzt prüfe ich, ob der Hafen beim Rücklauf genauso schnell ist wie beim Auslaufen.«
- **Wendepunkt:** Der Hinweis wird diesmal in einem Wärmebild mit einer unplausiblen Quelle konkret: im Eis werden regelmäßige dunkle Schichten gefunden, die nicht zu vulkanischem Staub passen. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Noch ein Notlauf. Wenn der klappt, lasse ich Leute davon abhängig werden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Dr. Edda Vey:** »Damit darf jemand außer uns davon abhängig werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Expeditionspersonal**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Expeditionspersonal.
- Storyflag: `st_aus_001_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_001_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-002 — Wärme ist Leben

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Australis unterscheidet sich systemisch von normalen Regionen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stabilisiere Wärme, Wasser aus Schnee/Eis und medizinische Versorgung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an der Außenschleuse, von der die Kamera über Eis und Wetterstationen zum Missionsziel schwenkt. Australis unterscheidet sich systemisch von normalen Regionen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Ich plane grundsätzlich mit dem Wetter, das uns nicht gefallen wird. Für Sonnenschein braucht niemand einen Notfallplan.«

**Elias Renn:** »Technisch heißt das für mich: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Schicht unter dem jüngeren Eis konkret: ein Sensor registriert tief unter dem Eis periodische Wärmeimpulse. **Dr. Edda Vey:** »Das ist nicht vom Wetter geformt. Mira soll es sehen, bevor wir weiter freilegen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Wir schließen erst ab, wenn der Reserveweg ohne Improvisation anspringt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Dr. Edda Vey:** »Der Zustand hält auch unter schlechten Bedingungen. In Australis ist das die eigentliche Abnahme.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Wärmequalität 2**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Wärmequalität 2.
- Storyflag: `st_aus_002_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_002_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-003 — Grün unter Glas

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Dauerhafte Besiedlung beginnt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue Gewächshaus/Hydroponik und reduziere Importabhängigkeit.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Dauerhafte Besiedlung beginnt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Jede Anlage hier braucht einen zweiten Weg. Der erste friert irgendwann ein.«

**Helena Voss:** »Für die Stadt ist dabei klar: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: pflanzen reagieren kurz auf einen nächtlichen elektromagnetischen/ätherischen Impuls, bevor die Messgeräte ausfallen. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Noch ein Notlauf. Wenn der klappt, lasse ich Leute davon abhängig werden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Dr. Edda Vey:** »Jetzt haben wir nicht nur Fortschritt, sondern einen Rückweg.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Lokale Frischversorgung**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Lokale Frischversorgung.
- Storyflag: `st_aus_003_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_003_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-004 — Wetterfenster

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Extremwetter wird mit Vorwarnung statt Zufallsfrust eingeführt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte Wetterstation und plane eine Expedition innerhalb eines sicheren Wetterfensters.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Windschutz des Polarstützpunkts; Schnee treibt quer über die Beleuchtung, während die Siedlung im Hintergrund weiterarbeitet. Extremwetter wird mit Vorwarnung statt Zufallsfrust eingeführt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Ich plane grundsätzlich mit dem Wetter, das uns nicht gefallen wird. Für Sonnenschein braucht niemand einen Notfallplan.«

**Dr. Mira Halden:** »Wärme, Wasser, Strom, Rückzug. Wenn einer dieser vier Punkte nur auf Glück beruht, bleiben wir hier.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Wetterstation empfängt ein Signal, das nicht atmosphärisch moduliert ist. **Dr. Edda Vey:** »Das ist nicht vom Wetter geformt. Mira soll es sehen, bevor wir weiter freilegen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Noch ein Notlauf. Wenn der klappt, lasse ich Leute davon abhängig werden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Dr. Edda Vey:** »Jetzt haben wir nicht nur Fortschritt, sondern einen Rückweg.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Wettervorhersage**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Wettervorhersage.
- Storyflag: `st_aus_004_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_004_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-005 — Der alte Cairn

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Dr. Edda Vey

### Dramatischer Zweck
Ein vermeintlich moderner Cairn ist deutlich älter als bekannte Südexpeditionen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Versorge eine kurze wissenschaftliche Expedition zu einem steinernen Markierungspunkt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Ein vermeintlich moderner Cairn ist deutlich älter als bekannte Südexpeditionen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wir sammeln erst Daten. Eine gute Geschichte kann warten; ein sauberer Messpunkt nicht.«

**Dr. Edda Vey:** »Für den Ernstfall heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Edda Vey:** »Normalbetrieb zählt hier wenig. Ich will sehen, was nach zehn Minuten Reservebetrieb noch funktioniert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: darunter liegt eine Platte mit demselben geometrischen Grundsymbol wie in vier Regionen. **Dr. Mira Halden:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Edda Vey:** »Gut. Das würde ich auch bei Nacht und Gegenwind benutzen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Polararchäologie**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Polararchäologie.
- Storyflag: `st_aus_005_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_005_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-006 — Unter dem Eis

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Die Mysterylage wird messbar.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte Tiefeneisbohrung/Geophysik und führe mehrere Messzyklen aus.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an der Außenschleuse, von der die Kamera über Eis und Wetterstationen zum Missionsziel schwenkt. Die Mysterylage wird messbar. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Nichts wäre mir lieber, als mich hier zu irren. Also bauen wir den Versuch so, dass wir das auch merken würden.«

**Elias Renn:** »Technisch heißt das für mich: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Schicht unter dem jüngeren Eis konkret: unter dem Eis existiert ein Hohlraum oder eine Grenzfläche mit ungewöhnlicher Energiedichte. **Dr. Mira Halden:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Geophysikzentrum**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Geophysikzentrum.
- Storyflag: `st_aus_006_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_006_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-007 — Dauerhafte Siedlung

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Expedition wird zur Siedlung; das Risiko wird bewusst akzeptiert.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche die Polarsiedler-Freischaltung und baue robuste Dauerquartiere.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an der Außenschleuse, von der die Kamera über Eis und Wetterstationen zum Missionsziel schwenkt. Die Expedition wird zur Siedlung; das Risiko wird bewusst akzeptiert. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Wir beweisen heute nicht, dass wir mutig sind. Wir beweisen, dass wir wieder nach Hause kommen.«

**Helena Voss:** »Für die Stadt ist dabei klar: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein altes Funkfragment spricht nicht von einer „Küste“, sondern einer „Wand“. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Noch einmal mit reduzierter Leistung. Danach ist es polar-tauglich.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Dr. Edda Vey:** »Gut. Das würde ich auch bei Nacht und Gegenwind benutzen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Polarsiedler**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Polarsiedler.
- Storyflag: `st_aus_007_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_007_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-008 — Die Linie im Weiß

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Kartographie zeigt, dass das Phänomen zu regelmäßig und zu ausgedehnt ist.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kartiere mehrere Punkte entlang einer ungewöhnlich geraden Eisformation.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Kartographie zeigt, dass das Phänomen zu regelmäßig und zu ausgedehnt ist. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Nichts wäre mir lieber, als mich hier zu irren. Also bauen wir den Versuch so, dass wir das auch merken würden.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der Kurs hält. Jetzt prüfe ich, ob der Hafen beim Rücklauf genauso schnell ist wie beim Auslaufen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: zum ersten Mal fällt im Dialog der neutrale Arbeitsbegriff „Eiswand“. **Dr. Mira Halden:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Letzte Gegenprobe. Danach bekommt die Hypothese einen Namen — vorher nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Eiswand-Forschungsziel**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Eiswand-Forschungsziel.
- Storyflag: `st_aus_008_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_008_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-009 — Schwerer Eisbrecher

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Großprojekt aus mehreren bekannten-Welt-Regionen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche Polarfachkräfte, baue schweren Hafen/Flugplatz und fertige einen schweren Eisbrecher.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Windschutz des Polarstützpunkts; Schnee treibt quer über die Beleuchtung, während die Siedlung im Hintergrund weiterarbeitet. Großprojekt aus mehreren bekannten-Welt-Regionen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Jede Anlage hier braucht einen zweiten Weg. Der erste friert irgendwann ein.«

**Elias Renn:** »Technisch heißt das für mich: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: ein Bauteil aus der unbekannten Legierung reagiert in Wandnähe schwach, obwohl es passiv sein sollte. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Letzte Prüfung bei schlechtem Wetter. Schönwetterwerte interessieren mich nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Dr. Edda Vey:** »Der Zustand hält auch unter schlechten Bedingungen. In Australis ist das die eigentliche Abnahme.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Schwerer Eisbrecher**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Schwerer Eisbrecher.
- Storyflag: `st_aus_009_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_009_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-010 — Vor der Wand

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Edda Vey

### Dramatischer Zweck
Große visuelle Enthüllung: Die Wand ist real und monumental, aber was dahinter liegt bleibt unbekannt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Führe eine sichere Erkundungsfahrt bis Sichtweite der Eiswand durch.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Große visuelle Enthüllung: Die Wand ist real und monumental, aber was dahinter liegt bleibt unbekannt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Routen scheitern selten am Horizont. Meist scheitern sie an dem, was jemand am Kai vergessen hat.«

**Dr. Edda Vey:** »Für den Ernstfall heißt das: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Edda Vey:** »Werte stabil. Jetzt simuliere ich den Ausfall, den niemand im Prospekt erwähnt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: messungen zeigen Wärme, Funk- und Resonanzsignaturen von jenseits der Wand. **Marek Vale:** »Markiert den Punkt. Wir ändern den Kurs nicht wegen einer Geschichte, aber wir vergessen ihn auch nicht.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Noch eine Fahrt mit echter Last. Danach weiß ich, ob das eine Route oder nur ein glücklicher Versuch war.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Edda Vey:** »Damit darf jemand außer uns davon abhängig werden.«

**Marek Vale:** »Damit ist der Weg offen. Was wir daraus machen, ist eine andere Frage.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Eiswand sichtbar**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Eiswand sichtbar.
- Storyflag: `st_aus_010_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_010_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-011 — Vorposten Null

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Der Spieler bereitet den Durchbruch wirtschaftlich vor.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte einen Eiswand-Vorposten mit Wärme 4, redundanter Energie, Forschung 3 und Langstreckenkommunikation.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Wir bereitet den Durchbruch wirtschaftlich vor. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Ich plane grundsätzlich mit dem Wetter, das uns nicht gefallen wird. Für Sonnenschein braucht niemand einen Notfallplan.«

**Elias Renn:** »Technisch heißt das für mich: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal in einem Wärmebild mit einer unplausiblen Quelle konkret: eine Nachtmessung empfängt eine Sequenz, die auf Miras frühe Occidentia-Messung antwortet. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Wir schließen erst ab, wenn der Reserveweg ohne Improvisation anspringt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Dr. Edda Vey:** »Jetzt haben wir nicht nur Fortschritt, sondern einen Rückweg.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Durchbruchsmission verfügbar**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Durchbruchsmission verfügbar.
- Storyflag: `st_aus_011_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_011_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-AUS-012 — Jenseits der Karte

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Inszenierter Wendepunkt mit Gameplay: Navigation, Energie, Reparatur und Schutz müssen funktionieren.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Starte die Durchbruchsmission, stabilisiere Route und führe die Expedition durch die Eiswand.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Windschutz des Polarstützpunkts; Schnee treibt quer über die Beleuchtung, während die Siedlung im Hintergrund weiterarbeitet. Inszenierter Wendepunkt mit Gameplay: Navigation, Energie, Reparatur und Schutz müssen funktionieren. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Keine unnötige Wartezeit. Gut. Einmal noch mit voller Ladung.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer überlagerten Kartenschicht konkret: hinter der Wand öffnet sich eine zweite Welt. Der Kartenname wechselt von TERRA INCOGNITA zu ULTIMA erst nach tatsächlicher Sichtung. **Dr. Mira Halden:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Ich will den Befund einmal ohne meine bevorzugte Methode sehen. Sonst testen wir nur unsere eigene Erwartung.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Systemhinweis:** Dauerhaft freigeschaltet: **`ultima_discovered = true`, Aurelia-Zugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** `ultima_discovered = true`, Aurelia-Zugang.
- Storyflag: `st_aus_012_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_aus_012_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---

# 3. Hauptstory — Ultima, 12 Akte

Die 60 bestehenden Ultima-Kernmissionen werden hier auf Produktionsniveau ausgeschrieben. Der jeweilige Akt behält Thema und Makrofolge aus dem Storykapitel, erhält aber konkrete Quest-/Skriptregeln.
## ST-ULT-01-01 — Fremde Küste

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Ankunft im unbekannten Küstenbecken und Aufbau eines kleinen stabilen Aurelia-Vorpostens.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Resonanzrelays, Gärten und Gebäuden, deren Technik ohne sichtbare Feuerung arbeitet. Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Ihr seid Gäste, aber keine Zuschauer. Wenn ihr hier handeln wollt, tragt ihr auch Verantwortung für die Folgen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: aurelia besitzt brennstofflose Technik und Karten, die die bekannte Welt anders einordnen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Noch ein Zyklus unter realer Netzlast. Dann wissen wir, ob eure Lösung Teil Aurelias werden kann.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Serin Ael:** »Für Aurelia heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aurelia-Grundzugang und erste lokale Dienste**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aurelia-Grundzugang und erste lokale Dienste.
- Storyflag: `st_ult_01_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_01_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-01-02 — Energie ohne Feuer

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erste Beobachtung einer Anlage, die ohne sichtbaren Brennstoff Energie liefert.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Das Weltgitter verbindet vieles. Es entschuldigt nichts.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Leistungsbilanz ohne sichtbaren Brennstoff konkret: aurelia besitzt brennstofflose Technik und Karten, die die bekannte Welt anders einordnen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Prüft den Rückfallzustand. Ein gutes System weiß, wie es kleiner wird, ohne zu brechen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aurelia-Grundzugang und erste lokale Dienste**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aurelia-Grundzugang und erste lokale Dienste.
- Storyflag: `st_ult_01_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_01_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-01-03 — Regeln einer anderen Welt

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kontakt mit Gemeindeleuten und Anerkennung grundlegender Regeln der lokalen Infrastruktur.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer erhöhten Plattform über Aurelias dichter, lautloser Infrastruktur. Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Unsere Technik wirkt mühelos, weil Generationen gelernt haben, wo Mühelosigkeit gefährlich wird.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: aurelia besitzt brennstofflose Technik und Karten, die die bekannte Welt anders einordnen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Die Funktion steht. Jetzt fehlt nur der Beweis, dass sie niemand anderen zur Reserve macht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aurelia-Grundzugang und erste lokale Dienste**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aurelia-Grundzugang und erste lokale Dienste.
- Storyflag: `st_ult_01_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_01_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-01-04 — Der beschädigte Kollektor

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Reparatur eines beschädigten Resonanzkollektors ohne Zugriff auf versiegeltes Wissen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer erhöhten Plattform über Aurelias dichter, lautloser Infrastruktur. Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Aurelia wird euch nicht daran messen, was ihr nehmen könnt, sondern daran, was ihr versteht, bevor ihr es berührt.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir testen den Engpass, nicht den Prospektwert. Dort entscheidet sich die Kette.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal im Lastprotokoll des Kollektors konkret: aurelia besitzt brennstofflose Technik und Karten, die die bekannte Welt anders einordnen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Fast fertig. Lasst das Netz einmal selbst ausgleichen, ohne manuelle Korrektur.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aurelia-Grundzugang und erste lokale Dienste**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aurelia-Grundzugang und erste lokale Dienste.
- Storyflag: `st_ult_01_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_01_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-01-05 — Die falsche Weltkarte

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entdeckung, dass alte Karten das bekannte Weltbild nicht vollständig zeigen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Akt 1 „Jenseits der Eiswand“: Erstkontakt, Orientierung und Überleben ohne sofortige Lore-Erklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Aurelia wird euch nicht daran messen, was ihr nehmen könnt, sondern daran, was ihr versteht, bevor ihr es berührt.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: aurelia besitzt brennstofflose Technik und Karten, die die bekannte Welt anders einordnen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Prüft den Rückfallzustand. Ein gutes System weiß, wie es kleiner wird, ohne zu brechen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Aurelia-Grundzugang und erste lokale Dienste**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Aurelia-Grundzugang und erste lokale Dienste.
- Storyflag: `st_ult_01_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_01_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-02-01 — Adern unter Stein

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kartierung lokaler Resonanzadern und Bau des ersten eigenen Äther-Induktionssystems.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Unsere Technik wirkt mühelos, weil Generationen gelernt haben, wo Mühelosigkeit gefährlich wird.«

**Iriath:** »Für das Netz heißt das: Wir testen den Engpass, nicht den Prospektwert. Dort entscheidet sich die Kette.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: das Weltgitter ist real, sehr alt und teilweise bewusst versiegelt. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Die Funktion steht. Jetzt fehlt nur der Beweis, dass sie niemand anderen zur Reserve macht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Gut. Die Verbindung schafft Möglichkeit, keine Pflicht.«

**Serin Ael:** »Für Aurelia heißt das: Die Kette läuft vom Rohstoff bis zum Ergebnis ohne versteckten Engpass.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang.
- Storyflag: `st_ult_02_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_02_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-02-02 — Erinnerung im Kristall

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bergen eines fragmentierten Erinnerungskristalls aus einer stillgelegten Station.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Ihr seid Gäste, aber keine Zuschauer. Wenn ihr hier handeln wollt, tragt ihr auch Verantwortung für die Folgen.«

**Iriath:** »Für das Netz heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Das System funktioniert. Deshalb müssen wir besonders genau klären, wer es verwenden darf.«
- **Wendepunkt:** Der Hinweis wird diesmal im Resonanzmuster des Kristalls konkret: das Weltgitter ist real, sehr alt und teilweise bewusst versiegelt. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Fast fertig. Lasst das Netz einmal selbst ausgleichen, ohne manuelle Korrektur.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang.
- Storyflag: `st_ult_02_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_02_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-02-03 — Relais am Limit

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stabilisieren eines überlasteten Relais, bevor ein Stadtbezirk Energie verliert.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Aurelia wird euch nicht daran messen, was ihr nehmen könnt, sondern daran, was ihr versteht, bevor ihr es berührt.«

**Iriath:** »Für das Netz heißt das: Wir testen den Engpass, nicht den Prospektwert. Dort entscheidet sich die Kette.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Hinweis wird diesmal in den gespeicherten Phasenverschiebungen des Relais konkret: das Weltgitter ist real, sehr alt und teilweise bewusst versiegelt. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Prüft den Rückfallzustand. Ein gutes System weiß, wie es kleiner wird, ohne zu brechen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang.
- Storyflag: `st_ult_02_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_02_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-02-04 — Der erste Wächter

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erster Zugang zu einem Archivvorraum nach einer Sicherheitsprüfung durch Wächter.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Resonanzrelays, Gärten und Gebäuden, deren Technik ohne sichtbare Feuerung arbeitet. Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Das Weltgitter verbindet vieles. Es entschuldigt nichts.«

**Iriath:** »Für das Netz heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Hinweis wird diesmal in der Reaktion des Wächters konkret: das Weltgitter ist real, sehr alt und teilweise bewusst versiegelt. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Noch ein Zyklus unter realer Netzlast. Dann wissen wir, ob eure Lösung Teil Aurelias werden kann.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang.
- Storyflag: `st_ult_02_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_02_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-02-05 — Die erste Wahl

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheidung, welche von mehreren ungefährlichen Technologien zuerst rekonstruiert wird.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer erhöhten Plattform über Aurelias dichter, lautloser Infrastruktur. Akt 2 „Das Netz unter der Welt“: Weltgitter, Resonanzadern und erste Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Ihr seid Gäste, aber keine Zuschauer. Wenn ihr hier handeln wollt, tragt ihr auch Verantwortung für die Folgen.«

**Iriath:** »Für das Netz heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Technisch ist der Zustand stabil. Politisch beginnt die Prüfung erst jetzt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: das Weltgitter ist real, sehr alt und teilweise bewusst versiegelt. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Noch ein Zyklus unter realer Netzlast. Dann wissen wir, ob eure Lösung Teil Aurelias werden kann.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Äther-Induktion, Weltgitter-Grundwissen und Wächterzugang.
- Storyflag: `st_ult_02_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_02_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-03-01 — Tor nach Viridia

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Freischaltung Viridias durch eine wissenschaftlich-medizinische Expedition.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter gewaltigen Blättern und bioätherischen Leitstrukturen, ohne die normale Simulation anzuhalten. Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Viridia gibt viel zurück, solange man nicht so tut, als wäre Rückgabe garantiert.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Ich rechne mit dem Verbrauch, nicht mit dem besten Ertrag. Danach dimensionieren wir Reserve.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal in den Torprotokollen und der Phasenlage des Tors konkret: äthertechnik kann biologische Systeme beeinflussen und wurde früher tief in Ökosysteme eingebunden. **Ilyra Venn:** »Nicht schneiden. Erst schauen, ob die Struktur auf Nähe, Licht oder Ätherfeld reagiert.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Ein letzter Test ohne Wachstumsbeschleunigung. Danach wissen wir, was natürlich trägt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Ilyra Venn:** »Gut. Wir nehmen etwas und lassen genug zurück, damit Viridia nicht ärmer wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Viridia und Bioäther-Grundsysteme**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Viridia und Bioäther-Grundsysteme.
- Storyflag: `st_ult_03_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_03_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-03-02 — Saatgut der Alten

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Wiederherstellung eines Saatgutarchivs ohne die lokale Biosphäre zu destabilisieren.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einer Kultivierungsstation, von der aus sich die Kamera durch Viridias dichtes Grün zum Ziel bewegt. Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »In Viridia ist „Rohstoff“ oft nur ein anderes Wort für etwas, das noch lebt. Behandelt es entsprechend.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Die Kette muss nicht groß sein, nur stetig. Ein leerer Topf interessiert sich nicht für Spitzenproduktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: äthertechnik kann biologische Systeme beeinflussen und wurde früher tief in Ökosysteme eingebunden. **Ilyra Venn:** »Das Muster ist nicht zufällig gewachsen. Aber ich möchte wissen, ob es Erinnerung oder nur Reaktion ist.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Fast fertig. Ich will sehen, ob die zweite Generation dieselben Eigenschaften behält.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Ilyra Venn:** »Für den Bestand heißt das: Die Versorgung trägt aus eigener Produktion. Das ist der Punkt, an dem eine Siedlung unabhängiger wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Viridia und Bioäther-Grundsysteme**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Viridia und Bioäther-Grundsysteme.
- Storyflag: `st_ult_03_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_03_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-03-03 — Kalte Kette, lebende Fracht

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Aufbau einer Kühl- und Biologistikkette zwischen Aurelia und Viridia.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einer Kultivierungsstation, von der aus sich die Kamera durch Viridias dichtes Grün zum Ziel bewegt. Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »In Viridia ist „Rohstoff“ oft nur ein anderes Wort für etwas, das noch lebt. Behandelt es entsprechend.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Ich rechne mit dem Verbrauch, nicht mit dem besten Ertrag. Danach dimensionieren wir Reserve.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: äthertechnik kann biologische Systeme beeinflussen und wurde früher tief in Ökosysteme eingebunden. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Ein letzter Test ohne Wachstumsbeschleunigung. Danach wissen wir, was natürlich trägt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Ilyra Venn:** »Für den Bestand heißt das: Die Versorgung trägt aus eigener Produktion. Das ist der Punkt, an dem eine Siedlung unabhängiger wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Viridia und Bioäther-Grundsysteme**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Viridia und Bioäther-Grundsysteme.
- Storyflag: `st_ult_03_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_03_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-03-04 — Die kranke Wurzel

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Behandlung einer regionalen Pflanzenkrankheit über Forschung statt pauschaler Vernichtung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand einer lebenden Kultivierungszone, in der Pflanzen, Wasser und technische Strukturen ineinander übergehen. Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Wir beginnen mit Beobachtung. Wenn ihr Wachstum erzwingt, bekommt ihr vielleicht mehr — aber nicht dasselbe.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: äthertechnik kann biologische Systeme beeinflussen und wurde früher tief in Ökosysteme eingebunden. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Fast fertig. Ich will sehen, ob die zweite Generation dieselben Eigenschaften behält.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Ilyra Venn:** »Für den Bestand heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Viridia und Bioäther-Grundsysteme**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Viridia und Bioäther-Grundsysteme.
- Storyflag: `st_ult_03_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_03_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-03-05 — Biologie des Gitters

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erster Hinweis, dass Teile des alten Weltgitters biologische Systeme beeinflussten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand einer lebenden Kultivierungszone, in der Pflanzen, Wasser und technische Strukturen ineinander übergehen. Akt 3 „Die lebende Bibliothek“: Viridia, Bioäther und ökologische Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Lebendiges Material lässt sich nicht wie Erz planen. Wer nur Mengen sieht, übersieht den Zustand.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: äthertechnik kann biologische Systeme beeinflussen und wurde früher tief in Ökosysteme eingebunden. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Noch ein Regenerationszyklus. Wenn die Quelle vollständig zurückkommt, können wir die Nutzung freigeben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Ilyra Venn:** »Für den Bestand heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Viridia und Bioäther-Grundsysteme**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Viridia und Bioäther-Grundsysteme.
- Storyflag: `st_ult_03_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_03_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-04-01 — Pass der Schwerlast

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Überquerung eines instabilen Hochlandpasses und Sicherung einer Schwerlastroute.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet vor einer titanischen Stützkonstruktion, an der Maßstab und Gewicht der Region sofort sichtbar werden. Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Wir teilen Lasten, weil selbst Riesen irgendwann müde werden.«

**Elias Renn:** »Technisch heißt das für mich: Last verteilen, Fundament lesen, Rückweg planen. Kraft allein ersetzt keine Statik.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Keine auffälligen Spitzen. Gut. Ich prüfe noch den schwächsten Anschluss, dann glaube ich dem Ganzen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: riesen waren Mitbauer der alten Zivilisation und keine bloßen Randwesen. **Kharum Tor:** »Nichts versetzen. Erst Schwingung, Fuge und Lastbezug messen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Fast fertig. Jetzt ohne Hilfe der stärksten Hebeeinheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Kharum Tor:** »Gut. Das Gewicht ist verteilt, nicht versteckt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Titania und Riesenbund**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Titania und Riesenbund.
- Storyflag: `st_ult_04_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_04_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-04-02 — Stimmen der Riesen

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erstkontakt mit einem Riesenclan ohne Kampfmechanik als Standardlösung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet vor einer titanischen Stützkonstruktion, an der Maßstab und Gewicht der Region sofort sichtbar werden. Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Wir teilen Lasten, weil selbst Riesen irgendwann müde werden.«

**Elias Renn:** »Technisch heißt das für mich: Wir bauen für das Gewicht, das wirklich kommt, nicht für das, das bequem wäre.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: riesen waren Mitbauer der alten Zivilisation und keine bloßen Randwesen. **Kharum Tor:** »Das ist alte Lasttechnik. Wer sie gebaut hat, kannte unsere Berge — vielleicht besser als wir.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Noch eine Lastverlagerung. Wenn das Fundament ruhig bleibt, akzeptiere ich die Konstruktion.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Kharum Tor:** »Gut. Das Gewicht ist verteilt, nicht versteckt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Titania und Riesenbund**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Titania und Riesenbund.
- Storyflag: `st_ult_04_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_04_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-04-03 — Brücke der Bündnisse

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Gemeinsamer Wiederaufbau einer Megalithbrücke als Vertrauensprojekt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer Riesenhalle, deren aktive Arbeitsbereiche im Hintergrund weiterlaufen. Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Wir teilen Lasten, weil selbst Riesen irgendwann müde werden.«

**Elias Renn:** »Technisch heißt das für mich: Wir bauen für das Gewicht, das wirklich kommt, nicht für das, das bequem wäre.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal an den Fugen und der Unterseite der Brückensegmente konkret: riesen waren Mitbauer der alten Zivilisation und keine bloßen Randwesen. **Kharum Tor:** »Nichts versetzen. Erst Schwingung, Fuge und Lastbezug messen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Wir prüfen den Rückweg. Heben ohne sicheres Absetzen ist keine Technik.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Kharum Tor:** »Die Last ist verteilt und die Konstruktion bleibt ruhig. Titania akzeptiert das.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Titania und Riesenbund**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Titania und Riesenbund.
- Storyflag: `st_ult_04_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_04_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-04-04 — Schwingungsarchiv

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bergung eines Schwingungsarchivs aus einer unterirdischen Halle.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer Hochlandterrasse zwischen Megalithen, Lastwegen und schweren Hebesystemen. Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Baut langsam genug, dass ihr das Gewicht versteht. Danach könnt ihr schnell werden.«

**Elias Renn:** »Technisch heißt das für mich: Last verteilen, Fundament lesen, Rückweg planen. Kraft allein ersetzt keine Statik.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal in Bearbeitungsspuren tief im Stein konkret: riesen waren Mitbauer der alten Zivilisation und keine bloßen Randwesen. **Kharum Tor:** »Nichts versetzen. Erst Schwingung, Fuge und Lastbezug messen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Noch eine Lastverlagerung. Wenn das Fundament ruhig bleibt, akzeptiere ich die Konstruktion.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Kharum Tor:** »Die Last ist verteilt und die Konstruktion bleibt ruhig. Titania akzeptiert das.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Titania und Riesenbund**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Titania und Riesenbund.
- Storyflag: `st_ult_04_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_04_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-04-05 — Wer das Gewicht trug

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erkenntnis, dass Riesen am alten Weltverbund beteiligt und keine bloßen Randfiguren waren.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer Hochlandterrasse zwischen Megalithen, Lastwegen und schweren Hebesystemen. Akt 4 „Hallen der Riesen“: Titania, Riesenbund und Megalithtechnik. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Baut langsam genug, dass ihr das Gewicht versteht. Danach könnt ihr schnell werden.«

**Elias Renn:** »Technisch heißt das für mich: Wir bauen für das Gewicht, das wirklich kommt, nicht für das, das bequem wäre.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: riesen waren Mitbauer der alten Zivilisation und keine bloßen Randwesen. **Kharum Tor:** »Nichts versetzen. Erst Schwingung, Fuge und Lastbezug messen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Wir prüfen den Rückweg. Heben ohne sicheres Absetzen ist keine Technik.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Jetzt kann ich nachts schlafen, ohne auf das Geräusch eines einzelnen Lagers zu warten.«

**Kharum Tor:** »Die Last ist verteilt und die Konstruktion bleibt ruhig. Titania akzeptiert das.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Titania und Riesenbund**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Titania und Riesenbund.
- Storyflag: `st_ult_04_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_04_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-05-01 — Stadt unter Asche

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichtung eines aschesicheren Stützpunkts und stabiler Kühl-/Energiekreisläufe.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einer gesicherten Plattform über glühenden Spalten; Flugbewegungen bleiben im Hintergrund sichtbar. Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Ignaria verzeiht keine Nachlässigkeit. Feuer verhandelt nicht, und Drachen noch weniger.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Kein Zwang, keine Jagd. Wenn Zusammenarbeit entsteht, dann freiwillig.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: drachen sind intelligente Vertragspartner und alte Atmosphärentechnik war Teil der Weltgitter-Katastrophe. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Ein letzter Flug mit voller Reserve. Danach können wir den Korridor freigeben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Vaelis:** »So kann Ignaria mit uns arbeiten, ohne dass wir es besitzen müssen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ignaria, Drachenabkommen und Ätherflug**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ignaria, Drachenabkommen und Ätherflug.
- Storyflag: `st_ult_05_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_05_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-05-02 — Abkommen mit Flügeln

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Beobachtung intelligenter Drachen und Aufbau eines ersten Schutzabkommens.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen dunklem Gestein und ätherischen Flugstrukturen, bevor die Kamera auf das konkrete Ziel zieht. Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Hitze ist kein Feind. Sie wird gefährlich, wenn jemand glaubt, sie vollständig zu beherrschen.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Zeit und Reserve liegen im Plan. Wenn die zweite Fahrt genauso aussieht, haben wir eine Route.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Reaktion der Drachen, bevor menschliche Sensoren ausschlagen konkret: drachen sind intelligente Vertragspartner und alte Atmosphärentechnik war Teil der Weltgitter-Katastrophe. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Noch ein Zyklus bei höherer Hitze. Wenn die Schutzgrenzen sauber bleiben, ist die Anlage bereit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Vaelis:** »Gut. Wir haben die Grenze genutzt, ohne so zu tun, als wäre sie verschwunden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ignaria, Drachenabkommen und Ätherflug**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ignaria, Drachenabkommen und Ätherflug.
- Storyflag: `st_ult_05_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_05_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-05-03 — Erster Ätherflug

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bau eines experimentellen Ätherfluggeräts für eine Hochatmosphärenmission.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen dunklem Gestein und ätherischen Flugstrukturen, bevor die Kamera auf das konkrete Ziel zieht. Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Unsere Technik funktioniert nahe an Grenzen. Deshalb markieren wir sie deutlicher als andere.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: drachen sind intelligente Vertragspartner und alte Atmosphärentechnik war Teil der Weltgitter-Katastrophe. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Fast fertig. Jetzt prüfen wir den Abbruch, nicht den Erfolg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Jetzt würde ich mein eigenes Schiff darauf setzen. Das reicht mir als Abnahme.«

**Vaelis:** »Der Korridor ist offen, und niemand musste dafür gejagt oder gezwungen werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ignaria, Drachenabkommen und Ätherflug**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ignaria, Drachenabkommen und Ätherflug.
- Storyflag: `st_ult_05_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_05_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-05-04 — Ring im Feuer

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Sicherung eines uralten Flugrings während einer vulkanischen Störung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen dunklem Gestein und ätherischen Flugstrukturen, bevor die Kamera auf das konkrete Ziel zieht. Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Hitze ist kein Feind. Sie wird gefährlich, wenn jemand glaubt, sie vollständig zu beherrschen.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: drachen sind intelligente Vertragspartner und alte Atmosphärentechnik war Teil der Weltgitter-Katastrophe. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Ein letzter Flug mit voller Reserve. Danach können wir den Korridor freigeben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Vaelis:** »Der Korridor ist offen, und niemand musste dafür gejagt oder gezwungen werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ignaria, Drachenabkommen und Ätherflug**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ignaria, Drachenabkommen und Ätherflug.
- Storyflag: `st_ult_05_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_05_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-05-05 — Asche der Vergangenheit

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Marek Vale

### Dramatischer Zweck
Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Enthüllung von Aufzeichnungen über eine frühere Atmosphären- und Energiekatastrophe.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen dunklem Gestein und ätherischen Flugstrukturen, bevor die Kamera auf das konkrete Ziel zieht. Akt 5 „Feuer und Himmel“: Ignaria, Drachen und Ätherflug. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Ignaria verzeiht keine Nachlässigkeit. Feuer verhandelt nicht, und Drachen noch weniger.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Marek Vale:** »Der erste Lauf ist sauber. Jetzt will ich denselben Weg noch einmal sehen, ohne dass jemand am Kai improvisiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: drachen sind intelligente Vertragspartner und alte Atmosphärentechnik war Teil der Weltgitter-Katastrophe. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Fast fertig. Jetzt prüfen wir den Abbruch, nicht den Erfolg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Marek Vale:** »So wird aus Entfernung Handel: nicht durch Mut, sondern durch Wiederholbarkeit.«

**Vaelis:** »Dann fliegen wir weiter — mit Respekt vor dem, was nicht uns gehört.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Ignaria, Drachenabkommen und Ätherflug**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Ignaria, Drachenabkommen und Ätherflug.
- Storyflag: `st_ult_05_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_05_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-06-01 — Hafen der Tiefe

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Aufbau eines Inselhafens und einer zuverlässigen Entsalzungs-/Flottenbasis.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im nassen Dockbereich, von wo aus die Kamera auf Meer, Sonarstation und Missionsort übergeht. Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Wenn das Sonar etwas Merkwürdiges meldet, heißt das zuerst: langsamer werden.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: pelagias versunkene Städte zeigen, dass die alte Katastrophe global und mehrdimensional wirkte. **Neris Pell:** »Sonarspur sichern und Abstand halten. Große Dinge im Wasser brauchen keine Einladung, um gefährlich zu werden.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Fast fertig. Ich will den Notaufstieg einmal vollständig sehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Neris Pell:** »Der Weg ist offen. Wir lassen genug Reserve, um ihn auch wieder zu schließen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Pelagia, Tiefsee und versunkene Städte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Pelagia, Tiefsee und versunkene Städte.
- Storyflag: `st_ult_06_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_06_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-06-02 — Stadt unter Wasser

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erkundung eines versunkenen Stadtbezirks mit Druckfahrzeugen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im nassen Dockbereich, von wo aus die Kamera auf Meer, Sonarstation und Missionsort übergeht. Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Unter Wasser wird jeder kleine Fehler zu einem Druckproblem. Deshalb sind unsere Reserven selten klein.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: pelagias versunkene Städte zeigen, dass die alte Katastrophe global und mehrdimensional wirkte. **Neris Pell:** »Position markieren, passiv beobachten, keine aktive Annäherung. Tiefe belohnt Zurückhaltung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Noch ein Druckzyklus bis zur geplanten Tiefe. Wenn alle Reserven unangetastet bleiben, gebe ich frei.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Neris Pell:** »Tiefe und Rückkehr sind beide abgesichert. Damit wird die Route Alltag.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Pelagia, Tiefsee und versunkene Städte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Pelagia, Tiefsee und versunkene Städte.
- Storyflag: `st_ult_06_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_06_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-06-03 — Die Leviathanlinie

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kontakt mit einem Tiefenleviathan und Einrichtung eines geschützten Korridors.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einer Plattform knapp über der Wasserlinie; darunter sind Tiefenlichter und Versorgungsschächte zu sehen. Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Wenn das Sonar etwas Merkwürdiges meldet, heißt das zuerst: langsamer werden.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: pelagias versunkene Städte zeigen, dass die alte Katastrophe global und mehrdimensional wirkte. **Neris Pell:** »Position markieren, passiv beobachten, keine aktive Annäherung. Tiefe belohnt Zurückhaltung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Ein letzter Schleusenlauf mit voller Nutzlast. Danach zählt es als Alltag.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Neris Pell:** »Damit kann Pelagia die Route nutzen, ohne jeden Tauchgang zum Abenteuer zu machen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Pelagia, Tiefsee und versunkene Städte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Pelagia, Tiefsee und versunkene Städte.
- Storyflag: `st_ult_06_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_06_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-06-04 — Druck und Erinnerung

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bergen einer Tiefenkarte, die mehrere uralte Portalknoten zeigt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im nassen Dockbereich, von wo aus die Kamera auf Meer, Sonarstation und Missionsort übergeht. Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Wenn das Sonar etwas Merkwürdiges meldet, heißt das zuerst: langsamer werden.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Druckschicht, die auf normalen Karten fehlt konkret: pelagias versunkene Städte zeigen, dass die alte Katastrophe global und mehrdimensional wirkte. **Neris Pell:** »Position markieren, passiv beobachten, keine aktive Annäherung. Tiefe belohnt Zurückhaltung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Wir schließen erst, wenn der Rückweg genauso zuverlässig ist wie der Abstieg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Neris Pell:** »Der Weg ist offen. Wir lassen genug Reserve, um ihn auch wieder zu schließen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Pelagia, Tiefsee und versunkene Städte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Pelagia, Tiefsee und versunkene Städte.
- Storyflag: `st_ult_06_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_06_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-06-05 — Tor unter dem Meer

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Wiederinbetriebnahme eines Teils der versunkenen Infrastruktur ohne großflächige Entwässerung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einer Plattform knapp über der Wasserlinie; darunter sind Tiefenlichter und Versorgungsschächte zu sehen. Akt 6 „Städte unter dem Meer“: Pelagia, Tiefsee und Leviathane. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Wenn das Sonar etwas Merkwürdiges meldet, heißt das zuerst: langsamer werden.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir testen die Tiefe mit Reserve; das Meer gibt uns keine zweite Chance aus Höflichkeit.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Hinweis wird diesmal in den Torprotokollen und der Phasenlage des Tors konkret: pelagias versunkene Städte zeigen, dass die alte Katastrophe global und mehrdimensional wirkte. **Neris Pell:** »Position markieren, passiv beobachten, keine aktive Annäherung. Tiefe belohnt Zurückhaltung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Wir schließen erst, wenn der Rückweg genauso zuverlässig ist wie der Abstieg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Neris Pell:** »Tiefe und Rückkehr sind beide abgesichert. Damit wird die Route Alltag.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Pelagia, Tiefsee und versunkene Städte**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Pelagia, Tiefsee und versunkene Städte.
- Storyflag: `st_ult_06_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_06_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-07-01 — Die Caelari treten hervor

**Typ:** Hauptstory · **Region:** Ultima / Caelari · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Abgleich widersprüchlicher Erinnerungskristalle aus allen Regionen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter einem ruhigen Resonanzfeld, das auf anwesende Caelari sichtbar, aber nicht spektakulär reagiert. Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Die Orden streiten nicht darüber, ob das Gitter funktioniert. Sie streiten darüber, wer entscheiden darf, wann es funktionieren soll.«

**Serin Ael:** »Jede Verbindung braucht eine Grenze, die auch praktisch funktioniert.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Caelari sind intern gespalten und kennen Teile der alten Katastrophe, widersprechen sich aber über Ursache und Schuld. **Iriath:** »Der Fund widerspricht mindestens zwei Ordenstraditionen. Das macht ihn wertvoll und gefährlich.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Fast fertig. Jetzt testen wir die Grenze, an der Zustimmung zurückgezogen wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Iriath:** »So kann das Netz tragen, ohne wieder zum Anspruch auf Kontrolle zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Caelari-Konvent und Ordenbeziehungen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Caelari-Konvent und Ordenbeziehungen.
- Storyflag: `st_ult_07_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_07_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-07-02 — Sechs Orden

**Typ:** Hauptstory · **Region:** Ultima / Caelari · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Aufbau eines weltweiten sicheren Datennetzes mit redundanten Archivkopien.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter einem ruhigen Resonanzfeld, das auf anwesende Caelari sichtbar, aber nicht spektakulär reagiert. Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Versiegelung kann Schutz sein oder Kontrolle. Beides sieht von außen oft gleich aus.«

**Serin Ael:** »Technisch können wir viel. Entscheidend ist, was davon mit Zustimmung geschieht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Caelari sind intern gespalten und kennen Teile der alten Katastrophe, widersprechen sich aber über Ursache und Schuld. **Iriath:** »Mehr als ein Orden bekommt Zugriff auf die Dokumentation. Kein einzelner Deutungsanspruch.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Noch eine Prüfung: Kann jede beteiligte Seite die Verbindung ohne Strafe verlassen? Wenn nicht, sind wir nicht fertig.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Iriath:** »So kann das Netz tragen, ohne wieder zum Anspruch auf Kontrolle zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Caelari-Konvent und Ordenbeziehungen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Caelari-Konvent und Ordenbeziehungen.
- Storyflag: `st_ult_07_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_07_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-07-03 — Die Streitfrage

**Typ:** Hauptstory · **Region:** Ultima / Caelari · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entschlüsselung der Gründe, warum gefährliche Systeme nach der Katastrophe getrennt wurden.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem neutralen Versammlungsort zwischen Ultima-Technik und Caelari-Architektur. Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Ein Netz verbindet. Es gibt niemandem das Recht, über die Verbundenen zu verfügen.«

**Serin Ael:** »Technisch können wir viel. Entscheidend ist, was davon mit Zustimmung geschieht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Caelari sind intern gespalten und kennen Teile der alten Katastrophe, widersprechen sich aber über Ursache und Schuld. **Iriath:** »Mehr als ein Orden bekommt Zugriff auf die Dokumentation. Kein einzelner Deutungsanspruch.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Ein letzter Zyklus ohne privilegierten Zugriff. Danach kann der Konvent entscheiden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Iriath:** »Die Verbindung steht und respektiert Zustimmung. Genau deshalb darf sie bestehen bleiben.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Caelari-Konvent und Ordenbeziehungen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Caelari-Konvent und Ordenbeziehungen.
- Storyflag: `st_ult_07_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_07_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-07-04 — Wacht am Gitter

**Typ:** Hauptstory · **Region:** Ultima / Caelari · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Verhandlung zwischen Fraktionen über Umfang und Tempo der Wissensfreigabe.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem neutralen Versammlungsort zwischen Ultima-Technik und Caelari-Architektur. Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Macht ist im Äther leicht zu übertragen. Verantwortung deutlich weniger.«

**Serin Ael:** »Jede Verbindung braucht eine Grenze, die auch praktisch funktioniert.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Die erste Verbindung hält. In Aurelia beginnt die eigentliche Prüfung immer danach.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Caelari sind intern gespalten und kennen Teile der alten Katastrophe, widersprechen sich aber über Ursache und Schuld. **Iriath:** »Mehr als ein Orden bekommt Zugriff auf die Dokumentation. Kein einzelner Deutungsanspruch.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Ein letzter Zyklus ohne privilegierten Zugriff. Danach kann der Konvent entscheiden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Iriath:** »Gut. Die Verbindung schafft Möglichkeit, keine Pflicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Caelari-Konvent und Ordenbeziehungen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Caelari-Konvent und Ordenbeziehungen.
- Storyflag: `st_ult_07_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_07_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-07-05 — Konvent der Resonanz

**Typ:** Hauptstory · **Region:** Ultima / Caelari · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erkennen, dass der alte Weltverbund technisch leistungsfähig, organisatorisch aber zu wenig fehlertolerant war.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter einem ruhigen Resonanzfeld, das auf anwesende Caelari sichtbar, aber nicht spektakulär reagiert. Akt 7 „Die versiegelte Geschichte“: Zusammenführung der fünf regionalen Archive. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Die Orden streiten nicht darüber, ob das Gitter funktioniert. Sie streiten darüber, wer entscheiden darf, wann es funktionieren soll.«

**Serin Ael:** »Technisch können wir viel. Entscheidend ist, was davon mit Zustimmung geschieht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Caelari sind intern gespalten und kennen Teile der alten Katastrophe, widersprechen sich aber über Ursache und Schuld. **Iriath:** »Öffnet nichts allein. Manche Siegel schützen nicht den Inhalt vor uns, sondern uns vor der Entscheidung, die der Inhalt erzwingt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Noch eine Prüfung: Kann jede beteiligte Seite die Verbindung ohne Strafe verlassen? Wenn nicht, sind wir nicht fertig.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Caelari-Konvent und Ordenbeziehungen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Caelari-Konvent und Ordenbeziehungen.
- Storyflag: `st_ult_07_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_07_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-08-01 — Zwei Tore

**Typ:** Hauptstory · **Region:** Aurelia / Portalnetz · **Leitfigur:** Iriath · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kalibrierung zweier Testtore für kleine Frachtmengen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen zwei räumlich getrennten Toranlagen, die im UI als ein gemeinsames Netz erscheinen. Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Macht ist im Äther leicht zu übertragen. Verantwortung deutlich weniger.«

**Elias Renn:** »Zwei Seiten, ein Takt. Wenn eine Seite nicht sauber abbrechen kann, ist das kein Tor, sondern eine Falle.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Das hält unter Normalbetrieb. Entscheidend ist jetzt der Wiederanlauf nach einer Störung.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: portale waren Infrastruktur des alten Weltverbunds und benötigen politische wie technische Sicherheitsregeln. **Iriath:** »Keine Aktivierung aus Neugier. Erst Zustand und Rückfallprotokoll sichern.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Noch eine Prüfung: Kann jede beteiligte Seite die Verbindung ohne Strafe verlassen? Wenn nicht, sind wir nicht fertig.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Iriath:** »Das Tor verbindet, ohne den Rückweg zu verschlucken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **begrenztes Portalnetz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** begrenztes Portalnetz.
- Storyflag: `st_ult_08_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_08_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-08-02 — Reserve für Menschen

**Typ:** Hauptstory · **Region:** Aurelia / Portalnetz · **Leitfigur:** Iriath · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Aufbau ausreichender Energie- und Sicherheitsreserve für Personenverkehr.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen zwei räumlich getrennten Toranlagen, die im UI als ein gemeinsames Netz erscheinen. Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Versiegelung kann Schutz sein oder Kontrolle. Beides sieht von außen oft gleich aus.«

**Elias Renn:** »Zwei Seiten, ein Takt. Wenn eine Seite nicht sauber abbrechen kann, ist das kein Tor, sondern eine Falle.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Leistungsbilanz ohne sichtbaren Brennstoff konkret: portale waren Infrastruktur des alten Weltverbunds und benötigen politische wie technische Sicherheitsregeln. **Iriath:** »Der Fund widerspricht mindestens zwei Ordenstraditionen. Das macht ihn wertvoll und gefährlich.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Wir schließen erst, wenn das System ein Nein genauso sauber verarbeitet wie ein Ja.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Iriath:** »Gut. Die Verbindung schafft Möglichkeit, keine Pflicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **begrenztes Portalnetz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** begrenztes Portalnetz.
- Storyflag: `st_ult_08_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_08_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-08-03 — Phasenfehler

**Typ:** Hauptstory · **Region:** Aurelia / Portalnetz · **Leitfigur:** Iriath · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Behebung einer Phaseninstabilität über Forschung und Materialqualität statt Minispiel.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem abgeschalteten Portalring, dessen einzelne Segmente nur auf Prüfimpulse reagieren. Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Macht ist im Äther leicht zu übertragen. Verantwortung deutlich weniger.«

**Elias Renn:** »Zwei Seiten, ein Takt. Wenn eine Seite nicht sauber abbrechen kann, ist das kein Tor, sondern eine Falle.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: portale waren Infrastruktur des alten Weltverbunds und benötigen politische wie technische Sicherheitsregeln. **Iriath:** »Keine Aktivierung aus Neugier. Erst Zustand und Rückfallprotokoll sichern.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Fast fertig. Jetzt testen wir die Grenze, an der Zustimmung zurückgezogen wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Iriath:** »So kann das Netz tragen, ohne wieder zum Anspruch auf Kontrolle zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **begrenztes Portalnetz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** begrenztes Portalnetz.
- Storyflag: `st_ult_08_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_08_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-08-04 — Protokoll der Schwelle

**Typ:** Hauptstory · **Region:** Aurelia / Portalnetz · **Leitfigur:** Iriath · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Einbindung von Torwächtern und regionalen Behörden in ein gemeinsames Betriebsprotokoll.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem abgeschalteten Portalring, dessen einzelne Segmente nur auf Prüfimpulse reagieren. Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Macht ist im Äther leicht zu übertragen. Verantwortung deutlich weniger.«

**Elias Renn:** »Wir testen zuerst kleine Lasten und einen vollständigen Rückfallzustand.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Die Last kommt sauber durch. Jetzt nehme ich einen Puffer weg und sehe, ob das System immer noch ehrlich bleibt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: portale waren Infrastruktur des alten Weltverbunds und benötigen politische wie technische Sicherheitsregeln. **Iriath:** »Keine Aktivierung aus Neugier. Erst Zustand und Rückfallprotokoll sichern.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Ein letzter Zyklus ohne privilegierten Zugriff. Danach kann der Konvent entscheiden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Damit kann die nächste Stufe kommen. Diese hier trägt ihr Gewicht.«

**Iriath:** »Das Tor verbindet, ohne den Rückweg zu verschlucken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **begrenztes Portalnetz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** begrenztes Portalnetz.
- Storyflag: `st_ult_08_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_08_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-08-05 — Das kleine Portalnetz

**Typ:** Hauptstory · **Region:** Aurelia / Portalnetz · **Leitfigur:** Iriath · **Unterstützung:** Elias Renn

### Dramatischer Zweck
Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Freischaltung eines begrenzten Portalnetzes für bereits erschlossene Standorte.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen zwei räumlich getrennten Toranlagen, die im UI als ein gemeinsames Netz erscheinen. Akt 8 „Tore zwischen den Regionen“: Portaltechnik und neue Logistik ohne klassische Netze zu entwerten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Zugang und Zustimmung sind zwei verschiedene Dinge. Unsere Geschichte wurde gefährlich, als wir sie verwechselten.«

**Elias Renn:** »Wir testen zuerst kleine Lasten und einen vollständigen Rückfallzustand.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Elias Renn:** »Der erste Test ist sauber. Beim zweiten nehme ich ihm die bequemen Bedingungen weg.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: portale waren Infrastruktur des alten Weltverbunds und benötigen politische wie technische Sicherheitsregeln. **Iriath:** »Wir bewahren zuerst den Zustand. Über Zugang entscheidet danach mehr als eine Stimme.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Fast fertig. Jetzt testen wir die Grenze, an der Zustimmung zurückgezogen wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **begrenztes Portalnetz**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** begrenztes Portalnetz.
- Storyflag: `st_ult_08_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_08_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-09-01 — Station über den Wolken

**Typ:** Hauptstory · **Region:** Aurelia + Ignaria + Pelagia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Vaelis

### Dramatischer Zweck
Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erkundung einer alten Hochatmosphärenstation.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Karten, Instrumenten und einer offenen Sicht auf Ultimas Himmel. Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Vaelis:** »Für den Korridor heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Vaelis:** »Die Temperatur bleibt im Fenster. Jetzt erhöhen wir die Last, nicht die Geschwindigkeit.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sternenkarten und ein fremder Navigationskern lassen den Ursprung einzelner Alttechnologien bewusst offen. **Dr. Mira Halden:** »Drei Datensätze getrennt halten, bevor wir sie auf eine gemeinsame Erklärung zwingen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Noch eine unabhängige Messung. Wenn die dasselbe sagt, dürfen wir anfangen, Konsequenzen zu ziehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Vaelis:** »Gut. Wir haben die Grenze genutzt, ohne so zu tun, als wäre sie verschwunden.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hochatmosphären-/Sternenfahrer-Forschungszweig**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hochatmosphären-/Sternenfahrer-Forschungszweig.
- Storyflag: `st_ult_09_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_09_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-09-02 — Drei Sternkarten

**Typ:** Hauptstory · **Region:** Aurelia + Ignaria + Pelagia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Vaelis

### Dramatischer Zweck
Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Vergleich von Sternenkarten aus Aurelia, Ignaria und Pelagia.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Forschungsplattform, deren Datenströme aus Aurelia, Ignaria und Pelagia eintreffen. Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Vaelis:** »Für den Korridor heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Vaelis:** »Die erste Passage war ruhig. Beim zweiten Lauf rechnen wir mit Seitenwind und mehr Masse.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sternenkarten und ein fremder Navigationskern lassen den Ursprung einzelner Alttechnologien bewusst offen. **Dr. Mira Halden:** »Drei Datensätze getrennt halten, bevor wir sie auf eine gemeinsame Erklärung zwingen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Vaelis:** »Dann fliegen wir weiter — mit Respekt vor dem, was nicht uns gehört.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hochatmosphären-/Sternenfahrer-Forschungszweig**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hochatmosphären-/Sternenfahrer-Forschungszweig.
- Storyflag: `st_ult_09_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_09_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-09-03 — Der fremde Kern

**Typ:** Hauptstory · **Region:** Aurelia + Ignaria + Pelagia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Vaelis

### Dramatischer Zweck
Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Bergung eines nicht zuordenbaren Navigationskerns mit absichtlich offener Herkunft.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Karten, Instrumenten und einer offenen Sicht auf Ultimas Himmel. Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Vaelis:** »Für den Korridor heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Vaelis:** »Die Temperatur bleibt im Fenster. Jetzt erhöhen wir die Last, nicht die Geschwindigkeit.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sternenkarten und ein fremder Navigationskern lassen den Ursprung einzelner Alttechnologien bewusst offen. **Dr. Mira Halden:** »Drei Datensätze getrennt halten, bevor wir sie auf eine gemeinsame Erklärung zwingen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Letzte Gegenprobe. Danach bekommt die Hypothese einen Namen — vorher nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Vaelis:** »Der Korridor ist offen, und niemand musste dafür gejagt oder gezwungen werden.«

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hochatmosphären-/Sternenfahrer-Forschungszweig**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hochatmosphären-/Sternenfahrer-Forschungszweig.
- Storyflag: `st_ult_09_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_09_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-09-04 — Wem gehört die Wahrheit?

**Typ:** Hauptstory · **Region:** Aurelia + Ignaria + Pelagia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Vaelis

### Dramatischer Zweck
Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheidung, ob die Information öffentlich, wissenschaftlich beschränkt oder vorerst versiegelt wird.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Karten, Instrumenten und einer offenen Sicht auf Ultimas Himmel. Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Wir sammeln erst Daten. Eine gute Geschichte kann warten; ein sauberer Messpunkt nicht.«

**Vaelis:** »Für den Korridor heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Vaelis:** »Die erste Passage war ruhig. Beim zweiten Lauf rechnen wir mit Seitenwind und mehr Masse.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sternenkarten und ein fremder Navigationskern lassen den Ursprung einzelner Alttechnologien bewusst offen. **Dr. Mira Halden:** »Drei Datensätze getrennt halten, bevor wir sie auf eine gemeinsame Erklärung zwingen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Letzte Gegenprobe. Danach bekommt die Hypothese einen Namen — vorher nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Vaelis:** »Gut. Wir haben die Grenze genutzt, ohne so zu tun, als wäre sie verschwunden.«

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hochatmosphären-/Sternenfahrer-Forschungszweig**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hochatmosphären-/Sternenfahrer-Forschungszweig.
- Storyflag: `st_ult_09_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_09_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-09-05 — Weltkonferenz

**Typ:** Hauptstory · **Region:** Aurelia + Ignaria + Pelagia · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Vaelis

### Dramatischer Zweck
Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Vorbereitung einer Weltkonferenz über die nächsten Schritte des Nexusprojekts.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einer Hochatmosphärenstation mit sichtbaren Verbindungen zu Meer und Weltgitter. Akt 9 „Der Himmel erinnert sich“: Sternenfahrer-Hinweise und Hochatmosphäre. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Vaelis:** »Für den Korridor heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Vaelis:** »Das System hält die Hitze. Jetzt sehen wir, ob die Schutzschicht den Zyklus wiederholt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: sternenkarten und ein fremder Navigationskern lassen den Ursprung einzelner Alttechnologien bewusst offen. **Dr. Mira Halden:** »Drei Datensätze getrennt halten, bevor wir sie auf eine gemeinsame Erklärung zwingen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Vaelis:** »Gut. Wir haben die Grenze genutzt, ohne so zu tun, als wäre sie verschwunden.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Hochatmosphären-/Sternenfahrer-Forschungszweig**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Hochatmosphären-/Sternenfahrer-Forschungszweig.
- Storyflag: `st_ult_09_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_09_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-10-01 — Kaskade

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Kaskadierende Resonanzstörungen treffen mehrere Regionen gleichzeitig.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Netzleitstelle, in der alle fünf Ultima-Regionen gleichzeitig als reale Lasten und Reserven sichtbar sind. Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Serin Ael:** »Für Aurelia heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Die erste Verbindung hält. In Aurelia beginnt die eigentliche Prüfung immer danach.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die alten Sicherheitsreserven waren unzureichend; das neue System muss dezentrale Rückfallebenen besitzen. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Nexus-Sicherheitsarchitektur**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Nexus-Sicherheitsarchitektur.
- Storyflag: `st_ult_10_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_10_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-10-02 — Reservewege

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Reservekapazitäten, alternative Routen und regionale Spezialisierungen müssen zusammenarbeiten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Netzleitstelle, in der alle fünf Ultima-Regionen gleichzeitig als reale Lasten und Reserven sichtbar sind. Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Ich will keine schöne Momentaufnahme. Ich will eine Lösung, die morgen früh noch funktioniert.«

**Serin Ael:** »Für Aurelia heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Ihr habt die Funktion erreicht. Jetzt prüft, ob eure Lösung auch Rücksicht auf das Netz nimmt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die alten Sicherheitsreserven waren unzureichend; das neue System muss dezentrale Rückfallebenen besitzen. **Helena Voss:** »Sichert den Fund und schreibt auf, wer ihn wo gesehen hat. Keine Gerüchte, bevor wir wissen, womit wir es zu tun haben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Helena Voss:** »Die Systeme arbeiten zusammen, ohne ihre eigene Reserve und Entscheidungshoheit zu verlieren.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Nexus-Sicherheitsarchitektur**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Nexus-Sicherheitsarchitektur.
- Storyflag: `st_ult_10_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_10_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-10-03 — Verbündete Systeme

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Riesen sichern Megastrukturen, Drachen helfen bei Atmosphärenbeobachtung, Pelagia stabilisiert Kühlung und Viridia medizinische Versorgung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im internationalen Ultima-Konvent, bevor die Kamera direkt in die betroffene Region springt. Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Serin Ael:** »Für Aurelia heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Die erste Verbindung hält. In Aurelia beginnt die eigentliche Prüfung immer danach.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Reaktion der Drachen, bevor menschliche Sensoren ausschlagen konkret: die alten Sicherheitsreserven waren unzureichend; das neue System muss dezentrale Rückfallebenen besitzen. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Nexus-Sicherheitsarchitektur**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Nexus-Sicherheitsarchitektur.
- Storyflag: `st_ult_10_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_10_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-10-04 — Priorität

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Der Spieler entscheidet, welche Netze priorisiert werden, ohne einzelne Ventile oder Fahrzeuge mikrozusteuern.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im internationalen Ultima-Konvent, bevor die Kamera direkt in die betroffene Region springt. Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Serin Ael:** »Für Aurelia heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Die erste Verbindung hält. In Aurelia beginnt die eigentliche Prüfung immer danach.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die alten Sicherheitsreserven waren unzureichend; das neue System muss dezentrale Rückfallebenen besitzen. **Helena Voss:** »Niemand baut daraus heute eine Legende. Wir sichern es, prüfen es und reden dann weiter.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Nexus-Sicherheitsarchitektur**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Nexus-Sicherheitsarchitektur.
- Storyflag: `st_ult_10_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_10_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-10-05 — Architektur der Sicherheit

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Nach der Stabilisierung wird klar, welche Sicherheitsarchitektur der neue World Nexus braucht.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Weltgitter-Knoten, dessen Anzeigen die fünf Regionen getrennt und zusammen zeigen. Akt 10 „Das gebrochene Weltgitter“: Krisenphase und Stresstest aller Systeme. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Serin Ael:** »Für Aurelia heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Hinweis wird diesmal im gegenseitigen Phasenabgleich der verbundenen Regionalnetze konkret: die alten Sicherheitsreserven waren unzureichend; das neue System muss dezentrale Rückfallebenen besitzen. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Nexus-Sicherheitsarchitektur**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Nexus-Sicherheitsarchitektur.
- Storyflag: `st_ult_10_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_10_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-11-01 — Kernkomponenten

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Produktion regionaler Nexus-Kernkomponenten in allen fünf Regionen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Netzleitstelle, in der alle fünf Ultima-Regionen gleichzeitig als reale Lasten und Reserven sichtbar sind. Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Iriath:** »Für das Netz heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Technisch ist der Zustand stabil. Politisch beginnt die Prüfung erst jetzt.«
- **Wendepunkt:** Der Hinweis wird diesmal im gegenseitigen Phasenabgleich der verbundenen Regionalnetze konkret: der World Nexus kann nur durch reale Kooperation aller Regionen und Bündnispartner sicher funktionieren. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Systemhinweis:** Dauerhaft freigeschaltet: **World-Nexus-Bauphasen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** World-Nexus-Bauphasen.
- Storyflag: `st_ult_11_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_11_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-11-02 — Redundante Schichten

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Aufbau redundanter Fundament-, Energie-, Bio-, Kühl-, Daten- und Transportmodule.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Netzleitstelle, in der alle fünf Ultima-Regionen gleichzeitig als reale Lasten und Reserven sichtbar sind. Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Ich will keine schöne Momentaufnahme. Ich will eine Lösung, die morgen früh noch funktioniert.«

**Iriath:** »Für das Netz heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Die Verbindung steht, ohne eine Seite zur Reserve der anderen zu machen. Das ist wichtig.«
- **Wendepunkt:** Der Hinweis wird diesmal in einer Leistungsbilanz ohne sichtbaren Brennstoff konkret: der World Nexus kann nur durch reale Kooperation aller Regionen und Bündnispartner sicher funktionieren. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **World-Nexus-Bauphasen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** World-Nexus-Bauphasen.
- Storyflag: `st_ult_11_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_11_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-11-03 — Zustimmung

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Politische Zustimmung der wichtigsten Fraktionen und Bündnispartner.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Weltgitter-Knoten, dessen Anzeigen die fünf Regionen getrennt und zusammen zeigen. Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Ich will keine schöne Momentaufnahme. Ich will eine Lösung, die morgen früh noch funktioniert.«

**Iriath:** »Für das Netz heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der World Nexus kann nur durch reale Kooperation aller Regionen und Bündnispartner sicher funktionieren. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Helena Voss:** »Die Systeme arbeiten zusammen, ohne ihre eigene Reserve und Entscheidungshoheit zu verlieren.«

**Systemhinweis:** Dauerhaft freigeschaltet: **World-Nexus-Bauphasen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** World-Nexus-Bauphasen.
- Storyflag: `st_ult_11_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_11_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-11-04 — Lasttests

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stufenweise Inbetriebnahme mit messbaren Lasttests statt einem einzigen Endklick.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer gemeinsamen Netzleitstelle, in der alle fünf Ultima-Regionen gleichzeitig als reale Lasten und Reserven sichtbar sind. Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Iriath:** »Für das Netz heißt das: Wir testen jede Schicht getrennt, bevor wir sie gemeinsam belasten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Technisch ist der Zustand stabil. Politisch beginnt die Prüfung erst jetzt.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: der World Nexus kann nur durch reale Kooperation aller Regionen und Bündnispartner sicher funktionieren. **Helena Voss:** »Das wird dokumentiert und getrennt von allen Vermutungen gelagert. Mira bekommt zuerst die Fakten.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **World-Nexus-Bauphasen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** World-Nexus-Bauphasen.
- Storyflag: `st_ult_11_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_11_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-11-05 — Die letzte Prüfung

**Typ:** Hauptstory · **Region:** Alle Ultima-Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Iriath

### Dramatischer Zweck
Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Abschlussprüfung durch Wächter, Hüter, Lebensgelehrte, Gipfelhüter, Drachenhüter und Ozeanmeister.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Weltgitter-Knoten, dessen Anzeigen die fünf Regionen getrennt und zusammen zeigen. Akt 11 „World Nexus“: Bau des großen Weltverbund-Megaprojekts. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Iriath:** »Für das Netz heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Hinweis wird diesmal in der Reaktion des Wächters konkret: der World Nexus kann nur durch reale Kooperation aller Regionen und Bündnispartner sicher funktionieren. **Helena Voss:** »Jede Region bestätigt ihren eigenen Befund. Erst danach behandeln wir das als Weltgitter-Ereignis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Wir haben Funktion und Grenze gleichzeitig gebaut. Beides gehört zusammen.«

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Systemhinweis:** Dauerhaft freigeschaltet: **World-Nexus-Bauphasen**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** World-Nexus-Bauphasen.
- Storyflag: `st_ult_11_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_11_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-12-01 — Aktivierung

**Typ:** Hauptstory · **Region:** World Nexus / alle Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stabile Aktivierung des World Nexus ohne Wiederholung der alten Katastrophe.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem der realen Nexus-Ringe; die Simulation läuft im Hintergrund weiter, während die Aktivierung vorbereitet wird. Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Hinweis wird diesmal im gegenseitigen Phasenabgleich der verbundenen Regionalnetze konkret: die Welt bleibt größer als die gelösten Fragen; einige Archive und Herkunftsfragen bleiben bewusst offen. **Helena Voss:** »Nichts am Nexus wird wegen eines einzelnen Signals beschleunigt. Erst Ursache, dann Aktivierung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Storyabschluss, Postgame und Aufbauspielmodus**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Storyabschluss, Postgame und Aufbauspielmodus.
- Storyflag: `st_ult_12_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_12_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-12-02 — Eine offene Welt

**Typ:** Hauptstory · **Region:** World Nexus / alle Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Abschluss der Hauptstory und profilweite Freischaltung des **Aufbauspielmodus** (`buildModeUnlocked = true`).
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in der Nexus-Leitstelle, während die fünf regionalen Beiträge als getrennte Systeme sichtbar bleiben. Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Welt bleibt größer als die gelösten Fragen; einige Archive und Herkunftsfragen bleiben bewusst offen. **Helena Voss:** »Das wird dokumentiert und getrennt von allen Vermutungen gelagert. Mira bekommt zuerst die Fakten.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Storyabschluss, Postgame und Aufbauspielmodus**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Storyabschluss, Postgame und Aufbauspielmodus.
- Storyflag: `st_ult_12_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_12_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-12-03 — Nach dem Ende

**Typ:** Hauptstory · **Region:** World Nexus / alle Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Öffnung zusätzlicher Endgame-Forschungs-, Spezialisierungs- und Handelsprojekte.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem der realen Nexus-Ringe; die Simulation läuft im Hintergrund weiter, während die Aktivierung vorbereitet wird. Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Welt bleibt größer als die gelösten Fragen; einige Archive und Herkunftsfragen bleiben bewusst offen. **Helena Voss:** »Nichts am Nexus wird wegen eines einzelnen Signals beschleunigt. Erst Ursache, dann Aktivierung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Storyabschluss, Postgame und Aufbauspielmodus**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Storyabschluss, Postgame und Aufbauspielmodus.
- Storyflag: `st_ult_12_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_12_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-12-04 — Archive ohne Zwang

**Typ:** Hauptstory · **Region:** World Nexus / alle Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Optionale Rekonstruktion weiterer Archive und Portale ohne Zwang zum Storyabschluss.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem der realen Nexus-Ringe; die Simulation läuft im Hintergrund weiter, während die Aktivierung vorbereitet wird. Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir lösen zuerst das Problem, das die Leute heute tatsächlich spüren. Alles andere kann warten.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Welt bleibt größer als die gelösten Fragen; einige Archive und Herkunftsfragen bleiben bewusst offen. **Helena Voss:** »Nichts am Nexus wird wegen eines einzelnen Signals beschleunigt. Erst Ursache, dann Aktivierung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Storyabschluss, Postgame und Aufbauspielmodus**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Storyabschluss, Postgame und Aufbauspielmodus.
- Storyflag: `st_ult_12_04_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_12_04_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## ST-ULT-12-05 — Postgame

**Typ:** Hauptstory · **Region:** World Nexus / alle Regionen · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Diese Mission treibt den Akt konkret voran und muss die fortgeschrittenen Ultima-Systeme im normalen Aufbauspiel nutzbar machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Übergang ins fortgesetzte **Story-Postgame** mit allen fünf Regionen und Handel zur bekannten Welt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in der Nexus-Leitstelle, während die fünf regionalen Beiträge als getrennte Systeme sichtbar bleiben. Akt 12 „Eine offene Welt“: Ende des Hauptbogens und Übergang ins freie Endgame. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Jede Region bleibt eigenständig. Der Nexus darf verbinden, aber nie einen einzigen Ausfallpunkt schaffen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Hinweis wird diesmal am zentralen Objekt dieser Mission konkret: die Welt bleibt größer als die gelösten Fragen; einige Archive und Herkunftsfragen bleiben bewusst offen. **Helena Voss:** »Nichts am Nexus wird wegen eines einzelnen Signals beschleunigt. Erst Ursache, dann Aktivierung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Noch ein Durchlauf unter echter Belastung. Wenn es dann hält, können wir die Leute darauf bauen lassen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Storyabschluss, Postgame und Aufbauspielmodus**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Storyabschluss, Postgame und Aufbauspielmodus.
- Storyflag: `st_ult_12_05_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `st_ult_12_05_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---

# 4. Feste narrative Nebenquestketten

Diese Nebenquests sind **nicht prozedural**. Sie besitzen feste Figuren- und Storyinhalte, sind optional und dürfen die Hauptstory nicht hardlocken. Wiederholbare Wirtschaftsaufträge werden separat als datengetriebene Templates behandelt und benötigen kein festes Drehbuch pro zufälliger Instanz.

## Helena Voss — Verantwortung

**Region:** Occidentia · **Status:** optional, feste narrative Questkette
## SQ-HEL-01 — Die leere Liste

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Zeigt Helenas Grundsatz: Verwaltung beginnt bei Menschen, nicht Zahlen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stelle Versorgung für neu ankommende Familien her, bevor Helena ihre Namen offiziell einträgt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Zeigt Helenas Grundsatz: Verwaltung beginnt bei Menschen, nicht Zahlen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Wir entscheiden nicht für eine Statistik. Hinter jeder Zahl stehen Leute, die mit dem Ergebnis leben müssen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: zeigt Helenas Grundsatz: Verwaltung beginnt bei Menschen, nicht Zahlen. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Gebaut ist es. Verlässlich ist es erst, wenn der Betrieb nicht von unserer Aufmerksamkeit abhängt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Helena Voss:** »Dann steht die Grundlage. Den nächsten Schritt gehen wir, ohne diesen hier wieder einzureißen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Helena Voss — Verantwortung“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Helena Voss — Verantwortung“.
- Storyflag: `sq_hel_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_hel_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-HEL-02 — Wer bekommt zuerst?

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Helena lernt, Entscheidungen transparent zu machen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Löse einen Engpass über Prioritäten und Reserveaufbau; keine perfekte Lösung ist möglich.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet nahe dem Hafen, von wo aus die Kamera über die bereits gebaute Stadt zum eigentlichen Missionsort fährt. Helena lernt, Entscheidungen transparent zu machen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Ich will keine schöne Momentaufnahme. Ich will eine Lösung, die morgen früh noch funktioniert.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: helena lernt, Entscheidungen transparent zu machen. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Lasst es einen vollständigen Zyklus laufen. Ich will kein Häkchen, ich will Sicherheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Helena Voss — Verantwortung“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Helena Voss — Verantwortung“.
- Storyflag: `sq_hel_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_hel_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-HEL-03 — Eine Stadt ohne mich

**Typ:** Hauptstory · **Region:** Occidentia · **Leitfigur:** Helena Voss · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Schließt ihren Bogen vom Krisenmanager zur Institutionenbauerin.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte eine Verwaltung, die auch ohne Helenas direkte Präsenz stabil arbeitet.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer echten Straße der wachsenden Occidentia-Siedlung; Arbeiter und Karren bleiben während der Einstellung in Bewegung. Schließt ihren Bogen vom Krisenmanager zur Institutionenbauerin. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Helena Voss:** »Bevor wir größer denken, machen wir diesen Teil verlässlich. Das ist Verantwortung, nicht Vorsicht.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: schließt ihren Bogen vom Krisenmanager zur Institutionenbauerin. **Helena Voss:** »Sichert den Fund und trennt Beobachtung von Gerücht. Wir brauchen noch keine Erklärung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Helena Voss:** »Wir schließen erst ab, wenn der Zustand ohne Sonderbehandlung bestehen bleibt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Helena Voss:** »Für die Stadt ist dabei klar: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Helena Voss — Verantwortung“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Helena Voss — Verantwortung“.
- Storyflag: `sq_hel_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_hel_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Marek Vale — Karten und Schulden

**Region:** Globale bekannte Welt · **Status:** optional, feste narrative Questkette
## SQ-MAR-01 — Eine alte Schuld

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Öffnet Zugang zu alten Seefahrerakten.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Hilf Marek, eine alte Handelsschuld über Warenlieferung statt Geld zu begleichen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im internationalen Hafen, wo Schiffe aus mehreren erschlossenen Regionen gleichzeitig abgefertigt werden. Öffnet Zugang zu alten Seefahrerakten. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Ich brauche keine Heldengeschichte. Gib mir einen klaren Kurs, genug Reserve und einen Hafen, der uns wieder annimmt.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: öffnet Zugang zu alten Seefahrerakten. **Marek Vale:** »Alle vier Quellen getrennt halten. Wenn sie dasselbe Loch zeigen, ist gerade ihre Unabhängigkeit der Beweis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Noch eine Fahrt mit echter Last. Danach weiß ich, ob das eine Route oder nur ein glücklicher Versuch war.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Marek Vale — Karten und Schulden“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Marek Vale — Karten und Schulden“.
- Storyflag: `sq_mar_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mar_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-MAR-02 — Der Kapitän, der nicht zurückkam

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Verbindet persönliche Geschichte mit südlichen Legenden.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Suche über Häfen nach Spuren von Mareks früherem Mentor.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem großen Planungstisch, auf dem nur tatsächlich entdeckte Regionen und Routen eingezeichnet sind. Verbindet persönliche Geschichte mit südlichen Legenden. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Ein Schiff ist nur dann schnell, wenn es nicht wegen schlechter Planung zweimal fahren muss.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: verbindet persönliche Geschichte mit südlichen Legenden. **Marek Vale:** »Das gehört ins Kartenlog. Position genau markieren; auf See ist eine falsche Erinnerung schlimmer als gar keine.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Lasst den Hafen einmal unter voller Abfertigung arbeiten. Dann sehen wir, ob die Zahlen ehrlich waren.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Marek Vale:** »Vom Kurs her ist die Sache einfach: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Marek Vale — Karten und Schulden“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Marek Vale — Karten und Schulden“.
- Storyflag: `sq_mar_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mar_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-MAR-03 — Kurs ohne Karte

**Typ:** Hauptstory · **Region:** Globale bekannte Welt · **Leitfigur:** Marek Vale · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Marek entscheidet sich endgültig für Erforschung statt bloßen Handel.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Führe eine freiwillige Hochrisikoexpedition mit alternativen Routen durch.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Kartenraum mit realen Kartenständen aus allen erschlossenen Regionen; anschließend springt die Kamera in die Weltansicht. Marek entscheidet sich endgültig für Erforschung statt bloßen Handel. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Marek Vale:** »Ein Schiff ist nur dann schnell, wenn es nicht wegen schlechter Planung zweimal fahren muss.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Wendepunkt entsteht in einer überlagerten Kartenschicht: marek entscheidet sich endgültig für Erforschung statt bloßen Handel. **Marek Vale:** »Alle vier Quellen getrennt halten. Wenn sie dasselbe Loch zeigen, ist gerade ihre Unabhängigkeit der Beweis.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Marek Vale:** »Lasst den Hafen einmal unter voller Abfertigung arbeiten. Dann sehen wir, ob die Zahlen ehrlich waren.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Marek Vale:** »Gut. Der Kurs steht, die Reserve bleibt Reserve und niemand muss am Kai raten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Marek Vale — Karten und Schulden“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Marek Vale — Karten und Schulden“.
- Storyflag: `sq_mar_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mar_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Mira Halden — Beweise

**Region:** Mehrere Regionen · **Status:** optional, feste narrative Questkette
## SQ-MIR-01 — Fehlerquote

**Typ:** Hauptstory · **Region:** Mehrere Regionen · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Zeigt Miras Skepsis gegenüber ihren eigenen Erwartungen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Wiederhole eine anomale Messung mit unabhängiger Ausrüstung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Forschungsarbeitsplatz, dessen Proben und Karten nur aus bereits erschlossenen Regionen stammen. Zeigt Miras Skepsis gegenüber ihren eigenen Erwartungen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Ich habe eine Vermutung. Genau deshalb will ich jetzt Messwerte, die mir widersprechen können.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Der Anfang steht. Jetzt bitte nichts schönreden, falls die Versorgung an einer anderen Stelle nachgibt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: zeigt Miras Skepsis gegenüber ihren eigenen Erwartungen. **Dr. Mira Halden:** »Ich sage noch nicht, was es ist. Ich kann aber inzwischen ziemlich gut sagen, was es nicht ist.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Mira Halden — Beweise“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Mira Halden — Beweise“.
- Storyflag: `sq_mir_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mir_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-MIR-02 — Gegenbeweis

**Typ:** Hauptstory · **Region:** Mehrere Regionen · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Das negative Ergebnis macht die spätere Enthüllung glaubwürdiger.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Finanziere ein Projekt, das ausdrücklich versucht, die Ultima-Hypothese zu widerlegen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Reisecontainern, Messgeräten und den Karten der aktuell erschlossenen Gebiete. Das negative Ergebnis macht die spätere Enthüllung glaubwürdiger. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Nichts wäre mir lieber, als mich hier zu irren. Also bauen wir den Versuch so, dass wir das auch merken würden.«

**Helena Voss:** »Für die Stadt ist dabei klar: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Der Anfang steht. Jetzt bitte nichts schönreden, falls die Versorgung an einer anderen Stelle nachgibt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: das negative Ergebnis macht die spätere Enthüllung glaubwürdiger. **Dr. Mira Halden:** »Proben getrennt führen. Gemeinsame Muster zählen erst, wenn ihre Herkunft unabhängig bleibt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Der Befund ist belastbar genug, dass die nächste Frage endlich sinnvoll wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Mira Halden — Beweise“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Mira Halden — Beweise“.
- Storyflag: `sq_mir_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mir_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-MIR-03 — Was wir nicht wissen

**Typ:** Hauptstory · **Region:** Mehrere Regionen · **Leitfigur:** Dr. Mira Halden · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Mira akzeptiert offene Fragen statt einer bequemen Gesamterklärung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Ordne widersprüchliche Quellen in einem Weltarchiv.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einem mobilen Labor, bevor die Kamera zum jeweils relevanten regionalen Objekt springt. Mira akzeptiert offene Fragen statt einer bequemen Gesamterklärung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Mira Halden:** »Bevor jemand eine Erklärung liebt, will ich wissen, ob die Beobachtung überhaupt wiederholbar ist.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »So kann man damit arbeiten. Lasst es laufen; ich will sehen, was nach der ersten Belastung übrig bleibt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: mira akzeptiert offene Fragen statt einer bequemen Gesamterklärung. **Dr. Mira Halden:** »Proben getrennt führen. Gemeinsame Muster zählen erst, wenn ihre Herkunft unabhängig bleibt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Mira Halden:** »Wir sind fast da. Jetzt suche ich absichtlich nach dem Fehler, der alles wieder gewöhnlich machen würde.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Das ist jetzt Teil der Stadt und nicht mehr nur Teil unseres Plans.«

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Mira Halden — Beweise“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Mira Halden — Beweise“.
- Storyflag: `sq_mir_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_mir_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Elias Renn — Maschinen lügen anders

**Region:** Occidentia/Australis · **Status:** optional, feste narrative Questkette
## SQ-ELI-01 — Ein sauberer Fehler

**Typ:** Hauptstory · **Region:** Occidentia/Australis · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Elias zeigt, warum normale Erklärungen zuerst geprüft werden.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Finde einen technischen Defekt, der zunächst wie eine Anomalie wirkt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Materialprüfstand, der Daten aus Occidentia und Australis gegenüberstellt. Elias zeigt, warum normale Erklärungen zuerst geprüft werden. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Wenn wir schon bauen, bauen wir so, dass man später noch drankommt. Provisorien werden sonst sehr schnell dauerhaft.«

**Helena Voss:** »Für die Stadt ist dabei klar: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: elias zeigt, warum normale Erklärungen zuerst geprüft werden. **Elias Renn:** »Wenn das ein Maschinenteil ist, fehlt mir die Maschine dazu. Also Finger weg, bis wir wissen, was wir vor uns haben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Elias Renn:** »Sauber. Wenn morgen etwas ausfällt, wissen wir wenigstens, wo wir anfangen müssen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“.
- Storyflag: `sq_eli_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_eli_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-ELI-02 — Das Material

**Typ:** Hauptstory · **Region:** Occidentia/Australis · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Die Messwerte zwingen Elias, eine neue Physik innerhalb der Spielwelt zu akzeptieren.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue eine Testanlage für die unbekannte Legierung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Materialprüfstand, der Daten aus Occidentia und Australis gegenüberstellt. Die Messwerte zwingen Elias, eine neue Physik innerhalb der Spielwelt zu akzeptieren. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Wenn wir schon bauen, bauen wir so, dass man später noch drankommt. Provisorien werden sonst sehr schnell dauerhaft.«

**Helena Voss:** »Für die Stadt ist dabei klar: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Das trägt. Noch nicht perfekt, aber gut genug, dass wir den nächsten Schwachpunkt ehrlich sehen können.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: die Messwerte zwingen Elias, eine neue Physik innerhalb der Spielwelt zu akzeptieren. **Elias Renn:** »Material und Fundort getrennt dokumentieren. Die Ähnlichkeit darf die Herkunft nicht überschreiben.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »So soll Fortschritt aussehen: sichtbar, überprüfbar und für die Leute tatsächlich nützlich.«

**Elias Renn:** »Das System hat einen Fehlerweg und einen Rückweg. Mehr verlange ich für heute nicht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“.
- Storyflag: `sq_eli_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_eli_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-ELI-03 — Brücke ins Unbekannte

**Typ:** Hauptstory · **Region:** Occidentia/Australis · **Leitfigur:** Elias Renn · **Unterstützung:** Helena Voss

### Dramatischer Zweck
Sein Misstrauen wird zu verantwortlicher Neugier.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Leite die technische Sicherheitsfreigabe für den Eiswand-Vorposten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer Werkhalle, in der Bauteile aus Occidentia für den Einsatz in Australis vorbereitet werden. Sein Misstrauen wird zu verantwortlicher Neugier. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Elias Renn:** »Wenn wir schon bauen, bauen wir so, dass man später noch drankommt. Provisorien werden sonst sehr schnell dauerhaft.«

**Helena Voss:** »Für die Stadt ist dabei klar: Materialfluss zuerst. Wenn Zwischenprodukte warten, bringt uns die schnellste Endstufe nichts.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Helena Voss:** »Gut. Das ist der erste Zustand, auf den ich mich verlassen würde. Jetzt sehen wir, ob er auch unter Alltag hält.«
- **Wendepunkt:** Der Wendepunkt entsteht an den Fugen und der Unterseite der Brückensegmente: sein Misstrauen wird zu verantwortlicher Neugier. **Elias Renn:** »Ich kann euch sagen, was es nicht ist. Für den Rest brauche ich Messwerte.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Elias Renn:** »Letzter Punkt: Reserveweg prüfen. Hauptweg kann jeder.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Helena Voss:** »Gut. Darauf können wir morgen weiterbauen, ohne heute etwas zu verstecken.«

**Elias Renn:** »Technisch heißt das für mich: Die Kette läuft vom Rohstoff bis zum Ergebnis ohne versteckten Engpass.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Elias Renn — Maschinen lügen anders“.
- Storyflag: `sq_eli_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_eli_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Inés Araya — Der Fluss gehört allen

**Region:** Meridia · **Status:** optional, feste narrative Questkette
## SQ-INE-01 — Oberlauf

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Regionaler Interessenkonflikt ohne Kampf.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Stelle sicher, dass ein Exportprojekt die Wasserverfügbarkeit flussabwärts nicht verschlechtert.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einem bewirtschafteten Flussabschnitt, bevor die Kamera auf den konkreten Missionsort absinkt. Regionaler Interessenkonflikt ohne Kampf. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der Fluss gehört nicht dem ersten, der einen Pfahl hineinschlägt. Wir bauen so, dass auch flussabwärts noch jemand leben kann.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: regionaler Interessenkonflikt ohne Kampf. **Inés Araya:** »Sichert den Fund dort, wo er war, und nehmt erst Maße. Am Fluss verändert man einen Ort schneller, als man denkt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Noch ein kompletter Wasserzyklus. Wenn Ober- und Unterlauf stabil bleiben, gebe ich mein Ja.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Inés Araya:** »Am Fluss heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“.
- Storyflag: `sq_ine_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ine_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-INE-02 — Alte Rechte

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Diplomatie wird über Infrastruktur greifbar.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Verhandle zwischen zwei Gemeinden über einen historischen Pier.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Flussufer zwischen Anlegern, Feldern und den ersten dicht bebauten Uferstraßen. Diplomatie wird über Infrastruktur greifbar. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der schnellste Ausbau ist wertlos, wenn er beim nächsten Hochwasser gegen uns arbeitet.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir schreiben nicht nur Rechte auf, sondern auch Grenzen und Ausstiegsmöglichkeiten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: diplomatie wird über Infrastruktur greifbar. **Inés Araya:** »Keine Souvenirs. Alles bleibt zusammen, bis wir verstehen, warum es genau hier liegt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Noch ein kompletter Wasserzyklus. Wenn Ober- und Unterlauf stabil bleiben, gebe ich mein Ja.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Inés Araya:** »Das ist eine Lösung, die auch flussabwärts noch vernünftig aussieht.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“.
- Storyflag: `sq_ine_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ine_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-INE-03 — Gemeinsamer Strom

**Typ:** Hauptstory · **Region:** Meridia · **Leitfigur:** Inés Araya · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Meridia erhält dauerhaften Vertrauensbonus.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue ein gemeinsames Flusslogistiknetz mit fairen Zielbeständen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Bewässerungsgräben, Marktständen und dem braunen Wasser des großen Stroms. Meridia erhält dauerhaften Vertrauensbonus. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Inés Araya:** »Der Fluss gehört nicht dem ersten, der einen Pfahl hineinschlägt. Wir bauen so, dass auch flussabwärts noch jemand leben kann.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: meridia erhält dauerhaften Vertrauensbonus. **Inés Araya:** »Fundort und Wasserstand zuerst dokumentieren. Der Fluss verändert Spuren schneller als unsere Erinnerung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Inés Araya:** »Fast fertig. Jetzt prüfen wir, ob niemand außerhalb unseres Blickfelds die Rechnung bezahlt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Inés Araya:** »So kann der Fluss für uns arbeiten, ohne dass wir so tun, als gehörte er uns.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Inés Araya — Der Fluss gehört allen“.
- Storyflag: `sq_ine_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ine_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Lian Sora — Das versiegelte Regal

**Region:** Orientia · **Status:** optional, feste narrative Questkette
## SQ-LIA-01 — Randnotizen

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Kleine historische Hinweise.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Finde fehlende Seiten einer historischen Chronik.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Terrassen, schmalen Wasserläufen und den Dächern des bereits gewachsenen Viertels. Kleine historische Hinweise. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Ein Dokument ist kein Beweis nur weil es alt ist. Aber es ist eine Stimme, die wir nicht mehr befragen können.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Der erste Datensatz ist brauchbar. Jetzt brauche ich denselben Effekt unter anderen Bedingungen.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: kleine historische Hinweise. **Lian Sora:** »Diese Lücke ist zu sauber. Jemand wollte, dass spätere Leser genau hier nichts finden.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Fast fertig. Jetzt prüfen wir, ob unsere Reihenfolge nicht selbst schon eine Interpretation erzwingt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Lian Sora:** »Das Archiv ist offener geworden, nicht einfacher. Das ist meistens ein gutes Zeichen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“.
- Storyflag: `sq_lia_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_lia_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-LIA-02 — Der falsche Katalog

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Erhöht Mysteryspannung ohne Verschwörung realer Gruppen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Beweise, dass ein Archiv absichtlich falsch katalogisiert wurde.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet oberhalb der Terrassenfelder; von dort folgt die Kamera dem Wasser bis zum Missionsort. Erhöht Mysteryspannung ohne Verschwörung realer Gruppen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Was versiegelt wurde, wurde aus einem Grund versiegelt. Der Grund kann falsch sein — aber wir sollten ihn kennen, bevor wir das Siegel brechen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Wendepunkt entsteht in den wiederhergestellten Schichten des Archivs: erhöht Mysteryspannung ohne Verschwörung realer Gruppen. **Lian Sora:** »Nicht nur den Text sichern. Papier, Tinte, Bindung, Fundort — alles kann Teil der Aussage sein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Ich will die Gegenposition im Archiv finden, bevor wir unsere eigene Version festschreiben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Wir wissen mehr als vorher — und vor allem wissen wir genauer, was wir noch nicht wissen.«

**Lian Sora:** »Jetzt kann jeder sehen, was belegt ist, was fehlt und wo unsere Interpretation beginnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“.
- Storyflag: `sq_lia_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_lia_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-LIA-03 — Offenes Archiv

**Typ:** Hauptstory · **Region:** Orientia · **Leitfigur:** Lian Sora · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Beeinflusst spätere Wissensverfügbarkeit, nicht Kernprogression.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheide mit Lian über Zugangsregeln für sensible historische Daten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet oberhalb der Terrassenfelder; von dort folgt die Kamera dem Wasser bis zum Missionsort. Beeinflusst spätere Wissensverfügbarkeit, nicht Kernprogression. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Lian Sora:** »Bevor wir etwas verändern, halten wir fest, was schon da ist. Ein fehlender Randvermerk kann wichtiger sein als eine ganze Chronik.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir trennen Messung, Fund und Interpretation. Sonst bestätigt am Ende jeder nur sich selbst.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Das Signal bleibt bestehen. Gut — oder schlecht, je nachdem, wie sehr man Überraschungen mag.«
- **Wendepunkt:** Der Wendepunkt entsteht in den wiederhergestellten Schichten des Archivs: beeinflusst spätere Wissensverfügbarkeit, nicht Kernprogression. **Lian Sora:** »Originalzustand, Randnotizen und Herkunft sichern. Erst danach lesen wir Bedeutung hinein.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Lian Sora:** »Noch eine Quelle. Wenn sie unabhängig ist und dasselbe Problem zeigt, können wir den Befund veröffentlichen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Lian Sora:** »Jetzt kann jeder sehen, was belegt ist, was fehlt und wo unsere Interpretation beginnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Lian Sora — Das versiegelte Regal“.
- Storyflag: `sq_lia_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_lia_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Amara Selu — Wasser und Macht

**Region:** Aferia · **Status:** optional, feste narrative Questkette
## SQ-AMA-01 — Reservoir für zwei

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Kooperation durch Infrastruktur.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue Reservekapazität für zwei konkurrierende Siedlungen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Versorgungshof, während Wind Staub über Straße und Dächer zieht. Kooperation durch Infrastruktur. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Ich will keine Lösung, die im Zentrum glänzt und am Rand leerläuft.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir messen am schlechtesten versorgten Punkt; der Durchschnitt kann uns später trösten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: kooperation durch Infrastruktur. **Amara Selu:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Ein letzter Lauf mit Spitzenbedarf. Danach weiß ich, ob das Netz für alle reicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Amara Selu:** »Gut. Diesmal verteilt die Infrastruktur nicht nur Leistung, sondern auch Zugang.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Amara Selu — Wasser und Macht“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Amara Selu — Wasser und Macht“.
- Storyflag: `sq_ama_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ama_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-AMA-02 — Preis des Metalls

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Verbindet Industrie mit regionaler Verantwortung.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Reduziere Wasserverbrauch einer Metallkette durch Technologie.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen rotem Boden, Wasserstellen und den Gebäuden des aktuellen Siedlungskerns. Verbindet Industrie mit regionaler Verantwortung. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Rohstoffe machen eine Stadt reich. Verteilung entscheidet, ob die Menschen davon etwas merken.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Wir halten das Ziel klein genug, dass wir den Fehler sehen, wenn einer auftaucht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: verbindet Industrie mit regionaler Verantwortung. **Amara Selu:** »Der Ort bleibt dokumentiert und zugänglich. Niemand bekommt die Vergangenheit einfach zugesprochen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Fast fertig. Jetzt prüfen wir, ob die Reserve wirklich dort ankommt, wo sie gebraucht wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Amara Selu:** »Gut. Diesmal verteilt die Infrastruktur nicht nur Leistung, sondern auch Zugang.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Amara Selu — Wasser und Macht“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Amara Selu — Wasser und Macht“.
- Storyflag: `sq_ama_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ama_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-AMA-03 — Der gemeinsame Brunnen

**Typ:** Hauptstory · **Region:** Aferia · **Leitfigur:** Amara Selu · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Aferia-Vertrauen und Resilienzbonus.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Schließe ein dauerhaftes Wasserabkommen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen rotem Boden, Wasserstellen und den Gebäuden des aktuellen Siedlungskerns. Aferia-Vertrauen und Resilienzbonus. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Amara Selu:** »Rohstoffe machen eine Stadt reich. Verteilung entscheidet, ob die Menschen davon etwas merken.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Menge allein reicht nicht. Druck, Verteilung und Rückweg müssen zusammenpassen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Wir haben Wiederholbarkeit. Jetzt interessiert mich, was sich ändern muss, damit der Effekt verschwindet.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: aferia-Vertrauen und Resilienzbonus. **Amara Selu:** »Der Fund bleibt öffentlich dokumentiert. Niemand bekommt ein Monopol auf etwas, das unter gemeinsamem Boden lag.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Amara Selu:** »Noch die Randbezirke. Wenn es dort hält, können wir von einer Versorgung sprechen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Amara Selu:** »Das Ergebnis hält auch am Rand. Dann kann ich es vertreten.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Amara Selu — Wasser und Macht“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Amara Selu — Wasser und Macht“.
- Storyflag: `sq_ama_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ama_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Edda Vey — Kein Heldentod

**Region:** Australis · **Status:** optional, feste narrative Questkette
## SQ-EDD-01 — Umkehren

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Lehrt, dass Rückzug kein Scheitern ist.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Brich eine Expedition wegen Wetterwarnung kontrolliert ab und sichere alle Teilnehmer.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet im Windschutz des Polarstützpunkts; Schnee treibt quer über die Beleuchtung, während die Siedlung im Hintergrund weiterarbeitet. Lehrt, dass Rückzug kein Scheitern ist. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Wir beweisen heute nicht, dass wir mutig sind. Wir beweisen, dass wir wieder nach Hause kommen.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: lehrt, dass Rückzug kein Scheitern ist. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Wir schließen erst ab, wenn der Reserveweg ohne Improvisation anspringt.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Jetzt steht etwas Belastbares im Log. Alles Weitere ist wieder Forschung, nicht Wunschdenken.«

**Dr. Edda Vey:** »Der Zustand hält auch unter schlechten Bedingungen. In Australis ist das die eigentliche Abnahme.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Edda Vey — Kein Heldentod“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Edda Vey — Kein Heldentod“.
- Storyflag: `sq_edd_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_edd_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-EDD-02 — Reserve für die Reserve

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Edda akzeptiert die Siedlung als dauerhaft.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue redundante Wärme-/Stromkapazität.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen Wärmeleitungen, Versorgungskisten und vereisten Außenwegen des aktuellen Stützpunkts. Edda akzeptiert die Siedlung als dauerhaft. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Jede Anlage hier braucht einen zweiten Weg. Der erste friert irgendwann ein.«

**Dr. Mira Halden:** »Wärme, Wasser, Strom, Rückzug. Wenn einer dieser vier Punkte nur auf Glück beruht, bleiben wir hier.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Wendepunkt entsteht in einem Wärmebild mit einer unplausiblen Quelle: edda akzeptiert die Siedlung als dauerhaft. **Dr. Edda Vey:** »Fundstelle sichern. Niemand geht allein zurück, nur weil das jetzt plötzlich interessant geworden ist.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Letzte Prüfung bei schlechtem Wetter. Schönwetterwerte interessieren mich nicht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Das reicht für den nächsten Schritt: nicht Gewissheit, sondern eine Frage, die endlich präzise genug ist.«

**Dr. Edda Vey:** »Sauber. Heute hat die Kälte nichts gewonnen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Edda Vey — Kein Heldentod“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Edda Vey — Kein Heldentod“.
- Storyflag: `sq_edd_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_edd_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-EDD-03 — Die Namen im Eis

**Typ:** Hauptstory · **Region:** Australis · **Leitfigur:** Dr. Edda Vey · **Unterstützung:** Dr. Mira Halden

### Dramatischer Zweck
Verbindet Australis mit seiner Geschichte.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte ein Denkmal/Archiv für frühere Expeditionen.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an der Außenschleuse, von der die Kamera über Eis und Wetterstationen zum Missionsziel schwenkt. Verbindet Australis mit seiner Geschichte. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Dr. Edda Vey:** »Jede Anlage hier braucht einen zweiten Weg. Der erste friert irgendwann ein.«

**Dr. Mira Halden:** »Für die Prüfung heißt das: Reserve ist hier keine Bequemlichkeit, sondern Teil der Grundfunktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Dr. Mira Halden:** »Die Messung ist sauber genug für eine Gegenprobe. Erst danach nenne ich das ein Muster.«
- **Wendepunkt:** Der Wendepunkt entsteht in den wiederhergestellten Schichten des Archivs: verbindet Australis mit seiner Geschichte. **Dr. Edda Vey:** »Markieren, sichern, Abstand halten. Unter Eis ist Eile der schnellste Weg, Kontext zu zerstören.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Dr. Edda Vey:** »Noch ein Notlauf. Wenn der klappt, lasse ich Leute davon abhängig werden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Dr. Mira Halden:** »Gut. Wir haben keinen Beweis für jede Erklärung, aber wir haben einen Befund, der nicht mehr verschwindet, wenn man genauer hinsieht.«

**Dr. Edda Vey:** »Der Zustand hält auch unter schlechten Bedingungen. In Australis ist das die eigentliche Abnahme.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Edda Vey — Kein Heldentod“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Edda Vey — Kein Heldentod“.
- Storyflag: `sq_edd_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_edd_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Serin Ael — Vertrauen ist Kapazität

**Region:** Aurelia · **Status:** optional, feste narrative Questkette
## SQ-SER-01 — Gastrecht

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Zeigt Ultima als bewohnte Welt, nicht Beute.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erfülle lokale Infrastrukturregeln, bevor Aurelia Technologie freigibt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer erhöhten Plattform über Aurelias dichter, lautloser Infrastruktur. Zeigt Ultima als bewohnte Welt, nicht Beute. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Das Weltgitter verbindet vieles. Es entschuldigt nichts.«

**Iriath:** »Für das Netz heißt das: Wir schreiben nicht nur Rechte auf, sondern auch Grenzen und Ausstiegsmöglichkeiten.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Der Knoten antwortet. Jetzt prüfen wir, ob er auch ein Nein respektiert.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: zeigt Ultima als bewohnte Welt, nicht Beute. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Fast fertig. Lasst das Netz einmal selbst ausgleichen, ohne manuelle Korrektur.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“.
- Storyflag: `sq_ser_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ser_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-SER-02 — Geteilte Verantwortung

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Serins Vertrauen wächst messbar.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Betreibe ein gemeinsames Relais unter gemischter Verwaltung.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Serins Vertrauen wächst messbar. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Unsere Technik wirkt mühelos, weil Generationen gelernt haben, wo Mühelosigkeit gefährlich wird.«

**Iriath:** »Für das Netz heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Das System funktioniert. Deshalb müssen wir besonders genau klären, wer es verwenden darf.«
- **Wendepunkt:** Der Wendepunkt entsteht in den gespeicherten Phasenverschiebungen des Relais: serins Vertrauen wächst messbar. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Fast fertig. Lasst das Netz einmal selbst ausgleichen, ohne manuelle Korrektur.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“.
- Storyflag: `sq_ser_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ser_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-SER-03 — Das offene Protokoll

**Typ:** Hauptstory · **Region:** Aurelia · **Leitfigur:** Serin Ael · **Unterstützung:** Iriath

### Dramatischer Zweck
Beeinflusst Konvent- und Wächtervertrauen.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheide, wie technische Sicherheitsdaten veröffentlicht werden.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter den ruhigen Lichtbändern eines aure­lischen Energienetzes, dessen Relais im Hintergrund sichtbar reagieren. Beeinflusst Konvent- und Wächtervertrauen. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Serin Ael:** »Das Weltgitter verbindet vieles. Es entschuldigt nichts.«

**Iriath:** »Für das Netz heißt das: Erst Funktion, dann Belastung, dann Freigabe.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Iriath:** »Technisch ist der Zustand stabil. Politisch beginnt die Prüfung erst jetzt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: beeinflusst Konvent- und Wächtervertrauen. **Serin Ael:** »Der Fund wird im gemeinsamen Protokoll gesichert. Zugang folgt erst nach Prüfung und Zustimmung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Serin Ael:** »Die Funktion steht. Jetzt fehlt nur der Beweis, dass sie niemand anderen zur Reserve macht.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Iriath:** »Dann ist dieser Zugang offen — und ebenso klar ist, wie er wieder geschlossen werden kann.«

**Serin Ael:** »Für Aurelia heißt das: Der Zustand ist stabil genug, dass der nächste Schritt ihn nicht sofort wieder infrage stellt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Serin Ael — Vertrauen ist Kapazität“.
- Storyflag: `sq_ser_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ser_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Ilyra Venn — Die lebende Grenze

**Region:** Viridia · **Status:** optional, feste narrative Questkette
## SQ-ILY-01 — Nicht ernten

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Lehrt Schutzgebiete.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Verlagere eine Produktionskette aus einem sensiblen Biom.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand einer lebenden Kultivierungszone, in der Pflanzen, Wasser und technische Strukturen ineinander übergehen. Lehrt Schutzgebiete. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Wir beginnen mit Beobachtung. Wenn ihr Wachstum erzwingt, bekommt ihr vielleicht mehr — aber nicht dasselbe.«

**Serin Ael:** »Für Aurelia heißt das: Die Kette muss nicht groß sein, nur stetig. Ein leerer Topf interessiert sich nicht für Spitzenproduktion.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Das System nimmt eure Last an, ohne die Nachbarn aus dem Takt zu bringen. Das ist ein guter Anfang.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: lehrt Schutzgebiete. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Noch ein Regenerationszyklus. Wenn die Quelle vollständig zurückkommt, können wir die Nutzung freigeben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Ilyra Venn:** »Für den Bestand heißt das: Die Versorgung trägt aus eigener Produktion. Das ist der Punkt, an dem eine Siedlung unabhängiger wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“.
- Storyflag: `sq_ily_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ily_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-ILY-02 — Erinnerung im Samen

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Bioäther wird positiv eingesetzt.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Rekonstruiere eine ausgestorbene Nutzpflanze aus Archivmaterial.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand einer lebenden Kultivierungszone, in der Pflanzen, Wasser und technische Strukturen ineinander übergehen. Bioäther wird positiv eingesetzt. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Wir beginnen mit Beobachtung. Wenn ihr Wachstum erzwingt, bekommt ihr vielleicht mehr — aber nicht dasselbe.«

**Serin Ael:** »Für Aurelia heißt das: Eine Gegenprobe spart uns später zehn falsche Erklärungen.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Das System nimmt eure Last an, ohne die Nachbarn aus dem Takt zu bringen. Das ist ein guter Anfang.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: bioäther wird positiv eingesetzt. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Wir schließen erst, wenn Nutzung und Erholung im Gleichgewicht bleiben.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Ilyra Venn:** »So kann diese Nutzung wachsen, ohne die Quelle kleiner zu machen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“.
- Storyflag: `sq_ily_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ily_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-ILY-03 — Was wachsen darf

**Typ:** Hauptstory · **Region:** Viridia · **Leitfigur:** Ilyra Venn · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Ökologie- und Produktionsbonus je nach Wahl.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Lege Regeln für industrielle Biosynthese fest.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet am Rand einer lebenden Kultivierungszone, in der Pflanzen, Wasser und technische Strukturen ineinander übergehen. Ökologie- und Produktionsbonus je nach Wahl. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Ilyra Venn:** »Was hier wächst, reagiert auf unsere Entscheidungen. Wir ernten nicht schneller, als es sich erneuern kann.«

**Serin Ael:** »Für Aurelia heißt das: Ich rechne mit dem Verbrauch, nicht mit dem besten Ertrag. Danach dimensionieren wir Reserve.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: ökologie- und Produktionsbonus je nach Wahl. **Ilyra Venn:** »Nicht beschädigen. Wenn die Struktur lebt oder reagiert, ist ihr Zustand Teil der Information.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Ilyra Venn:** »Fast fertig. Ich will sehen, ob die zweite Generation dieselben Eigenschaften behält.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Ilyra Venn:** »Für den Bestand heißt das: Die Versorgung trägt aus eigener Produktion. Das ist der Punkt, an dem eine Siedlung unabhängiger wird.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Ilyra Venn — Die lebende Grenze“.
- Storyflag: `sq_ily_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ily_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Kharum Tor — Getragene Last

**Region:** Titania · **Status:** optional, feste narrative Questkette
## SQ-KHA-01 — Ein Stein für zwei

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Erster echter Kooperationsbeweis.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue mit Riesenhilfe eine Schwerlaststruktur.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einer Riesenhalle, deren aktive Arbeitsbereiche im Hintergrund weiterlaufen. Erster echter Kooperationsbeweis. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »In Titania trägt nichts allein. Nicht der Pfeiler, nicht der Kran und auch kein Clan.«

**Serin Ael:** »Für Aurelia heißt das: Last verteilen, Fundament lesen, Rückweg planen. Kraft allein ersetzt keine Statik.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Ihr habt die Funktion erreicht. Jetzt prüft, ob eure Lösung auch Rücksicht auf das Netz nimmt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: erster echter Kooperationsbeweis. **Kharum Tor:** »Dieser Stein wurde nicht nur behauen. Er wurde auf eine Weise gesetzt, die unsere heutigen Werkzeuge nicht erklären.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Fast fertig. Jetzt ohne Hilfe der stärksten Hebeeinheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Kharum Tor:** »Die Konstruktion hält, weil niemand so getan hat, als könnte er alles allein tragen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Kharum Tor — Getragene Last“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Kharum Tor — Getragene Last“.
- Storyflag: `sq_kha_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_kha_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-KHA-02 — Die Halle der Namen

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Vertieft Riesen als Kultur.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Versorge eine kulturelle Riesenstätte, ohne sie zu industrialisieren.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet vor einer titanischen Stützkonstruktion, an der Maßstab und Gewicht der Region sofort sichtbar werden. Vertieft Riesen als Kultur. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Baut langsam genug, dass ihr das Gewicht versteht. Danach könnt ihr schnell werden.«

**Serin Ael:** »Für Aurelia heißt das: Last verteilen, Fundament lesen, Rückweg planen. Kraft allein ersetzt keine Statik.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Das System nimmt eure Last an, ohne die Nachbarn aus dem Takt zu bringen. Das ist ein guter Anfang.«
- **Wendepunkt:** Der Wendepunkt entsteht in Bearbeitungsspuren tief im Stein: vertieft Riesen als Kultur. **Kharum Tor:** »Das ist alte Lasttechnik. Wer sie gebaut hat, kannte unsere Berge — vielleicht besser als wir.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Noch eine Lastverlagerung. Wenn das Fundament ruhig bleibt, akzeptiere ich die Konstruktion.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Kharum Tor:** »Gut. Das Gewicht ist verteilt, nicht versteckt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Kharum Tor — Getragene Last“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Kharum Tor — Getragene Last“.
- Storyflag: `sq_kha_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_kha_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-KHA-03 — Lastenteilung

**Typ:** Hauptstory · **Region:** Titania · **Leitfigur:** Kharum Tor · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Schaltet kooperative Megabauweise frei.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entwickle ein Bauverfahren, das Riesen- und Maschinentechnik kombiniert.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet auf einer Hochlandterrasse zwischen Megalithen, Lastwegen und schweren Hebesystemen. Schaltet kooperative Megabauweise frei. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Kharum Tor:** »Baut langsam genug, dass ihr das Gewicht versteht. Danach könnt ihr schnell werden.«

**Serin Ael:** »Für Aurelia heißt das: Wir bauen für das Gewicht, das wirklich kommt, nicht für das, das bequem wäre.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Das System nimmt eure Last an, ohne die Nachbarn aus dem Takt zu bringen. Das ist ein guter Anfang.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: schaltet kooperative Megabauweise frei. **Kharum Tor:** »Nichts versetzen. Erst Schwingung, Fuge und Lastbezug messen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Kharum Tor:** »Fast fertig. Jetzt ohne Hilfe der stärksten Hebeeinheit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Kharum Tor:** »Gut. Das Gewicht ist verteilt, nicht versteckt.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Kharum Tor — Getragene Last“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Kharum Tor — Getragene Last“.
- Storyflag: `sq_kha_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_kha_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Vaelis — Kein Besitz am Himmel

**Region:** Ignaria · **Status:** optional, feste narrative Questkette
## SQ-VAE-01 — Nestgrenze

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Respekt statt Domestikation.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Verlege Flugrouten weg von Drachenbrutgebieten.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet zwischen dunklem Gestein und ätherischen Flugstrukturen, bevor die Kamera auf das konkrete Ziel zieht. Respekt statt Domestikation. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Hitze ist kein Feind. Sie wird gefährlich, wenn jemand glaubt, sie vollständig zu beherrschen.«

**Serin Ael:** »Für Aurelia heißt das: Kein Zwang, keine Jagd. Wenn Zusammenarbeit entsteht, dann freiwillig.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Ihr habt die Funktion erreicht. Jetzt prüft, ob eure Lösung auch Rücksicht auf das Netz nimmt.«
- **Wendepunkt:** Der Wendepunkt entsteht an alten Grenzmarkierungen rund um das Brutgebiet: respekt statt Domestikation. **Vaelis:** »Das ist drakonisch, aber nicht im Sinn eines Eigentumszeichens. Eher eine Warnung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Fast fertig. Jetzt prüfen wir den Abbruch, nicht den Erfolg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Vaelis:** »Der Korridor ist offen, und niemand musste dafür gejagt oder gezwungen werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“.
- Storyflag: `sq_vae_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_vae_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-VAE-02 — Schuppe, nicht Jagd

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Keine Tötungs-Produktionspflicht.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Errichte eine Sammellogistik für natürlich abgestoßene drakonische Materialien.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einer gesicherten Plattform über glühenden Spalten; Flugbewegungen bleiben im Hintergrund sichtbar. Keine Tötungs-Produktionspflicht. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Unsere Technik funktioniert nahe an Grenzen. Deshalb markieren wir sie deutlicher als andere.«

**Serin Ael:** »Für Aurelia heißt das: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht in der Mikrostruktur einer natürlich abgestoßenen Schuppe: keine Tötungs-Produktionspflicht. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Noch ein Zyklus bei höherer Hitze. Wenn die Schutzgrenzen sauber bleiben, ist die Anlage bereit.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Vaelis:** »So kann Ignaria mit uns arbeiten, ohne dass wir es besitzen müssen.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“.
- Storyflag: `sq_vae_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_vae_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-VAE-03 — Gemeinsamer Wind

**Typ:** Hauptstory · **Region:** Ignaria · **Leitfigur:** Vaelis · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Schaltet freiwilligen Drachen-Wetterdienst frei.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Koordiniere Wetterbeobachtung mit Drachenhütern.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet unter einem aschigen Himmel zwischen Wärmeschilden, Landeplattformen und vulkanischem Licht. Schaltet freiwilligen Drachen-Wetterdienst frei. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Vaelis:** »Ein Abkommen mit Flügeln beginnt nicht mit einem Sattel. Es beginnt mit Abstand.«

**Serin Ael:** »Für Aurelia heißt das: Abstand, Hitze, Flugkorridor. Nichts davon wird durch Mut kleiner.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Das System nimmt eure Last an, ohne die Nachbarn aus dem Takt zu bringen. Das ist ein guter Anfang.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: schaltet freiwilligen Drachen-Wetterdienst frei. **Vaelis:** »Abstand halten und beobachten. Wenn Drachen oder das Feld reagieren, ändern wir nicht zuerst ihre Umgebung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Vaelis:** »Fast fertig. Jetzt prüfen wir den Abbruch, nicht den Erfolg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Vaelis:** »Gut. Wir haben die Grenze genutzt, ohne so zu tun, als wäre sie verschwunden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Vaelis — Kein Besitz am Himmel“.
- Storyflag: `sq_vae_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_vae_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Neris Pell — Unter der Oberfläche

**Region:** Pelagia · **Status:** optional, feste narrative Questkette
## SQ-NER-01 — Drucklinie

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Führt Tiefseelogistik ein.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Baue eine sichere Tiefenstation.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einer Plattform knapp über der Wasserlinie; darunter sind Tiefenlichter und Versorgungsschächte zu sehen. Führt Tiefseelogistik ein. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Wenn das Sonar etwas Merkwürdiges meldet, heißt das zuerst: langsamer werden.«

**Serin Ael:** »Für Aurelia heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: führt Tiefseelogistik ein. **Neris Pell:** »Position markieren, passiv beobachten, keine aktive Annäherung. Tiefe belohnt Zurückhaltung.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Wir schließen erst, wenn der Rückweg genauso zuverlässig ist wie der Abstieg.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Neris Pell:** »Tiefe und Rückkehr sind beide abgesichert. Damit wird die Route Alltag.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“.
- Storyflag: `sq_ner_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ner_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-NER-02 — Das Lied im Sonar

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Leviathane als intelligente/komplexe Fauna.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Untersuche Leviathan-Kommunikationsmuster ohne Jagd.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem Tiefenhafen, wo Wasser, Druckschleusen und Schiffsbewegungen die Szene bestimmen. Leviathane als intelligente/komplexe Fauna. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Ein Hafen hier endet nicht an der Wasserlinie. Der wichtigste Teil liegt oft darunter.«

**Serin Ael:** »Für Aurelia heißt das: Wir testen die Tiefe mit Reserve; das Meer gibt uns keine zweite Chance aus Höflichkeit.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht in einer wiederkehrenden Antwortfolge im Sonar: leviathane als intelligente/komplexe Fauna. **Neris Pell:** »Das Signal bewegt sich nicht wie ein Schiff. Wir beobachten, bevor wir nähergehen.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Fast fertig. Ich will den Notaufstieg einmal vollständig sehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Die Verbindung steht, und niemand musste dafür Kontrolle abgeben. Das ist selten genug.«

**Neris Pell:** »Tiefe und Rückkehr sind beide abgesichert. Damit wird die Route Alltag.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“.
- Storyflag: `sq_ner_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ner_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-NER-03 — Stadt unter Wasser

**Typ:** Hauptstory · **Region:** Pelagia · **Leitfigur:** Neris Pell · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Schaltet Tiefenarchiv frei.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Reaktiviere einen kleinen Bezirk einer versunkenen Stadt.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet über einer Plattform knapp über der Wasserlinie; darunter sind Tiefenlichter und Versorgungsschächte zu sehen. Schaltet Tiefenarchiv frei. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Neris Pell:** »Das Meer verzeiht viel, aber nicht, wenn man einen Rückweg nur auf der Karte besitzt.«

**Serin Ael:** »Für Aurelia heißt das: Druck, Schleuse, Rückkehr. Der Weg nach unten zählt nur zusammen mit dem Weg nach oben.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Die erste Verbindung hält. In Aurelia beginnt die eigentliche Prüfung immer danach.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: schaltet Tiefenarchiv frei. **Neris Pell:** »Die Struktur ist gebaut, nicht gewachsen. Markiert die Position; Bergung kommt viel später.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Neris Pell:** »Fast fertig. Ich will den Notaufstieg einmal vollständig sehen.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Neris Pell:** »Tiefe und Rückkehr sind beide abgesichert. Damit wird die Route Alltag.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Neris Pell — Unter der Oberfläche“.
- Storyflag: `sq_ner_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_ner_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## Iriath — Grenze des Gitters

**Region:** Ultima · **Status:** optional, feste narrative Questkette
## SQ-IRI-01 — Ein Netz ist kein Recht

**Typ:** Hauptstory · **Region:** Ultima · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Selbstbegrenzung als Kompetenz.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Reduziere absichtlich einen Ätherknoten unter Maximalleistung, um Stabilität zu demonstrieren.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem neutralen Weltgitter-Knoten, an dem mehrere Ultima-Fraktionen Zugang haben. Selbstbegrenzung als Kompetenz. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Ein Netz verbindet. Es gibt niemandem das Recht, über die Verbundenen zu verfügen.«

**Serin Ael:** »Technisch können wir viel. Entscheidend ist, was davon mit Zustimmung geschieht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: selbstbegrenzung als Kompetenz. **Iriath:** »Zugang protokollieren und Zustimmung klären, bevor aus einem Fund ein Recht abgeleitet wird.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Fast fertig. Jetzt testen wir die Grenze, an der Zustimmung zurückgezogen wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Gut. Ihr habt nicht nur Zugang erhalten, sondern gezeigt, dass ihr ihn begrenzen könnt.«

**Iriath:** »So kann das Netz tragen, ohne wieder zum Anspruch auf Kontrolle zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Iriath — Grenze des Gitters“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Iriath — Grenze des Gitters“.
- Storyflag: `sq_iri_01_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_iri_01_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-IRI-02 — Sechs Orden

**Typ:** Hauptstory · **Region:** Ultima · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Engelartige Fraktion wird politisch statt monolithisch.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Erreiche Kontakt zu mehreren Caelari-Orden und dokumentiere ihre Konflikte.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet in einem offenen Ultima-Archiv, dessen gesperrte Bereiche sichtbar, aber nicht automatisch zugänglich sind. Engelartige Fraktion wird politisch statt monolithisch. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Versiegelung kann Schutz sein oder Kontrolle. Beides sieht von außen oft gleich aus.«

**Serin Ael:** »Jede Verbindung braucht eine Grenze, die auch praktisch funktioniert.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Der Knoten akzeptiert den Betrieb. Jetzt sehen wir, ob er euch auch einen Fehler verzeiht.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: engelartige Fraktion wird politisch statt monolithisch. **Iriath:** »Öffnet nichts allein. Manche Siegel schützen nicht den Inhalt vor uns, sondern uns vor der Entscheidung, die der Inhalt erzwingt.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Fast fertig. Jetzt testen wir die Grenze, an der Zustimmung zurückgezogen wird.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Damit kann Aurelia euch mehr anvertrauen, ohne weniger vorsichtig zu werden.«

**Iriath:** »So kann das Netz tragen, ohne wieder zum Anspruch auf Kontrolle zu werden.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Iriath — Grenze des Gitters“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Iriath — Grenze des Gitters“.
- Storyflag: `sq_iri_02_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_iri_02_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---
## SQ-IRI-03 — Was versiegelt bleibt

**Typ:** Hauptstory · **Region:** Ultima · **Leitfigur:** Iriath · **Unterstützung:** Serin Ael

### Dramatischer Zweck
Alternative Endgameforschung, aber kein Kernhardlock.

### Trigger und Voraussetzungen
- Die unmittelbar vorherige Pflichtmission dieses Storypfads ist abgeschlossen oder die Region wurde durch den dafür vorgesehenen Expeditionsknoten freigeschaltet.
- Alle Gebäude/Waren, die diese Mission verlangt, müssen regulär über den aktuellen Fortschritt erreichbar sein; die Mission darf keine noch nicht freigeschaltete Produktionskette voraussetzen.
- Bei bereits weiter entwickelten Spielständen erkennt das Questskript bestehende Infrastruktur an und verlangt keinen künstlichen Rückbau.

### Primärziele
- Entscheide mit Iriath über eine gefährliche Alttechnologie.
- Halte alle für die Aufgabe kritischen Pflichtversorgungen mindestens 60 In-Game-Sekunden stabil, damit ein zufälliger kurzer Lagerimpuls nicht als vollständiger Erfolg zählt.
- Erhalte die bestehende Grundversorgung der Region; Storyfortschritt darf nicht dadurch optimiert werden, dass die restliche Bevölkerung absichtlich kollabiert.

### Missionsdrehbuch — individueller Ablauf
**Szenenauftakt:** Die Kamera eröffnet an einem neutralen Weltgitter-Knoten, an dem mehrere Ultima-Fraktionen Zugang haben. Alternative Endgameforschung, aber kein Kernhardlock. Die Einstellung bleibt kurz und benutzt ausschließlich den tatsächlichen Spielstand; anschließend geht die Kontrolle ohne Ladebildschirm zurück an den Spieler.

**Iriath:** »Versiegelung kann Schutz sein oder Kontrolle. Beides sieht von außen oft gleich aus.«

**Serin Ael:** »Technisch können wir viel. Entscheidend ist, was davon mit Zustimmung geschieht.«

Danach bleibt die Mission vollständig im normalen Aufbauspiel steuerbar. Der Questtracker nennt die messbaren Ziele; Figuren sprechen über Situation, Risiko und Motivation statt über UI-Formulierungen.

### Missionsspezifische Storybeats
- **Erste echte Reaktion:** Der Beat wird nicht nach einem Prozentwert ausgelöst, sondern sobald der erste für die Mission relevante Systemzustand tatsächlich funktioniert. **Serin Ael:** »Ihr habt die Funktion erreicht. Jetzt prüft, ob eure Lösung auch Rücksicht auf das Netz nimmt.«
- **Wendepunkt:** Der Wendepunkt entsteht am zentralen Objekt dieser Mission: alternative Endgameforschung, aber kein Kernhardlock. **Iriath:** »Der Fund widerspricht mindestens zwei Ordenstraditionen. Das macht ihn wertvoll und gefährlich.«
- **Vor dem Abschluss:** Die Mission verlangt den im Primärziel beschriebenen stabilen Zustand bzw. eine sinnvolle Gegenprobe; bereits vorhandene Infrastruktur zählt und muss nicht künstlich zurückgebaut werden. **Iriath:** »Ein letzter Zyklus ohne privilegierten Zugriff. Danach kann der Konvent entscheiden.«

### Missionsdrehbuch — Abschluss
**Inszenierung:** Die Abschlussreaktion findet am realen Missionsort statt und zeigt das konkrete Ergebnis im eigenen Spielstand. Die Kamera darf für wenige Sekunden geführt werden, unterbricht die Simulation aber nur bei einer tatsächlich großen Enthüllung.

**Serin Ael:** »Dann gehen wir weiter. Nicht weil das Risiko verschwunden ist, sondern weil ihr gelernt habt, damit umzugehen.«

**Iriath:** »Die Verbindung steht und respektiert Zustimmung. Genau deshalb darf sie bestehen bleiben.«

**Systemhinweis:** Dauerhaft freigeschaltet: **Beziehungsfortschritt in „Iriath — Grenze des Gitters“**.

### Belohnungen und Freischaltungen
- **Dauerhaft:** Beziehungsfortschritt in „Iriath — Grenze des Gitters“.
- Storyflag: `sq_iri_03_complete = true`.
- Relevante Codexeinträge werden aktualisiert; Mysteryinformationen zeigen nur Wissen, das zu diesem Zeitpunkt tatsächlich enthüllt wurde.
- Geld- oder Warenbelohnungen sind höchstens kleine Unterstützung und niemals der Hauptgrund der Mission.

### Fail-Safe / Wiederaufnahme
- Kein normales Scheitern durch Zeitablauf. Kritische Ereignisse besitzen Pause-/Wiederaufnahmestufen.
- Wird ein benötigtes Gebäude zerstört, wechselt das Ziel zurück auf „wiederherstellen“ statt die Mission endgültig zu verlieren.
- Geht ein Storyschiff verloren, muss eine regulär produzierbare/charterbare Ersatzoption verfügbar sein.
- Save/Load muss in jeder Zielphase funktionieren; der Dialogstatus darf weder doppelt ausgelöst noch übersprungen werden.

### QA-Gates
- [ ] Ziel erkennt bereits vorhandene Infrastruktur korrekt.
- [ ] Kein versteckter Import-Hardlock.
- [ ] Dialoge können übersprungen und im Log nachgelesen werden.
- [ ] Questfortschritt reagiert auf echte Systemwerte, nicht auf spezielle Fake-Objekte.
- [ ] `sq_iri_03_complete` wird genau einmal gesetzt und korrekt gespeichert.
- [ ] Abschluss funktioniert nach Save/Load, Regionswechsel und längerer Spielpause.

---

# 5. Prozedurale und wiederholbare Aufträge

Nicht jede zufällig erzeugte Handelslieferung erhält ein handgeschriebenes Drehbuch. Stattdessen existieren geprüfte Questfamilien mit Variablen. Sie dürfen nie Hauptstorywissen enthüllen. Beispiele: Lieferauftrag, Produktionsziel, Wiederaufbau, Forschungsprobe, Expeditionsversorgung, Fraktionshilfe, Notfallreserve, regionale Kulturveranstaltung. Jede Instanz verwendet Lokalisierungsbausteine und klare Systemwerte.

**Verbot:** Ein prozeduraler Auftrag darf niemals einen festen Hauptstoryflag setzen, Ultima vorzeitig freischalten, eine einzigartige Figur töten oder eine unersetzbare Ressource verlangen.

# 6. Technisches Quest-Datenmodell

Jede Mission erhält stabile IDs und wird datengetrieben definiert. Mindestfelder:

```xml
<Quest id="story.occidentia.001" category="MainStory" version="1">
  <Title loc="quest.story.occidentia.001.title"/>
  <Region id="occidentia"/>
  <Prerequisites>
    <Flag id="storyMode" value="true"/>
  </Prerequisites>
  <Objectives mode="All">
    <Build building="occidentia.harbor.office.t1" count="1"/>
    <Build building="occidentia.road.t1" count="1"/>
    <Build building="occidentia.residence.farmer.t1" count="1"/>
  </Objectives>
  <Dialogue ref="dialogue.story.occidentia.001.open"/>
  <OnComplete>
    <SetFlag id="st_occ_001_complete" value="true"/>
    <Unlock id="occidentia.market"/>
  </OnComplete>
</Quest>
```

Dialoge liegen getrennt von Questlogik und verwenden Lokalisierungsschlüssel. Cutscene-/Kameraanweisungen sind optionale Datenblöcke; eine Mission darf nicht nur funktionieren, wenn eine bestimmte Kameraanimation erfolgreich abgespielt wird.

# 7. Storyflags und Savegame-Regeln
- Jeder Hauptmissionsabschluss setzt genau einen stabilen Completion-Flag.
- Mehrphasige Ziele speichern ihre aktuelle Phase separat.
- Dialogzeilen besitzen `played`, `skipped` und gegebenenfalls `choice`-Status.
- Regionale Freischaltungen werden getrennt von Quest-UI-Zuständen gespeichert.
- Profilweite Unlocks (`buildModeUnlocked`) werden erst nach bestätigtem Abschluss der dafür vorgesehenen Hauptstorymission gesetzt.
- Save-Migrationen dürfen abgeschlossene Missionen niemals stillschweigend zurücksetzen.

# 8. Mission-Bible Definition of Done

**Zusatz ab Voice-Lock Pass 07:** Vor Content Lock muss jede feste Mission den Dialog-Lint bestehen: keine gesprochenen Missionstitel/IDs, keine Tracker-Prosa im Dialog, keine pauschalen Prozenttrigger, keine verbotenen Serienformeln und keine exakt doppelten gesprochenen Zeilen.

Eine Mission gilt erst als produktionsfertig, wenn Questdaten, Dialoglokalisierung, UI-Texte, Trigger, Kameraoptionen, Save/Load, alternative bereits-erfüllt-Pfade, Fail-Safes und automatisierte Tests vorhanden sind. Platzhalter wie „Dialog später“ oder „Questziel TBD“ sind für Hauptmissionen vor Content Lock nicht zulässig.

# CINEMATIC STORY ANCHOR SYNC — PASS 08

Die großen filmischen Storyanker A–N sind im Mega-Prompt unter „Cinematic Story Anchor Pass 08“ autoritativ definiert. Einzelmissionen dieser Bible müssen diese Anker vorbereiten und auslösen, dürfen aber keine konkurrierende Lore-Wahrheit erzeugen. Cinematics verwenden nach Möglichkeit die reale Spielerwelt und tatsächliche Gebäude/Schiffe. Jeder Anker benötigt Checkpoint, Skip-Regel, Untertitel, barrierearme Kameraoption und einen sauberen Rückweg ins Gameplay.

---


# PASS-09-SYNCHRONISIERUNG

Die Mission Bible bleibt inhaltlich auf **Voice-Lock Pass 07 / Cinematic Pass 08**. Pass 09 ändert keine Missionsdialoge, sondern ergänzt die Produktionsgrundlage. Für Missionen gelten zusätzlich:

- IDs/Contentobjekte aus `CONTENT_CATALOG_v1.0.md`;
- Forschungsgates aus `RESEARCH_TREE_v1.0.md`;
- Wirtschafts-/Zeitbaselines aus `BALANCING_BIBLE_v1.0.md`;
- Inszenierung aus `ART_BIBLE_v1.0.md`;
- Musik/Voice/SFX aus `AUDIO_BIBLE_v1.0.md`.

Keine Mission darf für ihre Umsetzung ein zweites konkurrierendes Datenobjekt erfinden.

# RESEARCH TREE v1.0 — Content Lock Pass 09


## 1. Verbindliche Regeln

Der Forschungsbaum ist **kein bloßer Bonusbaum**. Jeder Knoten muss mindestens eine konkrete Anlage, ein Rezept, ein Fahrzeug/Schiff, einen Dienst, eine neue Qualitätsstufe, eine Expeditionseigenschaft oder eine Sicherheits-/Automationsfunktion freischalten.

- Keine Pflichtforschung darf durch eine optionale Handelsware hardlocken.
- Regionale Forschung ergänzt die globalen Zweige.
- Der Spieler sieht Voraussetzungen und Folgen vor Start eines Projekts.
- Abgeschlossene Projekte bleiben dauerhaft im Spielstand.
- Mods dürfen neue Kategorien/Knoten hinzufügen.
- `RQ1–RQ8` bleiben Ultimas sichtbare Forschungsqualitäts-/Fortschrittsgates und bündeln mehrere konkrete Knoten.

## 2. Konkreter Baum — 154 Knoten

| ID | Region | Zweig | Pos. | Projekt | Voraussetzung | Konkrete Freischaltung |
|---|---|---|---|---|---|---|
| resrch.kw.agr.01 | Bekannte Welt | Landwirtschaft & Nahrung | 1 | Fruchtbarkeitskartierung | regionale Startbedingung | zeigt regionale Fruchtbarkeiten präziser und schaltet Planungsfilter frei |
| resrch.kw.agr.02 | Bekannte Welt | Landwirtschaft & Nahrung | 2 | Modulare Feldwirtschaft | resrch.kw.agr.01 | 1×1/2×2/3×3 Feldmodule und effizientere Hofplanung |
| resrch.kw.agr.03 | Bekannte Welt | Landwirtschaft & Nahrung | 3 | Tiergesundheit | resrch.kw.agr.02 | stabilere Tierhaltung, Veterinärdienst |
| resrch.kw.agr.04 | Bekannte Welt | Landwirtschaft & Nahrung | 4 | Bewässerungsgrundlagen | resrch.kw.agr.03 | Bewässerungsbezirke/Reservoirintegration |
| resrch.kw.agr.05 | Bekannte Welt | Landwirtschaft & Nahrung | 5 | Saatgutselektion | resrch.kw.agr.04 | alternative regionale Kulturen und robustere Saisonprofile |
| resrch.kw.agr.06 | Bekannte Welt | Landwirtschaft & Nahrung | 6 | Kühlkette | resrch.kw.agr.05 | Kühlhäuser und gekühlte Transporte für empfindliche Nahrung |
| resrch.kw.agr.07 | Bekannte Welt | Landwirtschaft & Nahrung | 7 | Mechanisierte Landwirtschaft | resrch.kw.agr.06 | höhere Hofleistung durch konkrete Maschinenmodule |
| resrch.kw.agr.08 | Bekannte Welt | Landwirtschaft & Nahrung | 8 | Präzisionsagrarwirtschaft | resrch.kw.agr.07 | späte Sensornutzung und automatisierte Planung ohne Feldmikro |
| resrch.kw.mat.01 | Bekannte Welt | Materialien & Bauwesen | 1 | Normierte Bauhölzer | regionale Startbedingung | Bretterstandard und frühe Baukits |
| resrch.kw.mat.02 | Bekannte Welt | Materialien & Bauwesen | 2 | Ziegelnormen | resrch.kw.mat.01 | Ziegelwerke/standardisierte Baukosten |
| resrch.kw.mat.03 | Bekannte Welt | Materialien & Bauwesen | 3 | Stahlbau | resrch.kw.mat.02 | Stahlträger und größere Spannweiten |
| resrch.kw.mat.04 | Bekannte Welt | Materialien & Bauwesen | 4 | Betonbau | resrch.kw.mat.03 | Beton-/Zementkette |
| resrch.kw.mat.05 | Bekannte Welt | Materialien & Bauwesen | 5 | Stahlbeton | resrch.kw.mat.04 | Hochbau und dichte Wohnformen |
| resrch.kw.mat.06 | Bekannte Welt | Materialien & Bauwesen | 6 | Bauglas | resrch.kw.mat.05 | große Fenster/Fassaden/Hochhäuser |
| resrch.kw.mat.07 | Bekannte Welt | Materialien & Bauwesen | 7 | Hochhausstatik | resrch.kw.mat.06 | Wohnblock-/Wolkenkratzerfreigaben |
| resrch.kw.mat.08 | Bekannte Welt | Materialien & Bauwesen | 8 | Modulare Megastrukturen | resrch.kw.mat.07 | späte Großprojekte und Monumentmodule |
| resrch.kw.mac.01 | Bekannte Welt | Maschinen & Produktion | 1 | Werkzeugnormung | regionale Startbedingung | Werkzeuge und Werkstattmodule |
| resrch.kw.mac.02 | Bekannte Welt | Maschinen & Produktion | 2 | Dampfmechanisierung | resrch.kw.mac.01 | mechanisierte Produktionsgebäude |
| resrch.kw.mac.03 | Bekannte Welt | Maschinen & Produktion | 3 | Fließfertigung | resrch.kw.mac.02 | konkrete Mehrmaschinenlinien |
| resrch.kw.mac.04 | Bekannte Welt | Maschinen & Produktion | 4 | Elektromotoren | resrch.kw.mac.03 | elektrifizierte Produktionsmodule |
| resrch.kw.mac.05 | Bekannte Welt | Maschinen & Produktion | 5 | Automatische Regelung | resrch.kw.mac.04 | Produktionscontroller und Fehlermeldungen |
| resrch.kw.mac.06 | Bekannte Welt | Maschinen & Produktion | 6 | Prozessintegration | resrch.kw.mac.05 | alternative Rezepte und gekoppelte Linien |
| resrch.kw.mac.07 | Bekannte Welt | Maschinen & Produktion | 7 | Präzisionsmaschinen | resrch.kw.mac.06 | Hightech-Komponenten |
| resrch.kw.mac.08 | Bekannte Welt | Maschinen & Produktion | 8 | Flexible Fertigung | resrch.kw.mac.07 | späte Rezeptslots und schnellere Umrüstung |
| resrch.kw.trn.01 | Bekannte Welt | Transport & Schifffahrt | 1 | Verbesserte Straßen | regionale Startbedingung | nächste Straßenqualität und Transportreichweite |
| resrch.kw.trn.02 | Bekannte Welt | Transport & Schifffahrt | 2 | Hafenkräne | resrch.kw.trn.01 | zusätzliche Hafenabfertigung |
| resrch.kw.trn.03 | Bekannte Welt | Transport & Schifffahrt | 3 | Dampfschifffahrt | resrch.kw.trn.02 | Dampf-Fracht-/Passagierschiffe |
| resrch.kw.trn.04 | Bekannte Welt | Transport & Schifffahrt | 4 | Eisenbahn | resrch.kw.trn.03 | Personen- und Frachtbahn |
| resrch.kw.trn.05 | Bekannte Welt | Transport & Schifffahrt | 5 | Straßenbahn | resrch.kw.trn.04 | städtischer ÖPNV |
| resrch.kw.trn.06 | Bekannte Welt | Transport & Schifffahrt | 6 | U-Bahn | resrch.kw.trn.05 | Metropolenverkehr |
| resrch.kw.trn.07 | Bekannte Welt | Transport & Schifffahrt | 7 | Expressnetz | resrch.kw.trn.06 | Fern-/Flughafenexpress |
| resrch.kw.trn.08 | Bekannte Welt | Transport & Schifffahrt | 8 | Intermodale Knoten | resrch.kw.trn.07 | automatische Übergabe Straße/Bahn/Hafen/Luft |
| resrch.kw.ene.01 | Bekannte Welt | Energie & Elektrifizierung | 1 | Mechanische Energie | regionale Startbedingung | Wasser-/Wind-/Dampfantriebe je Region |
| resrch.kw.ene.02 | Bekannte Welt | Energie & Elektrifizierung | 2 | Stromerzeugung I | resrch.kw.ene.01 | frühe Elektrifizierung |
| resrch.kw.ene.03 | Bekannte Welt | Energie & Elektrifizierung | 3 | Verteilnetze | resrch.kw.ene.02 | Kapazität, Prioritäten und Netzdiagnose |
| resrch.kw.ene.04 | Bekannte Welt | Energie & Elektrifizierung | 4 | Stromerzeugung II | resrch.kw.ene.03 | größere Kraftwerke/regionale Varianten |
| resrch.kw.ene.05 | Bekannte Welt | Energie & Elektrifizierung | 5 | Speicher & Reserve | resrch.kw.ene.04 | Reserven und Redundanz |
| resrch.kw.ene.06 | Bekannte Welt | Energie & Elektrifizierung | 6 | Hocheffizienznetze | resrch.kw.ene.05 | geringere Verluste und höhere Kapazität |
| resrch.kw.ene.07 | Bekannte Welt | Energie & Elektrifizierung | 7 | Erneuerbare Integration | resrch.kw.ene.06 | Wasser/Solar/Wind/Geothermie je Region |
| resrch.kw.ene.08 | Bekannte Welt | Energie & Elektrifizierung | 8 | Smart Grid | resrch.kw.ene.07 | späte automatische Lastverteilung ohne Mikromanagement |
| resrch.kw.wat.01 | Bekannte Welt | Wasser, Hygiene & Umwelt | 1 | Brunnen & Grundwasser | regionale Startbedingung | frühe Wassergewinnung |
| resrch.kw.wat.02 | Bekannte Welt | Wasser, Hygiene & Umwelt | 2 | Filtration | resrch.kw.wat.01 | sauberes Wasser |
| resrch.kw.wat.03 | Bekannte Welt | Wasser, Hygiene & Umwelt | 3 | Kanalisation | resrch.kw.wat.02 | Abwassernetz |
| resrch.kw.wat.04 | Bekannte Welt | Wasser, Hygiene & Umwelt | 4 | Klärtechnik | resrch.kw.wat.03 | Kläranlagen |
| resrch.kw.wat.05 | Bekannte Welt | Wasser, Hygiene & Umwelt | 5 | Abfallwirtschaft | resrch.kw.wat.04 | Sammlung und Verwertung |
| resrch.kw.wat.06 | Bekannte Welt | Wasser, Hygiene & Umwelt | 6 | Kreislaufwasser | resrch.kw.wat.05 | Wiederverwendung |
| resrch.kw.wat.07 | Bekannte Welt | Wasser, Hygiene & Umwelt | 7 | Umweltmessung | resrch.kw.wat.06 | Verschmutzungs-/Umweltfeedback |
| resrch.kw.wat.08 | Bekannte Welt | Wasser, Hygiene & Umwelt | 8 | Resiliente Netze | resrch.kw.wat.07 | Reserve, Notversorgung und Ausfallsicherheit |
| resrch.kw.com.01 | Bekannte Welt | Kommunikation & Verwaltung | 1 | Lokale Post | regionale Startbedingung | Post P1 |
| resrch.kw.com.02 | Bekannte Welt | Kommunikation & Verwaltung | 2 | Regionale Post | resrch.kw.com.01 | Post P2 |
| resrch.kw.com.03 | Bekannte Welt | Kommunikation & Verwaltung | 3 | Fernpost | resrch.kw.com.02 | Post P3 |
| resrch.kw.com.04 | Bekannte Welt | Kommunikation & Verwaltung | 4 | Telegraphie/Frühfunk | resrch.kw.com.03 | P4 |
| resrch.kw.com.05 | Bekannte Welt | Kommunikation & Verwaltung | 5 | Rundfunk & Datennetze | resrch.kw.com.04 | P5 |
| resrch.kw.com.06 | Bekannte Welt | Kommunikation & Verwaltung | 6 | Internationale Kommunikation | resrch.kw.com.05 | P6 |
| resrch.kw.com.07 | Bekannte Welt | Kommunikation & Verwaltung | 7 | Verwaltungsstandards | resrch.kw.com.06 | Rathaus-/Politikmodule |
| resrch.kw.com.08 | Bekannte Welt | Kommunikation & Verwaltung | 8 | Krisenkoordination | resrch.kw.com.07 | späte globale Verwaltungsdienste |
| resrch.kw.med.01 | Bekannte Welt | Medizin & öffentliche Gesundheit | 1 | Sanitätsstation | regionale Startbedingung | frühe Gesundheitsversorgung |
| resrch.kw.med.02 | Bekannte Welt | Medizin & öffentliche Gesundheit | 2 | Apothekennetz | resrch.kw.med.01 | Medikamenten-/Versorgungslogik |
| resrch.kw.med.03 | Bekannte Welt | Medizin & öffentliche Gesundheit | 3 | Krankenhaus | resrch.kw.med.02 | mittlere Gesundheitsqualität |
| resrch.kw.med.04 | Bekannte Welt | Medizin & öffentliche Gesundheit | 4 | Hygienestandards | resrch.kw.med.03 | Wasser/Hygiene-Kopplung |
| resrch.kw.med.05 | Bekannte Welt | Medizin & öffentliche Gesundheit | 5 | Notfallmedizin | resrch.kw.med.04 | Rettungsdienste |
| resrch.kw.med.06 | Bekannte Welt | Medizin & öffentliche Gesundheit | 6 | Spezialkliniken | resrch.kw.med.05 | späte Gesundheitsdienste |
| resrch.kw.med.07 | Bekannte Welt | Medizin & öffentliche Gesundheit | 7 | Epidemiologie | resrch.kw.med.06 | Frühwarnung statt unfairer Zufallsepidemien |
| resrch.kw.med.08 | Bekannte Welt | Medizin & öffentliche Gesundheit | 8 | Hochleistungsmedizin | resrch.kw.med.07 | späte bekannte-Welt-Medizin/Brücke zu Ultima |
| resrch.kw.log.01 | Bekannte Welt | Logistik & Lagerung | 1 | Lagergrundlagen | regionale Startbedingung | Lagerhäuser und Mindestreserven |
| resrch.kw.log.02 | Bekannte Welt | Logistik & Lagerung | 2 | Kleine Lagerhäuser | resrch.kw.log.01 | kompakter Footprint bei gleicher Lagerkapazität, weniger Rampen |
| resrch.kw.log.03 | Bekannte Welt | Logistik & Lagerung | 3 | Rampenmodule I | resrch.kw.log.02 | zusätzliche parallele Abfertigung |
| resrch.kw.log.04 | Bekannte Welt | Logistik & Lagerung | 4 | Rampenmodule II | resrch.kw.log.03 | weitere Abfertigung/Automatisierung |
| resrch.kw.log.05 | Bekannte Welt | Logistik & Lagerung | 5 | Frachtprioritäten | resrch.kw.log.04 | Warenklassen priorisieren |
| resrch.kw.log.06 | Bekannte Welt | Logistik & Lagerung | 6 | Kühl-/Speziallager | resrch.kw.log.05 | spezialisierte Waren |
| resrch.kw.log.07 | Bekannte Welt | Logistik & Lagerung | 7 | Hochregallager | resrch.kw.log.06 | späte Flächeneffizienz |
| resrch.kw.log.08 | Bekannte Welt | Logistik & Lagerung | 8 | Automatisierte Umschlagzentren | resrch.kw.log.07 | späte intermodale Logistik |
| resrch.kw.exp.01 | Bekannte Welt | Exploration & Expeditionen | 1 | Küstennavigation | regionale Startbedingung | frühe Expeditionen |
| resrch.kw.exp.02 | Bekannte Welt | Exploration & Expeditionen | 2 | Kartografie | resrch.kw.exp.01 | größere Reichweite/Information |
| resrch.kw.exp.03 | Bekannte Welt | Exploration & Expeditionen | 3 | Langstreckenversorgung | resrch.kw.exp.02 | Expeditionsrationen und Reserven |
| resrch.kw.exp.04 | Bekannte Welt | Exploration & Expeditionen | 4 | Klimaschutz | resrch.kw.exp.03 | Tropen/Arid/Polar-Ausrüstung |
| resrch.kw.exp.05 | Bekannte Welt | Exploration & Expeditionen | 5 | Wissenschaftsexpeditionen | resrch.kw.exp.04 | Forschungschecks |
| resrch.kw.exp.06 | Bekannte Welt | Exploration & Expeditionen | 6 | Diplomatische Expeditionen | resrch.kw.exp.05 | Fraktionschecks |
| resrch.kw.exp.07 | Bekannte Welt | Exploration & Expeditionen | 7 | Polarexpedition | resrch.kw.exp.06 | Australis/Eisbrecher |
| resrch.kw.exp.08 | Bekannte Welt | Exploration & Expeditionen | 8 | Anomalie-Navigation | resrch.kw.exp.07 | Storybrücke Eiswand/Ultima |
| resrch.occidentia.spec.01 | Occidentia | Regionalspezialisierung | 1 | Industrieplanung | passende globale Forschung + Regionsstufe | größere Fabrikcluster und Verwaltungswerkzeuge |
| resrch.occidentia.spec.02 | Occidentia | Regionalspezialisierung | 2 | Hochhausversorgung | resrch.occidentia.spec.01 | Dienste für hohe Dichte |
| resrch.occidentia.spec.03 | Occidentia | Regionalspezialisierung | 3 | Massenverkehr | resrch.occidentia.spec.02 | U-Bahn/Express |
| resrch.occidentia.spec.04 | Occidentia | Regionalspezialisierung | 4 | Internationale Finanzkoordination | resrch.occidentia.spec.03 | Magnaten-/Elite-Institutionen |
| resrch.occidentia.spec.05 | Occidentia | Regionalspezialisierung | 5 | Metropolenresilienz | resrch.occidentia.spec.04 | Redundanzpakete |
| resrch.occidentia.spec.06 | Occidentia | Regionalspezialisierung | 6 | Luftfahrtlogistik | resrch.occidentia.spec.05 | Flughafen-/Geschäftsflugzugang |
| resrch.meridia.spec.01 | Meridia | Regionalspezialisierung | 1 | Tropische Bewässerung | passende globale Forschung + Regionsstufe | automatisierte Bewässerungsbezirke |
| resrch.meridia.spec.02 | Meridia | Regionalspezialisierung | 2 | Flussumschlag | resrch.meridia.spec.01 | Flusspiers/leichte Frachter |
| resrch.meridia.spec.03 | Meridia | Regionalspezialisierung | 3 | Plantagenverarbeitung | resrch.meridia.spec.02 | Kaffee/Kakao/Zucker effizient verarbeiten |
| resrch.meridia.spec.04 | Meridia | Regionalspezialisierung | 4 | Kautschukchemie | resrch.meridia.spec.03 | Gummiwaren/Industrie |
| resrch.meridia.spec.05 | Meridia | Regionalspezialisierung | 5 | Großwasserkraft | resrch.meridia.spec.04 | späte Energie |
| resrch.meridia.spec.06 | Meridia | Regionalspezialisierung | 6 | Kühl-Exportketten | resrch.meridia.spec.05 | Tropenfrüchte/Fleisch exportfähig |
| resrch.orientia.spec.01 | Orientia | Regionalspezialisierung | 1 | Terrassenautomation | passende globale Forschung + Regionsstufe | Feldraster passt sich Hängen an |
| resrch.orientia.spec.02 | Orientia | Regionalspezialisierung | 2 | Kanal-Schienen-Knoten | resrch.orientia.spec.01 | vereinfachte Intermodalität |
| resrch.orientia.spec.03 | Orientia | Regionalspezialisierung | 3 | Seidenpräzision | resrch.orientia.spec.02 | Seiden-/Textilhighend |
| resrch.orientia.spec.04 | Orientia | Regionalspezialisierung | 4 | Porzellanöfen | resrch.orientia.spec.03 | hochwertige Keramik |
| resrch.orientia.spec.05 | Orientia | Regionalspezialisierung | 5 | Präzisionskommunikation | resrch.orientia.spec.04 | Radio/Kommunikationstechnik |
| resrch.orientia.spec.06 | Orientia | Regionalspezialisierung | 6 | Hochgeschwindigkeitsaufzüge | resrch.orientia.spec.05 | dichte Metropolen |
| resrch.aferia.spec.01 | Aferia | Regionalspezialisierung | 1 | Trockenlandreservoirs | passende globale Forschung + Regionsstufe | Wasserresilienz |
| resrch.aferia.spec.02 | Aferia | Regionalspezialisierung | 2 | Karawanenlogistik | resrch.aferia.spec.01 | Binnenposten |
| resrch.aferia.spec.03 | Aferia | Regionalspezialisierung | 3 | Rohstoffaufbereitung | resrch.aferia.spec.02 | Kupfer/Gold/Erze |
| resrch.aferia.spec.04 | Aferia | Regionalspezialisierung | 4 | Solare Netze | resrch.aferia.spec.03 | regionale Energie |
| resrch.aferia.spec.05 | Aferia | Regionalspezialisierung | 5 | Kühl- und Konservierungstechnik | resrch.aferia.spec.04 | Exportketten |
| resrch.aferia.spec.06 | Aferia | Regionalspezialisierung | 6 | Fernhandelsnavigation | resrch.aferia.spec.05 | lange Seewege |
| resrch.australis.spec.01 | Australis | Regionalspezialisierung | 1 | Isolation I | passende globale Forschung + Regionsstufe | Wärmeverluste reduzieren |
| resrch.australis.spec.02 | Australis | Regionalspezialisierung | 2 | Isolation II | resrch.australis.spec.01 | höhere Polarwohnstufen |
| resrch.australis.spec.03 | Australis | Regionalspezialisierung | 3 | Eisbrechertechnik | resrch.australis.spec.02 | leichte/schwere Eisbrecher |
| resrch.australis.spec.04 | Australis | Regionalspezialisierung | 4 | Hydroponik | resrch.australis.spec.03 | lokale Frischwaren |
| resrch.australis.spec.05 | Australis | Regionalspezialisierung | 5 | Geothermische Polarenergie | resrch.australis.spec.04 | lokale Energie |
| resrch.australis.spec.06 | Australis | Regionalspezialisierung | 6 | Geophysik & Tiefeneis | resrch.australis.spec.05 | Eiswand-Storytechnik |
| resrch.australis.spec.07 | Australis | Regionalspezialisierung | 7 | Extremwetterprognose | resrch.australis.spec.06 | Vorwarnung |
| resrch.australis.spec.08 | Australis | Regionalspezialisierung | 8 | Langstrecken-Polarflug | resrch.australis.spec.07 | späte Verbindung |
| resrch.ultima.aer.01 | Ultima | Äther & Resonanz | 1 | Kopplung | RQ-Stufe + Regionszugang | AQ1-Grundkopplung |
| resrch.ultima.aer.02 | Ultima | Äther & Resonanz | 2 | Stabilisierung | resrch.ultima.aer.01 | Stabilitätsdienste |
| resrch.ultima.aer.03 | Ultima | Äther & Resonanz | 3 | Drahtlose Übertragung | resrch.ultima.aer.02 | Äthertürme |
| resrch.ultima.aer.04 | Ultima | Äther & Resonanz | 4 | Weltgitter | resrch.ultima.aer.03 | Relais und Fernkopplung |
| resrch.ultima.aer.05 | Ultima | Äther & Resonanz | 5 | Portale | resrch.ultima.aer.04 | kalibrierte Toranlagen |
| resrch.ultima.aer.06 | Ultima | Äther & Resonanz | 6 | Fernsensorik | resrch.ultima.aer.05 | Fernspiegel/Äthersensorik |
| resrch.ultima.aut.01 | Ultima | Automation & Daten | 1 | Servosysteme | RQ-Stufe + Regionszugang | U1-Automation |
| resrch.ultima.aut.02 | Ultima | Automation & Daten | 2 | Steuerkerne | resrch.ultima.aut.01 | präzise Prozesssteuerung |
| resrch.ultima.aut.03 | Ultima | Automation & Daten | 3 | Integrierte Fabriken | resrch.ultima.aut.02 | U3-Komplexe |
| resrch.ultima.aut.04 | Ultima | Automation & Daten | 4 | Adaptive Netze | resrch.ultima.aut.03 | automatische Last-/Frachtverteilung |
| resrch.ultima.aut.05 | Ultima | Automation & Daten | 5 | Archiv-/Datenanalyse | resrch.ultima.aut.04 | alte Systeme rekonstruieren |
| resrch.ultima.aut.06 | Ultima | Automation & Daten | 6 | Krisenautomation | resrch.ultima.aut.05 | Nexus-/Resilienzsteuerung |
| resrch.ultima.bio.01 | Ultima | Bio & Medizin | 1 | Fermentation | RQ-Stufe + Regionszugang | Bio-Grundprozesse |
| resrch.ultima.bio.02 | Ultima | Bio & Medizin | 2 | Sterile Prozesse | resrch.ultima.bio.01 | Medizin/Produktion |
| resrch.ultima.bio.03 | Ultima | Bio & Medizin | 3 | Biopolymere | resrch.ultima.bio.02 | lebende/leichte Werkstoffe |
| resrch.ultima.bio.04 | Ultima | Bio & Medizin | 4 | Regeneration | resrch.ultima.bio.03 | Regenerationszentren |
| resrch.ultima.bio.05 | Ultima | Bio & Medizin | 5 | Bioäther | resrch.ultima.bio.04 | Äther-Bio-Kopplung |
| resrch.ultima.bio.06 | Ultima | Bio & Medizin | 6 | Planetare Ökologie | resrch.ultima.bio.05 | große Ökosystemsteuerung |
| resrch.ultima.meg.01 | Ultima | Material & Megabau | 1 | Präzisionsstein | RQ-Stufe + Regionszugang | Megalithtechnik |
| resrch.ultima.meg.02 | Ultima | Material & Megabau | 2 | Hartmetalle | resrch.ultima.meg.01 | Schwertechnik |
| resrch.ultima.meg.03 | Ultima | Material & Megabau | 3 | Titanstahl | resrch.ultima.meg.02 | Titania-Hochbau |
| resrch.ultima.meg.04 | Ultima | Material & Megabau | 4 | Selbstheilende Verbünde | resrch.ultima.meg.03 | Viridia/Titania-Verbund |
| resrch.ultima.meg.05 | Ultima | Material & Megabau | 5 | Gravmontage | resrch.ultima.meg.04 | Gravkräne |
| resrch.ultima.meg.06 | Ultima | Material & Megabau | 6 | Weltfundamente | resrch.ultima.meg.05 | World-Nexus-Basis |
| resrch.ultima.thr.01 | Ultima | Thermal & Luftfahrt | 1 | Thermokeramik | RQ-Stufe + Regionszugang | Hitzetechnik |
| resrch.ultima.thr.02 | Ultima | Thermal & Luftfahrt | 2 | Hochtemperaturkühlung | resrch.ultima.thr.01 | Ignaria-Industrie |
| resrch.ultima.thr.03 | Ultima | Thermal & Luftfahrt | 3 | Äthertriebwerke | resrch.ultima.thr.02 | Ätherflug |
| resrch.ultima.thr.04 | Ultima | Thermal & Luftfahrt | 4 | Hochatmosphäre | resrch.ultima.thr.03 | Langstreckenflug |
| resrch.ultima.thr.05 | Ultima | Thermal & Luftfahrt | 5 | Wettertechnik | resrch.ultima.thr.04 | Atmosphärenkontrolle |
| resrch.ultima.thr.06 | Ultima | Thermal & Luftfahrt | 6 | Drachenkooperation | resrch.ultima.thr.05 | freiwillige drakonische Dienste |
| resrch.ultima.oce.01 | Ultima | Ozean & Druck | 1 | Entsalzung | RQ-Stufe + Regionszugang | Pelagia-Wasser |
| resrch.ultima.oce.02 | Ultima | Ozean & Druck | 2 | Druckkeramik | resrch.ultima.oce.01 | Tiefseematerial |
| resrch.ultima.oce.03 | Ultima | Ozean & Druck | 3 | Abyssalsensorik | resrch.ultima.oce.02 | Tiefseeforschung |
| resrch.ultima.oce.04 | Ultima | Ozean & Druck | 4 | Submersibles | resrch.ultima.oce.03 | Unterwasserfahrzeuge |
| resrch.ultima.oce.05 | Ultima | Ozean & Druck | 5 | Weltkühlung | resrch.ultima.oce.04 | große Kühlverbünde |
| resrch.ultima.oce.06 | Ultima | Ozean & Druck | 6 | Tiefenportale | resrch.ultima.oce.05 | spätes Pelagia-Portal |
| resrch.ultima.soc.01 | Ultima | Gesellschaft & Archive | 1 | Bildung | RQ-Stufe + Regionszugang | Ultima-Bildung |
| resrch.ultima.soc.02 | Ultima | Gesellschaft & Archive | 2 | Diplomatie | resrch.ultima.soc.01 | Fraktionsabkommen |
| resrch.ultima.soc.03 | Ultima | Gesellschaft & Archive | 3 | Wächteranerkennung | resrch.ultima.soc.02 | Wächterzugänge |
| resrch.ultima.soc.04 | Ultima | Gesellschaft & Archive | 4 | Archivzugang | resrch.ultima.soc.03 | versiegeltes Wissen |
| resrch.ultima.soc.05 | Ultima | Gesellschaft & Archive | 5 | Interregionale Standards | resrch.ultima.soc.04 | Cross-Region-Systeme |
| resrch.ultima.soc.06 | Ultima | Gesellschaft & Archive | 6 | Nexus-Governance | resrch.ultima.soc.05 | World-Nexus-Kontrolle |

## 3. Ultima RQ1–RQ8 Gate-Matrix

| RQ | Schwerpunkt | Gate | Freischaltungen |
|---|---|---|---|
| RQ1 | Vermessung & Rekonstruktion | mind. 1 Knoten aus AER/AUT/SOC Stufe 1 | Grundscanner, Materialanalyse, sichere Ruinenarbeit |
| RQ2 | Regionale Verfahren | mehrere Regionalstufe-2-Knoten | Biofilter, Präzisionsstein, Thermokeramik, Küsten-/Berglogistik |
| RQ3 | Ätherkopplung & Elektrifizierung | AER 2+, AUT 2+ | AQ2/AQ3, elektrische Transitnetze, frühe Automation |
| RQ4 | Integrierte Systeme | AUT 3, MEG 2, BIO 2 | U2/U3, Maglev-Grundlagen, Resonanzsensorik |
| RQ5 | Interregionale Hochtechnologie | mind. 4 Ultima-Regionen erschlossen | Gravkräne, Ätherflug, Biosynthese, Tiefensensorik |
| RQ6 | Weltverbund | alle 5 Regionen + mehrere Diplomatiebedingungen | globale Kommunikation, Portalkalibrierung, AQ5 |
| RQ7 | Rekonstruktion uralter Systeme | Archivzugang + Wächterprotokolle | Apex-Steuerkerne, Weltfundamente, fortgeschrittene Tore |
| RQ8 | World-Nexus-Endgame | 12-Akt-Storygate oder äquivalente Aufbau-Endgamebedingungen | AQ6, World Nexus, hochstabile Portalnetze, globale Resilienz |

## 4. Forschungsprojektkosten

Jeder Knoten besitzt:

`baseResearchWork`, `requiredGoods`, `requiredBuildingIds`, `requiredPopulationConditions`, `prerequisiteResearchIds`, optionale `diplomacyConditions`.

Arbeitsbasis für `baseResearchWork`:

- frühe bekannte Welt: 2–8 Forschungsminuten bei voller Kapazität;
- mittlere bekannte Welt: 6–18 Minuten;
- späte bekannte Welt: 12–30 Minuten;
- frühes Ultima: 8–20 Minuten;
- mittleres Ultima: 15–35 Minuten;
- Apex/World-Nexus: 25–60 Minuten.

Das sind **Balancingbaselines**, keine künstlichen Wartezeiten: größere Forschungsverbünde, Materialbereitstellung und Kooperation können die effektive Zeit senken. Ein Projekt darf nicht einfach durch einen Echtgeld-/Premium-Timer übersprungen werden.

## 5. Forschungs-UI

Der Baum zeigt pro Knoten:

- ID/Name;
- Region/Zweig;
- Vorgänger/Nachfolger;
- Materialbedarf;
- Forschungskapazität;
- geschätzte Dauer;
- konkrete Freischaltungen;
- Story-/Diplomatiegate;
- Alternativen;
- Modquelle.

Kategorien sind einklappbar; Suche und Filter sind Pflicht.

# Tests & Gates Rule

Jede Codeänderung benötigt passende Tests oder eine begründete dokumentierte Ausnahme.

Vor Paketabschluss:
1. Compile/Importfehler = 0.
2. EditMode/Unit Tests ausführen.
3. PlayMode/Integration Tests ausführen, wenn Systemverhalten betroffen ist.
4. Daten-/XML-Validator ausführen, wenn Contentdaten betroffen sind.
5. Save/Load testen, wenn persistent state betroffen ist.
6. Build-/Größen-/Performance-Gates prüfen, wenn Assets/Simulation betroffen sind.
7. Fehler vor Abschluss beheben.

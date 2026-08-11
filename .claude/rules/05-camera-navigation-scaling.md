# Camera Navigation Scaling Rule

Verbindlich für jedes World-/Map-/Kamera-Paket, nicht nur 0.1.1. Grund: derselbe
Geschwindigkeitsfehler (Pan/Zoom fühlen sich bei größeren Karten wieder zu langsam an)
wurde bei Varynth 0.1.1 zweimal gemeldet, weil die Kamera mit festen absoluten
Einheiten/Sekunde bzw. Einheiten/Notch konfiguriert war.

## Regel

- Pan-Geschwindigkeit und Zoom-Schrittweite dürfen **nicht** als feste absolute
  Konstanten (Units/Sekunde, Units/Scroll-Notch) implementiert werden.
- Beide müssen als **Funktion der aktuellen Kameradistanz/des aktuellen Zoomlevels**
  ausgedrückt werden, damit dieselbe Konfiguration ohne manuelles Nachjustieren sowohl
  für kleine Prototype-Inseln als auch für um Größenordnungen größere spätere Karten
  funktioniert.
- Referenzimplementierung: `Assets/Game/Presentation/Camera/CameraRigMath.cs`
  (`ComputePanSpeed`, `ComputeZoomTarget`, `SmoothZoom`) — pure, zustandslose,
  testbare Funktionen, aufgerufen aus `StrategyCameraController`.
  - `ComputePanSpeed`: Geschwindigkeit proportional zur aktuellen Zoomdistanz,
    mit unterer Grenze (`MinPanSpeed`) für Nahbereich-Präzision.
  - `ComputeZoomTarget`: multiplikativ/prozentual (`distance *= (1 - percent)^scrollDelta`),
    nicht additiv — der Schritt skaliert automatisch mit der aktuellen Distanz.
- Scroll-Rohinput wird weiterhin **nicht** mit `Time.deltaTime` multipliziert (Input ist
  ein Delta, keine Rate). Visuelles Smoothing (`SmoothZoom`) bleibt frame-rate-unabhängig
  über die etablierte `1 - e^(-speed*dt)`-Form.
- Min-/Max-Zoom-Clamping und Pan-Bounds-Clamping bleiben in jedem Fall erhalten
  (`ClampZoom`, `ClampPosition`).

## Tests

Regressionstests für Pan-/Zoom-Skalierung müssen **konfigurationsunabhängig** sein: sie
dürfen nicht die konkrete aktuelle Kartengröße/Zoom-Range hardcoden, sondern müssen die
Skalierungsgesetzmäßigkeit selbst prüfen (z. B. "gleicher Scroll-Input bei größerer
aktueller Distanz erzeugt größere absolute Distanzänderung", nicht "bei Distanz X ändert
sich der Wert um genau Y"). Siehe `Assets/Game/Tests/EditMode/Presentation/CameraRigMathTests.cs`.

## Verboten

- Ein neuer fester Multiplikator/eine neue Konstante als alleinige Reaktion auf "Kamera
  fühlt sich langsam an", ohne die zugrunde liegende Distanz-Skalierung zu prüfen.
- Tests, die nur gegen die aktuelle Prototype-Kartengröße bestehen und bei einer
  zukünftigen, um Größenordnungen größeren Karte stillschweigend wieder brechen würden.

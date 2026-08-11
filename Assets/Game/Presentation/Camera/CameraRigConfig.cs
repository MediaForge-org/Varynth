using System;
using UnityEngine;

namespace Varynth.Presentation.Camera
{
    /// <summary>
    /// Serializable prototype tuning values for the strategy camera. All numbers
    /// here are Phase 2A / Varynth 0.1.0 prototype values, not final balancing.
    /// </summary>
    [Serializable]
    public sealed class CameraRigConfig
    {
        public float RotationSpeedDegreesPerSecond = 90f;
        public float PitchDegrees = 55f;

        // --- Pan ---
        // Pan speed is deliberately NOT a fixed world-units/second constant: a fixed
        // constant that felt right for Phase 2A's single 260-unit test island became
        // imperceptibly slow once Phase 2B's archipelago pushed the zoom range out to
        // ~1000 units (reported twice as "still too slow" even after raising the old
        // constant). Instead, pan speed scales with the camera's CURRENT zoom distance:
        // zoomed far out for an overview, the same screen-space pan gesture must cover
        // much more world-space ground per second to feel responsive; zoomed in close,
        // the same formula naturally slows down for precise placement. This adapts
        // automatically to whatever ZoomMinDistance/ZoomMaxDistance a given world's
        // scene builder computes, with no separate per-world speed tuning needed.
        // See CameraRigMath.ComputePanSpeed.
        public float PanSpeedPerZoomDistance = 0.6f;
        public float MinPanSpeed = 25f;
        public float FastPanMultiplier = 2.2f;

        public float ZoomMinDistance = 15f;
        public float ZoomMaxDistance = 120f;

        /// <summary>
        /// Zoom distance used on scene start instead of the Zoom Min/Max midpoint.
        /// 0 (default) keeps the old midpoint behavior. Scene builders that know the
        /// real world/camera-target bounds (e.g. an archipelago far larger than the
        /// zoom range originally tuned for a single island) should compute and set
        /// this explicitly -- pointing the rig at a bounds center does not by itself
        /// guarantee anything is actually inside the camera frustum at start.
        /// </summary>
        public float InitialZoomDistance;

        // --- Zoom ---
        // Percentage-based (multiplicative), not a fixed absolute distance per scroll
        // notch: each raw scroll unit changes the CURRENT distance by this fraction, so
        // one notch is an equally noticeable ~20% jump whether the current view is a
        // close-up (small absolute change) or a full-archipelago overview (large
        // absolute change) -- unlike the old fixed-units-per-notch value, this does not
        // need re-tuning when a world's zoom range changes. Calibrated against
        // Mouse.scroll.y producing ~1 unit per physical wheel notch (consistent with
        // the previously-reported "needed dozens of notches" symptom at the old
        // ZoomSensitivity=8/notch constant over a ~105-unit range). See
        // CameraRigMath.ComputeZoomTarget.
        public float ZoomPercentPerNotch = 0.2f;
        public float ZoomSmoothSpeed = 10f;

        public Vector2 BoundsMin = new Vector2(20f, 20f);
        public Vector2 BoundsMax = new Vector2(280f, 280f);
    }
}

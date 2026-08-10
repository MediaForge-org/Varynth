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
        public float PanSpeed = 40f;
        public float FastPanMultiplier = 2.2f;
        public float RotationSpeedDegreesPerSecond = 90f;
        public float PitchDegrees = 55f;

        public float ZoomMinDistance = 15f;
        public float ZoomMaxDistance = 120f;

        /// <summary>
        /// Units of zoom distance change per raw Mouse.scroll.y input unit. Applied
        /// directly to the scroll delta (never multiplied by Time.deltaTime -- only
        /// the subsequent distance interpolation uses ZoomSmoothSpeed/deltaTime).
        /// Prototype value, deliberately not final: the previous default effectively
        /// required dozens of notches to reach either zoom bound and was reported as
        /// far too slow in manual review.
        /// </summary>
        public float ZoomSensitivity = 8f;
        public float ZoomSmoothSpeed = 10f;

        public Vector2 BoundsMin = new Vector2(20f, 20f);
        public Vector2 BoundsMax = new Vector2(280f, 280f);
    }
}

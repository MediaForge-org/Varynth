using UnityEngine;

namespace Varynth.Presentation.Camera
{
    /// <summary>
    /// Pure, static camera calculation helpers -- no MonoBehaviour, no
    /// UnityEngine.Input -- so clamping/wrapping is unit-testable without a scene.
    /// </summary>
    public static class CameraRigMath
    {
        public static float ClampZoom(float distance, CameraRigConfig config)
        {
            return Mathf.Clamp(distance, config.ZoomMinDistance, config.ZoomMaxDistance);
        }

        public static Vector2 ClampPosition(Vector2 positionXZ, CameraRigConfig config)
        {
            return new Vector2(
                Mathf.Clamp(positionXZ.x, config.BoundsMin.x, config.BoundsMax.x),
                Mathf.Clamp(positionXZ.y, config.BoundsMin.y, config.BoundsMax.y));
        }

        public static float WrapYaw(float degrees)
        {
            return Mathf.Repeat(degrees, 360f);
        }

        /// <summary>
        /// Distance a camera must sit from the center of a bounding sphere of the given
        /// radius so the sphere fits inside the given vertical field of view, with an
        /// optional headroom multiplier. Independent of pitch: this is the distance
        /// from camera to target-center needed for the sphere to fit inside the view
        /// cone, which does not change based on the angle of approach to that center.
        /// </summary>
        public static float ComputeFitDistance(float boundsRadius, float verticalFovDegrees, float margin = 1f)
        {
            var halfFovRad = Mathf.Clamp(verticalFovDegrees, 1f, 179f) * 0.5f * Mathf.Deg2Rad;
            return boundsRadius / Mathf.Sin(halfFovRad) * margin;
        }

        /// <summary>
        /// Pan speed for the current zoom distance: proportional to how far out the
        /// camera currently is (so panning scales with world/zoom-range size instead of
        /// needing a fixed constant re-tuned per world), floored at MinPanSpeed so
        /// close-up precision panning never crawls to a standstill.
        /// </summary>
        public static float ComputePanSpeed(float currentZoomDistance, CameraRigConfig config, bool fastModifierActive)
        {
            var baseSpeed = Mathf.Max(config.MinPanSpeed, config.PanSpeedPerZoomDistance * currentZoomDistance);
            return fastModifierActive ? baseSpeed * config.FastPanMultiplier : baseSpeed;
        }

        /// <summary>
        /// Multiplicative (percentage-based) zoom step: scrollDelta is a raw input delta
        /// (e.g. Mouse.scroll.y, not multiplied by Time.deltaTime -- only the subsequent
        /// SmoothZoom interpolation is frame-rate dependent). A positive scrollDelta
        /// zooms in (reduces distance); the result is clamped to the configured range.
        /// </summary>
        public static float ComputeZoomTarget(float currentTarget, float scrollDelta, CameraRigConfig config)
        {
            if (scrollDelta == 0f)
            {
                return currentTarget;
            }

            var percentPerNotch = Mathf.Clamp(config.ZoomPercentPerNotch, 0f, 0.95f);
            var factor = Mathf.Pow(1f - percentPerNotch, scrollDelta);
            return ClampZoom(currentTarget * factor, config);
        }

        /// <summary>
        /// Exponential-decay smoothing step from current toward target, independent of
        /// frame rate (uses the standard 1-e^(-speed*dt) formulation).
        /// </summary>
        public static float SmoothZoom(float current, float target, float smoothSpeed, float deltaTime)
        {
            var t = 1f - Mathf.Exp(-smoothSpeed * deltaTime);
            return Mathf.Lerp(current, target, t);
        }
    }
}

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
    }
}

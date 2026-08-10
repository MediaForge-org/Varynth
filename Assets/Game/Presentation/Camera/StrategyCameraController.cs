using UnityEngine;
using UnityEngine.InputSystem;

namespace Varynth.Presentation.Camera
{
    /// <summary>
    /// The one strategy-camera MonoBehaviour. Rig structure: this component sits on
    /// the CameraRigRoot, _yawPivot is its child, _camera is the yaw pivot's child at
    /// a fixed local back+up offset scaled by the current zoom distance. Reads the new
    /// Input System directly (device polling) -- deliberately not a .inputactions asset
    /// for this handful of raw axes; a documented prototype simplification. Presentation
    /// only -- no Simulation coupling.
    /// </summary>
    public sealed class StrategyCameraController : MonoBehaviour
    {
        [SerializeField] private CameraRigConfig _config = new CameraRigConfig();
        [SerializeField] private Transform _yawPivot;
        [SerializeField] private Transform _cameraTransform;

        private Vector2 _positionXZ;
        private float _yawDegrees;
        private float _zoomDistance;
        private float _zoomTarget;

        public CameraRigConfig Config => _config;

        private void Awake()
        {
            _positionXZ = new Vector2(transform.position.x, transform.position.z);
            _zoomDistance = _config.ZoomMinDistance + (_config.ZoomMaxDistance - _config.ZoomMinDistance) * 0.5f;
            _zoomTarget = _zoomDistance;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;

            if (keyboard != null)
            {
                ReadPan(keyboard, deltaTime);
                ReadRotation(keyboard, deltaTime);
            }

            if (mouse != null)
            {
                ReadZoomInput(mouse);
            }

            ApplyZoomSmoothing(deltaTime);
            ApplyTransforms();
        }

        private void ReadPan(Keyboard keyboard, float deltaTime)
        {
            var forward = 0f;
            var right = 0f;

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) forward += 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) forward -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) right += 1f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) right -= 1f;

            if (forward == 0f && right == 0f)
            {
                return;
            }

            var speed = _config.PanSpeed * (keyboard.leftShiftKey.isPressed ? _config.FastPanMultiplier : 1f);

            var yawRad = _yawDegrees * Mathf.Deg2Rad;
            var forwardDir = new Vector2(Mathf.Sin(yawRad), Mathf.Cos(yawRad));
            var rightDir = new Vector2(forwardDir.y, -forwardDir.x);

            var move = (forwardDir * forward + rightDir * right).normalized * speed * deltaTime;
            _positionXZ = CameraRigMath.ClampPosition(_positionXZ + move, _config);
        }

        private void ReadRotation(Keyboard keyboard, float deltaTime)
        {
            var rotate = 0f;
            if (keyboard.eKey.isPressed) rotate += 1f;
            if (keyboard.qKey.isPressed) rotate -= 1f;

            if (rotate == 0f)
            {
                return;
            }

            _yawDegrees = CameraRigMath.WrapYaw(_yawDegrees + rotate * _config.RotationSpeedDegreesPerSecond * deltaTime);
        }

        private void ReadZoomInput(Mouse mouse)
        {
            var scrollDelta = mouse.scroll.ReadValue().y;
            if (scrollDelta == 0f)
            {
                return;
            }

            // Scroll is an input delta, not a rate -- never multiplied by deltaTime here.
            // Applied directly (no extra hidden attenuation factor) so ZoomSensitivity in the
            // Inspector is the whole story; fast scrolling within one frame accumulates
            // naturally since scrollDelta already reflects everything read since last frame.
            _zoomTarget = CameraRigMath.ClampZoom(_zoomTarget - scrollDelta * _config.ZoomSensitivity, _config);
        }

        private void ApplyZoomSmoothing(float deltaTime)
        {
            var t = 1f - Mathf.Exp(-_config.ZoomSmoothSpeed * deltaTime);
            _zoomDistance = Mathf.Lerp(_zoomDistance, _zoomTarget, t);
        }

        private void ApplyTransforms()
        {
            transform.position = new Vector3(_positionXZ.x, transform.position.y, _positionXZ.y);

            if (_yawPivot != null)
            {
                _yawPivot.localRotation = Quaternion.Euler(0f, _yawDegrees, 0f);
            }

            if (_cameraTransform != null)
            {
                var pitchRad = _config.PitchDegrees * Mathf.Deg2Rad;
                var localOffset = new Vector3(0f, Mathf.Sin(pitchRad), -Mathf.Cos(pitchRad)) * _zoomDistance;
                _cameraTransform.localPosition = localOffset;
                _cameraTransform.localRotation = Quaternion.Euler(_config.PitchDegrees, 0f, 0f);
            }
        }
    }
}

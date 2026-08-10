using NUnit.Framework;
using UnityEngine;
using Varynth.Presentation.Camera;

namespace Varynth.Tests.EditMode.Presentation
{
    public class CameraRigMathTests
    {
        private static CameraRigConfig DefaultConfig()
        {
            return new CameraRigConfig
            {
                ZoomMinDistance = 15f,
                ZoomMaxDistance = 120f,
                BoundsMin = new Vector2(20f, 20f),
                BoundsMax = new Vector2(280f, 280f)
            };
        }

        [Test]
        public void ClampZoom_BelowMin_ClampsToMin()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampZoom(1f, config);

            Assert.AreEqual(config.ZoomMinDistance, result);
        }

        [Test]
        public void ClampZoom_AboveMax_ClampsToMax()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampZoom(999f, config);

            Assert.AreEqual(config.ZoomMaxDistance, result);
        }

        [Test]
        public void ClampZoom_WithinRange_Unchanged()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampZoom(50f, config);

            Assert.AreEqual(50f, result);
        }

        [Test]
        public void ClampPosition_OutsideBounds_ClampsToBounds()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampPosition(new Vector2(-100f, 5000f), config);

            Assert.AreEqual(config.BoundsMin.x, result.x);
            Assert.AreEqual(config.BoundsMax.y, result.y);
        }

        [Test]
        public void ClampPosition_WithinBounds_Unchanged()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampPosition(new Vector2(150f, 150f), config);

            Assert.AreEqual(new Vector2(150f, 150f), result);
        }

        [Test]
        public void WrapYaw_WithinRange_Unchanged()
        {
            Assert.AreEqual(90f, CameraRigMath.WrapYaw(90f), 1e-4f);
        }

        [Test]
        public void WrapYaw_Negative_WrapsIntoPositiveRange()
        {
            var result = CameraRigMath.WrapYaw(-30f);

            Assert.AreEqual(330f, result, 1e-4f);
        }

        [Test]
        public void WrapYaw_Above360_WrapsBackDown()
        {
            var result = CameraRigMath.WrapYaw(370f);

            Assert.AreEqual(10f, result, 1e-4f);
        }

        [Test]
        public void ClampZoom_ValidInput_NeverProducesNaNOrInfinity()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampZoom(50f, config);

            Assert.IsFalse(float.IsNaN(result));
            Assert.IsFalse(float.IsInfinity(result));
        }

        [Test]
        public void ClampPosition_ValidInput_NeverProducesNaNOrInfinity()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ClampPosition(new Vector2(150f, 150f), config);

            Assert.IsFalse(float.IsNaN(result.x) || float.IsNaN(result.y));
            Assert.IsFalse(float.IsInfinity(result.x) || float.IsInfinity(result.y));
        }
    }
}

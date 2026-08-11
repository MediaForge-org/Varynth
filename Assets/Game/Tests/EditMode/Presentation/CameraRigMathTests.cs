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

        // Mirrors the real 0.1.0 single-island scene builder's camera framing (small
        // world/zoom range) -- used alongside LargeArchipelagoConfig to prove the same
        // raw inputs stay meaningful regardless of world scale.
        private static CameraRigConfig SmallWorldConfig()
        {
            return new CameraRigConfig
            {
                ZoomMinDistance = 15f,
                ZoomMaxDistance = 120f,
                BoundsMin = new Vector2(20f, 20f),
                BoundsMax = new Vector2(280f, 280f)
            };
        }

        // Mirrors the real 0.1.1 archipelago scene builder's camera framing (large
        // world/zoom range, per WorldPrototypeSceneBuilder.ComputeCameraFraming output).
        private static CameraRigConfig LargeArchipelagoConfig()
        {
            return new CameraRigConfig
            {
                ZoomMinDistance = 15f,
                ZoomMaxDistance = 997f,
                BoundsMin = new Vector2(-170f, -170f),
                BoundsMax = new Vector2(700f, 580f)
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

        [Test]
        public void ComputeFitDistance_KnownRadiusAndFov_MatchesHandComputedValue()
        {
            // radius / sin(fov/2) = 100 / sin(30 deg) = 100 / 0.5 = 200.
            var result = CameraRigMath.ComputeFitDistance(100f, 60f, margin: 1f);

            Assert.AreEqual(200f, result, 1e-3f);
        }

        [Test]
        public void ComputeFitDistance_WithMargin_ScalesLinearly()
        {
            var withoutMargin = CameraRigMath.ComputeFitDistance(100f, 60f, margin: 1f);
            var withMargin = CameraRigMath.ComputeFitDistance(100f, 60f, margin: 1.15f);

            Assert.AreEqual(withoutMargin * 1.15f, withMargin, 1e-3f);
        }

        [Test]
        public void ComputeFitDistance_LargerRadius_RequiresLargerDistance()
        {
            var small = CameraRigMath.ComputeFitDistance(100f, 60f);
            var large = CameraRigMath.ComputeFitDistance(500f, 60f);

            Assert.Greater(large, small);
        }

        [Test]
        public void ComputeFitDistance_ValidInput_NeverProducesNaNOrInfinity()
        {
            var result = CameraRigMath.ComputeFitDistance(517.93f, 60f, 1.15f);

            Assert.IsFalse(float.IsNaN(result));
            Assert.IsFalse(float.IsInfinity(result));
        }

        // ---------------------------------------------------------------- Pan speed

        [Test]
        public void ComputePanSpeed_ScalesWithCurrentZoomDistance()
        {
            var config = LargeArchipelagoConfig();

            var speedZoomedIn = CameraRigMath.ComputePanSpeed(50f, config, fastModifierActive: false);
            var speedZoomedOut = CameraRigMath.ComputePanSpeed(950f, config, fastModifierActive: false);

            Assert.Greater(speedZoomedOut, speedZoomedIn,
                "Pan speed must increase as the camera zooms out, so panning a large world doesn't feel slow.");
            Assert.AreEqual(config.PanSpeedPerZoomDistance * 950f, speedZoomedOut, 1e-3f);
        }

        [Test]
        public void ComputePanSpeed_NeverBelowMinPanSpeed_EvenAtMinZoom()
        {
            var config = SmallWorldConfig();

            var speed = CameraRigMath.ComputePanSpeed(config.ZoomMinDistance, config, fastModifierActive: false);

            Assert.GreaterOrEqual(speed, config.MinPanSpeed,
                "Close-up panning must never crawl below the configured floor.");
        }

        [Test]
        public void ComputePanSpeed_FastModifier_MultipliesSpeed()
        {
            var config = SmallWorldConfig();

            var normal = CameraRigMath.ComputePanSpeed(100f, config, fastModifierActive: false);
            var fast = CameraRigMath.ComputePanSpeed(100f, config, fastModifierActive: true);

            Assert.AreEqual(normal * config.FastPanMultiplier, fast, 1e-3f);
        }

        [Test]
        public void ComputePanSpeed_HeldForFixedDuration_CoversMeaningfulFractionOfBounds_RegardlessOfWorldSize()
        {
            // Simulates "hold pan key for 2 seconds" via the same pure speed function the
            // controller uses each frame, for both a small (Phase 2A) and a large
            // (Phase 2B archipelago) world -- proving identical raw input (held duration)
            // stays meaningful instead of becoming practically ineffective in a big world.
            const float heldSeconds = 2f;

            foreach (var config in new[] { SmallWorldConfig(), LargeArchipelagoConfig() })
            {
                var zoomDistance = (config.ZoomMinDistance + config.ZoomMaxDistance) * 0.5f;
                var speed = CameraRigMath.ComputePanSpeed(zoomDistance, config, fastModifierActive: false);
                var distanceCovered = speed * heldSeconds;

                var boundsDiagonal = (config.BoundsMax - config.BoundsMin).magnitude;
                var fractionCovered = distanceCovered / boundsDiagonal;

                Assert.GreaterOrEqual(fractionCovered, 0.05f,
                    $"2 seconds of panning should cover at least 5% of the map diagonal ({boundsDiagonal:F0} units), " +
                    $"got {fractionCovered:P1} -- panning must not become practically ineffective as the world grows.");
            }
        }

        // ------------------------------------------------------------------- Zoom

        [Test]
        public void ComputeZoomTarget_PositiveScroll_ZoomsIn()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: 1f, config);

            Assert.Less(result, 60f);
        }

        [Test]
        public void ComputeZoomTarget_NegativeScroll_ZoomsOut()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: -1f, config);

            Assert.Greater(result, 60f);
        }

        [Test]
        public void ComputeZoomTarget_ZeroScroll_Unchanged()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: 0f, config);

            Assert.AreEqual(60f, result);
        }

        [Test]
        public void ComputeZoomTarget_RespectsClamp_EvenForHugeScrollDelta()
        {
            var config = DefaultConfig();

            var zoomedIn = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: 1000f, config);
            var zoomedOut = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: -1000f, config);

            Assert.AreEqual(config.ZoomMinDistance, zoomedIn, 1e-3f);
            Assert.AreEqual(config.ZoomMaxDistance, zoomedOut, 1e-3f);
        }

        [Test]
        public void ComputeZoomTarget_SingleNotch_ChangesByMeaningfulFraction_RegardlessOfWorldSize()
        {
            // A single simulated scroll notch (scrollDelta = 1, matching one physical
            // wheel click at Mouse.scroll.y's ~1-unit-per-notch scale) must produce a
            // clearly perceptible relative change in both a small and a large zoom
            // range -- this is the direct regression test for the reported "still too
            // slow after raising the old fixed-units sensitivity" symptom.
            foreach (var config in new[] { SmallWorldConfig(), LargeArchipelagoConfig() })
            {
                var start = (config.ZoomMinDistance + config.ZoomMaxDistance) * 0.5f;
                var result = CameraRigMath.ComputeZoomTarget(start, scrollDelta: 1f, config);
                var relativeChange = Mathf.Abs(start - result) / start;

                Assert.GreaterOrEqual(relativeChange, 0.1f,
                    $"One scroll notch should change distance by at least 10% (zoom range " +
                    $"{config.ZoomMinDistance}-{config.ZoomMaxDistance}), got {relativeChange:P1}.");
            }
        }

        [Test]
        public void ComputeZoomTarget_FewNotches_CanCrossFromOverviewToCloseZoom()
        {
            // "wenige deutliche Mausradbewegungen" from a full archipelago overview down
            // to the closest zoom must be achievable in a reasonably small notch count,
            // not the "dozens" previously reported.
            var config = LargeArchipelagoConfig();
            var distance = config.ZoomMaxDistance;

            for (var notch = 0; notch < 25 && distance > config.ZoomMinDistance * 1.5f; notch++)
            {
                distance = CameraRigMath.ComputeZoomTarget(distance, scrollDelta: 1f, config);
            }

            Assert.LessOrEqual(distance, config.ZoomMinDistance * 1.5f,
                "25 scroll notches should be enough to go from the full archipelago overview to near the closest zoom.");
        }

        [Test]
        public void ComputeZoomTarget_ValidInput_NeverProducesNaNOrInfinity()
        {
            var config = DefaultConfig();

            var result = CameraRigMath.ComputeZoomTarget(60f, scrollDelta: 3f, config);

            Assert.IsFalse(float.IsNaN(result));
            Assert.IsFalse(float.IsInfinity(result));
        }

        // --------------------------------------------------------------- Smoothing

        [Test]
        public void SmoothZoom_ApproachesTarget_OverRepeatedSteps()
        {
            var current = 60f;
            const float target = 100f;

            for (var i = 0; i < 60; i++)
            {
                current = CameraRigMath.SmoothZoom(current, target, smoothSpeed: 10f, deltaTime: 1f / 60f);
            }

            Assert.AreEqual(target, current, 0.5f);
        }

        [Test]
        public void SmoothZoom_ZeroDeltaTime_Unchanged()
        {
            var result = CameraRigMath.SmoothZoom(60f, 100f, smoothSpeed: 10f, deltaTime: 0f);

            Assert.AreEqual(60f, result, 1e-3f);
        }

        // ------------------------------------------------------------------------
        // Map-size-independent scaling law tests (Camera Navigation Scaling Rule,
        // .claude/rules/05-camera-navigation-scaling.md / ARCHITECTURE.md §9). These
        // must never hardcode the current 0.1.1 archipelago's concrete numbers as the
        // thing being verified -- they check the scaling LAW itself (same config, same
        // scroll/held-time input, larger current distance => larger absolute response),
        // so they keep passing unchanged for any future map size, including one whose
        // zoom/bounds range is orders of magnitude larger than today's prototype.
        // ------------------------------------------------------------------------

        // Represents a hypothetical future map with a zoom/bounds range orders of
        // magnitude larger than the current 0.1.1 archipelago (~1000 units) -- proves
        // the scaling formulas generalize rather than having been tuned to fit one map.
        private static CameraRigConfig FutureHugeMapConfig()
        {
            return new CameraRigConfig
            {
                ZoomMinDistance = 15f,
                ZoomMaxDistance = 500_000f,
                BoundsMin = new Vector2(-250_000f, -250_000f),
                BoundsMax = new Vector2(250_000f, 250_000f)
            };
        }

        private static readonly System.Func<CameraRigConfig>[] AllConfigsIncludingFutureHugeMap =
        {
            SmallWorldConfig, LargeArchipelagoConfig, FutureHugeMapConfig
        };

        [Test]
        public void ComputeZoomTarget_SameConfig_LargerCurrentDistance_ProducesLargerAbsoluteStep(
            [ValueSource(nameof(AllConfigsIncludingFutureHugeMap))] System.Func<CameraRigConfig> configFactory)
        {
            var config = configFactory();
            var nearDistance = config.ZoomMinDistance * 2f;
            var farDistance = config.ZoomMaxDistance * 0.5f;

            var nearStep = Mathf.Abs(nearDistance - CameraRigMath.ComputeZoomTarget(nearDistance, 1f, config));
            var farStep = Mathf.Abs(farDistance - CameraRigMath.ComputeZoomTarget(farDistance, 1f, config));

            Assert.Greater(farStep, nearStep,
                "The same single scroll notch must produce a larger absolute world-distance " +
                "change when the camera is currently far away than when it is currently close, " +
                "for any zoom range -- this is what makes zoom step a function of current " +
                "distance instead of a fixed constant.");
        }

        [Test]
        public void ComputeZoomTarget_NearMinZoom_StepStaysFine(
            [ValueSource(nameof(AllConfigsIncludingFutureHugeMap))] System.Func<CameraRigConfig> configFactory)
        {
            var config = configFactory();
            var nearDistance = config.ZoomMinDistance * 1.2f;

            var result = CameraRigMath.ComputeZoomTarget(nearDistance, 1f, config);
            var step = Mathf.Abs(nearDistance - result);

            Assert.Less(step, nearDistance,
                "Near the minimum zoom, a single notch must not overshoot the current distance " +
                "itself -- precise close-up control must survive regardless of map size.");
        }

        [Test]
        public void ComputeZoomTarget_FromFarZoom_NotchesNeededScaleLogarithmicallyNotLinearly(
            [ValueSource(nameof(AllConfigsIncludingFutureHugeMap))] System.Func<CameraRigConfig> configFactory)
        {
            // Notch count to cross a zoom range is inherently O(log(range)) for
            // multiplicative zoom, vs. O(range) for an old-style fixed-units-per-notch
            // scheme -- this is what lets "wenige deutliche Mausradbewegungen" hold for
            // both today's ~1000-unit archipelago and a future map orders of magnitude
            // larger, with no per-map retuning. This test derives its own expectation
            // from the formula (not a fixed cap like "25"), so it stays valid for any
            // configured zoom range, however large.
            var config = configFactory();
            var start = config.ZoomMaxDistance;
            var nearTarget = config.ZoomMinDistance * 1.5f;

            var expectedNotches = Mathf.CeilToInt(
                Mathf.Log(nearTarget / start) / Mathf.Log(1f - config.ZoomPercentPerNotch));

            var distance = start;
            var notches = 0;
            while (distance > nearTarget && notches < expectedNotches + 2)
            {
                distance = CameraRigMath.ComputeZoomTarget(distance, 1f, config);
                notches++;
            }

            Assert.LessOrEqual(distance, nearTarget,
                $"Expected to reach near-min zoom within {expectedNotches + 2} notches (formula-derived), " +
                $"but distance was still {distance:F1} after that many.");

            // An additive scheme at the old fixed ZoomSensitivity=8/notch would need this
            // many notches to cross the same range -- multiplicative zoom must need far fewer.
            var linearNotchesForComparison = (start - nearTarget) / 8f;
            Assert.Less(notches, linearNotchesForComparison,
                "Multiplicative zoom must require far fewer notches than an old-style " +
                "fixed-units-per-notch scheme would for the same range.");
        }

        [Test]
        public void ComputePanSpeed_SameConfig_FarZoom_CoversMoreDistanceThanNearZoom_OverFixedTime(
            [ValueSource(nameof(AllConfigsIncludingFutureHugeMap))] System.Func<CameraRigConfig> configFactory)
        {
            const float heldSeconds = 2f;
            var config = configFactory();

            var nearSpeed = CameraRigMath.ComputePanSpeed(config.ZoomMinDistance, config, fastModifierActive: false);
            var farSpeed = CameraRigMath.ComputePanSpeed(config.ZoomMaxDistance, config, fastModifierActive: false);

            Assert.Greater(farSpeed * heldSeconds, nearSpeed * heldSeconds,
                "The same fixed hold duration must cover more world distance when zoomed far " +
                "out than when zoomed in close, for any configured zoom range.");
        }

        [Test]
        public void ComputeZoomTarget_AndComputePanSpeed_RemainWithinConfiguredClamps(
            [ValueSource(nameof(AllConfigsIncludingFutureHugeMap))] System.Func<CameraRigConfig> configFactory)
        {
            var config = configFactory();

            var zoomResult = CameraRigMath.ComputeZoomTarget(config.ZoomMaxDistance, -50f, config);
            Assert.LessOrEqual(zoomResult, config.ZoomMaxDistance);
            Assert.GreaterOrEqual(zoomResult, config.ZoomMinDistance);

            var panResult = CameraRigMath.ClampPosition(config.BoundsMax + Vector2.one * 1000f, config);
            Assert.AreEqual(config.BoundsMax, panResult);
        }
    }
}

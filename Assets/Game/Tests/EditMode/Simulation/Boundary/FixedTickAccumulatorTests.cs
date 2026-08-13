using NUnit.Framework;
using Varynth.Core.Simulation.Boundary;

namespace Varynth.Tests.EditMode.Simulation.Boundary
{
    // Pure math tests (Phase 2E prompt section 42) -- no MonoBehaviour/scene needed,
    // mirrors the CameraRigMath precedent of testing extracted pure functions
    // directly rather than only through the driving MonoBehaviour.
    public class FixedTickAccumulatorTests
    {
        private static SimulationTickConfig Config(double ticksPerSecond = 20.0, int maxCatchUp = 10)
            => new SimulationTickConfig(ticksPerSecond, maxCatchUp);

        [Test]
        public void NoElapsedTime_ZeroTicksDue()
        {
            var due = FixedTickAccumulator.ComputeDueTicks(0.0, Config(), out var remaining);
            Assert.AreEqual(0, due);
            Assert.AreEqual(0.0, remaining, 1e-9);
        }

        [Test]
        public void ExactlyOneTickDuration_OneTickDue_NoRemainder()
        {
            var config = Config(20.0);
            var due = FixedTickAccumulator.ComputeDueTicks(config.TickDurationSeconds, config, out var remaining);
            Assert.AreEqual(1, due);
            Assert.AreEqual(0.0, remaining, 1e-9);
        }

        [Test]
        public void SeveralTickDurations_CorrectCountAndRemainder()
        {
            var config = Config(20.0); // 0.05s per tick
            var due = FixedTickAccumulator.ComputeDueTicks(0.12, config, out var remaining); // 2 full ticks + 0.02s left
            Assert.AreEqual(2, due);
            Assert.AreEqual(0.02, remaining, 1e-9);
        }

        [Test]
        public void RemainderPersists_AcrossSubsequentCalls()
        {
            var config = Config(20.0);
            var due1 = FixedTickAccumulator.ComputeDueTicks(0.03, config, out var remaining1); // < one tick
            Assert.AreEqual(0, due1);
            Assert.Greater(remaining1, 0.0);

            // Simulate the caller feeding the remainder back in plus a new frame's delta.
            var accumulated2 = remaining1 + 0.03;
            var due2 = FixedTickAccumulator.ComputeDueTicks(accumulated2, config, out _);
            Assert.AreEqual(1, due2, "The persisted remainder plus the next frame's delta should cross the tick threshold.");
        }

        [Test]
        public void CatchUpLimit_ClampsTickCount_AndDiscardsExcessAccumulatedTime()
        {
            var config = Config(20.0, maxCatchUp: 10); // would naively want 1000 ticks for 50s
            var due = FixedTickAccumulator.ComputeDueTicks(50.0, config, out var remaining);

            Assert.AreEqual(10, due, "Due ticks must be clamped to MaxCatchUpTicksPerFrame.");
            Assert.AreEqual(0.0, remaining, 1e-9, "Excess accumulated time beyond the clamp must be discarded, not carried forward -- the spiral-of-death guard.");
        }

        [Test]
        public void CatchUpLimit_DoesNotCauseUnboundedBacklogGrowth_OverRepeatedOverloadedFrames()
        {
            var config = Config(20.0, maxCatchUp: 5);
            double accumulated = 0.0;
            var totalDue = 0;

            // Simulate 20 consecutive massively-overloaded frames (1 second of real
            // time each, far more than the tick rate could ever keep up with).
            for (var frame = 0; frame < 20; frame++)
            {
                accumulated += 1.0;
                var due = FixedTickAccumulator.ComputeDueTicks(accumulated, config, out accumulated);
                totalDue += due;
                Assert.AreEqual(5, due, "Every overloaded frame should clamp to exactly MaxCatchUpTicksPerFrame.");
                Assert.LessOrEqual(accumulated, config.TickDurationSeconds * 1.0001, "Accumulated backlog must never grow beyond a single tick's worth once clamped.");
            }

            Assert.AreEqual(100, totalDue);
        }

        [Test]
        public void InterpolationAlpha_StaysWithinZeroToOne()
        {
            var config = Config(20.0);
            Assert.AreEqual(0f, FixedTickAccumulator.ComputeInterpolationAlpha(0.0, config), 1e-6f);
            Assert.AreEqual(0.5f, FixedTickAccumulator.ComputeInterpolationAlpha(config.TickDurationSeconds * 0.5, config), 1e-5f);
            Assert.AreEqual(1f, FixedTickAccumulator.ComputeInterpolationAlpha(config.TickDurationSeconds * 5.0, config), 1e-6f, "Must clamp to 1, never exceed it.");
        }
    }
}

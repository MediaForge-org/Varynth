using System;
using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Diagnostics;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Context;
using Varynth.Core.Simulation.Scheduling;

namespace Varynth.Tests.EditMode.Simulation
{
    public class SimulationSchedulerTests
    {
        [Test]
        public void Register_Null_Throws()
        {
            var scheduler = new SimulationScheduler();
            Assert.Throws<ArgumentNullException>(() => scheduler.Register(null));
        }

        [Test]
        public void Register_DuplicateId_ThrowsAndKeepsOriginal()
        {
            var scheduler = new SimulationScheduler();
            var a = new TestSimulationSystem("population", 0, SimulationLevelMask.All);
            var b = new TestSimulationSystem("population", 1, SimulationLevelMask.All);

            scheduler.Register(a);

            Assert.Throws<DuplicateSimulationSystemException>(() => scheduler.Register(b));
            Assert.AreEqual(1, scheduler.Systems.Count);
        }

        [Test]
        public void Register_NoneSupportedLevelsMask_Throws()
        {
            var scheduler = new SimulationScheduler();
            var system = new TestSimulationSystem("population", 0, SimulationLevelMask.None);

            Assert.Throws<ArgumentException>(() => scheduler.Register(system));
        }

        [Test]
        public void Register_UndefinedMaskBits_Throws()
        {
            var scheduler = new SimulationScheduler();
            var garbage = (SimulationLevelMask)(1 << 10);
            var system = new TestSimulationSystem("population", 0, garbage);

            Assert.Throws<ArgumentException>(() => scheduler.Register(system));
        }

        [Test]
        public void Systems_AreOrderedByOrderThenId()
        {
            var scheduler = new SimulationScheduler();
            var c = new TestSimulationSystem("c.system", 5, SimulationLevelMask.All);
            var a = new TestSimulationSystem("a.system", 5, SimulationLevelMask.All);
            var b = new TestSimulationSystem("b.system", 1, SimulationLevelMask.All);

            scheduler.Register(c);
            scheduler.Register(a);
            scheduler.Register(b);

            var ids = new List<string>();
            foreach (var s in scheduler.Systems)
            {
                ids.Add(s.Id.ToString());
            }

            CollectionAssert.AreEqual(new[] { "b.system", "a.system", "c.system" }, ids);
        }

        [Test]
        public void RunTick_CallsEachSupportedSystemExactlyOnce()
        {
            var scheduler = new SimulationScheduler();
            var callCount = 0;
            var system = new TestSimulationSystem("population", 0, SimulationLevelMask.All, _ => callCount++);
            scheduler.Register(system);

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear));

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void RunTick_MultipleTicks_CallsSystemEachTime()
        {
            var scheduler = new SimulationScheduler();
            var callCount = 0;
            var system = new TestSimulationSystem("population", 0, SimulationLevelMask.All, _ => callCount++);
            scheduler.Register(system);

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear));
            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(2), SimulationLevel.ActiveNear));
            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(3), SimulationLevel.ActiveNear));

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void RunTick_PassesRequestedTickToSystem()
        {
            var scheduler = new SimulationScheduler();
            var observed = GameTick.Zero;
            var system = new TestSimulationSystem("population", 0, SimulationLevelMask.All, ctx => observed = ctx.Tick);
            scheduler.Register(system);

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(7), SimulationLevel.ActiveNear));

            Assert.AreEqual(GameTick.FromRaw(7), observed);
        }

        [Test]
        public void RunTick_SkipsSystemNotSupportingRequestedLevel()
        {
            var scheduler = new SimulationScheduler();
            var called = false;
            var system = new TestSimulationSystem("background.only", 0, SimulationLevelMask.Background, _ => called = true);
            scheduler.Register(system);

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear));

            Assert.IsFalse(called);
        }

        [Test]
        public void RunTick_FaultingSystem_IsLoggedAndExceptionPropagates()
        {
            var logger = new CollectingLogger();
            var scheduler = new SimulationScheduler(logger);
            var system = new TestSimulationSystem("faulty", 0, SimulationLevelMask.All, _ => throw new InvalidOperationException("boom"));
            scheduler.Register(system);

            var ex = Assert.Throws<SimulationSystemException>(
                () => scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear)));

            Assert.AreEqual(system.Id, ex.SystemId);
            Assert.IsInstanceOf<InvalidOperationException>(ex.InnerException);

            Assert.AreEqual(1, logger.Entries.Count);
            Assert.AreEqual(LogSeverity.Error, logger.Entries[0].Severity);
        }

        [Test]
        public void RunTick_FaultingSystem_SubsequentSystemDoesNotRun()
        {
            var scheduler = new SimulationScheduler();
            var faulty = new TestSimulationSystem("a.faulty", 0, SimulationLevelMask.All, _ => throw new InvalidOperationException("boom"));
            var laterCalled = false;
            var later = new TestSimulationSystem("b.later", 1, SimulationLevelMask.All, _ => laterCalled = true);
            scheduler.Register(faulty);
            scheduler.Register(later);

            Assert.Throws<SimulationSystemException>(
                () => scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear)));

            Assert.IsFalse(laterCalled);
        }

        [Test]
        public void RunTick_FaultingSystem_WithDefaultNullLogger_StillPropagates()
        {
            var scheduler = new SimulationScheduler();
            var system = new TestSimulationSystem("faulty", 0, SimulationLevelMask.All, _ => throw new InvalidOperationException("boom"));
            scheduler.Register(system);

            Assert.Throws<SimulationSystemException>(
                () => scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear)));
        }

        [Test]
        public void Determinism_SameSystemSet_DifferentRegistrationOrder_ProducesSameCallOrder()
        {
            var callOrder1 = RunAndRecordOrder(registerAscending: true);
            var callOrder2 = RunAndRecordOrder(registerAscending: false);

            CollectionAssert.AreEqual(callOrder1, callOrder2);
        }

        [Test]
        public void Determinism_IdenticalTickSequence_ProducesIdenticalCallOrder()
        {
            var scheduler = new SimulationScheduler();
            var order = new List<string>();
            scheduler.Register(new TestSimulationSystem("a.system", 0, SimulationLevelMask.All, _ => order.Add("a.system")));
            scheduler.Register(new TestSimulationSystem("b.system", 1, SimulationLevelMask.All, _ => order.Add("b.system")));

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear));
            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(2), SimulationLevel.ActiveNear));

            CollectionAssert.AreEqual(
                new[] { "a.system", "b.system", "a.system", "b.system" },
                order);
        }

        private static List<string> RunAndRecordOrder(bool registerAscending)
        {
            var order = new List<string>();
            var scheduler = new SimulationScheduler();

            var a = new TestSimulationSystem("a.system", 0, SimulationLevelMask.All, _ => order.Add("a.system"));
            var b = new TestSimulationSystem("b.system", 0, SimulationLevelMask.All, _ => order.Add("b.system"));
            var c = new TestSimulationSystem("c.system", 0, SimulationLevelMask.All, _ => order.Add("c.system"));

            if (registerAscending)
            {
                scheduler.Register(a);
                scheduler.Register(b);
                scheduler.Register(c);
            }
            else
            {
                scheduler.Register(c);
                scheduler.Register(b);
                scheduler.Register(a);
            }

            scheduler.RunTick(new SimulationContext(GameTick.FromRaw(1), SimulationLevel.ActiveNear));
            return order;
        }
    }
}

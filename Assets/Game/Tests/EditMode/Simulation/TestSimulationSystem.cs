using System;
using Varynth.Core.Simulation.Context;
using Varynth.Core.Simulation.Scheduling;

namespace Varynth.Tests.EditMode.Simulation
{
    internal sealed class TestSimulationSystem : ISimulationSystem
    {
        private readonly Action<SimulationContext> _onTick;

        public SimulationSystemId Id { get; }
        public int Order { get; }
        public SimulationLevelMask SupportedLevels { get; }

        public TestSimulationSystem(string id, int order, SimulationLevelMask supportedLevels, Action<SimulationContext> onTick = null)
        {
            Id = SimulationSystemId.Parse(id);
            Order = order;
            SupportedLevels = supportedLevels;
            _onTick = onTick;
        }

        public void Tick(SimulationContext context)
        {
            _onTick?.Invoke(context);
        }
    }
}

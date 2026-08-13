using System;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Handle returned by ISimulation.Submit(), assigned by an incrementing counter
    /// at the instant Submit() is called -- never a GameTick (multiple commands can
    /// be queued before a tick runs) and never derived from Dictionary/collection
    /// iteration order. Lets a caller later correlate a submitted command with its
    /// SimulationCommandResult/BuildingCommandResult/RoadCommandResult.
    /// </summary>
    public readonly struct SimulationCommandTicket : IEquatable<SimulationCommandTicket>
    {
        public ulong Value { get; }

        public SimulationCommandTicket(ulong value)
        {
            Value = value;
        }

        public bool Equals(SimulationCommandTicket other) => Value == other.Value;

        public override bool Equals(object obj) => obj is SimulationCommandTicket other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => $"Ticket({Value})";

        public static bool operator ==(SimulationCommandTicket left, SimulationCommandTicket right) => left.Equals(right);

        public static bool operator !=(SimulationCommandTicket left, SimulationCommandTicket right) => !left.Equals(right);
    }
}

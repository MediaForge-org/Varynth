using System;

namespace Varynth.Core.Simulation.Common
{
    /// <summary>
    /// Platform-independent player/owner identity. Deliberately backed by System.Guid,
    /// not a SteamID/Epic Account ID -- must remain valid in full offline singleplayer,
    /// per the standing "base game is always offline-playable" rule (see
    /// ARCHITECTURE.md, "Future Multiplayer / Optional Co-op DLC"). Not wired into any
    /// gameplay/ownership state in Phase 1C -- its existence is what keeps future
    /// ownership modeling (OwnerId fields on buildings/ships/islands) possible without
    /// a rewrite.
    ///
    /// IMPORTANT: NewId() is for profile/player-initialization time use only (e.g.
    /// creating a new local player identity). It must NEVER be called from inside a
    /// deterministic simulation tick to generate gameplay state -- a freshly-random
    /// Guid would be a hidden, uncontrolled, per-machine-divergent input into the
    /// simulation, undermining determinism and, later, host/client agreement in co-op.
    /// </summary>
    public readonly struct PlayerId : IEquatable<PlayerId>
    {
        public static readonly PlayerId None = default;

        private readonly Guid _value;
        private readonly bool _hasValue;

        private PlayerId(Guid value)
        {
            _value = value;
            _hasValue = true;
        }

        public bool IsNone => !_hasValue;

        /// <summary>
        /// Profile/player-initialization time only -- see the type-level warning.
        /// Never call from within a simulation tick.
        /// </summary>
        public static PlayerId NewId()
        {
            return new PlayerId(Guid.NewGuid());
        }

        public static PlayerId FromGuid(Guid value)
        {
            return new PlayerId(value);
        }

        public override string ToString()
        {
            return _hasValue ? _value.ToString() : "none";
        }

        public bool Equals(PlayerId other)
        {
            return _hasValue == other._hasValue && (!_hasValue || _value.Equals(other._value));
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _hasValue ? _value.GetHashCode() : 0;
        }

        public static bool operator ==(PlayerId left, PlayerId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerId left, PlayerId right)
        {
            return !left.Equals(right);
        }
    }
}

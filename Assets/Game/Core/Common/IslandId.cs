using System;
using System.Text;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Stable island identity for simulation/render-snapshot purposes (Phase 2E).
    /// Deliberately NOT tied to "position in the Terrain[]/config array" -- an
    /// internal array index still exists for O(1) lookups inside ManagedSimulation,
    /// but the gameplay-facing identity is this value, derived once (FromName) from
    /// the island's stable content-defined name via a deterministic FNV-1a string
    /// hash -- the same "own deterministic hash, never UnityEngine.Random or raw
    /// object.GetHashCode()" convention already used by IslandHeightmapGenerator's
    /// Hash()/WorldPrototypeSceneBuilder's DeterministicNoise(). A later real world
    /// generator can assign stable IDs by its own means; this is the prototype rule.
    /// </summary>
    public readonly struct IslandId : IEquatable<IslandId>
    {
        public static readonly IslandId None = default;

        public ulong Value { get; }

        private IslandId(ulong value)
        {
            Value = value;
        }

        public static IslandId FromRaw(ulong value) => new IslandId(value);

        public static IslandId FromName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Island name must not be null/empty.", nameof(name));
            }

            // FNV-1a, 64-bit. Deterministic across runs/machines/.NET versions --
            // unlike string.GetHashCode(), which is explicitly randomized per-process
            // in modern .NET and must never be used for stable/deterministic identity.
            const ulong offsetBasis = 14695981039346656037UL;
            const ulong prime = 1099511628211UL;

            var hash = offsetBasis;
            var bytes = Encoding.UTF8.GetBytes(name);
            foreach (var b in bytes)
            {
                hash ^= b;
                hash *= prime;
            }

            return new IslandId(hash);
        }

        public bool IsNone => Value == 0UL;

        public bool Equals(IslandId other) => Value == other.Value;

        public override bool Equals(object obj) => obj is IslandId other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => $"IslandId({Value:X16})";

        public static bool operator ==(IslandId left, IslandId right) => left.Equals(right);

        public static bool operator !=(IslandId left, IslandId right) => !left.Equals(right);
    }
}

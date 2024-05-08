using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace YEcs
{
    internal readonly struct ArchetypeHashValue : IEquatable<ArchetypeHashValue>
    {
        private readonly int? _hashCode;

        public ArchetypeHashValue(int? hashCode)
        {
            _hashCode = hashCode;
        }

        public bool Equals(ArchetypeHashValue other)
        {
            return _hashCode == other._hashCode;
        }

        public override int GetHashCode()
        {
            return _hashCode ?? 0;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ArchetypeHashValue other && Equals(other);
        }
    }

    public struct Archetype : IEquatable<Archetype>
    {
        private readonly IReadOnlyCollection<int> _componentTypeIds;
        private ArchetypeHashValue? _hashValue;

        private ArchetypeHashValue HashValue
        {
            get
            {
                _hashValue ??= CalculateHash();

                return _hashValue.Value;
            }
        }

        public Archetype(IReadOnlyCollection<int> componentTypeIds)
        {
            _hashValue = null;
            _componentTypeIds = componentTypeIds ?? throw new ArgumentNullException(nameof(componentTypeIds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ArchetypeHashValue CalculateHash()
        {
            if (_componentTypeIds == null || _componentTypeIds.Count == 0)
                return new ArchetypeHashValue(null);

            var hashCode = new HashCode();
            foreach (var componentTypeId in _componentTypeIds)
            {
                hashCode.Add(componentTypeId);
            }

            return new ArchetypeHashValue(hashCode.ToHashCode());
        }

        public bool Equals(Archetype other)
        {
            return HashValue.Equals(other.HashValue);
        }

        public override int GetHashCode()
        {
            return HashValue.GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Archetype other && Equals(other);
        }

        public bool IsCompatible(int componentTypeId)
        {
            return _componentTypeIds.Contains(componentTypeId);
        }
    }
}

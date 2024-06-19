using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using YEcs.Interface;

namespace YEcs
{
    internal readonly struct ArchetypeHashValue : IEquatable<ArchetypeHashValue>
    {
        private readonly int? _hashCode;

        public ArchetypeHashValue(int? hashCode)
        {
            _hashCode = hashCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ArchetypeHashValue other)
        {
            return _hashCode == other._hashCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _hashCode ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ArchetypeHashValue other && Equals(other);
        }
    }

    public struct Archetype : IArchetype, IEquatable<Archetype>
    {
        private readonly IReadOnlyCollection<Type> _componentTypes;
        private ArchetypeHashValue? _hashValue;

        private ArchetypeHashValue HashValue
        {
            get
            {
                _hashValue ??= CalculateHash();

                return _hashValue.Value;
            }
        }

        public static Archetype Empty => new (Array.Empty<Type>());

        public Archetype(IReadOnlyCollection<Type> componentTypes)
        {
            _hashValue = null;
            _componentTypes = componentTypes ?? throw new ArgumentNullException(nameof(componentTypes));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ArchetypeHashValue CalculateHash()
        {
            if (_componentTypes == null || _componentTypes.Count == 0)
                return new ArchetypeHashValue(null);

            var hashCode = new HashCode();
            foreach (var componentType in _componentTypes)
            {
                hashCode.Add(componentType);
            }

            return new ArchetypeHashValue(hashCode.ToHashCode());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Archetype other)
        {
            return HashValue.Equals(other.HashValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashValue.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals<TArchetype>(TArchetype other) where TArchetype : IArchetype
        {
            if (other is not Archetype typedOther)
                return false;

            return HashValue.Equals(typedOther.HashValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Archetype other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsCompatible(Type componentType)
        {
            return _componentTypes.Contains(componentType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Archetype Subtract(Type componentType)
        {
            var newComponentTypes = new List<Type>();
            foreach (var selfComponentType in _componentTypes)
            {
                if (selfComponentType == componentType)
                    continue;
                
                newComponentTypes.Add(selfComponentType);
            }

            return new Archetype(newComponentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Archetype Add(Type componentType)
        {
            var newComponentTypes = new List<Type>() { componentType };
            newComponentTypes.AddRange(_componentTypes);

            return new Archetype(newComponentTypes);
        }
    }
}

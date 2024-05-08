using System.Diagnostics.CodeAnalysis;

namespace YEcs
{
    internal readonly struct ArchetypeRestriction
    {
        private readonly int[] _withComponentTypeIds;
        private readonly int[] _exceptComponentTypeIds;

        public ArchetypeRestriction(int[] withComponentTypeIds, int[] exceptComponentTypeIds)
        {
            _withComponentTypeIds = withComponentTypeIds;
            _exceptComponentTypeIds = exceptComponentTypeIds;
        }

        public override int GetHashCode()
        {
            if (_withComponentTypeIds.Length > 0 && _exceptComponentTypeIds.Length > 0)
            {
                return HashCode
                    .Combine(
                        HashCodeExtensions.Create(_withComponentTypeIds).ToHashCode(),
                        HashCodeExtensions.Create(_exceptComponentTypeIds).ToHashCode());
            }

            if (_withComponentTypeIds.Length > 0)
                return HashCodeExtensions.Create(_withComponentTypeIds).ToHashCode();

            if (_exceptComponentTypeIds.Length > 0)
                return HashCodeExtensions.Create(_exceptComponentTypeIds).ToHashCode();
            
            return 0;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not ArchetypeRestriction other)
                return false;

            if (_withComponentTypeIds.Length != other._withComponentTypeIds.Length)
                return false;

            if (_exceptComponentTypeIds.Length != other._exceptComponentTypeIds.Length)
                return false;

            for (int i = 0; i < _withComponentTypeIds.Length; i++)
            {
                if (_withComponentTypeIds[i] != other._withComponentTypeIds[i])
                    return false;
            }

            for (int i = 0; i < _exceptComponentTypeIds.Length; i++)
            {
                if (_exceptComponentTypeIds[i] != other._exceptComponentTypeIds[i])
                    return false;
            }

            return true;
        }

        public bool IsCompatible(Archetype archetype)
        {
            for (var i = 0; i < _withComponentTypeIds.Length; i++)
            {
                if (!archetype.IsCompatible(_withComponentTypeIds[i]))
                    return false;
            }

            for (var i = 0; i < _exceptComponentTypeIds.Length; i++)
            {
                if (archetype.IsCompatible(_exceptComponentTypeIds[i]))
                    return false;
            }

            return true;
        }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace YEcs
{
    internal readonly struct ComponentTypeIdsKey
    {
        private readonly int[] _withComponentTypeIds;
        private readonly int[] _exceptComponentTypeIds;

        public ComponentTypeIdsKey(int[] withComponentTypeIds, int[] exceptComponentTypeIds)
        {
            _withComponentTypeIds = withComponentTypeIds;
            _exceptComponentTypeIds = exceptComponentTypeIds;
        }

        public override int GetHashCode()
        {
            var withHashCode = new HashCode();
            withHashCode.AddValues(_withComponentTypeIds);

            var exceptHashCode = new HashCode();
            exceptHashCode.AddValues(_exceptComponentTypeIds);

            if (_withComponentTypeIds.Length > 0 && _exceptComponentTypeIds.Length > 0)
                return HashCode.Combine(withHashCode.ToHashCode(), exceptHashCode.ToHashCode());

            if (_withComponentTypeIds.Length > 0)
                return withHashCode.ToHashCode();

            return exceptHashCode.ToHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is ComponentTypeIdsKey other))
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
    }
}

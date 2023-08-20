using System.Runtime.CompilerServices;

namespace YEcs
{
    internal static class ComponentTypesStorage
    {
        private static int _nextId;
        private static readonly Dictionary<Type, int> _identifierMap;

        static ComponentTypesStorage()
        {
            _identifierMap = new Dictionary<Type, int>();
            _nextId = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetId<ComponentType>() where ComponentType : struct
        {
            return GetId(typeof(ComponentType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetId(Type componentType)
        {
            if (_identifierMap.TryGetValue(componentType, out var id))
                return id;

            _identifierMap.Add(componentType, _nextId);

            return _nextId++;
        }
    }
}

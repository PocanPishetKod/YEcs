using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace YEcs
{
    internal readonly struct ComponentTypeIdsKey
    {
        private readonly int[] ComponentTypeIds;

        public ComponentTypeIdsKey(int[] componentTypeIds)
        {
            ComponentTypeIds = componentTypeIds;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            foreach (var componentTypeId in ComponentTypeIds)
            {
                hashCode.Add(componentTypeId);
            }

            return hashCode.ToHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is ComponentTypeIdsKey other))
                return false;

            if (ComponentTypeIds.Length != other.ComponentTypeIds.Length)
                return false;

            for (int i = 0; i < ComponentTypeIds.Length; i++)
            {
                if (ComponentTypeIds[i] != other.ComponentTypeIds[i])
                    return false;
            }

            return true;
        }
    }

    public class EntityFiltersManager
    {
        private readonly Dictionary<ComponentTypeIdsKey, EntityFilter> _entityFiltersMap;
        private readonly EntityStorage _entityStorage;

        internal EntityFiltersManager(EntityStorage entityStorage)
        {
            _entityFiltersMap = new Dictionary<ComponentTypeIdsKey, EntityFilter>();
            _entityStorage = entityStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillFilter(EntityFilter filter)
        {
            for (int i = 0; i < _entityStorage.Count; i++)
            {
                ref var entity = ref _entityStorage[i];
                if (filter.IsCompatible(entity))
                    continue;

                filter.AddEntity(entity.Index);
            }
        }

        public EntityFilter GetFilter(params Type[] componentTypes)
        {
            var componentTypeIds = componentTypes.Select(x => ComponentTypesStorage.GetId(x)).ToArray();
            var key = new ComponentTypeIdsKey(componentTypeIds);
            if (!_entityFiltersMap.TryGetValue(key, out var filter))
            {
                filter = new EntityFilter(Array.AsReadOnly(componentTypeIds), _entityStorage);
                _entityFiltersMap.Add(key, filter);

                FillFilter(filter);
            }

            return filter;
        }
    }
}

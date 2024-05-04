using System.Runtime.CompilerServices;

namespace YEcs
{
    public class FilterBuilder
    {
        private readonly List<Type> _withTypes;
        private readonly List<Type> _exceptTypes;
        private readonly EntityStorage _entityStorage;
        private readonly IDictionary<ComponentTypeIdsKey, EntityFilter> _entityFiltersStorage;

        internal FilterBuilder(IDictionary<ComponentTypeIdsKey, EntityFilter> entityFiltersStorage, EntityStorage entityStorage)
        {
            _entityFiltersStorage = entityFiltersStorage;
            _entityStorage = entityStorage;
            _withTypes = new List<Type>();
            _exceptTypes = new List<Type>();
        }

        public FilterBuilder With<TComponent>() where TComponent : struct
        {
            _withTypes.Add(typeof(TComponent));
            return this;
        }

        public FilterBuilder Except<TComponent>() where TComponent : struct
        {
            _exceptTypes.Add(typeof(TComponent));
            return this;
        }

        public EntityFilter Build()
        {
            var key = new ComponentTypeIdsKey(
                _withTypes.Select(ComponentTypesStorage.GetId).ToArray(),
                _exceptTypes.Select(ComponentTypesStorage.GetId).ToArray());

            if (!_entityFiltersStorage.TryGetValue(key, out var filter))
            {
                filter = new EntityFilter(
                    _withTypes.Select(ComponentTypesStorage.GetId).ToList(),
                    _exceptTypes.Select(ComponentTypesStorage.GetId).ToList(),
                    _entityStorage);

                _entityFiltersStorage.Add(key, filter);

                FillFilter(filter);
            }

            return filter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillFilter(EntityFilter filter)
        {
            for (var i = 0; i < _entityStorage.Count; i++)
            {
                ref var entity = ref _entityStorage[i];
                if (!filter.IsCompatible(entity))
                    continue;

                filter.AddEntity(entity.Index);
            }
        }
    }
}

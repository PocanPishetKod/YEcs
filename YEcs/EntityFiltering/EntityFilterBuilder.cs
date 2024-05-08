using System.Runtime.CompilerServices;

namespace YEcs
{
    public class EntityFilterBuilder
    {
        private readonly List<Type> _withTypes;
        private readonly List<Type> _exceptTypes;
        private readonly EntityStorage _entityStorage;
        private readonly IDictionary<ArchetypeRestriction, EntityFilter> _entityFiltersStorage;

        internal EntityFilterBuilder(IDictionary<ArchetypeRestriction, EntityFilter> entityFiltersStorage, EntityStorage entityStorage)
        {
            _entityFiltersStorage = entityFiltersStorage;
            _entityStorage = entityStorage;
            _withTypes = new List<Type>();
            _exceptTypes = new List<Type>();
        }

        public EntityFilterBuilder With<TComponent>() where TComponent : struct
        {
            _withTypes.Add(typeof(TComponent));
            return this;
        }

        public EntityFilterBuilder Except<TComponent>() where TComponent : struct
        {
            _exceptTypes.Add(typeof(TComponent));
            return this;
        }

        public EntityFilter Build()
        {
            var restriction = new ArchetypeRestriction(
                _withTypes.Select(ComponentTypesStorage.GetId).ToArray(),
                _exceptTypes.Select(ComponentTypesStorage.GetId).ToArray());

            if (_entityFiltersStorage.TryGetValue(restriction, out var filter))
                return filter;
            
            filter = new EntityFilter(restriction, _entityStorage);
            _entityFiltersStorage.Add(restriction, filter);
            FillFilter(filter);

            return filter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillFilter(EntityFilter filter)
        {
            for (var i = 0; i < _entityStorage.Count; i++)
            {
                ref var entity = ref _entityStorage[i];
                if (!filter.IsCompatible(ref entity))
                    continue;

                filter.AddEntity(entity.Index);
            }
        }
    }
}

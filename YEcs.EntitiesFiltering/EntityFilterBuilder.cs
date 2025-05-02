using System.Runtime.CompilerServices;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Storaging;

namespace YEcs.EntitiesFiltering;

public class EntityFilterBuilder : IEntityFilterBuilder
{
    private readonly HashSet<ComponentTypeId> _withTypes;
    private readonly HashSet<ComponentTypeId> _exceptTypes;
    private readonly IEntitiesStorage _entitiesStorage;
    private readonly IEntityFiltersStorage _entityFiltersStorage;
    private readonly IComponentTypeIdProvider _componentTypeProvider;
    private readonly IArchetypesStorage _archetypesStorage;

    internal EntityFilterBuilder(IEntityFiltersStorage entityFiltersStorage,
        IEntitiesStorage entitiesStorage,
        IComponentTypeIdProvider componentTypeProvider,
        IArchetypesStorage archetypesStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
        _entitiesStorage = entitiesStorage;
        _componentTypeProvider = componentTypeProvider;
        _archetypesStorage = archetypesStorage;
        _withTypes = new HashSet<ComponentTypeId>(2);
        _exceptTypes = new HashSet<ComponentTypeId>(1);
    }

    public IEntityFilterBuilder With<TComponent>() where TComponent : struct
    {
        _withTypes.Add(_componentTypeProvider.Get<TComponent>());
        return this;
    }

    public IEntityFilterBuilder Except<TComponent>() where TComponent : struct
    {
        _exceptTypes.Add(_componentTypeProvider.Get<TComponent>());
        return this;
    }

    public IReadOnlyEntityFilter Build()
    {
        var mask = new ArchetypeMask(_withTypes, _exceptTypes);

        if (_entityFiltersStorage.TryGet(mask, out var filter))
            return filter!;

        filter = new EntityFilter(mask, _entitiesStorage);
        _entityFiltersStorage.Add(mask, filter);
        FillFilter(filter);

        return filter;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FillFilter(EntityFilter filter)
    {
        for (var i = 0; i < _entitiesStorage.Count; i++)
        {
            ref var entity = ref _entitiesStorage[i];
            if (!filter.IsCompatible(_archetypesStorage.Get(entity.Index)))
                continue;

            filter.AddEntity(entity.Index);
        }
    }
}

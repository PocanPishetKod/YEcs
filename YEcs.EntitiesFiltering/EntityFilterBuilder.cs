using System.Runtime.CompilerServices;
using YEcs.Interface;
using YEcs.Interfaces.Storaging;

namespace YEcs.EntitiesFiltering;

public class EntityFilterBuilder : IEntityFilterBuilder<Entity, Archetype>
{
    private readonly List<Type> _withTypes;
    private readonly List<Type> _exceptTypes;
    private readonly IEntitiesStorage _entitiesStorage;
    private readonly IEntityFiltersStorage _entityFiltersStorage;

    internal EntityFilterBuilder(IEntityFiltersStorage entityFiltersStorage, IEntitiesStorage entitiesStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
        _entitiesStorage = entitiesStorage;
        _withTypes = new List<Type>();
        _exceptTypes = new List<Type>();
    }

    public IEntityFilterBuilder<Entity, Archetype> With<TComponent>() where TComponent : struct, IReusable
    {
        _withTypes.Add(typeof(TComponent));
        return this;
    }

    public IEntityFilterBuilder<Entity, Archetype> Except<TComponent>() where TComponent : struct, IReusable
    {
        _exceptTypes.Add(typeof(TComponent));
        return this;
    }

    public IEntityFilter<Entity, Archetype> Build()
    {
        var restriction = new ArchetypeMask(_withTypes, _exceptTypes);

        if (_entityFiltersStorage.TryGet(restriction, out var filter))
            return filter!;

        filter = new EntityFilter(restriction, _entitiesStorage);
        _entityFiltersStorage.Add(restriction, filter);
        FillFilter(filter);

        return filter;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FillFilter(EntityFilter filter)
    {
        for (var i = 0; i < _entitiesStorage.Count; i++)
        {
            ref var entity = ref _entitiesStorage[i];
            if (!filter.IsCompatible(ref entity))
                continue;

            filter.AddEntity(entity.Index);
        }
    }
}

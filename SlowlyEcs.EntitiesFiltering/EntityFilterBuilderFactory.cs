using SlowlyEcs.Interfaces.EntitiesFiltering;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs.EntitiesFiltering;

public class EntityFilterBuilderFactory : IEntityFiltersBuilderFactory
{
    private readonly IEntityFiltersStorage _entityFiltersStorage;
    private readonly IEntitiesStorage _entitiesStorage;
    private readonly IArchetypesStorage _archetypesStorage;
    private readonly IComponentTypeIdProvider _componentTypeIdProvider;

    public EntityFilterBuilderFactory(IEntitiesStorage entitiesStorage,
        IEntityFiltersStorage entityFiltersStorage,
        IArchetypesStorage archetypesStorage)
    {
        _entitiesStorage = entitiesStorage ?? throw new ArgumentNullException(nameof(entitiesStorage));
        _entityFiltersStorage = entityFiltersStorage ?? throw new ArgumentNullException(nameof(entityFiltersStorage));
        _archetypesStorage = archetypesStorage;
        _componentTypeIdProvider = new ComponentTypeIdProvider();
    }

    public IEntityFilterBuilder Create()
    {
        return new EntityFilterBuilder(_entityFiltersStorage,
            _entitiesStorage,
            _componentTypeIdProvider,
            _archetypesStorage);
    }
}
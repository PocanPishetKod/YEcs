using YEcs.Interface;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Storaging;

namespace YEcs.EntitiesFiltering;

public class EntityFilterBuilderFactory : IEntityFiltersBuilderFactory
{
    private readonly IEntityFiltersStorage _entityFiltersStorage;
    private readonly IEntitiesStorage _entitiesStorage;

    public EntityFilterBuilderFactory(IEntitiesStorage entitiesStorage)
    {
        _entitiesStorage = entitiesStorage ?? throw new ArgumentNullException(nameof(entitiesStorage));
        _entityFiltersStorage = new CachedByByArchetypeEntityFiltersStorage(new EntityFiltersStorage());
    }

    public IEntityFilterBuilder<Entity, Archetype> Create()
    {
        return new EntityFilterBuilder(_entityFiltersStorage, _entitiesStorage);
    }
}
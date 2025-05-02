using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating.Handlers;

public class ComponentCreatedHandler : IEventHandler
{
    private readonly IEntityFiltersStorage _entityFiltersStorage;
    private readonly IArchetypesStorage _archetypesStorage;

    public ComponentCreatedHandler(IEntityFiltersStorage entityFiltersStorage, IArchetypesStorage archetypesStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
        _archetypesStorage = archetypesStorage;
    }

    public void Handle(ref WorldEvent worldEvent)
    {
        ref var archetype = ref _archetypesStorage.Get(worldEvent.EntityIndex);

        var currentFilters = _entityFiltersStorage.Get(archetype);
        
        archetype.AddComponent(worldEvent.ComponentTypeId);
        
        var newFilters = _entityFiltersStorage.Get(archetype);

        foreach (var filter in currentFilters)
        {
            if (filter.IsCompatible(archetype))
            {
                continue;
            }
            
            filter.RemoveEntity(worldEvent.EntityIndex);
        }

        foreach (var filter in newFilters)
        {
            filter.AddEntity(worldEvent.EntityIndex);
        }
    }
}
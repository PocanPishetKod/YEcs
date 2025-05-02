using SlowlyEcs.Interfaces.Historicity;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs.EntitiesFiltering.Updating.Handlers;

internal class EntityDestroyedHandler : IEventHandler
{
    private readonly IEntityFiltersStorage _entityFiltersStorage;
    private readonly IArchetypesStorage _archetypesStorage;

    public EntityDestroyedHandler(IEntityFiltersStorage entityFiltersStorage, IArchetypesStorage archetypesStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
        _archetypesStorage = archetypesStorage;
    }
    
    public void Handle(ref WorldEvent worldEvent)
    {
        ref var archetype = ref _archetypesStorage.Get(worldEvent.EntityIndex);
        
        var filters = _entityFiltersStorage.Get(archetype);
        foreach (var filter in filters)
        {
            filter.RemoveEntity(worldEvent.EntityIndex);
        }
    }
}
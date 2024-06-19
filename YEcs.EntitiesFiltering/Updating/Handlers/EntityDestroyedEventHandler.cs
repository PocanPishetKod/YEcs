using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating.Handlers;

internal class EntityDestroyedEventHandler : IEventHandler
{
    private readonly IByArchetypeEntityFiltersStorage _entityFiltersStorage;

    public EntityDestroyedEventHandler(IByArchetypeEntityFiltersStorage entityFiltersStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
    }
    
    public void Handle(ref WorldEvent worldEvent)
    {
        var filters = _entityFiltersStorage.Get(worldEvent.PreviousArchetype!.Value);
        
        if (filters.Count == 0)
            return;
        
        for (var i = 0; i < filters.Count; i++)
        {
            filters[i].RemoveEntity(worldEvent.EntityIndex);
        }
    }
}
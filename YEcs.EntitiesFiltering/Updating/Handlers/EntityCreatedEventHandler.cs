using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating.Handlers;

internal class EntityCreatedEventHandler : IEventHandler
{
    private readonly IByArchetypeEntityFiltersStorage _entityFiltersStorage;

    public EntityCreatedEventHandler(IByArchetypeEntityFiltersStorage entityFiltersStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
    }

    public void Handle(ref WorldEvent worldEvent)
    {
        var filters = _entityFiltersStorage.Get(Archetype.Empty);
        
        if (filters.Count == 0)
            return;
        
        for (var i = 0; i < filters.Count; i++)
        {
            filters[i].AddEntity(worldEvent.EntityIndex);
        }
    }
}
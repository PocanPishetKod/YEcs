using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating.Handlers;

public class EntityArchetypeChangedEventHandler : IEventHandler
{
    private readonly IByArchetypeEntityFiltersStorage _entityFiltersStorage;

    public EntityArchetypeChangedEventHandler(IByArchetypeEntityFiltersStorage entityFiltersStorage)
    {
        _entityFiltersStorage = entityFiltersStorage;
    }

    public void Handle(ref WorldEvent worldEvent)
    {
        var oldFilters = _entityFiltersStorage.Get(worldEvent.PreviousArchetype!.Value);
        var newFilters = _entityFiltersStorage.Get(worldEvent.NewArchetype!.Value);

        if (oldFilters.Count != 0)
        {
            for (var i = 0; i < oldFilters.Count; i++)
            {
                oldFilters[i].RemoveEntity(worldEvent.EntityIndex);
            }
        }
        
        if (newFilters.Count == 0)
            return;

        for (var i = 0; i < newFilters.Count; i++)
        {
            newFilters[i].AddEntity(worldEvent.EntityIndex);
        }
    }
}
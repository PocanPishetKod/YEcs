namespace YEcs.EntitiesFiltering;

internal class EntityFiltersUpdater
{
    private readonly IByArchetypeEntityFiltersStorage _byArchetypeEntityFiltersStorage;

    public EntityFiltersUpdater(IByArchetypeEntityFiltersStorage byArchetypeEntityFiltersStorage)
    {
        _byArchetypeEntityFiltersStorage = byArchetypeEntityFiltersStorage;
    }

    public void OnComponentAdded(ref Archetype archetype)
    {
        
    }

    public void OnComponentRemoved(ref Archetype archetype)
    {
        
    }

    private void UpdateFilters(ref Entity entity, ref Archetype oldArchetype)
    {
        var currentFilters = _byArchetypeEntityFiltersStorage.Get(ref oldArchetype);
        foreach (var filter in currentFilters)
        {
            if (!filter.IsCompatible(ref entity))
                filter.RemoveEntity(entity.Index);
        }

        var newArchetype = entity.Archetype;
        var newFilters = _byArchetypeEntityFiltersStorage.Get(ref newArchetype);
        foreach (var filter in newFilters)
        {
            filter.AddEntity(entity.Index);
        }
    }
}
namespace YEcs;

internal class EntityFiltersUpdater
{
    private readonly IArchetypeEntityFiltersStorage _archetypeEntityFiltersStorage;

    public EntityFiltersUpdater(IArchetypeEntityFiltersStorage archetypeEntityFiltersStorage)
    {
        _archetypeEntityFiltersStorage = archetypeEntityFiltersStorage;
    }

    public void OnComponentAdded(ref Archetype archetype)
    {
        
    }

    public void OnComponentRemoved(ref Archetype archetype)
    {
        
    }

    private void UpdateFilters(ref Entity entity, ref Archetype oldArchetype)
    {
        var currentFilters = _archetypeEntityFiltersStorage.Get(ref oldArchetype);
        foreach (var filter in currentFilters)
        {
            if (!filter.IsCompatible(ref entity))
                filter.RemoveEntity(entity.Index);
        }

        var newArchetype = entity.Archetype;
        var newFilters = _archetypeEntityFiltersStorage.Get(ref newArchetype);
        foreach (var filter in newFilters)
        {
            filter.AddEntity(entity.Index);
        }
    }
}
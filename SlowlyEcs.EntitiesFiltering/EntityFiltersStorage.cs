namespace SlowlyEcs.EntitiesFiltering;

public class EntityFiltersStorage : IEntityFiltersStorage
{
    private readonly Dictionary<ArchetypeMask, EntityFilter> _entityFiltersMap;
    private readonly Dictionary<Archetype, List<EntityFilter>> _byArchetypeMap;

    public EntityFiltersStorage()
    {
        _entityFiltersMap = new Dictionary<ArchetypeMask, EntityFilter>();
        _byArchetypeMap = new Dictionary<Archetype, List<EntityFilter>>();
    }

    public IReadOnlyCollection<EntityFilter> Get(in Archetype archetype)
    {
        if (_byArchetypeMap.TryGetValue(archetype, out var entityFilters))
        {
            return entityFilters;
        }
        
        entityFilters = new List<EntityFilter>();
        _byArchetypeMap.Add(archetype, entityFilters);

        foreach (var (mask, filter) in _entityFiltersMap)
        {
            if (!mask.IsCompatible(archetype))
            {
                continue;
            }
            
            entityFilters.Add(filter);
        }
        
        return entityFilters;
    }

    public void Add(ArchetypeMask key, EntityFilter value)
    {
        _entityFiltersMap.Add(key, value);
    }

    public bool TryGet(in ArchetypeMask mask, out EntityFilter? filter)
    {
        return _entityFiltersMap.TryGetValue(mask, out filter);
    }
}
namespace YEcs;

internal class CachedArchetypeEntityFiltersStorage : IArchetypeEntityFiltersStorage
{
    private readonly Dictionary<Archetype, ICollection<EntityFilter>> _entityFiltersMap;
    private readonly IArchetypeEntityFiltersStorage _storage;

    public CachedArchetypeEntityFiltersStorage(IArchetypeEntityFiltersStorage storage)
    {
        _storage = storage;
        _entityFiltersMap = new Dictionary<Archetype, ICollection<EntityFilter>>();
    }

    public ICollection<EntityFilter> Get(ref Archetype archetype)
    {
        if (_entityFiltersMap.TryGetValue(archetype, out var filters))
            return filters;

        filters = _storage.Get(ref archetype);
        _entityFiltersMap.Add(archetype, filters);

        return filters;
    }
}
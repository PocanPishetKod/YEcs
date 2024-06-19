namespace YEcs.EntitiesFiltering;

public class CachedByByArchetypeEntityFiltersStorage : IByArchetypeEntityFiltersStorage
{
    private readonly Dictionary<Archetype, IReadOnlyList<EntityFilter>> _entityFiltersMap;
    private readonly Dictionary<Archetype, bool> _visitArchetypeMap;
    private readonly IByArchetypeEntityFiltersStorage _storage;

    public CachedByByArchetypeEntityFiltersStorage(IByArchetypeEntityFiltersStorage storage)
    {
        _storage = storage;
        _entityFiltersMap = new Dictionary<Archetype, IReadOnlyList<EntityFilter>>();
        _visitArchetypeMap = new Dictionary<Archetype, bool>();
    }

    public IReadOnlyList<EntityFilter> Get(in Archetype archetype)
    {
        if (!_visitArchetypeMap.TryGetValue(archetype, out var visited))
            _visitArchetypeMap[archetype] = true;

        if (visited)
            return _entityFiltersMap
                .TryGetValue(archetype, out var filters)
                ? filters
                : Array.Empty<EntityFilter>();

        var toCacheFilters = _storage.Get(archetype);
        _entityFiltersMap.Add(archetype, toCacheFilters);

        return toCacheFilters;
    }

    public bool TryGet(ArchetypeMask key, out EntityFilter? value)
    {
        return _storage.TryGet(key, out value);
    }

    public void Add(ArchetypeMask key, EntityFilter value)
    {
        _storage.Add(key, value);
    }
}
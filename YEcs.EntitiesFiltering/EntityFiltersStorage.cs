namespace YEcs.EntitiesFiltering;

internal class EntityFiltersStorage : IByArchetypeEntityFiltersStorage
{
    private readonly Dictionary<ArchetypeMask, EntityFilter> _entityFiltersMap;

    public EntityFiltersStorage()
    {
        _entityFiltersMap = new Dictionary<ArchetypeMask, EntityFilter>();
    }

    public void Add(ArchetypeMask key, EntityFilter value)
    {
        _entityFiltersMap.Add(key, value);
    }

    public bool TryGet(ArchetypeMask key, out EntityFilter? value)
    {
        return _entityFiltersMap.TryGetValue(key, out value);
    }

    public ICollection<EntityFilter> Get(ref Archetype archetype)
    {
        var result = new List<EntityFilter>();
        foreach (var pair in _entityFiltersMap)
        {
            if (pair.Key.IsCompatible(archetype))
                result.Add(pair.Value);
        }

        return result;
    }
}
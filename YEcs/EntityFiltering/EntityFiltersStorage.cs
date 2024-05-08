namespace YEcs;

internal class EntityFiltersStorage : IEntityFiltersStorage, IArchetypeEntityFiltersStorage
{
    private readonly Dictionary<ArchetypeRestriction, EntityFilter> _entityFiltersMap;

    public EntityFiltersStorage()
    {
        _entityFiltersMap = new Dictionary<ArchetypeRestriction, EntityFilter>();
    }

    public void Add(ArchetypeRestriction key, EntityFilter value)
    {
        _entityFiltersMap.Add(key, value);
    }

    public bool TryGet(ArchetypeRestriction key, out EntityFilter? value)
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
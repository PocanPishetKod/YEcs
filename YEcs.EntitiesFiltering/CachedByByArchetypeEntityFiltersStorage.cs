using System.Runtime.CompilerServices;

namespace YEcs.EntitiesFiltering;

public class CachedByByArchetypeEntityFiltersStorage : IByArchetypeEntityFiltersStorage
{
    private readonly IDictionary<ArchetypeMask, ICollection<Archetype>> _archetypesMap;
    private readonly IDictionary<Archetype, ICollection<EntityFilter>> _entityFiltersMap;
    private readonly IByArchetypeEntityFiltersStorage _storage;

    public CachedByByArchetypeEntityFiltersStorage(IByArchetypeEntityFiltersStorage storage)
    {
        _storage = storage;
        _entityFiltersMap = new Dictionary<Archetype, ICollection<EntityFilter>>();
        _archetypesMap = new Dictionary<ArchetypeMask, ICollection<Archetype>>();
    }

    public ICollection<EntityFilter> Get(ref Archetype archetype)
    {
        return _entityFiltersMap
            .TryGetValue(archetype, out var filters) ? filters : new List<EntityFilter>();
    }

    public bool TryGet(ArchetypeMask key, out EntityFilter? value)
    {
        return _storage.TryGet(key, out value);
    }

    public void Add(ArchetypeMask key, EntityFilter value)
    {
        _storage.Add(key, value);
        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddToCache(ArchetypeMask key, EntityFilter value)
    {
        if (!_archetypesMap.TryGetValue(key, out var archetypes))
        {
            archetypes = new List<Archetype>();
            _archetypesMap.Add(key, archetypes);
        }

        if (archetypes.Count == 0)
        {
            foreach (var pair in _entityFiltersMap)
            {
                if (!key.IsCompatible(pair.Key))
                    continue;
                
                archetypes.Add(pair.Key);
                pair.Value.Add(value);
            }
        }
        else
        {
            foreach (var archetype in archetypes)
            {
                
            }
        }
    }
}
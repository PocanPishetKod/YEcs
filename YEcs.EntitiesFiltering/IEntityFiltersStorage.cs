namespace YEcs.EntitiesFiltering;

public interface IEntityFiltersStorage
{
    bool TryGet(in ArchetypeMask mask, out EntityFilter? filter);

    IReadOnlyCollection<EntityFilter> Get(in Archetype archetype);
    
    void Add(ArchetypeMask key, EntityFilter value);
}
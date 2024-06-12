namespace YEcs.EntitiesFiltering;

public interface IEntityFiltersStorage
{
    bool TryGet(ArchetypeMask key, out EntityFilter? value);
    
    void Add(ArchetypeMask key, EntityFilter value);
}
namespace YEcs;

internal interface IEntityFiltersStorage
{
    bool TryGet(ArchetypeRestriction key, out EntityFilter? value);
    
    void Add(ArchetypeRestriction key, EntityFilter value);
}
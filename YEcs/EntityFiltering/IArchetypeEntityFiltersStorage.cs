namespace YEcs;

internal interface IArchetypeEntityFiltersStorage
{
    ICollection<EntityFilter> Get(ref Archetype archetype);
}
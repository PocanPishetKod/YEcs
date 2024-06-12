namespace YEcs.EntitiesFiltering;

public interface IByArchetypeEntityFiltersStorage : IEntityFiltersStorage
{
    ICollection<EntityFilter> Get(ref Archetype archetype);
}
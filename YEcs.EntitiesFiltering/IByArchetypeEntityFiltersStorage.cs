namespace YEcs.EntitiesFiltering;

public interface IByArchetypeEntityFiltersStorage : IEntityFiltersStorage
{
    IReadOnlyList<EntityFilter> Get(in Archetype archetype);
}
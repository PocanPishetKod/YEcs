using YEcs.Interface;

namespace YEcs.Interfaces.EntitiesFiltering;

public interface IEntityFiltersBuilderFactory
{
    IEntityFilterBuilder<Entity, Archetype> Create();
}
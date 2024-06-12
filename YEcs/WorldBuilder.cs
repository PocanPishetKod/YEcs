using YEcs.Interface;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs;

public class WorldBuilder : IWorldBuilder<Entity, Archetype>
{
    private readonly World _world;

    public WorldBuilder(
        IEntitiesStorage entitiesStorage,
        IEntityFiltersBuilderFactory entityFiltersBuilderFactory,
        IWorldHistory worldHistory,
        IFiltersUpdater filtersUpdater)
    {
        _world = new World(entitiesStorage,
            entityFiltersBuilderFactory,
            filtersUpdater,
            worldHistory);
    }
    
    public IWorldBuilder<Entity, Archetype> WithUpdateSystem(IUpdateSystem system)
    {
        _world.AddUpdateSystem(system);
        return this;
    }

    public IWorldBuilder<Entity, Archetype> WithInitializeSystem(IInitializationSystem system)
    {
        _world.AddInitializationSystem(system);
        return this;
    }

    public IWorld<Entity, Archetype> Build()
    {
        return _world;
    }
}
using SlowlyEcs.Interfaces.EntitiesFiltering;
using SlowlyEcs.Interfaces.Historicity;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs;

public class WorldBuilder : IWorldBuilder
{
    private readonly World _world;

    public WorldBuilder(
        IEntitiesStorage entitiesStorage,
        IEntityFiltersBuilderFactory entityFiltersBuilderFactory,
        IWorldHistory worldHistory,
        IFiltersUpdater filtersUpdater,
        IComponentStorageFactory componentStorageFactory)
    {
        _world = new World(entitiesStorage,
            entityFiltersBuilderFactory,
            filtersUpdater,
            worldHistory,
            componentStorageFactory);
    }
    
    public IWorldBuilder WithUpdateSystem(IUpdateSystem system)
    {
        _world.AddUpdateSystem(system);
        return this;
    }

    public IWorldBuilder WithInitializeSystem(IInitializationSystem system)
    {
        _world.AddInitializationSystem(system);
        return this;
    }

    public IWorld Build()
    {
        return _world;
    }
}
using SlowlyEcs;
using SlowlyEcs.EntitiesFiltering;
using SlowlyEcs.EntitiesFiltering.Updating;
using SlowlyEcs.Historicity;
using SlowlyEcs.Storaging;

namespace Tests.Integration;

public static class Common
{
    public static WorldBuilder CreateWorldBuilder(int capacity = 100, int expand = 25)
    {
        var worldHistory = new WorldHistory(capacity, expand);
        var componentTypeIdProvider = new ComponentTypeIdProvider();
        var componentStorageFactory = new ComponentStorageFactory(capacity, expand, worldHistory, componentTypeIdProvider);
        var entitiesStorage = new EntitiesStorage(capacity, expand);
        var entityFiltersStorage = new EntityFiltersStorage();
        var archetypesStorage = new ArchetypesStorage(capacity);
        var filtersUpdater = new EntityFiltersUpdater(archetypesStorage, entityFiltersStorage);
            
        return new WorldBuilder(
            entitiesStorage,
            new EntityFilterBuilderFactory(entitiesStorage, entityFiltersStorage, archetypesStorage),
            worldHistory,
            filtersUpdater,
            componentStorageFactory);
    }
}
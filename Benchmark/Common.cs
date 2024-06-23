using YEcs;
using YEcs.EntitiesFiltering;
using YEcs.EntitiesFiltering.Updating;
using YEcs.Historicity;
using YEcs.Storaging;

namespace Benchmark;

public static class Common
{
    public static WorldBuilder CreateWorldBuilder(int capacity, int expand)
    {
        var worldHistory = new WorldHistory(capacity, expand);
        var entitiesStorage = new EntitiesStorage(capacity, expand, new ComponentStorageFactory(capacity, expand), worldHistory);
        var entityFiltersStorage = new EntityFiltersStorage();
        var filtersUpdater = new EntityFiltersUpdater(
            new HistoryHandler(
                new EventHandlerResolver(entitiesStorage,
                    new CachedByByArchetypeEntityFiltersStorage(entityFiltersStorage))));
            
        return new WorldBuilder(
            entitiesStorage,
            new EntityFilterBuilderFactory(entitiesStorage, entityFiltersStorage),
            worldHistory,
            filtersUpdater);
    }
}
using YEcs;
using YEcs.EntitiesFiltering;
using YEcs.EntitiesFiltering.Updating;
using YEcs.Historicity;
using YEcs.Storaging;

namespace Tests.Integration;

public static class Common
{
    public static WorldBuilder CreateWorldBuilder()
    {
        var entitiesStorage = new EntitiesStorage(new ComponentStorageFactory(), new WorldHistory(100, 25));
        var worldHistory = new WorldHistory(100, 25);
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
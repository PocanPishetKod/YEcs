using System.Runtime.CompilerServices;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public class EntityFiltersUpdater : IFiltersUpdater
{
    private readonly HistoryHandler _historyHandler;

    public EntityFiltersUpdater(IArchetypesStorage archetypesStorage, IEntityFiltersStorage entityFiltersStorage)
    {
        _historyHandler = new HistoryHandler(new EventHandlerResolver(entityFiltersStorage, archetypesStorage));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(IWorldHistory worldHistory)
    {
        if (worldHistory.Length == 0)
            return;
        
        _historyHandler.Handle(worldHistory);
    }
}
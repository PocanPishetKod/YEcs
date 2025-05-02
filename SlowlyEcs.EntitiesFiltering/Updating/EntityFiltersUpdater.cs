using System.Runtime.CompilerServices;
using SlowlyEcs.Interfaces.EntitiesFiltering;
using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.EntitiesFiltering.Updating;

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
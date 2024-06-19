using System.Runtime.CompilerServices;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public class EntityFiltersUpdater : IFiltersUpdater
{
    private readonly IHistoryHandler _historyHandler;

    public EntityFiltersUpdater(IHistoryHandler historyHandler)
    {
        _historyHandler = historyHandler ?? throw new ArgumentNullException(nameof(historyHandler));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(IWorldHistory worldHistory)
    {
        if (worldHistory.Length == 0)
            return;
        
        _historyHandler.Handle(worldHistory.CreateNavigator());
    }
}
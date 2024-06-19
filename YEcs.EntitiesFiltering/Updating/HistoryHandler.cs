using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public class HistoryHandler : IHistoryHandler
{
    private readonly IEventHandlerResolver _eventHandlerResolver;

    public HistoryHandler(IEventHandlerResolver eventHandlerResolver)
    {
        _eventHandlerResolver = eventHandlerResolver ?? throw new ArgumentNullException(nameof(eventHandlerResolver));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Handle(IHistoryNavigator historyNavigator)
    {
        if (!historyNavigator.Forward())
            return;

        do
        {
            ref var worldEvent = ref historyNavigator.GetCurrent();
            _eventHandlerResolver.Resolve(worldEvent.Type).Handle(ref worldEvent);    
        } 
        while (historyNavigator.Forward());
    }
}
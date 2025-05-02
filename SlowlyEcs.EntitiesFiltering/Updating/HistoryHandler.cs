using System.Runtime.CompilerServices;
using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.EntitiesFiltering.Updating;

internal class HistoryHandler
{
    private readonly IEventHandlerResolver _eventHandlerResolver;

    public HistoryHandler(IEventHandlerResolver eventHandlerResolver)
    {
        _eventHandlerResolver = eventHandlerResolver ?? throw new ArgumentNullException(nameof(eventHandlerResolver));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Handle(IWorldHistory worldHistory)
    {
        var eventsCount = worldHistory.Length;

        for (var i = 0; i < eventsCount; i++)
        {
            ref var ev = ref worldHistory[i];
            _eventHandlerResolver.Resolve(ev.Type).Handle(ref ev);
        }
    }
}
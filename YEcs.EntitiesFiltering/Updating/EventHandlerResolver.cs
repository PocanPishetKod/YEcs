using System.Runtime.CompilerServices;
using YEcs.EntitiesFiltering.Updating.Handlers;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs.EntitiesFiltering.Updating;

public class EventHandlerResolver : IEventHandlerResolver
{
    private readonly IDictionary<WorldEventType, IEventHandler> _eventHandlers;

    public EventHandlerResolver(IEntitiesStorage entitiesStorage, IByArchetypeEntityFiltersStorage entityFiltersStorage)
    {
        ArgumentNullException.ThrowIfNull(entitiesStorage);
        ArgumentNullException.ThrowIfNull(entityFiltersStorage);

        _eventHandlers = new Dictionary<WorldEventType, IEventHandler>()
        {
            { WorldEventType.EntityCreated, new EntityCreatedEventHandler(entityFiltersStorage) },
            { WorldEventType.EntityDestroyed, new EntityDestroyedEventHandler(entityFiltersStorage) },
            { WorldEventType.EntityArchetypeChanged, new EntityArchetypeChangedEventHandler(entityFiltersStorage)}
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEventHandler Resolve(WorldEventType worldEventType)
    {
        return _eventHandlers[worldEventType];
    }
}
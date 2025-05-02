using System.Runtime.CompilerServices;
using YEcs.EntitiesFiltering.Updating.Handlers;
using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

internal class EventHandlerResolver : IEventHandlerResolver
{
    private readonly IEventHandler[] _eventHandlers;

    public EventHandlerResolver(IEntityFiltersStorage entityFiltersStorage, IArchetypesStorage archetypesStorage)
    {
        ArgumentNullException.ThrowIfNull(entityFiltersStorage);
        ArgumentNullException.ThrowIfNull(archetypesStorage);

        _eventHandlers = 
        [
            new EntityCreatedHandler(archetypesStorage),
            new EntityDestroyedHandler(entityFiltersStorage, archetypesStorage),
            new ComponentCreatedHandler(entityFiltersStorage, archetypesStorage),
            new ComponentRemovedHandler(entityFiltersStorage, archetypesStorage)
        ];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEventHandler Resolve(WorldEventType worldEventType)
    {
        return _eventHandlers[(int)worldEventType];
    }
}
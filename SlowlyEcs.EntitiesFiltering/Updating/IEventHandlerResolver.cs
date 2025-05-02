using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.EntitiesFiltering.Updating;

internal interface IEventHandlerResolver
{
    IEventHandler Resolve(WorldEventType worldEventType);
}
using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

internal interface IEventHandlerResolver
{
    IEventHandler Resolve(WorldEventType worldEventType);
}
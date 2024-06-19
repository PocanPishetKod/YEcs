using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public interface IEventHandlerResolver
{
    IEventHandler Resolve(WorldEventType worldEventType);
}
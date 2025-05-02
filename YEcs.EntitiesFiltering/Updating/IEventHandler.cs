using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

internal interface IEventHandler
{
    void Handle(ref WorldEvent worldEvent);
}
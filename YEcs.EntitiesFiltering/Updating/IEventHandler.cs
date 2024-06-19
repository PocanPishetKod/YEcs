using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public interface IEventHandler
{
    void Handle(ref WorldEvent worldEvent);
}
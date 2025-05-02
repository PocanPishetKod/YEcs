using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.EntitiesFiltering.Updating;

internal interface IEventHandler
{
    void Handle(ref WorldEvent worldEvent);
}
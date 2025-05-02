using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.EntitiesFiltering.Updating.Handlers;

internal class EntityCreatedHandler : IEventHandler
{
    private readonly IArchetypesStorage _archetypesStorage;

    public EntityCreatedHandler(IArchetypesStorage archetypesStorage)
    {
        _archetypesStorage = archetypesStorage;
    }

    public void Handle(ref WorldEvent worldEvent)
    {
        _archetypesStorage.Get(worldEvent.EntityIndex, true);
    }
}
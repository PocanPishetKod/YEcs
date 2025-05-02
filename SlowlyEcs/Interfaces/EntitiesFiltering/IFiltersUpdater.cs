using SlowlyEcs.Interfaces.Historicity;

namespace SlowlyEcs.Interfaces.EntitiesFiltering;

public interface IFiltersUpdater
{
    void Update(IWorldHistory worldHistory);
}
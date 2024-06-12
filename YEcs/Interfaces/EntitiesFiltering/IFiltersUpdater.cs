using YEcs.Interfaces.Historicity;

namespace YEcs.Interfaces.EntitiesFiltering;

public interface IFiltersUpdater
{
    void Update(IWorldHistory worldHistory);
}
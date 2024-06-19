using YEcs.Interfaces.Historicity;

namespace YEcs.EntitiesFiltering.Updating;

public interface IHistoryHandler
{
    void Handle(IHistoryNavigator historyNavigator);
}
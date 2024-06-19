namespace YEcs.Interfaces.Historicity;

public interface IHistoryNavigator
{
    ref WorldEvent GetCurrent();

    bool Forward();

    bool Back();
}
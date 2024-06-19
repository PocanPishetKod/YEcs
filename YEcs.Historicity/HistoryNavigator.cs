using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;

namespace YEcs.Historicity;

internal class HistoryNavigator : IHistoryNavigator
{
    private readonly WorldEvent[] _events;
    private readonly int _count;
    private readonly int _max;
    private int _current;

    public HistoryNavigator(WorldEvent[] events, int count)
    {
        _events = events;
        _count = count;
        _max = count - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref WorldEvent GetCurrent()
    {
        return ref _events[_current];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Forward()
    {
        if (_current == _max)
            return false;

        _current++;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Back()
    {
        if (_current == 0)
            return false;

        _current--;

        return true;
    }
}
using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;

namespace YEcs.Historicity;

public class WorldHistory : IWorldHistory
{
    private WorldEvent[] _events;
    private readonly int _capacityStep;
    private int _count;

    public int Length => _count;
    
    public WorldHistory(int capacity, int capacityStep)
    {
        if (capacity < 0)
            throw new ArgumentException(nameof(capacity));

        if (capacityStep <= 0)
            throw new AggregateException(nameof(capacityStep));
        
        _events = new WorldEvent[capacity];
        _capacityStep = capacityStep;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push(WorldEvent @event)
    {
        if (_count == _events.Length)
            Array.Resize(ref _events, _events.Length + _capacityStep);

        _events[_count++] = @event;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IHistoryNavigator CreateNavigator()
    {
        return new HistoryNavigator(_events, _count);
    }
}
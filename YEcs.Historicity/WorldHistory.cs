using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;

namespace YEcs.Historicity;

public class WorldHistory : IWorldHistory
{
    private readonly List<WorldEvent> _events;

    public WorldHistory(int capacity)
    {
        _events = new List<WorldEvent>(capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push(WorldEvent @event)
    {
        _events.Add(@event);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _events.Clear();
    }

    public int Length => _events.Count;

    public WorldEvent this[int index] => _events[index];
}
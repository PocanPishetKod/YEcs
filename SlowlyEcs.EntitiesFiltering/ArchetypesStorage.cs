using SlowlyEcs.Common;

namespace SlowlyEcs.EntitiesFiltering;

public class ArchetypesStorage : IArchetypesStorage
{
    private readonly Archetype[] _archetypes;
    private readonly int[] _removedIndices;

    public ArchetypesStorage(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _archetypes = new Archetype[capacity];
        _removedIndices = [];
    }

    public ref Archetype Get(int entityIndex, bool reset = false)
    {
        _archetypes.ResizeIfNeeded(entityIndex);

        ref var archetype = ref _archetypes[entityIndex];
        
        if (archetype.IsNull)
        {
            _archetypes[entityIndex] = new Archetype();
        }
        else if (reset)
        {
            archetype.Clear();
        }
        
        return ref _archetypes[entityIndex];
    }
}
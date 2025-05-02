using System.Runtime.CompilerServices;
using SlowlyEcs.Common;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs.Storaging;

public class EntitiesStorage : IEntitiesStorage
{
    private readonly int _expand;

    private readonly Entity[] _entities;
    private int _count;

    private readonly int[] _removedEntitiesIndices;
    private int _removedEntitiesCount;

    public int Count => _count;
    
    public ref Entity this[int entityIndex] => ref _entities[entityIndex];

    public EntitiesStorage(int capacity, int expand)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        if (expand <= 0)
            throw new ArgumentOutOfRangeException(nameof(expand));
        
        _expand = expand;
        _entities = new Entity[capacity];
        _count = 0;
        _removedEntitiesIndices = new int[capacity];
        _removedEntitiesCount = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool ContainsRemovedIndex(int index)
    {
        if (_removedEntitiesCount == 0)
            return false;

        for (var i = 0; i < _removedEntitiesCount; i++)
        {
            if (_removedEntitiesIndices[i] == index)
                return true;
        }

        return false;
    }

    public ref Entity Create()
    {
        if (_removedEntitiesCount > 0)
        {
            ref var entity = ref _entities[_removedEntitiesIndices[_removedEntitiesCount - 1]];
            _removedEntitiesCount--;

            return ref entity;
        }

        _entities.ResizeIfNeeded(_count, _expand);

        _entities[_count] = new Entity(_count);
        return ref _entities[_count++];
    }

    public void Remove(int entityIndex)
    {
#if DEBUG
        if (ContainsRemovedIndex(entityIndex))
            throw new InvalidOperationException($"Entity with index {entityIndex} already removed.");
#endif

        _removedEntitiesIndices.ResizeIfNeeded(_removedEntitiesCount, _expand);

        _removedEntitiesIndices[_removedEntitiesCount++] = entityIndex;
    }
}

using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs.Storaging;

public class EntitiesStorage : IEntitiesStorage
{
    private readonly int _expand;

    private Entity[] _entities;
    private int _count;

    private int[] _removedEntitiesIndices;
    private int _removedEntitiesCount;

    private readonly IComponentStorageFactory _componentStorageFactory;
    private readonly IWorldHistory _worldHistory;

    public int Count => _count;
    
    public ref Entity this[int entityIndex] => ref _entities[entityIndex];

    public EntitiesStorage(int capacity, int expand, IComponentStorageFactory componentStorageFactory, IWorldHistory worldHistory)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        if (expand <= 0)
            throw new ArgumentOutOfRangeException(nameof(expand));
        
        _componentStorageFactory = componentStorageFactory ?? throw new ArgumentNullException(nameof(componentStorageFactory));
        _worldHistory = worldHistory ?? throw new ArgumentNullException(nameof(worldHistory));
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

        for (int i = 0; i < _removedEntitiesCount; i++)
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

        if (_count == _entities.Length)
            Array.Resize(ref _entities, _entities.Length + _expand);

        _entities[_count] = new Entity(_count, _componentStorageFactory, _worldHistory);
        return ref _entities[_count++];
    }

    public void Remove(ref Entity entity)
    {
#if DEBUG
        if (ContainsRemovedIndex(entity.Index))
            throw new InvalidOperationException($"Entity with index {entity.Index} already removed.");
#endif

        if (_removedEntitiesCount == _removedEntitiesIndices.Length)
            Array.Resize(ref _removedEntitiesIndices, _removedEntitiesIndices.Length + _expand);

        _removedEntitiesIndices[_removedEntitiesCount++] = entity.Index;

        entity.Clear();
    }
}

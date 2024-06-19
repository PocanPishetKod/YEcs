using System.Runtime.CompilerServices;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs.Storaging;

public class EntitiesStorage : IEntitiesStorage
{
    private const int ExtensionValue = 10;
    private const int DefaultArraySize = 10;

    private Entity[] _entities;
    private int _count;

    private int[] _removedEntitiesIndices;
    private int _removedEntitiesCount;

    private readonly IComponentStorageFactory _componentStorageFactory;
    private readonly IWorldHistory _worldHistory;

    public int Count => _count;
    
    public ref Entity this[int entityIndex] => ref _entities[entityIndex];

    public EntitiesStorage(IComponentStorageFactory componentStorageFactory, IWorldHistory worldHistory)
    {
        _entities = new Entity[DefaultArraySize];
        _count = 0;
        _removedEntitiesIndices = new int[DefaultArraySize];
        _removedEntitiesCount = 0;
        _componentStorageFactory = componentStorageFactory;
        _worldHistory = worldHistory;
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
            Array.Resize(ref _entities, _entities.Length + ExtensionValue);

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
            Array.Resize(ref _removedEntitiesIndices, _removedEntitiesIndices.Length + ExtensionValue);

        _removedEntitiesIndices[_removedEntitiesCount++] = entity.Index;

        entity.Clear();
    }
}

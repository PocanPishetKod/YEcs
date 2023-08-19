using System.Runtime.CompilerServices;

namespace YEcs
{
    internal class EntityStorage
    {
        private const int ExtensionValue = 10;
        private const int DefaultArraySize = 10;

        private Entity[] _entities;
        private int _count;

        private int[] _removedEntitiesIndeces;
        private int _removedEntitiesCount;

        private readonly ComponentStoragesManager _componentStoragesManager;
        private readonly World _owner;

        public int Count => _count;

        public EntityStorage(ComponentStoragesManager componentStoragesManager, World owner)
        {
            _entities = new Entity[DefaultArraySize];
            _count = 0;
            _removedEntitiesIndeces = new int[DefaultArraySize];
            _removedEntitiesCount = 0;
            _componentStoragesManager = componentStoragesManager;
            _owner = owner;
        }

        public ref Entity this[int index]
        {
            get
            {
                return ref _entities[index];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ContainsRemovedIndex(int index)
        {
            if (_removedEntitiesCount == 0)
                return false;

            for (int i = 0; i < _removedEntitiesCount; i++)
            {
                if (_removedEntitiesIndeces[i] == index)
                    return true;
            }

            return false;
        }

        public ref Entity CreateEntity()
        {
            if (_removedEntitiesCount > 0)
            {
                ref var entity = ref _entities[_removedEntitiesIndeces[_removedEntitiesCount - 1]];
                entity.IsRemoved = false;
                _removedEntitiesCount--;

                return ref entity;
            }

            if (_count == _entities.Length)
                Array.Resize(ref _entities, _entities.Length + ExtensionValue);

            _entities[_count] = new Entity(_count, _componentStoragesManager, _owner);
            return ref _entities[_count++];
        }

        public void RemoveEntity(ref Entity entity)
        {
#if DEBUG

            if (ContainsRemovedIndex(entity.Index))
                throw new InvalidOperationException($"Entity with index {entity.Index} already removed");
#endif

            if (_removedEntitiesCount == _removedEntitiesIndeces.Length)
                Array.Resize(ref _removedEntitiesIndeces, _removedEntitiesIndeces.Length + ExtensionValue);

            _removedEntitiesIndeces[_removedEntitiesCount++] = entity.Index;

            entity.Clear();
            entity.IsRemoved = true;
        }
    }
}

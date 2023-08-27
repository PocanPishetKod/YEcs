namespace YEcs
{
    public class EntityFilter
    {
        private readonly EntityStorage _entityStorage;
        private readonly List<int> _entityIndeces;
        private readonly IReadOnlyList<int> _withComponentTypeIds;
        private readonly IReadOnlyList<int> _exceptComponentTypeIds;

        internal EntityFilter(IReadOnlyList<int> withComponentTypeIds, IReadOnlyList<int> exceptComponentTypeIds, EntityStorage entityStorage)
        {
            _entityStorage = entityStorage;
            _entityIndeces = new List<int>();
            _withComponentTypeIds = withComponentTypeIds;
            _exceptComponentTypeIds = exceptComponentTypeIds;
        }

        public ref Entity this[int index]
        {
            get
            {
                return ref _entityStorage[_entityIndeces[index]];
            }
        }

        public bool IsCompatible(in Entity entity)
        {
            foreach (var componentTypeId in _withComponentTypeIds)
            {
                if (!entity.HasComponent(componentTypeId))
                    return false;
            }

            foreach (var componentTypeId in _exceptComponentTypeIds)
            {
                if (entity.HasComponent(componentTypeId))
                    return false;
            }

            return true;
        }

        internal void AddEntity(int index)
        {
            _entityIndeces.Add(index);
        }

        internal void RemoveEntity(int index)
        {
            _entityIndeces.Remove(index);
        }
    }
}

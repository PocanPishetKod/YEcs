using System.Collections.Generic;

namespace NoNameGame.Framework
{
    public class EntityFilter
    {
        private readonly EntityStorage _entityStorage;
        private readonly List<int> _entityIndeces;
        private readonly IReadOnlyList<int> _componentTypeIds;

        internal EntityFilter(IReadOnlyList<int> componentTypeIds, EntityStorage entityStorage)
        {
            _entityStorage = entityStorage;
            _entityIndeces = new List<int>();
            _componentTypeIds = componentTypeIds;
        }

        public ref Entity this[int index]
        {
            get
            {
                return ref _entityStorage[_entityIndeces[index]];
            }
        }

        public bool IsCompatable(in Entity entity)
        {
            foreach (var componentTypeId in _componentTypeIds)
            {
                if (entity.HasComponent(componentTypeId))
                    return true;
            }

            return false;
        }

        public void AddEntity(int index)
        {
            _entityIndeces.Add(index);
        }

        public void RemoveEntity(int index)
        {
            _entityIndeces.Remove(index);
        }
    }
}

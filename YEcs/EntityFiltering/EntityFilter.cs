namespace YEcs
{
    public class EntityFilter
    {
        private readonly EntityStorage _entityStorage;
        private readonly List<int> _entityIndices;
        private readonly ArchetypeRestriction _restriction;

        internal EntityFilter(ArchetypeRestriction restriction, EntityStorage entityStorage)
        {
            _entityStorage = entityStorage;
            _entityIndices = new List<int>();
        }

        public int Count => _entityIndices.Count;
        
        public ref Entity this[int index] => ref _entityStorage[_entityIndices[index]];

        public bool IsCompatible(ref Entity entity)
        {
            return _restriction.IsCompatible(entity.Archetype);
        }

        internal void AddEntity(int index)
        {
            _entityIndices.Add(index);
        }

        internal void RemoveEntity(int index)
        {
            _entityIndices.Remove(index);
        }
    }
}

using YEcs.Common;

namespace YEcs.EntitiesFiltering
{
    public struct Archetype : IEquatable<Archetype>
    {
        private readonly ISet<ComponentTypeId> _componentTypeIds;
        private int _hashCode = -1;
        private byte _isChanged;

        public Archetype()
        {
            _componentTypeIds = new HashSet<ComponentTypeId>(5);
        }
        
        public bool IsNull => _componentTypeIds == null;
        
        public void SubtractComponent(ComponentTypeId componentTypeId)
        {
            _componentTypeIds.Remove(componentTypeId);
            _isChanged = 1;
        }

        public void AddComponent(ComponentTypeId componentTypeId)
        {
            
            _componentTypeIds.Add(componentTypeId);
            _isChanged = 1;
        }

        public void Clear()
        {
            _componentTypeIds.Clear();
            _isChanged = 1;
        }

        public bool Equals(Archetype other)
        {
            if (_componentTypeIds.Count != other._componentTypeIds.Count)
            {
                return false;
            }

            foreach (var componentTypeId in _componentTypeIds)
            {
                if (!other._componentTypeIds.Contains(componentTypeId))
                {
                    return false;
                }
            }
            
            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is Archetype other && Equals(other);
        }

        public override int GetHashCode()
        {
            if (_hashCode == -1 || _isChanged == 1)
            {
                _hashCode = HashCodeExtensions.Create(_componentTypeIds).ToHashCode();
                _isChanged = 0;
            }
            
            return _hashCode;
        }

        public static bool operator ==(Archetype left, Archetype right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Archetype left, Archetype right)
        {
            return !left.Equals(right);
        }

        public IEnumerator<ComponentTypeId> GetEnumerator()
        {
            return _componentTypeIds.GetEnumerator();
        }
    }
}

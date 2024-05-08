namespace YEcs
{
    public struct Entity
    {
        private readonly ComponentStoragesManager _componentStoragesManager;
        private readonly Dictionary<int, int> _componentIndices;
        private readonly World _owner;

        internal int Index { get; }

        public int ComponentsCount => _componentIndices.Count;

        internal bool IsRemoved { get; set; }

        public Archetype Archetype => new Archetype(_componentIndices.Keys);

        internal Entity(int index, ComponentStoragesManager componentStoragesManager,
            World owner)
        {
            _componentStoragesManager = componentStoragesManager;
            _componentIndices = new Dictionary<int, int>();
            Index = index;
            _owner = owner;
            IsRemoved = false;
        }

        public bool HasComponent(int componentTypeId)
        {
            foreach (var entityComponentTypeId in _componentIndices.Keys)
            {
                if (entityComponentTypeId == componentTypeId)
                    return true;
            }

            return false;
        }

        public ref TComponent CreateComponent<TComponent>() where TComponent : struct
        {
#if DEBUG
            if (_componentIndices.Keys.Contains(ComponentTypesStorage.GetId(typeof(TComponent))))
                throw new InvalidOperationException("Component already exists on entity");
#endif
            var componentRef = _componentStoragesManager.CreateComponent<TComponent>();
            _componentIndices.Add(componentRef.Storage.ComponentTypeId, componentRef.Index);

            return ref componentRef.Storage[componentRef.Index];
        }

        public ref TComponent GetComponent<TComponent>() where TComponent : struct
        {
#if DEBUG
            if (!_componentIndices.Keys.Contains(ComponentTypesStorage.GetId(typeof(TComponent))))
                throw new InvalidOperationException($"Component {typeof(TComponent).Name} not found");
#endif

            return ref _componentStoragesManager
                .FindComponent<TComponent>(_componentIndices[ComponentTypesStorage.GetId<TComponent>()]);
        }

        public void RemoveComponent<TComponent>() where TComponent : struct
        {
            var componentTypeId = ComponentTypesStorage.GetId(typeof(TComponent));
#if DEBUG
            if (!_componentIndices.TryGetValue(componentTypeId, out var componentIndex))
                throw new InvalidOperationException($"Component with type id {componentTypeId} is missing from entity {Index}");
#endif
        }

        internal void Clear()
        {
            foreach (var componentMapElement in _componentIndices)
            {
                _componentStoragesManager.RemoveComponent(componentMapElement.Key, componentMapElement.Value);
            }

            _componentIndices.Clear();
        }
    }
}

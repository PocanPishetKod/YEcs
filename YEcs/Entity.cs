using System;
using System.Collections.Generic;
using System.Linq;

namespace YEcs
{
    public struct Entity
    {
        private readonly ComponentStoragesManager _componentStoragesManager;
        private readonly Dictionary<int, int> _componentIndeces;
        private readonly World _owner;

        internal int Index { get; }

        public int ComponentsCount => _componentIndeces.Count;

        internal bool IsRemoved { get; set; }

        internal Entity(int index, ComponentStoragesManager componentStoragesManager,
            World owner)
        {
            _componentStoragesManager = componentStoragesManager;
            _componentIndeces = new Dictionary<int, int>();
            Index = index;
            _owner = owner;
            IsRemoved = false;
        }

        public bool HasComponent(int componentTypeId)
        {
            foreach (var entityComponentTypeId in _componentIndeces.Keys)
            {
                if (entityComponentTypeId == componentTypeId)
                    return true;
            }

            return false;
        }

        public ref TComponent CreateComponent<TComponent>() where TComponent : struct
        {
#if DEBUG
            if (_componentIndeces.Keys.Contains(ComponentTypesStorage.GetId(typeof(TComponent))))
                throw new InvalidOperationException("Component already exists on entity");
#endif
            var componentRef = _componentStoragesManager.CreateComponent<TComponent>();
            _componentIndeces.Add(componentRef.Storage.ComponentTypeId, componentRef.Index);

            return ref componentRef.Storage[componentRef.Index];
        }

        public ref TComponent GetComponent<TComponent>() where TComponent : struct
        {
#if DEBUG
            if (!_componentIndeces.Keys.Contains(ComponentTypesStorage.GetId(typeof(TComponent))))
                throw new InvalidOperationException($"Component {typeof(TComponent).Name} not found");
#endif

            return ref _componentStoragesManager
                .FindComponent<TComponent>(_componentIndeces[ComponentTypesStorage.GetId<TComponent>()]);
        }

        public void RemoveComponent<TComponent>() where TComponent : struct
        {
            var componentTypeId = ComponentTypesStorage.GetId(typeof(TComponent));
#if DEBUG
            if (!_componentIndeces.TryGetValue(componentTypeId, out var componentIndex))
                throw new InvalidOperationException($"Component with type id {componentTypeId} is missing from entity {Index}");
#endif
        }

        internal void Clear()
        {
            foreach (var componentMapElement in _componentIndeces)
            {
                _componentStoragesManager.RemoveComponent(componentMapElement.Key, componentMapElement.Value);
            }

            _componentIndeces.Clear();
        }
    }
}

using System.Runtime.CompilerServices;

namespace YEcs
{
    internal class ComponentStoragesManager
    {
        private readonly Dictionary<int, IComponentStorage> _storagesMap;

        public ComponentStoragesManager()
        {
            _storagesMap = new Dictionary<int, IComponentStorage>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComponentStorage<TComponent>? FindStorage<TComponent>() where TComponent : struct
        {
            _storagesMap.TryGetValue(ComponentTypesStorage.GetId<TComponent>(), out var storage);
            return storage == null ? null : storage as ComponentStorage<TComponent>;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComponentStorage<TComponent> CreateStorage<TComponent>() where TComponent : struct
        {
            var storage = new ComponentStorage<TComponent>();
            _storagesMap.Add(ComponentTypesStorage.GetId<TComponent>(), storage);
            return storage;
        }

        public ComponentRef<TComponent> CreateComponent<TComponent>() where TComponent : struct
        {
            var storage = FindStorage<TComponent>() ?? CreateStorage<TComponent>();
            return storage.CreateComponent();
        }

        public ref TComponent FindComponent<TComponent>(int index) where TComponent : struct
        {
            var storage = FindStorage<TComponent>() ?? CreateStorage<TComponent>();
            return ref storage[index];
        }

        public void RemoveComponent(int componentTypeId, int index)
        {
            if (!_storagesMap.TryGetValue(componentTypeId, out var storage))
                throw new InvalidOperationException("Component not found");

            storage.RemoveComponent(index);
        }
    }
}

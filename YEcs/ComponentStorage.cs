using System;

namespace NoNameGame.Framework
{
    internal readonly struct ComponentRef<TComponent> where TComponent : struct
    {
        public readonly int Index;
        public readonly ComponentStorage<TComponent> Storage;

        public ComponentRef(int index, ComponentStorage<TComponent> componentStorage)
        {
            Index = index;
            Storage = componentStorage;
        }
    }

    internal interface IComponentStorage
    {
        int ComponentTypeId { get; }

        void RemoveComponent(int index);
    }

    internal class ComponentStorage<T> : IComponentStorage
        where T : struct
    {
        private const int DefaultArraySize = 10;
        private const int ExtensionValue = 10;

        private T[] _components;
        private int _count;

        private int[] _removedComponentsIndeces;
        private int _removedComponentsCount;

        public int ComponentTypeId { get; }

        public ComponentStorage()
        {
            _components = new T[DefaultArraySize];
            _count = 0;
            _removedComponentsIndeces = new int[DefaultArraySize];
            _removedComponentsCount = 0;
            ComponentTypeId = ComponentTypesStorage.GetId<T>();
        }

        public ref T this[int index]
        {
            get
            {
                return ref _components[index];
            }
        }

        public ComponentRef<T> CreateComponent()
        {
            if (_removedComponentsCount > 0)
            {
                _components[_removedComponentsIndeces[_removedComponentsCount - 1]] = new T();
                return new ComponentRef<T>(_removedComponentsIndeces[--_removedComponentsCount], this);
            }

            if (_count == _components.Length)
                Array.Resize(ref _components, _components.Length + ExtensionValue);

            _components[_count] = new T();

            return new ComponentRef<T>(_count++, this);
        }

        public void RemoveComponent(int index)
        {
#if DEBUG
            if (index < 0 || index >= _components.Length || index >= _count)
                throw new ArgumentException($"Invalid index = {index}. ComponentsArrayLength = {_components.Length}, ComponentsCount = {_count}");
#endif

            if (_removedComponentsCount == _removedComponentsIndeces.Length)
                Array.Resize(ref _removedComponentsIndeces, _removedComponentsIndeces.Length + ExtensionValue);

            _removedComponentsIndeces[_removedComponentsCount++] = index;
        }
    }
}

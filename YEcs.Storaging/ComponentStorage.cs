using YEcs.Interface;
using YEcs.Interfaces.Storaging;

namespace YEcs.Storaging
{
    internal class ComponentStorage<T> : IComponentStorage<T>
        where T : struct, IReusable
    {
        private readonly int _expand;

        private T[] _components;
        private int _count;

        private int[] _removedComponentsIndices;
        private int _removedComponentsCount;

        public ref T this[int index] => ref _components[index];
        
        public ComponentStorage(int capacity, int expand)
        {
            _expand = expand;
            _components = new T[capacity];
            _count = 0;
            _removedComponentsIndices = new int[capacity];
            _removedComponentsCount = 0;
        }

        public ComponentRef<T> Create()
        {
            if (_removedComponentsCount > 0)
            {
                _removedComponentsCount--;
                
                return new ComponentRef<T>(
                    ref _components[_removedComponentsIndices[_removedComponentsCount]],
                    _removedComponentsIndices[_removedComponentsCount]);
            }

            if (_components.Length == _count)
                Array.Resize(ref _components, _components.Length + _expand);

            _components[_count] = new T();

            return new ComponentRef<T>(ref _components[_count], _count++);
        }

        public void Remove(int index)
        {
#if DEBUG
            if (index < 0 || index >= _components.Length || index >= _count)
                throw new ArgumentException($"Invalid index = {index}. ComponentsArrayLength = {_components.Length}, ComponentsCount = {_count}");
#endif

            if (_removedComponentsCount == _removedComponentsIndices.Length)
                Array.Resize(ref _removedComponentsIndices, _removedComponentsIndices.Length + _expand);

            _removedComponentsIndices[_removedComponentsCount++] = index;
            ref var component = ref _components[index];
            component.Clear();
        }
    }
}

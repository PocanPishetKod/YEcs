using System.Runtime.CompilerServices;
using SlowlyEcs.Common;
using SlowlyEcs.Interfaces.Historicity;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs.Storaging;

internal class ComponentStorage<T> : IComponentStorage<T> where T : struct
{
    private readonly IWorldHistory _worldHistory;
    private readonly ComponentTypeId _componentTypeId;
    
    private readonly int _expand;

    private readonly T[] _components;
    private int _count;

    private readonly int[] _removedComponentsIndices;
    private int _removedComponentsCount;

    private readonly int[] _entityComponentLinks;

    public ref T this[int index] => ref _components[index];
        
    public ComponentStorage(int capacity, int expand, IWorldHistory worldHistory, ComponentTypeId componentTypeId)
    {
        _expand = expand;
        _worldHistory = worldHistory;
        _componentTypeId = componentTypeId;
        _components = new T[capacity];
        _count = 0;
        _removedComponentsIndices = new int[capacity];
        _removedComponentsCount = 0;
        _entityComponentLinks = new int[capacity];
    }

    public ref T Get(int entityIndex)
    {
#if DEBUG
            if (entityIndex < 0 || entityIndex >= _entityComponentLinks.Length)
                throw new ArgumentException(
                    $"Invalid entity index = {entityIndex}. EntityComponentLinksLength = {_entityComponentLinks.Length}");
#endif

        return ref _components[_entityComponentLinks[entityIndex]];
    }

    public ComponentRef<T> Create(int entityIndex)
    {
        if (_removedComponentsCount > 0)
        {
            _removedComponentsCount--;
            var index = _removedComponentsIndices[_removedComponentsCount];
                
            CreateLink(entityIndex, index);
                
            return new ComponentRef<T>(ref _components[index], index);
        }

        _components.ResizeIfNeeded(_count, _expand);
            
        CreateLink(entityIndex, _count);
            
        _worldHistory.Push(WorldEvent.CreatedComponentEvent(entityIndex, _componentTypeId));
        
        return new ComponentRef<T>(ref _components[_count], _count++);
    }

    public void Remove(int entityIndex)
    {
#if DEBUG
            if (entityIndex < 0 || entityIndex >= _entityComponentLinks.Length)
                throw new ArgumentException(
                    $"Invalid entity index = {entityIndex}. EntityComponentLinksLength = {_entityComponentLinks.Length}");
#endif
            
        RemoveCore(_entityComponentLinks[entityIndex]);
        RemoveLink(entityIndex);
        
        _worldHistory.Push(WorldEvent.RemovedComponentEvent(entityIndex, _componentTypeId));
    }

    public bool HasComponent(int entityIndex)
    {
        return entityIndex < _entityComponentLinks.Length && entityIndex >= 0 &&
               _entityComponentLinks[entityIndex] != -1;
    }

    private void RemoveCore(int index)
    {
        _removedComponentsIndices.ResizeIfNeeded(_removedComponentsCount, _expand);

        _removedComponentsIndices[_removedComponentsCount++] = index;
        _components[index] = new T();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CreateLink(int entityIndex, int componentIndex)
    {
        _entityComponentLinks.ResizeIfNeeded(entityIndex);
            
        _entityComponentLinks[entityIndex] = componentIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RemoveLink(int entityIndex)
    {
        _entityComponentLinks[entityIndex] = -1;
    }
}
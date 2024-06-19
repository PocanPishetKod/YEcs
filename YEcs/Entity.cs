using YEcs.Interface;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs;

public readonly struct Entity : IEntity<Archetype>, IReusable
{
    private readonly IComponentStorageFactory _componentStorageFactory;
    private readonly Dictionary<Type, int> _componentKeys;
    private readonly IWorldHistory _worldHistory;

    public int Index { get; }

    public Archetype Archetype => new(_componentKeys.Keys);

    public Entity(int index, IComponentStorageFactory componentStorageFactory, IWorldHistory worldHistory)
    {
        _componentStorageFactory = componentStorageFactory;
        _componentKeys = new Dictionary<Type, int>();
        Index = index;
        _worldHistory = worldHistory;
    }

    public bool HasComponent<TComponent>() where TComponent : struct, IReusable
    {
        var searchType = typeof(TComponent);

        foreach (var entityComponentType in _componentKeys.Keys)
        {
            if (entityComponentType == searchType)
                return true;
        }

        return false;
    }

    public ref TComponent CreateComponent<TComponent>() where TComponent : struct, IReusable
    {
        var componentType = typeof(TComponent);
#if DEBUG
        if (_componentKeys.Keys.Contains(componentType))
            throw new InvalidOperationException("Component already exists on entity.");
#endif
        
        var componentRef = _componentStorageFactory.Get<TComponent>().Create();
        _componentKeys.Add(componentType, componentRef.Key);

        var archetype = Archetype;
        _worldHistory.Push(WorldEvent
            .NewEntityArchetypeChangedEvent(Index, archetype, archetype.Add(componentType) ));
        
        return ref componentRef.Component;
    }

    public ref TComponent GetComponent<TComponent>() where TComponent : struct, IReusable
    {
#if DEBUG
        if (!_componentKeys.Keys.Contains(typeof(TComponent)))
            throw new InvalidOperationException($"Component {typeof(TComponent).Name} not found.");
#endif

        return ref _componentStorageFactory
            .Get<TComponent>()[_componentKeys[typeof(TComponent)]];
    }

    public void RemoveComponent<TComponent>() where TComponent : struct, IReusable
    {
        var componentType = typeof(TComponent);
        if (!_componentKeys.Remove(componentType, out var componentKey))
            return;

        _componentStorageFactory
            .Get<TComponent>()
            .Remove(componentKey);

        var archetype = Archetype;
        _worldHistory.Push(WorldEvent
            .NewEntityArchetypeChangedEvent(Index, archetype, archetype.Subtract(componentType)));
    }

    public void Clear()
    {
        foreach (var componentMapElement in _componentKeys)
        {
            (_componentStorageFactory
                .Get(componentMapElement.Key) as IComponentStorage)
                !.Remove(componentMapElement.Value);
        }

        _componentKeys.Clear();
    }
}

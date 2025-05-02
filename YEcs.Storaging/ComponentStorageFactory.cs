using System.Runtime.CompilerServices;
using YEcs.EntitiesFiltering;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs.Storaging;

public class ComponentStorageFactory : IComponentStorageFactory
{
    private readonly IWorldHistory _worldHistory;
    private readonly IComponentTypeIdProvider _componentTypeIdProvider;
    private readonly Dictionary<Type, object> _storagesMap;
    private readonly int _capacity;
    private readonly int _expand;

    public ComponentStorageFactory(int capacity, int expand, IWorldHistory worldHistory,
        IComponentTypeIdProvider componentTypeIdProvider)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        if (expand <= 0)
            throw new ArgumentOutOfRangeException(nameof(expand));
        
        _storagesMap = new Dictionary<Type, object>();
        _capacity = capacity;
        _expand = expand;
        _worldHistory = worldHistory ?? throw new ArgumentNullException(nameof(worldHistory));
        _componentTypeIdProvider =
            componentTypeIdProvider ?? throw new ArgumentNullException(nameof(componentTypeIdProvider));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComponentStorage<TComponent> CreateStorage<TComponent>() where TComponent : struct
    {
        var storage = new ComponentStorage<TComponent>(_capacity, _expand, _worldHistory,
            _componentTypeIdProvider.Get<TComponent>());
        
        _storagesMap.Add(typeof(TComponent), storage);
        
        return storage;
    }

    public IComponentStorage<TComponent> Get<TComponent>() where TComponent : struct
    {
        if (_storagesMap.TryGetValue(typeof(TComponent), out var storage))
        {
            return (IComponentStorage<TComponent>)storage;
        }

        return CreateStorage<TComponent>();
    }

    public object? Get(Type componentType)
    {
        _storagesMap.TryGetValue(componentType, out var storage);
        return storage;
    }
}

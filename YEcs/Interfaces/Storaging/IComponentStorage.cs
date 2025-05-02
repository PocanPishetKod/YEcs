namespace YEcs.Interfaces.Storaging;

public ref struct ComponentRef<TComponent> where TComponent : struct
{
    public readonly ref TComponent Component;
    public readonly int Key;

    public ComponentRef(ref TComponent component, int key)
    {
        Key = key;
        Component = ref component;
    }
}

public interface IComponentStorage<TComponent> where TComponent : struct
{
    ref TComponent this[int index] { get; }

    ref TComponent Get(int entityIndex);
    
    ComponentRef<TComponent> Create(int entityIndex);
    
    void Remove(int entityIndex);
    
    bool HasComponent(int entityIndex);
}
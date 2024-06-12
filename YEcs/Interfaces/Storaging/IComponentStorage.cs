using YEcs.Interface;

namespace YEcs.Interfaces.Storaging;

public ref struct ComponentRef<TComponent> where TComponent : struct, IReusable
{
    public readonly ref TComponent Component;
    public readonly int Key;

    public ComponentRef(ref TComponent component, int key)
    {
        Key = key;
        Component = ref component;
    }
}

public interface IComponentStorage
{
    void Remove(int key);
}

public interface IComponentStorage<TComponent> : IComponentStorage
    where TComponent : struct, IReusable
{
    ref TComponent this[int key] { get; }

    ComponentRef<TComponent> Create();
}
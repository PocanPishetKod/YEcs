using YEcs.Interface;

namespace YEcs.Interfaces.Storaging;

public interface IComponentStorageFactory
{
    IComponentStorage<TComponent> Get<TComponent>() where TComponent : struct, IReusable;

    object? Get(Type componentType);
}
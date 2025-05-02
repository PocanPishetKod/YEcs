namespace SlowlyEcs.Interfaces.Storaging;

public interface IComponentStorageFactory
{
    IComponentStorage<TComponent> Get<TComponent>() where TComponent : struct;

    object? Get(Type componentType);
}
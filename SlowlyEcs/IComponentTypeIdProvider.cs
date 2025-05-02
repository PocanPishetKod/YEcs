namespace SlowlyEcs.EntitiesFiltering;

public interface IComponentTypeIdProvider
{
    ComponentTypeId Get<TComponent>() where TComponent : struct;
}
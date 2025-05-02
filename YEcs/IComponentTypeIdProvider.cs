namespace YEcs.EntitiesFiltering;

public interface IComponentTypeIdProvider
{
    ComponentTypeId Get<TComponent>() where TComponent : struct;
}
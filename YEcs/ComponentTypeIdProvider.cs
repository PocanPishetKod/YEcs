namespace YEcs.EntitiesFiltering;

public class ComponentTypeIdProvider : IComponentTypeIdProvider
{
    public ComponentTypeId Get<TComponent>() where TComponent : struct
    {
        var type = typeof(TComponent);
        return new ComponentTypeId(type.Name.GetHashCode(), type);
    }
}
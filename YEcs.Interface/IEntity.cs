namespace YEcs.Interface;

public interface IEntity<out TArchetype> where TArchetype : struct, IArchetype
{
    public TArchetype Archetype { get; }

    public ref TComponent CreateComponent<TComponent>() where TComponent : struct, IReusable;

    public ref TComponent GetComponent<TComponent>() where TComponent : struct, IReusable;

    public void RemoveComponent<TComponent>() where TComponent : struct, IReusable;
}
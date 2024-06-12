namespace YEcs.Interface;

public interface IEntityFilterBuilder<TEntity, TArchetype> 
    where TEntity : struct, IEntity<TArchetype>
    where TArchetype : struct, IArchetype
{
    public IEntityFilterBuilder<TEntity, TArchetype> With<TComponent>() where TComponent : struct, IReusable;

    public IEntityFilterBuilder<TEntity, TArchetype> Except<TComponent>() where TComponent : struct, IReusable;

    public IEntityFilter<TEntity, TArchetype> Build();
}
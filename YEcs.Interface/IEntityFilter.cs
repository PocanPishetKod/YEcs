namespace YEcs.Interface;

public interface IEntityFilter<TEntity, TArchetype> : IEnumerable<TEntity> 
    where TEntity : struct, IEntity<TArchetype> 
    where TArchetype : struct, IArchetype
{
    int Count { get; }
        
    ref TEntity this[int index] { get; }
}
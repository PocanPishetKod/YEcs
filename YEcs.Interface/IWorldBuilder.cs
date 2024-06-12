namespace YEcs.Interface;

public interface IWorldBuilder<TEntity, TArchetype> 
    where TEntity : struct, IEntity<TArchetype>
    where TArchetype : struct, IArchetype
{
    /// <summary>
    /// Adds the update system to the end of the world update system execution queue.
    /// </summary>
    /// <param name="system">Update system</param>
    /// <returns></returns>
    IWorldBuilder<TEntity, TArchetype> WithUpdateSystem(IUpdateSystem system);
    
    /// <summary>
    /// Adds the initialization system to the end of the world initialization system execution queue.
    /// </summary>
    /// <param name="system">InitializeSystem</param>
    /// <returns></returns>
    IWorldBuilder<TEntity, TArchetype> WithInitializeSystem(IInitializationSystem system);

    /// <summary>
    /// Creates a world.
    /// </summary>
    /// <returns></returns>
    IWorld<TEntity, TArchetype> Build();
}
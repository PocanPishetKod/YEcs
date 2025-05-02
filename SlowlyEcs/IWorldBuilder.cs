namespace SlowlyEcs;

public interface IWorldBuilder
{
    /// <summary>
    /// Adds the update system to the end of the world update system execution queue.
    /// </summary>
    /// <param name="system">Update system</param>
    /// <returns></returns>
    IWorldBuilder WithUpdateSystem(IUpdateSystem system);
    
    /// <summary>
    /// Adds the initialization system to the end of the world initialization system execution queue.
    /// </summary>
    /// <param name="system">InitializeSystem</param>
    /// <returns></returns>
    IWorldBuilder WithInitializeSystem(IInitializationSystem system);

    /// <summary>
    /// Creates a world.
    /// </summary>
    /// <returns></returns>
    IWorld Build();
}
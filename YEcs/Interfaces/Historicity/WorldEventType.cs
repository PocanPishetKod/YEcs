namespace YEcs.Interfaces.Historicity;

/// <summary>
/// World event type.
/// </summary>
public enum WorldEventType : byte
{
    /// <summary>
    /// A new entity was created.
    /// </summary>
    EntityCreated,
    
    /// <summary>
    /// An entity has been destroyed.
    /// </summary>
    EntityDestroyed,
    
    /// <summary>
    /// A component was created in the entity.
    /// </summary>
    EntityComponentCreated,
    
    /// <summary>
    /// A component has been removed from an entity.
    /// </summary>
    EntityComponentRemoved
}
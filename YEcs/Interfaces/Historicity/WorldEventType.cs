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
    /// Component created.
    /// </summary>
    ComponentCreated,
    
    /// <summary>
    /// Component removed.
    /// </summary>
    ComponentRemoved,
}
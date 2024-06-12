using System.Runtime.CompilerServices;

namespace YEcs.Interfaces.Historicity;

/// <summary>
/// World event.
/// </summary>
public readonly struct WorldEvent
{
    /// <summary>
    /// Event type.
    /// </summary>
    public readonly WorldEventType Type;
    
    /// <summary>
    /// Index of changed entity.
    /// </summary>
    public readonly int EntityIndex;
    
    /// <summary>
    /// The type of component removed or added.
    /// Can be null if the event is not related to the deletion or addition of a component.
    /// </summary>
    public readonly Type? TargetComponentType;

    private WorldEvent(WorldEventType type, int entityIndex, Type? targetComponentType)
    {
        Type = type;
        EntityIndex = entityIndex;
        TargetComponentType = targetComponentType;
    }
    
    private WorldEvent(WorldEventType type, int entityIndex)
    {
        Type = type;
        EntityIndex = entityIndex;
        TargetComponentType = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityCreatedEvent(int entityIndex)
    {
        return new WorldEvent(WorldEventType.EntityCreated, entityIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityDestroyedEvent(int entityIndex)
    {
        return new WorldEvent(WorldEventType.EntityDestroyed, entityIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityComponentCreatedEvent(int entityIndex, Type componentType)
    {
        return new WorldEvent(WorldEventType.EntityComponentCreated, entityIndex, componentType);
    }

    public static WorldEvent NewEntityComponentRemovedEvent(int entityIndex, Type componentType)
    {
        return new WorldEvent(WorldEventType.EntityComponentRemoved, entityIndex, componentType);
    }
}
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
    /// Added or removed component id.
    /// </summary>
    public readonly ComponentTypeId ComponentTypeId;

    private WorldEvent(WorldEventType type, int entityIndex, ComponentTypeId componentTypeId)
    {
        Type = type;
        EntityIndex = entityIndex;
        ComponentTypeId = componentTypeId;
    }
    
    private WorldEvent(WorldEventType type, int entityIndex)
    {
        Type = type;
        EntityIndex = entityIndex;
        ComponentTypeId = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent EntityCreatedEvent(int entityIndex)
    {
        return new WorldEvent(WorldEventType.EntityCreated, entityIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent EntityDestroyedEvent(int entityIndex)
    {
        return new WorldEvent(WorldEventType.EntityDestroyed, entityIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent CreatedComponentEvent(int entityIndex, ComponentTypeId componentTypeId)
    {
        return new WorldEvent(WorldEventType.ComponentCreated, entityIndex, componentTypeId);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent RemovedComponentEvent(int entityIndex, ComponentTypeId componentTypeId)
    {
        return new WorldEvent(WorldEventType.ComponentRemoved, entityIndex, componentTypeId);
    }
}
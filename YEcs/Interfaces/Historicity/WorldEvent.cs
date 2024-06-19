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
    /// The previous archetype of entity.
    /// Can be null if the event is not related to the deletion or addition of a component.
    /// </summary>
    public readonly Archetype? PreviousArchetype;

    /// <summary>
    /// The new archetype of entity.
    /// Can be null if the event is not related to the deletion or addition of a component.
    /// </summary>
    public readonly Archetype? NewArchetype;

    private WorldEvent(WorldEventType type, int entityIndex, Archetype newArchetype, Archetype previousArchetype)
    {
        Type = type;
        EntityIndex = entityIndex;
        PreviousArchetype = previousArchetype;
        NewArchetype = newArchetype;
    }
    
    private WorldEvent(WorldEventType type, int entityIndex, Archetype archetype)
    {
        Type = type;
        EntityIndex = entityIndex;
        PreviousArchetype = archetype;
    }
    
    private WorldEvent(WorldEventType type, int entityIndex)
    {
        Type = type;
        EntityIndex = entityIndex;
        PreviousArchetype = null;
        NewArchetype = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityCreatedEvent(int entityIndex)
    {
        return new WorldEvent(WorldEventType.EntityCreated, entityIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityDestroyedEvent(int entityIndex, Archetype archetype)
    {
        return new WorldEvent(WorldEventType.EntityDestroyed, entityIndex, archetype);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static WorldEvent NewEntityArchetypeChangedEvent(int entityIndex, Archetype newArchetype,
        Archetype previousArchetype)
    {
        return new WorldEvent(WorldEventType.EntityArchetypeChanged, entityIndex, newArchetype, previousArchetype);
    }
}
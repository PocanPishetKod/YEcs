namespace YEcs.Interfaces.EntitiesFiltering;

public interface IEntityRemovedHandler
{
    void OnEntityRemoved(ref Archetype entityArchetype, int entityIndex);
}
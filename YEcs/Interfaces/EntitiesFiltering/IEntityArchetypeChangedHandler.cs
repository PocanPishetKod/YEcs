namespace YEcs.Interfaces.EntitiesFiltering;

public interface IEntityArchetypeChangedHandler
{
    void OnArchetypeChanged(ref Archetype oldArchetype, ref Archetype newArchetype, int entityIndex);
}
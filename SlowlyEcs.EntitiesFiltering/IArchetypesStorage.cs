namespace SlowlyEcs.EntitiesFiltering;

public interface IArchetypesStorage
{
    ref Archetype Get(int entityIndex, bool reset = false);
}
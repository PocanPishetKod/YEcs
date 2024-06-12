namespace YEcs.Interfaces.EntitiesFiltering;

public interface IEntityCreatedHandler
{
    void OnEntityCreated(ref Entity entity);
}
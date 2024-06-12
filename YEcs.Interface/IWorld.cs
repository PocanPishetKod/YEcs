namespace YEcs.Interface;

public interface IWorld<TEntity, TArchetype> 
    where TEntity : struct, IEntity<TArchetype>
    where TArchetype : struct, IArchetype
{
    /// <summary>
    /// Move the world to the next state.
    /// </summary>
    /// <param name="deltaTime"></param>
    void Update(float deltaTime);

    /// <summary>
    /// Initializes the world.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Creates a new entity in the world.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ref TEntity CreateEntity();

    /// <summary>
    /// Destroys the entity. Related components will also be destroyed.
    /// </summary>
    /// <param name="entity"></param>
    void DestroyEntity(ref TEntity entity);

    /// <summary>
    /// Creates filter builder.
    /// </summary>
    /// <returns></returns>
    IEntityFilterBuilder<TEntity, TArchetype> CreateFilterBuilder();

    /// <summary>
    /// Updates all filters.
    /// </summary>
    void UpdateFilters();
}
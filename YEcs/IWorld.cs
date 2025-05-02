using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Storaging;

namespace YEcs;

public interface IWorld
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
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    ref Entity CreateEntity();

    /// <summary>
    /// Destroys the entity. Related components will also be destroyed.
    /// </summary>
    /// <param name="entity"></param>
    void DestroyEntity(ref Entity entity);
    
    /// <summary>
    /// Get component storage.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    IComponentStorage<TComponent> GetComponentStorage<TComponent>() where TComponent : struct;

    /// <summary>
    /// Creates filter builder.
    /// </summary>
    /// <returns></returns>
    IEntityFilterBuilder CreateFilterBuilder();

    /// <summary>
    /// Updates all filters.
    /// </summary>
    void UpdateFilters();

    /// <summary>
    /// Create scope of changes. After exiting the scope, the filters are updated.
    /// </summary>
    /// <returns></returns>
    ChangeScope CreateChangeScope();
}
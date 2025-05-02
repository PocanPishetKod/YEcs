using SlowlyEcs.Interfaces.EntitiesFiltering;
using SlowlyEcs.Interfaces.Historicity;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs
{
    public class World : IWorld
    {
        private readonly List<IInitializationSystem> _initializationSystems;
        private readonly List<IUpdateSystem> _updateSystems;
        private readonly IEntitiesStorage _entitiesStorage;
        private readonly IEntityFiltersBuilderFactory _entityFiltersBuilderFactory;
        private readonly IFiltersUpdater _filtersUpdater;
        private readonly IWorldHistory _worldHistory;
        private readonly IComponentStorageFactory _componentStorageFactory;

        public World(IEntitiesStorage entitiesStorage,
            IEntityFiltersBuilderFactory entityFiltersBuilderFactory,
            IFiltersUpdater filtersUpdater,
            IWorldHistory worldHistory,
            IComponentStorageFactory componentStorageFactory)
        {
            _initializationSystems = new List<IInitializationSystem>();
            _updateSystems = new List<IUpdateSystem>();
            _entitiesStorage = entitiesStorage ?? throw new ArgumentNullException(nameof(entitiesStorage));
            _entityFiltersBuilderFactory = entityFiltersBuilderFactory ??
                                           throw new ArgumentNullException(nameof(entityFiltersBuilderFactory));
            _filtersUpdater = filtersUpdater ?? throw new ArgumentNullException(nameof(filtersUpdater));
            _worldHistory = worldHistory ?? throw new ArgumentNullException(nameof(worldHistory));
            _componentStorageFactory = componentStorageFactory ??
                                       throw new ArgumentNullException(nameof(componentStorageFactory));
        }

        public void Initialize()
        {
            foreach (var system in _initializationSystems)
            {
                system.Execute();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _updateSystems)
            {
                system.Execute(deltaTime);
            }
        }

        internal void AddInitializationSystem(IInitializationSystem system)
        {
            ArgumentNullException.ThrowIfNull(system);

            _initializationSystems.Add(system);
        }

        internal void AddUpdateSystem(IUpdateSystem system)
        {
            ArgumentNullException.ThrowIfNull(system);

            _updateSystems.Add(system);
        }

        public ref Entity CreateEntity()
        {
            ref var entity = ref _entitiesStorage.Create();
            
            _worldHistory.Push(WorldEvent.EntityCreatedEvent(entity.Index));
            
            return ref entity;
        }

        public IComponentStorage<TComponent> GetComponentStorage<TComponent>() where TComponent : struct
        {
            return _componentStorageFactory.Get<TComponent>();
        }

        public IEntityFilterBuilder CreateFilterBuilder()
        {
            return _entityFiltersBuilderFactory.Create();
        }

        public void UpdateFilters()
        {
            _filtersUpdater.Update(_worldHistory);
            _worldHistory.Clear();
        }

        public void DestroyEntity(ref Entity entity)
        {
            _entitiesStorage.Remove(entity.Index);
            _worldHistory.Push(WorldEvent.EntityDestroyedEvent(entity.Index));
        }

        public ChangeScope CreateChangeScope()
        {
            return new ChangeScope(this);
        }
    }
}

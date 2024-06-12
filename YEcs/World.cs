using YEcs.Interface;
using YEcs.Interfaces.EntitiesFiltering;
using YEcs.Interfaces.Historicity;
using YEcs.Interfaces.Storaging;

namespace YEcs
{
    public class World : IWorld<Entity, Archetype>
    {
        private readonly List<IInitializationSystem> _initializationSystems;
        private readonly List<IUpdateSystem> _updateSystems;
        private readonly IEntitiesStorage _entitiesStorage;
        private readonly IEntityFiltersBuilderFactory _entityFiltersBuilderFactory;
        private readonly IFiltersUpdater _filtersUpdater;
        private readonly IWorldHistory _worldHistory;

        public int EntitiesCount => _entitiesStorage.Count;

        public World(
            IEntitiesStorage entitiesStorage,
            IEntityFiltersBuilderFactory entityFiltersBuilderFactory,
            IFiltersUpdater filtersUpdater, IWorldHistory worldHistory)
        {
            _initializationSystems = new List<IInitializationSystem>();
            _updateSystems = new List<IUpdateSystem>();
            _entitiesStorage = entitiesStorage ?? throw new ArgumentNullException(nameof(entitiesStorage));
            _entityFiltersBuilderFactory = entityFiltersBuilderFactory ??
                                           throw new ArgumentNullException(nameof(entityFiltersBuilderFactory));
            _filtersUpdater = filtersUpdater ?? throw new ArgumentNullException(nameof(filtersUpdater));
            _worldHistory = worldHistory ?? throw new ArgumentNullException(nameof(worldHistory));
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
            
            _worldHistory.Push(WorldEvent.NewEntityCreatedEvent(entity.Index));
            
            return ref entity;
        }

        public IEntityFilterBuilder<Entity, Archetype> CreateFilterBuilder()
        {
            return _entityFiltersBuilderFactory.Create();
        }

        public void UpdateFilters()
        {
            _filtersUpdater.Update(_worldHistory);
        }

        public void DestroyEntity(ref Entity entity)
        {
            _entitiesStorage.Remove(ref entity);
            _worldHistory.Push(WorldEvent.NewEntityDestroyedEvent(entity.Index));
        }
    }
}

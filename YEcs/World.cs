using System;
using System.Collections.Generic;

namespace YEcs
{
    public class World
    {
        private readonly List<IInitializationSystem> _initializationSystems;
        private readonly List<IUpdateSystem> _updateSystems;
        private readonly ComponentStoragesManager _componentStoragesManager;
        private readonly EntityStorage _entityStorage;
        private readonly EntityFiltersManager _entityFiltersManager;

        public int EntitiesCount => _entityStorage.Count;

        public World()
        {
            _initializationSystems = new List<IInitializationSystem>();
            _updateSystems = new List<IUpdateSystem>();
            _componentStoragesManager = new ComponentStoragesManager();
            _entityStorage = new EntityStorage(_componentStoragesManager, this);
            _entityFiltersManager = new EntityFiltersManager(_entityStorage);
        }

        public void Initialize()
        {
            foreach (var system in _initializationSystems)
            {
                system.Process();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _updateSystems)
            {
                system.Process(deltaTime);
            }
        }

        public World AddInitializationSystem(IInitializationSystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            _initializationSystems.Add(system);
            return this;
        }

        public World AddUpdateSystem(IUpdateSystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            _updateSystems.Add(system);
            return this;
        }

        public ref Entity CreateEntity()
        {
            return ref _entityStorage.CreateEntity();
        }

        public EntityFilter GetEntityFilter(params Type[] componentTypes)
        {
            return _entityFiltersManager.GetFilter(componentTypes);
        }

        public void RemoveEntity(ref Entity entity)
        {
            _entityStorage.RemoveEntity(ref entity);
        }

        internal void UpdateFilters(in Entity entity, int componentTypeId, bool isAdd)
        {
            _entityFiltersManager.UpdateFilters(entity, componentTypeId, isAdd);
        }

        internal void OnEntityRemoved(int index)
        {
            _entityFiltersManager.OnEntityRemoved(index);
        }
    }
}

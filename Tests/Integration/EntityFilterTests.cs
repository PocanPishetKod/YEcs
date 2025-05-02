using SlowlyEcs;

namespace Tests.Integration
{
    public struct TestPositionComponent
    {
        
    }

    public struct TestInputComponent
    {
        
    }

    public class EntityFilterTests
    {
        [Fact]
        public void BuildFilter_OneWith_OneComponent_FilterWithSingle()
        {
            var world = Common.CreateWorldBuilder().Build();
            var filter = world.CreateFilterBuilder()
                .With<TestInputComponent>()
                .Build();
            var compStorage = world.GetComponentStorage<TestInputComponent>();
            
            using (var scope = world.CreateChangeScope())
            {
                ref var entity = ref world.CreateEntity();
                compStorage.Create(entity.Index);
            }
            
            var indicesInFilter = filter.ToList();
            
            Assert.Single(indicesInFilter);
        }
        
        [Fact]
        public void BuildFilter_OneComponent_ReturnSingleEntityFilter()
        {
            var world = Common.CreateWorldBuilder().Build();
            var filter = world.CreateFilterBuilder()
                .With<TestInputComponent>()
                .Build();

            var posStorage = world.GetComponentStorage<TestPositionComponent>();
            var inputStorage = world.GetComponentStorage<TestInputComponent>();
            
            ref var entity = ref world.CreateEntity();

            posStorage.Create(entity.Index);
            inputStorage.Create(entity.Index);

            world.UpdateFilters();
            
            var indicesInFilter = filter.ToList();
            
            Assert.Single(indicesInFilter);
            Assert.Equal(entity.Index, indicesInFilter[0]);
        }

        [Fact]
        public void BuildFilter_ExceptOneComponentAndOneWith_ReturnEmptyFilter()
        {
            var world = Common.CreateWorldBuilder().Build();
            var filter = world.CreateFilterBuilder()
                .With<TestInputComponent>()
                .Except<TestPositionComponent>()
                .Build();
            
            ref var entity = ref world.CreateEntity();
            
            var posStorage = world.GetComponentStorage<TestPositionComponent>();
            var inputStorage = world.GetComponentStorage<TestInputComponent>();

            posStorage.Create(entity.Index);
            inputStorage.Create(entity.Index);

            world.UpdateFilters();
            
            Assert.Equal(0, filter.Count);
        }

        [Fact]
        public void BuildFilter_ExceptOtherComponent_ReturnSingleFiler()
        {
            var world = Common.CreateWorldBuilder().Build();
            var filter = world.CreateFilterBuilder()
                .Except<TestPositionComponent>()
                .Build();
            
            ref var entity = ref world.CreateEntity();
            
            var inputStorage = world.GetComponentStorage<TestInputComponent>();
            inputStorage.Create(entity.Index);
            
            world.UpdateFilters();
            
            Assert.Equal(1, filter.Count);
            Assert.Equal(entity.Index, filter.First());
        }

        [Fact]
        public void BuildFilter_ExceptEqualComponent_ReturnEmptyFilter()
        {
            var world = Common.CreateWorldBuilder().Build();
            var filter = world.CreateFilterBuilder()
                .Except<TestPositionComponent>()
                .Build();
            
            ref var entity = ref world.CreateEntity();
            
            var posStorage = world.GetComponentStorage<TestPositionComponent>();
            posStorage.Create(entity.Index);

            world.UpdateFilters();
            
            Assert.Equal(0, filter.Count);
        }
    }
}

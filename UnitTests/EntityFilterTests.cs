using YEcs;
using YEcs.Interface;

namespace UnitTests
{
    public struct TestPositionComponent : IReusable
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }

    public struct TestInputComponent : IReusable
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }

    public class EntityFilterTests
    {
        [Fact]
        public void BuildFilter_OneComponent_ReturnSingleEntityFilter()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();
            entity.CreateComponent<TestPositionComponent>();
            entity.CreateComponent<TestInputComponent>();

            var filter = world.CreateFilterBuilder()
                .With<TestInputComponent>()
                .Build();
            
            ref var filteredEntity = ref filter[0];
            
            Assert.Equal(1, filter.Count);
            Assert.Equal(entity.Index, filteredEntity.Index);
        }

        [Fact]
        public void BuildFilter_ExceptOneComponentAndOneWith_ReturnEmptyFilter()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();
            entity.CreateComponent<TestPositionComponent>();
            entity.CreateComponent<TestInputComponent>();

            var filter = world.CreateFilterBuilder()
                .With<TestInputComponent>()
                .Except<TestPositionComponent>()
                .Build();

            Assert.Equal(0, filter.Count);
        }

        [Fact]
        public void BuildFilter_ExceptOtherComponent_ReturnSingleFiler()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();
            entity.CreateComponent<TestInputComponent>();

            var filter = world.CreateFilterBuilder()
                .Except<TestPositionComponent>()
                .Build();
            
            Assert.Equal(1, filter.Count);
            Assert.Equal(entity.Index, filter[0].Index);
        }

        [Fact]
        public void BuildFilter_ExceptEqualComponent_ReturnEmptyFilter()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();
            entity.CreateComponent<TestPositionComponent>();

            var filter = world.CreateFilterBuilder()
                .Except<TestPositionComponent>()
                .Build();

            Assert.Equal(0, filter.Count);
        }
    }
}

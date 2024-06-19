using YEcs;
using YEcs.EntitiesFiltering;
using YEcs.EntitiesFiltering.Updating;
using YEcs.Historicity;
using YEcs.Storaging;

namespace Tests.Integration
{
    public class WorldTests
    {
        [Fact]
        public void CreateOneEntity_HavingSingleEntity()
        {
            var world = Common.CreateWorldBuilder().Build();
            var emptyFilter = world.CreateFilterBuilder().Build();
            
            ref var entity = ref world.CreateEntity();
            world.UpdateFilters();
            
            Assert.Equal(1, emptyFilter.Count);
        }
        
        [Fact]
        public void DestroyEntity_HavingZeroEntities()
        {
            var world = Common.CreateWorldBuilder().Build();
            var emptyFilter = world.CreateFilterBuilder().Build();
            
            ref var entity = ref world.CreateEntity();
            world.DestroyEntity(ref entity);
            world.UpdateFilters();
            
            Assert.Equal(0, emptyFilter.Count);
        }

        [Fact]
        public void RemoveEntity_RemovingEntityTwice_ThrowException()
        {
            var world = Common.CreateWorldBuilder().Build();

            Assert.Throws<InvalidOperationException>(() =>
            {
                ref var entity = ref world.CreateEntity();
                world.DestroyEntity(ref entity);

                world.DestroyEntity(ref entity);
            });
        }
    }
}

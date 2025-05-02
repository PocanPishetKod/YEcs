using SlowlyEcs;
using SlowlyEcs.EntitiesFiltering;
using SlowlyEcs.EntitiesFiltering.Updating;
using SlowlyEcs.Historicity;
using SlowlyEcs.Storaging;

namespace Tests.Integration
{
    public class WorldTests
    {
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

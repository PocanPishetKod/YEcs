using YEcs;

namespace UnitTests
{
    public class WorldTests
    {
        [Fact]
        public void CreateEntity_CreateOneEntityAndRemove_HavingZeroEntities()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();

            Assert.Equal(1, world.EntitiesCount);

            world.DestroyEntity(ref entity);

            Assert.Equal(0, world.EntitiesCount);
        }

        [Fact]
        public void CreateEntity_CreateTwoEntities_SecondEntityEqualsFirstEntity()
        {
            var world = new World();

            ref var firstEntity = ref world.CreateEntity();
            world.DestroyEntity(ref firstEntity);
            ref var secondEntity = ref world.CreateEntity();

            Assert.Equal(firstEntity, secondEntity);
            Assert.False(secondEntity.IsRemoved);
        }

        [Fact]
        public void RemoveEntity_RemovingEntityTwice_ThrowException()
        {
            var world = new World();

            Assert.Throws<InvalidOperationException>(() =>
            {
                ref var entity = ref world.CreateEntity();
                world.DestroyEntity(ref entity);

                world.DestroyEntity(ref entity);
            });
        }
    }
}

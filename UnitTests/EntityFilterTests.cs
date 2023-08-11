using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NoNameGame.Framework.UnitTests
{
    public struct TestPositionComponent
    {
        public int X;
        public int Y;
    }

    public struct TestInputComponent
    {
        public int KeyCode;
    }

    public class EntityFilterTests
    {
        [Fact]
        public void GetFilter_OneComponent_ReturnOneComponentEntityFilter()
        {
            var world = new World();
            ref var entity = ref world.CreateEntity();
            ref var positionComponent = ref entity.CreateComponent<TestPositionComponent>();
            ref var inputComponent = ref entity.CreateComponent<TestInputComponent>();
            
            var filter = world.GetEntityFilter(typeof(TestPositionComponent));

            ref var filteredEntity = ref filter[0];

            Assert.Equal(entity, filteredEntity);
        }
    }
}

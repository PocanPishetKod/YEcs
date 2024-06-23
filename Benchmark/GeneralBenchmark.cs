using BenchmarkDotNet.Attributes;
using YEcs.Interface;

namespace Benchmark;

[MemoryDiagnoser]
public class GeneralBenchmark
{
    [Benchmark]
    public void Create_1_000_000_Entities_And_Components_And_Update_Filters()
    {
        var world = Common.CreateWorldBuilder(100000, 10000).Build();
        var filter = world.CreateFilterBuilder()
            .With<Component>()
            .Build();

        for (var i = 0; i < 1_000_000; i++)
        {
            ref var entity = ref world.CreateEntity();
            entity.CreateComponent<Component>();
        }
        
        world.UpdateFilters();
    }
}

struct Component : IReusable
{
    public long a;
    public long a2;
    public long a3;
    public long a4;
    public long a5;
    public long a6;
    public long a7;
    public long a8;
    public long a9;
    public long a10;
    public long a11;
    public long a12;
    public long a13;
    public long a14;
    public long a15;
    public void Clear()
    {
        throw new NotImplementedException();
    }
}
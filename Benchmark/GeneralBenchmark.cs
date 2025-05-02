using BenchmarkDotNet.Attributes;
using Leopotam.EcsLite;
using Tests.Integration;
using YEcs;
using YEcs.Interfaces.Storaging;

namespace Benchmark;

[MemoryDiagnoser]
public class GeneralBenchmark
{
    private const int _iterations = 1_000_00;
    
    private IWorld _world;
    private IComponentStorage<Component> _componentStorage;
    
    private EcsWorld _ecsWorld;
    private EcsPool<Component> _componentsPool;

    [GlobalSetup]
    public void Setup()
    {
        _world = Common.CreateWorldBuilder(_iterations, 10000).Build();
        _componentStorage = _world.GetComponentStorage<Component>();
        
        _ecsWorld = new EcsWorld(new EcsWorld.Config(){ Entities = _iterations});
        _componentsPool = _ecsWorld.GetPool<Component>();
    }
    
    [Benchmark]
    public void Create_1_000_000_Entities_And_Components_And_Update_Filters()
    {
        //_world = Common.CreateWorldBuilder(1_000_000, 10000).Build();
        //_componentStorage = _world.GetComponentStorage<Component>();
        
        for (var i = 0; i < _iterations; i++)
        {
            ref var entity = ref _world.CreateEntity();
            _componentStorage.Create(entity.Index);
        }
    }

    [Benchmark]
    public void Create_1_000_000_Entities_And_Components_Leo_LiteEcs()
    {
        //_ecsWorld = new EcsWorld(new EcsWorld.Config(){ Entities = 1_000_000});
        //_componentsPool = _ecsWorld.GetPool<Component>();
        
        for (var i = 0; i < _iterations; i++)
        {
            var entityId = _ecsWorld.NewEntity();
            _componentsPool.Add(entityId);
        }
    }
}

struct Component
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
}
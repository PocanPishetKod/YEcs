namespace YEcs;

public readonly ref struct ChangeScope : IDisposable
{
    private readonly World _world;

    public ChangeScope(World world)
    {
        _world = world;
    }

    public void Dispose()
    {
        _world.UpdateFilters();
    }
}
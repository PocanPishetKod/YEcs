namespace SlowlyEcs.Interfaces.EntitiesFiltering;

public interface IEntityFilterBuilder
{
    public IEntityFilterBuilder With<TComponent>() where TComponent : struct;

    public IEntityFilterBuilder Except<TComponent>() where TComponent : struct;

    public IReadOnlyEntityFilter Build();
}
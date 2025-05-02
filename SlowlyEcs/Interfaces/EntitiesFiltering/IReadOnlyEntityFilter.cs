namespace SlowlyEcs.Interfaces.EntitiesFiltering;

public interface IReadOnlyEntityFilter : IEnumerable<int>
{
    int Count { get; }
}
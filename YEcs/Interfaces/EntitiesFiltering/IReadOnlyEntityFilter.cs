namespace YEcs.Interfaces.EntitiesFiltering;

public interface IReadOnlyEntityFilter : IEnumerable<int>
{
    int Count { get; }
}
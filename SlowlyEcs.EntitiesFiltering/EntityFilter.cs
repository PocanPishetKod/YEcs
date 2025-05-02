using System.Collections;
using SlowlyEcs.Interfaces.EntitiesFiltering;
using SlowlyEcs.Interfaces.Storaging;

namespace SlowlyEcs.EntitiesFiltering;

public class EntityFilter : IReadOnlyEntityFilter
{
    private readonly IEntitiesStorage _entitiesStorage;
    private readonly ISet<int> _entityIndices;
    private readonly ArchetypeMask _mask;

    internal EntityFilter(ArchetypeMask mask, IEntitiesStorage entitiesStorage)
    {
        _entitiesStorage = entitiesStorage;
        _entityIndices = new HashSet<int>();
        _mask = mask;
    }

    public int Count => _entityIndices.Count;

    internal bool IsCompatible(in Archetype archetype)
    {
        return _mask.IsCompatible(archetype);
    }

    internal void AddEntity(int index)
    {
        _entityIndices.Add(index);
    }

    internal void RemoveEntity(int index)
    {
        _entityIndices.Remove(index);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return _entityIndices.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

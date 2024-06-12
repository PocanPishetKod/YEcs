using System.Collections;
using YEcs.Interface;
using YEcs.Interfaces.Storaging;

namespace YEcs.EntitiesFiltering;

public class EntityFilter : IEntityFilter<Entity, Archetype>
{
    private readonly IEntitiesStorage _entitiesStorage;
    private readonly List<int> _entityIndices;
    private readonly ArchetypeMask _mask;

    internal EntityFilter(ArchetypeMask mask, IEntitiesStorage entitiesStorage)
    {
        _entitiesStorage = entitiesStorage;
        _entityIndices = new List<int>();
        _mask = mask;
    }

    public int Count => _entityIndices.Count;
        
    public ref Entity this[int index] => ref _entitiesStorage[_entityIndices[index]];

    internal bool IsCompatible(ref Entity entity)
    {
        return _mask.IsCompatible(entity.Archetype);
    }

    internal void AddEntity(int index)
    {
        _entityIndices.Add(index);
    }

    internal void RemoveEntity(int index)
    {
        _entityIndices.Remove(index);
    }

    public IEnumerator<Entity> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

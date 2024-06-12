namespace YEcs.Interfaces.Storaging;

public interface IEntitiesStorage
{
    ref Entity this[int index] { get; }
    
    int Count { get; }

    void Remove(ref Entity entity);

    ref Entity Create();
}
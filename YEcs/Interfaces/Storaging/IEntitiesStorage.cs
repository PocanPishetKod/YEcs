namespace YEcs.Interfaces.Storaging;

public interface IEntitiesStorage
{
    ref Entity this[int entityIndex] { get; }
    
    int Count { get; }
    
    void Remove(ref Entity entity);

    ref Entity Create();
}
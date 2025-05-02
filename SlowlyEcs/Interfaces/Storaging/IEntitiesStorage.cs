namespace SlowlyEcs.Interfaces.Storaging;

public interface IEntitiesStorage
{
    ref Entity this[int entityIndex] { get; }
    
    int Count { get; }
    
    void Remove(int entityIndex);

    ref Entity Create();
}
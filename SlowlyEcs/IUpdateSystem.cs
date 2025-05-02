namespace SlowlyEcs;

public interface IUpdateSystem
{
    void Execute(float deltaTime);
}
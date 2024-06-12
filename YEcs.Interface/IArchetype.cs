namespace YEcs.Interface;

public interface IArchetype
{
    bool Equals<TArchetype>(TArchetype other) where TArchetype : IArchetype;
}
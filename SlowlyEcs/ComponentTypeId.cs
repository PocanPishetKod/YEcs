namespace SlowlyEcs;

public readonly record struct ComponentTypeId
{
    private readonly Type _type;
    private readonly int _value;

    public ComponentTypeId(int value, Type type)
    {
        _value = value;
        _type = type;
    }

    public override string ToString()
    {
        return $"{_type.Name}: {_value}";
    }
}
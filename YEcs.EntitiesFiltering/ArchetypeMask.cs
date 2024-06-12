using System.Diagnostics.CodeAnalysis;
using YEcs.Common;

namespace YEcs.EntitiesFiltering;

public readonly struct ArchetypeMask
{
    private readonly IReadOnlyList<Type> _withComponentTypes;
    private readonly IReadOnlyList<Type> _exceptComponentTypes;

    public ArchetypeMask(IReadOnlyList<Type> withComponentTypes, IReadOnlyList<Type> exceptComponentTypes)
    {
        _withComponentTypes = withComponentTypes;
        _exceptComponentTypes = exceptComponentTypes;
    }

    public override int GetHashCode()
    {
        if (_withComponentTypes.Count > 0 && _exceptComponentTypes.Count > 0)
        {
            return HashCode
                .Combine(
                    HashCodeExtensions.Create(_withComponentTypes).ToHashCode(),
                    HashCodeExtensions.Create(_exceptComponentTypes).ToHashCode());
        }

        if (_withComponentTypes.Count > 0)
            return HashCodeExtensions.Create(_withComponentTypes).ToHashCode();

        if (_exceptComponentTypes.Count > 0)
            return HashCodeExtensions.Create(_exceptComponentTypes).ToHashCode();

        return 0;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ArchetypeMask other)
            return false;

        if (_withComponentTypes.Count != other._withComponentTypes.Count)
            return false;

        if (_exceptComponentTypes.Count != other._exceptComponentTypes.Count)
            return false;

        for (int i = 0; i < _withComponentTypes.Count; i++)
        {
            if (_withComponentTypes[i] != other._withComponentTypes[i])
                return false;
        }

        for (int i = 0; i < _exceptComponentTypes.Count; i++)
        {
            if (_exceptComponentTypes[i] != other._exceptComponentTypes[i])
                return false;
        }

        return true;
    }

    public bool IsCompatible(Archetype archetype)
    {
        for (var i = 0; i < _withComponentTypes.Count; i++)
        {
            if (!archetype.IsCompatible(_withComponentTypes[i]))
                return false;
        }

        for (var i = 0; i < _exceptComponentTypes.Count; i++)
        {
            if (archetype.IsCompatible(_exceptComponentTypes[i]))
                return false;
        }

        return true;
    }

    public static bool operator ==(ArchetypeMask left, ArchetypeMask right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ArchetypeMask left, ArchetypeMask right)
    {
        return !(left == right);
    }
}

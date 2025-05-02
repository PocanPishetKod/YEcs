using System.Diagnostics.CodeAnalysis;
using YEcs.Common;

namespace YEcs.EntitiesFiltering;

public struct ArchetypeMask : IEquatable<ArchetypeMask>
{
    private readonly IReadOnlySet<ComponentTypeId> _withComponentTypes;
    private readonly IReadOnlySet<ComponentTypeId> _exceptComponentTypes;
    private int _lazyHashCode = - 1;

    public ArchetypeMask(IReadOnlySet<ComponentTypeId> withComponentTypes, IReadOnlySet<ComponentTypeId> exceptComponentTypes)
    {
        _withComponentTypes = withComponentTypes;
        _exceptComponentTypes = exceptComponentTypes;
    }

    public readonly bool IsCompatible(in Archetype archetype)
    {
        using var archetypeEnumerator = archetype.GetEnumerator();

        var empty = !archetypeEnumerator.MoveNext();
        
        if (empty && _withComponentTypes.Count > 0)
        {
            return false;
        }

        if (empty)
        {
            return false;
        }

        var withPassCount = 0;
        
        do
        {
            var componentTypeId = archetypeEnumerator.Current;
            
            if (_withComponentTypes.Count != 0 && _withComponentTypes.Contains(componentTypeId))
            {
                withPassCount++;
            }

            if (_exceptComponentTypes.Contains(componentTypeId))
            {
                return false;
            }
            
        } while (archetypeEnumerator.MoveNext());

        return _withComponentTypes.Count == withPassCount;
    }
    
    public override int GetHashCode()
    {
        if (_lazyHashCode != -1)
            return _lazyHashCode;
        
        if (_withComponentTypes.Count > 0 && _exceptComponentTypes.Count > 0)
        {
            _lazyHashCode = HashCode
                .Combine(
                    HashCodeExtensions.Create(_withComponentTypes).ToHashCode(),
                    HashCodeExtensions.Create(_exceptComponentTypes).ToHashCode());

            return _lazyHashCode;
        }

        if (_withComponentTypes.Count > 0)
        {
            _lazyHashCode = HashCodeExtensions.Create(_withComponentTypes).ToHashCode();
            return _lazyHashCode;
        }

        if (_exceptComponentTypes.Count > 0)
        {
            _lazyHashCode = HashCodeExtensions.Create(_exceptComponentTypes).ToHashCode();
            return _lazyHashCode;
        }

        return 0;
    }
    
    public bool Equals(ArchetypeMask other)
    {
        if (_withComponentTypes.Count != other._withComponentTypes.Count)
            return false;

        if (_exceptComponentTypes.Count != other._exceptComponentTypes.Count)
            return false;

        foreach (var componentTypeId in _withComponentTypes)
        {
            if (!other._withComponentTypes.Contains(componentTypeId))
            {
                return false;
            }
        }

        foreach (var componentTypeId in _exceptComponentTypes)
        {
            if (!other._exceptComponentTypes.Contains(componentTypeId))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is ArchetypeMask archetypeMask && Equals(archetypeMask);
    }

    public static bool operator ==(ArchetypeMask left, ArchetypeMask right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ArchetypeMask left, ArchetypeMask right)
    {
        return !left.Equals(right);
    }
}

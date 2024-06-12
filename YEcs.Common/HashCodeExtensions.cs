namespace YEcs.Common;

public static class HashCodeExtensions
{
    private static void AddValues<T>(this HashCode hashCode, IEnumerable<T> values)
    {
        foreach (var componentTypeId in values)
        {
            hashCode.Add(componentTypeId);
        }
    }

    public static HashCode Create<T>(IEnumerable<T> values)
    {
        var result = new HashCode();
        result.AddValues(values);

        return result;
    }
}

namespace YEcs
{
    internal static class HashCodeExtensions
    {
        public static void AddValues(this HashCode hashCode, int[] values)
        {
            foreach (var componentTypeId in values)
            {
                hashCode.Add(componentTypeId);
            }
        }
    }
}

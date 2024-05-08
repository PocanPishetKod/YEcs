namespace YEcs
{
    internal static class HashCodeExtensions
    {
        public static void AddValues(this HashCode hashCode, IReadOnlyCollection<int> values)
        {
            foreach (var componentTypeId in values)
            {
                hashCode.Add(componentTypeId);
            }
        }

        public static HashCode Create(IReadOnlyCollection<int> values)
        {
            var result = new HashCode();
            result.AddValues(values);

            return result;
        }
    }
}

namespace AdventOfCode2023.Helpers
{
    public static class ListExtension
    {
        public static void AddIfNotNull(this List<int> list, int? value)
        {
            if (value != null)
                list.Add((int)value);
        }

        public static List<T> RemoveNulls<T>(this IEnumerable<T?> list) where T : struct
        {
            return list.Where(x => x.HasValue).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets the first item in the list which is not null
        /// </summary>
        public static T FirstNonNullOrDefault<T>(this IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                if (item != null)
                    return item;
            }
            return default;
        }
    }
}

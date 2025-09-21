using System.Collections.ObjectModel;

namespace CacaMantos.Admin.API.Common.Utils
{
    public static class CollectionsUtils
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> collection)
        {
            return !IsNullOrEmpty(collection);
        }

        public static bool IsNullOrEmpty<T>(Collection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(Collection<T> collection)
        {
            return !IsNullOrEmpty(collection);
        }
    }
}

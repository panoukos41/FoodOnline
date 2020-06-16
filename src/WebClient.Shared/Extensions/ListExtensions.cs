using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.WebClient.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null
            || collection.Count() == 0;

        public static bool IsNull<T>(this IEnumerable<T> collection) =>
            collection == null;

        public static bool IsEmpty<T>(this IEnumerable<T> collection) =>
            collection.Count() == 0;
    }
}
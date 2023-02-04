using System.Collections.Generic;

namespace Extensions
{
    public static class Utility
    {
        public static void Shuffle<T>(this T[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                var k = UnityEngine.Random.Range(0, n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        public static void Shuffle<T>(this List<T> array)
        {
            var n = array.Count;
            while (n > 1)
            {
                var k = UnityEngine.Random.Range(0, n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
        public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;
    }
}
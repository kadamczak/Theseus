namespace Theseus.Domain.Extensions
{
    public static class ListExtension
    {
        private static Random rng = new Random();

        public static IList<T> FisherYatesShuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                list.Swap(k, n);
            }
            return list;
        }

        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}

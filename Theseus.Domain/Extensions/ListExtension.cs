namespace Theseus.Domain.Extensions
{
    /// <summary>
    /// Class containing extension methods for Lists.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Randomly shuffle <paramref name="self"/>.
        /// </summary>
        /// <typeparam name="T">Type of objects inside List <paramref name="self"/>.</typeparam>
        /// <param name="self">Container.</param>
        /// <param name="rnd">Random number generator.</param>
        /// <returns>Randomly shuffled <paramref name="self"/>.</returns>
        public static IList<T> FisherYatesShuffle<T>(this IList<T> self, Random rnd)
        {
            int n = self.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                self.Swap(k, n);
            }
            return self;
        }

        /// <summary>
        /// Swaps elements at <paramref name="indexA"/> and <paramref name="indexB"/> in <paramref name="self"/>.
        /// </summary>
        /// <typeparam name="T">Type of objects inside List <paramref name="self"/>.</typeparam>
        /// <param name="self">Container.</param>
        /// <param name="indexA">First index.</param>
        /// <param name="indexB">Second index.</param>
        /// <returns>List with swapped elements.</returns>
        public static IList<T> Swap<T>(this IList<T> self, int indexA, int indexB)
        {
            T tmp = self[indexA];
            self[indexA] = self[indexB];
            self[indexB] = tmp;
            return self;
        }
    }
}
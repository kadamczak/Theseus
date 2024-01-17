namespace Theseus.Domain.Extensions
{
    /// <summary>
    /// The <c>IEnumerableExtension</c> class contains extension methods for IEnumerable.
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Enables foreach with iteration.
        /// </summary>
        /// <typeparam name="T">Type of objects inside IEnumerable <paramref name="self"/>.</typeparam>
        /// <param name="self">Collection.</param>
        /// <returns>Collections items with indexes.</returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
         => self.Select((item, index) => (item, index));

        /// <summary>
        /// Returns random item from <paramref name="self"/>.
        /// </summary>
        /// <typeparam name="T">Type of objects inside IEnumerable <paramref name="self"/>.</typeparam>
        /// <param name="self">Collection.</param>
        /// <param name="rnd">Random number generator.</param>
        /// <returns>Random item from <paramref name="self"/>.</returns>
        public static T GetRandomItem<T>(this IEnumerable<T> self, Random rnd)
        {
            int index = rnd.Next(0, self.Count());
            return self.ElementAt(index);
        }
    }
}
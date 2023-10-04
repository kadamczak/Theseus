namespace Theseus.Domain.Extensions
{
    static class IEnumerableExtension
    {
        public static T GetRandomItem<T>(this IEnumerable<T> source, Random rnd)
        {
            int index = rnd.Next(0, source.Count());
            return source.ElementAt(index);
        }
    }
}

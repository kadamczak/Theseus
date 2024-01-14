using System.Drawing;
using Xunit;

namespace Theseus.Domain.Extensions.Tests
{
    public class ListExtensionTests
    {
        //=======================================================================
        //static IList<T> Swap<T>(this IList<T> self, int indexA, int indexB)
        //=======================================================================
        public static IEnumerable<object[]> Swap_Data => new List<object[]>
            {
                new object[] { new List<int>() { 30, 50 }, 0, 1, new List<int>() { 50, 30 } },
                new object[] { new List<int>() { 10, 30, 50 }, 2, 0, new List<int>() { 50, 30, 10 } },
                new object[] { new List<string>() { "a", "b", "c" }, 0, 1, new List<string>() { "b", "a", "c" } },
                new object[] { new List<Point>() { new Point(20, 10), new Point(40, 70) }, 1, 0, new List<Point>() { new Point(40, 70), new Point(20, 10) } }
            };

        [Theory]
        [MemberData(nameof(Swap_Data))]
        public void Swap_ShouldSwapOnSelf<T>(List<T> list, int indexA, int indexB, List<T> expectedResult)
        {
            //arrange

            //act
            list.Swap(indexA, indexB);

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedResult, list));
        }

        //=======================================================================
        //static IList<T> FisherYatesShuffle<T>(this IList<T> self, Random rnd)
        //=======================================================================
        public static IEnumerable<object[]> FisherYatesShuffle_Data => new List<object[]>
            {
                new object[] { new List<int>() { 30, 50 }, 41, new List<int>() { 50, 30 } },
                new object[] { new List<int>() { 10, 30, 50 }, 37, new List<int>() { 50, 30, 10 } },
                new object[] { new List<string>() { "a", "b", "c" }, 46, new List<string>() { "b", "a", "c" } },
                new object[] { new List<Point>() { new Point(20, 10), new Point(40, 70) }, 49, new List<Point>() { new Point(40, 70), new Point(20, 10) } }
            };

        [Theory]
        [MemberData(nameof(FisherYatesShuffle_Data))]
        public void FisherYatesShuffle_ShouldShuffle_WhenListHasElements<T>(List<T> list, int randomSeed, List<T> expectedResult)
        {
            //arrange
            Random rnd = new Random(randomSeed);

            //act
            list.FisherYatesShuffle(rnd);

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedResult, list));
        }
    }
}
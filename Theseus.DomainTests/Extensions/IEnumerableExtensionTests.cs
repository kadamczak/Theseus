using FluentAssertions;
using System.Drawing;
using Xunit;

namespace Theseus.Domain.Extensions.Tests
{
    public class IEnumerableExtensionTests
    {
        //=======================================================================
        //static T GetRandomItem<T>(this IEnumerable<T> self, Random rnd)
        //=======================================================================
        public static IEnumerable<object[]> GetRandomItem_Data => new List<object[]>
            {
                new object[] { new List<int>() { 1 }, 40, 1 },
                new object[] { new List<int>() { 1, 2, 4 }, 40, 2},
                new object[] { new List<string>() { "a", "b", "c" }, 40, "b" },
                new object[] { new List<string>() { "a", "b", "c" }, 42, "c" },
                new object[] { new List<Point>() { new Point(2, 10), new Point(4, 11), new Point(40, 20)}, 41, new Point(2, 10) }
            };

        [Theory]
        [MemberData(nameof(GetRandomItem_Data))]
        public void GetRandomItem_ShouldReturnItem_WhenContainsItems<T>(IEnumerable<T> list, int rndSeed, T expectedResult)
        {
            //arrange
            Random rnd = new Random(rndSeed);

            //act
            var result = list.GetRandomItem(rnd);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact()]
        public void GetRandomItem_ShouldThrowArgumentOutOfRangeException_WhenContainsNoItems()
        {
            //arrange
            Random rnd = new Random(46);
            var emptyList = new List<int>();

            //act
            Action action = () => emptyList.GetRandomItem(rnd);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
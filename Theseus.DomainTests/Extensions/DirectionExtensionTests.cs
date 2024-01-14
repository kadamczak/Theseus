using Theseus.Domain.Models.MazeRelated.Enums;
using Xunit;

namespace Theseus.Domain.Extensions.Tests
{
    public class DirectionExtensionTests
    {
        //=======================================================================
        //static Direction Reverse(this Direction self)
        //=======================================================================
        public static IEnumerable<object[]> Reverse_Data => new List<object[]>
            {
                new object[] { Direction.South, Direction.North },
                new object[] { Direction.West, Direction.East },
                new object[] { Direction.East, Direction.West },
                new object[] { Direction.North, Direction.South }
            };

        [Theory]
        [MemberData(nameof(Reverse_Data))]
        public void Reverse_ShouldReturnReversedDirection(Direction originalDirection, Direction expectedReversedDirection)
        {
            //arrange

            //act
            Direction result = originalDirection.Reverse();

            //assert
            Assert.Equal(expectedReversedDirection, result);
        }

        //=======================================================================
        //static int CalculateRow(this Direction directionOfNeighbour, int originalRow)
        //=======================================================================
        public static IEnumerable<object[]> CalculateRow_Data => new List<object[]>
            {
                new object[] { 2, Direction.West, 2 },
                new object[] { 4, Direction.East, 4 },
                new object[] { 3, Direction.North, 2 },
                new object[] { 8, Direction.South, 9 }
            };

        [Theory]
        [MemberData(nameof(CalculateRow_Data))]
        public void CalculateRow_ShouldCalculateRow(int originalRow, Direction direction, int expectedResult)
        {
            //arrange

            //act
            int result = direction.CalculateRow(originalRow);

            //assert
            Assert.Equal(expectedResult, result);
        }

        //=======================================================================
        //static int CalculateRow(this Direction directionOfNeighbour, int originalRow)
        //=======================================================================
        public static IEnumerable<object[]> CalculateColumn_Data => new List<object[]>
            {
                new object[] { 2, Direction.West, 1 },
                new object[] { 4, Direction.East, 5 },
                new object[] { 3, Direction.North, 3 },
                new object[] { 8, Direction.South, 8 }
            };

        [Theory]
        [MemberData(nameof(CalculateColumn_Data))]
        public void CalculateColumn_ShouldCalculateColumn(int originalColumn, Direction direction, int expectedResult)
        {
            //arrange

            //act
            int result = direction.CalculateColumn(originalColumn);

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}
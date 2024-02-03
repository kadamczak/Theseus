using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation.Tests
{
    public class MazeTests
    {
        //=======================================================================
        //Cell? GetCell(int row, int column)
        //=======================================================================

        public static IEnumerable<object[]> GetCellValid_Data => new List<object[]>
            {
                new object[] { 0, 0 },
                new object[] { 0, 1 },
                new object[] { 2, 4 },
                new object[] { 2, 2 },
                new object[] { 0, 4 },
                new object[] { 2, 0 }
            };

        [Theory]
        [MemberData(nameof(GetCellValid_Data))]
        public void GetCell_ShouldReturnCell_WhenIndexesExistInMaze(int rowIndex, int columnIndex)
        {
            //arrange
            Maze maze = new Maze(3, 5);

            //act
            Cell? cell = maze.GetCell(rowIndex, columnIndex);

            //assert
            Assert.NotNull(cell);
            Assert.Equal(rowIndex, cell.RowIndex);
            Assert.Equal(columnIndex, cell.ColumnIndex);
        }

        public static IEnumerable<object[]> GetCellNull_Data => new List<object[]>
            {
                new object[] { 0, -1 },
                new object[] { -1, -1 },
                new object[] { -2, 2 },
                new object[] { -2, 4 },
                new object[] { 3, 2 },
                new object[] { 1, 5 },
                new object[] { 6, 6 },
            };

        [Theory]
        [MemberData(nameof(GetCellNull_Data))]
        public void GetCell_ShouldReturnNull_WhenIndexesDoNotExistInMaze(int rowIndex, int columnIndex)
        {
            //arrange
            Maze maze = new Maze(3, 5);

            //act
            Cell? cell = maze.GetCell(rowIndex, columnIndex);

            //assert
            Assert.Null(cell);
        }

        //=======================================================================
        //Cell GetRandomCell(Random rnd)
        //=======================================================================
        public static IEnumerable<object[]> GetRandomCell_Data => new List<object[]>
            {
                new object[] { 40, 1, 2},
                new object[] { 42, 2, 0 },
                new object[] { 60, 0, 2 }
            };

        [Theory]
        [MemberData(nameof(GetRandomCell_Data))]
        public void GetRandomCell_ShouldReturnRandomCell(int randomSeed, int expectedRowIndex, int expectedColumnIndex)
        {
            //arrange
            Maze maze = new Maze(3, 5);
            Random rnd = new Random(randomSeed);
            Cell expectedResult = maze.GetCell(expectedRowIndex, expectedColumnIndex)!;

            //act
            Cell? cell = maze.GetRandomCell(rnd);

            //assert
            Assert.Equal(expectedResult, cell);
        }


        //=======================================================================
        //IEnumerable<Cell> GetBorderCells()
        //=======================================================================
        public static IEnumerable<object[]> GetBorderCells_Data => new List<object[]>
            {
                new object[] { 3, 4 },
                new object[] { 3, 3 },
                new object[] { 4, 13 },
                new object[] { 7, 3 }
            };

        [Theory]
        [MemberData(nameof(GetBorderCells_Data))]
        public void GetBorderCells_ShouldReturnBorderCells(int rowAmount, int columnAmount)
        {
            //arrange
            Maze maze = new Maze(rowAmount, columnAmount);
            var expectedResult = maze.Where(c => c.RowIndex == 0 || c.RowIndex == rowAmount - 1 || c.ColumnIndex == 0 || c.ColumnIndex == columnAmount - 1);

            //act
            var borderCells = maze.GetBorderCells();

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedResult, borderCells));
        }

        //=======================================================================
        //IEnumerable<Cell> ExcludeCellsCloseTo(Cell rootCell, IEnumerable<Cell> cellList)
        //=======================================================================
        public static IEnumerable<object[]> ExcludeCellsCloseToCell_Data => new List<object[]>
            {
                new object[] { new Maze(6, 6), (0, 0), (-2, 2), (-2, 2) },
                new object[] { new Maze(6, 6), (0, 3), (-2, 2), (1, 5) },
                new object[] { new Maze(6, 6), (0, 5), (-2, 2), (3, 7) },
                new object[] { new Maze(6, 6), (2, 0), (0, 4), (-2, 2) },
                new object[] { new Maze(6, 6), (2, 4), (0, 4), (2, 6) },
                
                new object[] { new Maze(3, 5), (1, 1), (0, 2), (0, 2) },
                new object[] { new Maze(13, 10), (5, 6), (1, 9), (3, 9) },
                new object[] { new Maze(23, 23), (10, 22), (3, 17), (15, 29) },
            };

        [Theory]
        [MemberData(nameof(ExcludeCellsCloseToCell_Data))]
        public void ExcludeCellsCloseToCell_ShouldExclude(Maze maze,
                                                          (int row, int column) cellCoordinates,
                                                          (int start, int end) expectedRowExclusion,
                                                          (int start, int end) expectedColumnExclusion)
        {
            //arrange
            Cell rootCell = maze.GetCell(cellCoordinates)!;
            var cells = maze.Select(c => c);

            //act
            var cellsLeftAfterExclusion = maze.ExcludeCellsCloseTo(rootCell, cells);

            //assert
            Assert.True(cellsLeftAfterExclusion.All(c => (c.RowIndex <= expectedRowExclusion.start || c.RowIndex >= expectedRowExclusion.end) &&
                                                         (c.ColumnIndex <= expectedColumnExclusion.start || c.ColumnIndex >= expectedColumnExclusion.end)));
        }
    }
}
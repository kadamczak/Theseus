using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses.Tests
{
    public class DistanceGridTests
    {
        //=======================================================================
        //IEnumerable<Cell> FindFarthestCells(IEnumerable<Cell> cellList)
        //=======================================================================
        public static IEnumerable<object[]> FindFarthestCells_Data => new List<object[]>
            {
                new object[] { new Maze(8, 12), 44, (0, 0), MazeStructureGenAlgorithm.AldousBroder, new List<(int row, int column)> { (6, 6) } },
                new object[] { new Maze(9, 7), 42, (5, 4), MazeStructureGenAlgorithm.HuntAndKill, new List<(int row, int column)> { (6, 0) } },
                new object[] { new Maze(3, 3), 51, (1, 1), MazeStructureGenAlgorithm.HuntAndKill, new List<(int row, int column)> { (0, 2), (2, 0) } }
            };

        [Theory]
        [MemberData(nameof(FindFarthestCells_Data))]
        public void FindFarthestCells_ShouldFindFarthestCells(Maze maze,
                                                              int randomSeed,
                                                              (int rowIndex, int colIndex) rootCellCoordinates,
                                                              MazeStructureGenAlgorithm mazeStructureAlgorithm,
                                                              List<(int row, int col)> expectedFarthestCellCoordinates)
        {
            //arrange
            var generatorFactory = new MazeStructureGeneratorFactory();
            var generator = generatorFactory.Create(mazeStructureAlgorithm);
            generator.GenerateMazeStructureInGrid(maze, randomSeed);
            DistanceGridFactory gridFactory = new DistanceGridFactory();
            var distanceGrid = gridFactory.CreateDistanceGrid(maze.GetCell(rootCellCoordinates)!);

            var expectedFarthestCells = expectedFarthestCellCoordinates.Select(maze.GetCell);

            //act
            var farthestCells = distanceGrid.FindFarthestCells(maze.Select(c => c));

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedFarthestCells, farthestCells));
        }

        //=======================================================================
        //List<Cell> FindPathTo(Cell endCell)
        //=======================================================================
        public static IEnumerable<object[]> FindPathTo_Data => new List<object[]>
            {
                new object[] { new Maze(8, 12), 44, (0, 0), (1, 1), MazeStructureGenAlgorithm.AldousBroder, new List<(int row, int column)> { (0, 0), (0, 1), (0, 2), (1, 2), (1, 1) } },
                new object[] { new Maze(9, 7), 42, (5, 0), (4, 0), MazeStructureGenAlgorithm.HuntAndKill, new List<(int row, int column)> { (5, 0), (5, 1), (4, 1), (3, 1), (3, 0), (4, 0) } },
                new object[] { new Maze(9, 7), 42, (3, 5), (5, 2), MazeStructureGenAlgorithm.HuntAndKill, new List<(int row, int column)> { (3, 5), (3, 4), (3, 3), (4, 3), (5, 3), (5, 2) } },
            };

        [Theory]
        [MemberData(nameof(FindPathTo_Data))]
        public void FindPathTo_ShouldFindCellPath(Maze maze,
                                                  int randomSeed,
                                                  (int rowIndex, int colIndex) rootCellCoordinates,
                                                  (int rowIndex, int colIndex) targetCellCoordinates,
                                                  MazeStructureGenAlgorithm mazeStructureAlgorithm,
                                                  List<(int row, int col)> expectedPathCellCoordinates)
        {
            //arrange
            var generatorFactory = new MazeStructureGeneratorFactory();
            var generator = generatorFactory.Create(mazeStructureAlgorithm);
            generator.GenerateMazeStructureInGrid(maze, randomSeed);
            DistanceGridFactory gridFactory = new DistanceGridFactory();
            var distanceGrid = gridFactory.CreateDistanceGrid(maze.GetCell(rootCellCoordinates)!);

            var expectedPath = expectedPathCellCoordinates.Select(maze.GetCell);

            //act
            var path = distanceGrid.FindPathTo(maze.GetCell(targetCellCoordinates)!);

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedPath, path));
        }
    }
}
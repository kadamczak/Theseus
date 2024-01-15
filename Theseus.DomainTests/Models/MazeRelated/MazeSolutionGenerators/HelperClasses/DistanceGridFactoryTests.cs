using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses.Tests
{
    public class DistanceGridFactoryTests
    {
        public static IEnumerable<object[]> GenerateMazeStructure_Data => new List<object[]>
            {
                new object[] { new Maze(8, 12), 44, (0, 0), MazeStructureGenAlgorithm.AldousBroder, new Dictionary<(int row, int col), int>()
                                                                                                    {
                                                                                                        {(0, 1), 1},
                                                                                                        {(0, 0), 0},
                                                                                                        {(1, 0), 5},
                                                                                                        {(3, 1), 8},
                                                                                                        {(4, 3), 15},
                                                                                                        {(6, 11), 51},
                                                                                                        {(6, 6), 54},
                                                                                                    } },
                new object[] { new Maze(9, 7), 42, (5, 4), MazeStructureGenAlgorithm.HuntAndKill, new Dictionary<(int row, int col), int>()
                                                                                                    {
                                                                                                        {(8, 1), 12},
                                                                                                        {(8, 2), 5},
                                                                                                        {(0, 3), 10},
                                                                                                        {(7, 3), 17}
                                                                                                    } }
            };

        [Theory]
        [MemberData(nameof(GenerateMazeStructure_Data))]
        public void CreateDistanceGrid_ShouldCreateDistanceGrid(Maze maze,
                                                                int randomSeed,
                                                                (int rowIndex, int colIndex) rootCellCoordinates,
                                                                MazeStructureGenAlgorithm mazeStructureAlgorithm,
                                                                Dictionary<(int row, int col), int> expectedDistancesForCells)
        {
            //arrange
            var generatorFactory = new MazeStructureGeneratorFactory();
            var generator = generatorFactory.Create(mazeStructureAlgorithm);
            generator.GenerateMazeStructureInGrid(maze, randomSeed);

            DistanceGridFactory gridFactory = new DistanceGridFactory();

            //act
            var distanceGrid = gridFactory.CreateDistanceGrid(maze.GetCell(rootCellCoordinates)!);

            //assert
            foreach (var example in expectedDistancesForCells)
            {
                var cell = maze.GetCell(example.Key)!;
                Assert.Equal(example.Value, distanceGrid.Distance[cell]);
            }
        }
    }
}
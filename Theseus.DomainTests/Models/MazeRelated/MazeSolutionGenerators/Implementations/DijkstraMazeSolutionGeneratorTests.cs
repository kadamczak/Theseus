using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;
using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations.Tests
{
    public class DijkstraMazeSolutionGeneratorTests
    {
        public static IEnumerable<object[]> Generate_Data => new List<object[]>
            {
                new object[] { new Maze(5, 6), 46, MazeStructureGenAlgorithm.HuntAndKill, true,  (3, 0), (4, 2)},
                new object[] { new Maze(8, 6), 50, MazeStructureGenAlgorithm.AldousBroder, true,  (4, 5), (0, 0)},
                new object[] { new Maze(7, 7), 30, MazeStructureGenAlgorithm.TruePrim, false,  (4, 6), (0, 0)},
            };

        [Theory]
        [MemberData(nameof(Generate_Data))]
        public void GenerateSolutionInMaze_ShouldGenerate(Maze maze,
                                                          int randomSeed,
                                                          MazeStructureGenAlgorithm mazeStructureAlgorithm,
                                                          bool shouldExcludeCellsCloseToRoot,
                                                          (int row, int column) expectedStartCellCoordinates,
                                                          (int row, int column) expectedEndCellCoordinates)
        {
            //arrange
            var generatorFactory = new MazeStructureGeneratorFactory();
            var generator = generatorFactory.Create(mazeStructureAlgorithm);
            generator.GenerateMazeStructureInGrid(maze, randomSeed);

            DistanceGridFactory gridFactory = new DistanceGridFactory();
            var mazeSolutionGenerator = new DijkstraMazeSolutionGenerator(gridFactory, shouldExcludeCellsCloseToRoot);

            Cell expectedStartCell = maze.GetCell(expectedStartCellCoordinates)!;
            Cell expectedEndCell = maze.GetCell(expectedEndCellCoordinates)!;

            MazeWithSolution mazeWithSolution = new MazeWithSolution(maze);

            //act
            mazeSolutionGenerator.GenerateSolutionInMaze(mazeWithSolution, randomSeed);

            //assert
            Cell startCell = mazeWithSolution.SolutionPath.First();
            Cell endCell = mazeWithSolution.SolutionPath.Last();

            Assert.Equal(expectedStartCell, startCell);
            Assert.Equal(expectedEndCell, endCell);
        }
    }
}
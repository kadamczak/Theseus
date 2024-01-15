using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations.Tests
{
    public class KruskalMazeStructureGeneratorTests
    {
        public static IEnumerable<object[]> GenerateMazeStructure_Data => new List<object[]>
            {
                new object[] { new Maze(8, 12), 44, (0, 0), new List<Direction>() { Direction.East, Direction.South } },
                new object[] { new Maze(8, 12), 44, (6, 3), new List<Direction>() { Direction.North, Direction.South, Direction.West } },
                new object[] { new Maze(8, 12), 44, (6, 9), new List<Direction>() { Direction.North, Direction.East, Direction.West } },
                new object[] { new Maze(8, 12), 44, (7, 11), new List<Direction>() { Direction.North, Direction.West } },
                new object[] { new Maze(8, 12), 44, (0, 6), new List<Direction>() { Direction.West, Direction.South, Direction.East } },

                new object[] { new Maze(5, 3), 40, (3, 1), new List<Direction>() { Direction.North } },
                new object[] { new Maze(5, 3), 40, (0, 2), new List<Direction>() { Direction.South } },
                new object[] { new Maze(5, 3), 40, (2, 1), new List<Direction>() { Direction.East, Direction.West, Direction.North, Direction.South } },
            };

        [Theory]
        [MemberData(nameof(GenerateMazeStructure_Data))]
        public void GenerateMazeStructureInGrid_ShouldGenerateMaze(Maze maze,
                                                                   int randomSeed,
                                                                   (int rowIndex, int colIndex) testedCellCoordinates,
                                                                   List<Direction> expectedDirectionsOfAllLinkedNeighbours)
        {
            //arrange
            Cell testedCell = maze.GetCell(testedCellCoordinates)!;
            var generator = new KruskalMazeStructureGenerator();

            //act
            generator.GenerateMazeStructureInGrid(maze, randomSeed);

            //assert
            var directionsOfAllLinkedNeighbours = testedCell.GetLinkedCells().Select(c => testedCell.GetNeighbourDirection(c));
            Assert.True(Enumerable.SequenceEqual(expectedDirectionsOfAllLinkedNeighbours, directionsOfAllLinkedNeighbours));
        }
    }
}
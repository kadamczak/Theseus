using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Maze;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;

namespace Theseus.Domain.Models.MazeRelated.MazeCreators
{
    public class MazeCreator
    {
        public MazeGrid CreateMazeGrid(int height, int width, MazeStructureGenAlgorithm algorithm)
        {
            MazeGrid grid = new MazeGrid(height, width);

            var generator = MazeStructureGeneratorFactory.Create(algorithm);
            generator.GenerateMazeStructureInGrid(grid);

            return grid;
        }

        public SolvableMaze CreateSolvableMaze(int height,
                                               int width,
                                               MazeStructureGenAlgorithm structureAlgorithm,
                                               MazeSolutionGenAlgorithm solutionAlgorithm)
        {
            SolvableMaze maze = CreateMazeGrid(height, width, structureAlgorithm);

            var generator = MazeSolutionGeneratorFactory.Create(solutionAlgorithm);
            generator.GenerateSolutionInMaze(maze);

            return maze;
        }
    }
}

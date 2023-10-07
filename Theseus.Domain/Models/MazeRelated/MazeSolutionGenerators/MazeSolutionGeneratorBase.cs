using Theseus.Domain.Models.MazeRelated.Maze;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    public abstract class MazeSolutionGeneratorBase
    {
        public abstract void GenerateSolutionInMaze(SolvableMaze maze);
    }
}
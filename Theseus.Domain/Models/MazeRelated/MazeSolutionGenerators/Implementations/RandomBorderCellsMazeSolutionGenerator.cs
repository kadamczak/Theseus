using Theseus.Domain.Models.MazeRelated.Maze;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    public class RandomBorderCellsMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public override void GenerateSolutionInMaze(SolvableMaze maze)
        {
            Random rnd = new Random();
        }
    }
}
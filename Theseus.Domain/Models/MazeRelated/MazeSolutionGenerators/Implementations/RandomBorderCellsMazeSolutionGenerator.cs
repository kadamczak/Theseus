using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    public class RandomBorderCellsMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public override void GenerateSolutionInMaze(MazeWithSolution maze)
        {
            Random rnd = new Random();
        }
    }
}
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    public class MazeSolutionGeneratorFactory
    {
        public static MazeSolutionGeneratorBase Create(MazeSolutionGenAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case MazeSolutionGenAlgorithm.Dijkstra:
                    return new DijkstraMazeSolutionGenerator();
                case MazeSolutionGenAlgorithm.RandomBorderCells:
                    return new RandomBorderCellsMazeSolutionGenerator();
                default:
                    throw new NotImplementedException("Asked to generate a maze solution with algorithm that has not been implemented yet.");
            }
        }
    }
}

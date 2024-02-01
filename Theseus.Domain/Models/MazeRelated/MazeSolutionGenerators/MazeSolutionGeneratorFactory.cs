using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    /// <summary>
    /// The <c>MazeSolutionGeneratorFactory</c> creates objects of classes deriving from <c>MazeSolutionGeneratorBase</c>.
    /// </summary>
    public class MazeSolutionGeneratorFactory
    {
        private readonly DistanceGridFactory _distanceGridFactory;
        public MazeSolutionGeneratorFactory(DistanceGridFactory distanceGridFactory)
        {
            this._distanceGridFactory = distanceGridFactory;
        }

        public MazeSolutionGeneratorBase Create(MazeSolutionGenAlgorithm algorithm, bool shouldExcludeCellsCloseToRoot)
        {
            return algorithm switch
            {
                MazeSolutionGenAlgorithm.Dijkstra => new DijkstraMazeSolutionGenerator(_distanceGridFactory, shouldExcludeCellsCloseToRoot),
                MazeSolutionGenAlgorithm.RandomBorderCells => new RandomBorderCellsMazeSolutionGenerator(_distanceGridFactory, shouldExcludeCellsCloseToRoot),
                _ => throw new NotImplementedException("Asked to generate a maze solution with algorithm that has not been implemented."),
            };
        }
    }
}
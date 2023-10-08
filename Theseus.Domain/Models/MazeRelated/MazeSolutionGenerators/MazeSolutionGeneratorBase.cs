using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    public abstract class MazeSolutionGeneratorBase
    {
        public DistanceGridFactory DistanceGridFactory { get; }
        public MazeSolutionGeneratorBase(DistanceGridFactory distanceGridFactory)
        {
            this.DistanceGridFactory = distanceGridFactory;
        }

        public abstract void GenerateSolutionInMaze(MazeWithSolution maze);
    }
}
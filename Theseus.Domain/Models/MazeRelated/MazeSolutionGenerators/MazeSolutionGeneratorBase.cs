using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
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
        public Direction ChooseRandomExitDirection(Cell cell, Random rnd) => GetAdjecentEmptySpaces(cell).GetRandomItem(rnd);

        private IEnumerable<Direction> GetAdjecentEmptySpaces(Cell cell)
        {
            var emptySpaces = cell.AdjecentCellSpaces.Where(c => c.Value == null).ToDictionary(x => x.Key, x => x.Value);
            return emptySpaces.Keys;
        }
    }
}
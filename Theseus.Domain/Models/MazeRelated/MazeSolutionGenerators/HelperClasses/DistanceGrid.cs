using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    public class DistanceGrid
    {
        public Cell StartCell { get; }
        public Dictionary<Cell, int> DistanceFromStartCell { get; } = new Dictionary<Cell, int>();

        public DistanceGrid(Cell startCell)
        {
            StartCell = startCell;

        }

    }
}
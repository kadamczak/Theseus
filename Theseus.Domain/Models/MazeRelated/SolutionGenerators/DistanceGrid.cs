using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.SolutionGenerators
{
    public class DistanceGrid
    {
        public Cell StartCell { get; }
        public Dictionary<Cell, int> DistanceFromStartCell { get; } = new Dictionary<Cell, int>();

        public DistanceGrid(Cell startCell)
        {
            this.StartCell = startCell;

        }

    }
}
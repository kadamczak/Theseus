using System.IO;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    public class DistanceGrid
    {
        public Cell RootCell { get; }
        public Dictionary<Cell, int> Distance { get; } = new Dictionary<Cell, int>();

        public DistanceGrid(Cell rootCell)
        {
            RootCell = rootCell;
            Distance.Add(rootCell, 0);
        }

        public bool DistanceAlreadyFound(Cell cell)
        {
            return this.Distance.ContainsKey(cell);
        }

        public List<Cell> FindPathTo(Cell endCell)
        {
            List<Cell> path = new List<Cell>() { endCell };
            Cell currentCell = endCell;

            while (currentCell != RootCell)
            {
                Cell closerCell = FindCellWithShortestDistance(currentCell);
                path.Add(closerCell);
                currentCell = closerCell;
            }

            path.Reverse();
            return path;
        }

        private Cell FindCellWithShortestDistance(Cell currentCell)
        {
            int currentCellDistance = Distance[currentCell];
            foreach (var linkedCell in currentCell.LinkedCells)
            {
                if (Distance[linkedCell] < currentCellDistance)
                {
                    return linkedCell;
                }
            }
            return currentCell;
        }
    }
}
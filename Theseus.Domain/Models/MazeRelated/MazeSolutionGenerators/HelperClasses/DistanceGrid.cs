using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    /// <summary>
    /// The <c>DistanceGrid</c> class contains distances of other <c>Cell</c>s in a <c>Maze</c> relative to a specific root <c>Cell</c>.
    /// </summary>
    public class DistanceGrid
    {
        public Cell RootCell { get; }
        public Dictionary<Cell, int> Distance { get; } = new Dictionary<Cell, int>();

        public DistanceGrid(Cell rootCell)
        {
            this.RootCell = rootCell;
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
                Cell closerCell = FindCloserCellThan(currentCell);
                path.Add(closerCell);
                currentCell = closerCell;
            }

            path.Reverse();
            return path;
        }

        private Cell FindCloserCellThan(Cell currentCell)
        {
            int currentCellDistance = Distance[currentCell];
            foreach (var linkedCell in currentCell.GetLinkedCells())
            {
                if (Distance[linkedCell] < currentCellDistance)
                {
                    return linkedCell;
                }
            }
            return currentCell;
        }

        public IEnumerable<Cell> FindFarthestCells(IEnumerable<Cell> cellList)
        {
            var cellDistances = Distance.Where(c => cellList.Contains(c.Key)).ToDictionary(x => x.Key, x => x.Value);
            int maxDistance = cellDistances.Values.Max();
            return cellDistances.Where(d => d.Value == maxDistance).ToDictionary(x => x.Key, x => x.Value).Keys;
        }
    }
}
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    public class DistanceGrid
    {
        public int RowAmount { get; } = 0;
        public int ColumnAmount { get; } = 0;

        public Cell RootCell { get; }
        public Dictionary<Cell, int> Distance { get; } = new Dictionary<Cell, int>();

        public DistanceGrid(Cell rootCell, int rowAmount, int columnAmount)
        {
            this.RootCell = rootCell;
            this.RowAmount = rowAmount;
            this.ColumnAmount = columnAmount;

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
                Cell closerCell = FindCloserCell(currentCell);
                path.Add(closerCell);
                currentCell = closerCell;
            }

            path.Reverse();
            return path;
        }

        private Cell FindCloserCell(Cell currentCell)
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

        //TODO
        //Areas?
        public IEnumerable<Cell> FindFarthestBorderCells()
        {
            var borderCellDistances = Distance.Where(c => c.Key.IsOnBorder(RowAmount, ColumnAmount)).ToDictionary(x => x.Key, x => x.Value);

            int maxDistance = borderCellDistances.Values.Max();
            var farthestBorderCells = borderCellDistances.Where(d => d.Value == maxDistance).ToDictionary(x => x.Key, x => x.Value);
            return farthestBorderCells.Keys;
        }
    }
}
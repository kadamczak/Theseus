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
            foreach (var linkedCell in currentCell.LinkedCells)
            {
                if (Distance[linkedCell] < currentCellDistance)
                {
                    return linkedCell;
                }
            }
            return currentCell;
        }

        public IEnumerable<Cell> FindFarthestBorderCells(bool shouldExcludeCellsCloseToRoot = false)
        {
            var borderCellDistances = Distance.Where(c => c.Key.IsOnBorder(RowAmount, ColumnAmount)).ToDictionary(x => x.Key, x => x.Value);

            if(shouldExcludeCellsCloseToRoot)
            {
                borderCellDistances = ExcludeCellsCloseToRoot(borderCellDistances);
            }

            int maxDistance = borderCellDistances.Values.Max();
            return borderCellDistances.Where(d => d.Value == maxDistance).ToDictionary(x => x.Key, x => x.Value).Keys;
        }

        private Dictionary<Cell, int> ExcludeCellsCloseToRoot(Dictionary<Cell, int> cells)
        {
            var rowExclusionZone = CalculateExclusionZone(this.RootCell.RowIndex, this.RowAmount);
            var columnExclusionZone = CalculateExclusionZone(this.RootCell.ColumnIndex, this.ColumnAmount);

            return cells.Where(c => (OutsideOfExclusionZone(c.Key.RowIndex, rowExclusionZone)) &&
                                    (OutsideOfExclusionZone(c.Key.ColumnIndex, columnExclusionZone))).ToDictionary(x => x.Key, x => x.Value);
        }

        private (int Beginning, int End) CalculateExclusionZone(int index, int dimensionLength)
        {
            return (index - dimensionLength / 2, index + dimensionLength / 2);
        }

        private bool OutsideOfExclusionZone(int index, (int Beginning, int End) exclusionZone)
        {
            return (index <= exclusionZone.Beginning ||  index >= exclusionZone.End);
        }
    }
}
using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.MazeRelated.Maze
{
    public class Cell
    {
        //Cell properties
        public int RowIndex { get; }         //Y
        public int ColumnIndex { get; }      //X

        //Connections to other cells
        public Dictionary<Direction, Cell?> AdjecentCellSpaces { get; } = new Dictionary<Direction, Cell?>();
        public List<Cell> LinkedCells { get; } = new List<Cell>();

        //INITIALIZATION==============================
        public Cell(int rowIndex, int colIndex)
        {
            if (rowIndex < 0)
                throw new ArgumentException("Row index in a cell must be equal or above 0.");

            if (colIndex < 0)
                throw new ArgumentException("Column index in a cell must be equal or above 0.");

            RowIndex = rowIndex;
            ColumnIndex = colIndex;
        }

        //CELL LINKING==============================
        public bool IsLinkedToNeighbour(Direction direction)
        {
            return this.IsLinked(this.AdjecentCellSpaces[direction]);
        }

        public bool IsLinked(Cell? anotherCell)
        {
            if (anotherCell is null)
                return false;

            return LinkedCells.Contains(anotherCell);
        }

        public void LinkToNeighbour(Direction direction)
        {
            Cell? neighbourCell = this.AdjecentCellSpaces[direction];

            this.LinkTo(neighbourCell);
        }

        public void LinkTo(Cell anotherCell, bool bidirectional = true)
        {
            if (anotherCell is null)
                throw new ArgumentException("Can't link cell to a null space.");

            LinkedCells.Add(anotherCell);

            if (bidirectional)
            {
                anotherCell.LinkTo(this, false);
            }
        }

        public void UnlinkFromNeighbour(Direction direction)
        {
            Cell? neighbourCell = this.AdjecentCellSpaces[direction];

            this.UnlinkFrom(neighbourCell);
        }

        public void UnlinkFrom(Cell anotherCell, bool bidirectional = true)
        {
            if (anotherCell is null)
                throw new ArgumentException("Can't unlink cell from empty space.");

            LinkedCells.Remove(anotherCell);

            if (bidirectional)
            {
                anotherCell.UnlinkFrom(this, false);
            }
        }

        //ADJECENT CELLS ON THE GRID=====================
        public IEnumerable<Cell> GetAdjecentCells()
        {
            return AdjecentCellSpaces.Values
                                          .Where(c => c is not null)
                                          .Select(c => c!);
        }

        public IEnumerable<Cell> GetAdjecentCells(params Direction[] directions)
        {
            return AdjecentCellSpaces.Where(c => directions.Contains(c.Key))
                                          .Where(c => c.Value is not null)
                                          .Select(c => c.Value!);
        }

        public bool HasNeighbour(Direction direction)
        {
            return this.AdjecentCellSpaces[direction] is not null;
        }

    }
}

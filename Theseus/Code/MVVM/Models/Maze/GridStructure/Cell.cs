using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theseus.Code.MVVM.Models.Maze.Enums;

namespace Theseus.Code.MVVM.Models.Maze.GridStructure
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
            {
                throw new ArgumentException("Row index in a cell must be equal or above 0.");
            }

            if (colIndex < 0)
            {
                throw new ArgumentException("Column index in a cell must be equal or above 0.");
            }

            RowIndex = rowIndex;
            ColumnIndex = colIndex;
        }

        //CELL LINKING==============================
        public bool IsLinked(Cell? anotherCell)
        {
            if (anotherCell is null)
                return false;

            return LinkedCells.Contains(anotherCell);
        }

        public void LinkToAnotherCell(Cell anotherCell, bool bidirectional = true)
        {
            LinkedCells.Add(anotherCell);

            if (bidirectional)
            {
                anotherCell.LinkToAnotherCell(this, false);
            }
        }

        public void UnlinkFromAnotherCell(Cell anotherCell, bool bidirectional = true)
        {
            LinkedCells.Remove(anotherCell);

            if (bidirectional)
            {
                anotherCell.UnlinkFromAnotherCell(this, false);
            }
        }

        //GETTING ADJECENT CELLS ON THE GRID=====================
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

    }
}

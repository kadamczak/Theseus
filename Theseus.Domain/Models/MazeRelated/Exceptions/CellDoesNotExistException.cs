
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    public class CellDoesNotExistException : CellException
    {
        public CellDoesNotExistException(int row, int column) : base("The cell was expected to exist, but is null instead.", row, column) {}

        public CellDoesNotExistException((int row, int column) coordinates) : this(coordinates.row, coordinates.column) { }
        public CellDoesNotExistException(Cell cell, Direction directionOfNeighbour) : this(CalculateRow(cell.RowIndex, directionOfNeighbour),
                                                                                           CalculateColumn(cell.ColumnIndex, directionOfNeighbour)) { }
    }
}
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    public class CellException : Exception
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public CellException(string message, int row, int column) : base($"An exception occured with the ({row}, {column}) cell: {message}")
        {
            Row = row;
            Column = column;
        }
        public CellException(string message, (int row, int column) coordinates) : this(message, coordinates.row, coordinates.column) { }
        public CellException(string message, Cell cell, Direction directionOfNeighbour) : this(message,
                                                                                               CalculateRow(cell.RowIndex, directionOfNeighbour),
                                                                                               CalculateColumn(cell.ColumnIndex, directionOfNeighbour)) { }

        protected static int CalculateRow(int originalRow, Direction directionOfNeighbour)
        {
            return directionOfNeighbour switch
            {
                Direction.North => originalRow--,
                Direction.South => originalRow++,
                _ => originalRow
            };
        }

        protected static int CalculateColumn(int originalColumn, Direction directionOfNeighbour)
        {
            return directionOfNeighbour switch
            {
                Direction.West => originalColumn--,
                Direction.East => originalColumn++,
                _ => originalColumn
            };
        }
    }
}

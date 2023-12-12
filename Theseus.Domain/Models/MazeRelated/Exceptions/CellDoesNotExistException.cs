
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    /// <summary>
    /// The <c>CellDoesNotExistException</c> represents an exception that occurs when the program expects a particular <c>Cell</c> to exist but it does not.
    /// </summary>
    public class CellDoesNotExistException : CellException
    {
        /// <summary>
        /// Initializes <c>CellDoesNotExistException</c> with the missing <c>Cell</c>'s row index and column index.
        /// </summary>
        /// <param name="row">Missing <c>Cell</c>'s row index.</param>
        /// <param name="column">Missing <c>Cell</c>'s column index.</param>
        public CellDoesNotExistException(int row, int column) : base("The cell was expected to exist, but is null instead.", row, column) {}

        /// <summary>
        /// Initializes <c>CellDoesNotExistException</c> with the missing <c>Cell</c>'s coordinates.
        /// </summary>
        /// <param name="coordinates">Missing <c>Cell</c>'s coordinates.</param>
        public CellDoesNotExistException((int row, int column) coordinates) : this(coordinates.row, coordinates.column) { }

        /// <summary>
        /// Initializes <c>CellDoesNotExistException</c> with existing <c>Cell</c> and the <c>Direction</c> from it in which
        /// the missing <c>Cell</c> was expected to be found.
        /// </summary>
        /// <param name="cell">Existing <c>Cell</c>.</param>
        /// <param name="directionOfNeighbour"><c>Direction</c> from existing <c>Cell</c> in which the missing <c>Cell</c> was expected to be found.</param>
        public CellDoesNotExistException(Cell cell, Direction directionOfNeighbour) : this(CalculateRow(cell.RowIndex, directionOfNeighbour),
                                                                                           CalculateColumn(cell.ColumnIndex, directionOfNeighbour)) { }
    }
}
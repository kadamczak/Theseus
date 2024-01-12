using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    /// <summary>
    /// The <c>CellException</c> represents a general error with a <c>Cell</c>.
    /// </summary>
    public class CellException : Exception
    {
        /// <summary>
        /// Gets or sets the row index of the <c>Cell</c> that caused an exception.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column index of the <c>Cell</c> that caused an exception.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Initializes <c>CellException</c> with a custom message and row index and column index of
        /// the <c>Cell</c> that caused the exception.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="row">Row index of the <c>Cell</c> that caused the exception.</param>
        /// <param name="column">Column index of the <c>Cell</c> that caused the exception.</param>
        public CellException(string message, int row, int column) : base($"An exception occured with the ({row}, {column}) cell: {message}")
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Initializes <c>CellException</c> with a custom message and coordinates of the <c>Cell</c> that caused the exception.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="coordinates">Coordinates of the <c>Cell</c> that caused the exception.</param>
        public CellException(string message, (int row, int column) coordinates) : this(message, coordinates.row, coordinates.column) { }

        /// <summary>
        /// Initializes <c>CellException</c> with existing <c>Cell</c> and the <c>Direction</c> from it in which
        /// a <c>Cell</c> caused an exception.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="cell">Existing <c>Cell</c>.</param>
        /// <param name="directionOfNeighbour"><c>Direction</c> from existing <c>Cell</c> in which a <c>Cell</c> caused an exception.</param>
        public CellException(string message, Cell cell, Direction directionOfNeighbour) : this(message,
                                                                                               directionOfNeighbour.CalculateRow(cell.RowIndex),
                                                                                               directionOfNeighbour.CalculateColumn(cell.ColumnIndex)) { }

    }
}

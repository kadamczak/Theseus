using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    /// <summary>
    /// The <c>SolutionCellsNotLinkedException</c> represents an exception that occurs when two <c>Cell</c>s are expected to be linked to each other but they are not.
    /// </summary>
    public class SolutionCellsNotLinkedException : CellException
    {
        /// <summary>
        /// Initialize <c>SolutionCellsNotLinkedException</c> with the pair of <c>Cell</c>s that were expected to be linked but were not.
        /// </summary>
        /// <param name="previousCell">First <c>Cell</c> in the pair.</param>
        /// <param name="nextCell">Second <c>Cell</c> in the pair.</param>
        public SolutionCellsNotLinkedException(Cell previousCell, Cell nextCell)
            : base($"The cell was expected to be the next step of maze solution, but it is not linked to the previous cell in the solution: ({previousCell.RowIndex},{previousCell.ColumnIndex})",
                  nextCell.RowIndex,
                  nextCell.ColumnIndex) { }
    }
}
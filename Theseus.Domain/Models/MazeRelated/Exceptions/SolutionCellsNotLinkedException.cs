using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.Exceptions
{
    public class SolutionCellsNotLinkedException : CellException
    {
        public SolutionCellsNotLinkedException(Cell previousCell, Cell nextCell)
            : base($"The cell was expected to be the next step of maze solution, but it is not linked to the previous cell in the solution: ({previousCell.RowIndex},{previousCell.ColumnIndex})",
                  nextCell.RowIndex,
                  nextCell.ColumnIndex) { }
    }
}
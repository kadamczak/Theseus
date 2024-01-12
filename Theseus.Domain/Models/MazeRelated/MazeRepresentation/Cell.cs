using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Exceptions;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    /// <summary>
    /// The <c>Cell</c> cell represents a single cell in a <c>Maze</c>.
    /// </summary>
    /// <remarks>
    /// This class is responsible for creating/removing links between <c>Cell</c>s.
    /// </remarks>
    public class Cell
    {
        /// <summary>
        /// Gets the row index of <c>Cell</c> within a <c>Maze</c>.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets the column index of <c>Cell</c> within a <c>Maze</c>.
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Gets a dictionary whose keys are <c>Direction</c>s and whose values represent a potential neighbour <c>Cell</c> in that <c>Direction</c>.
        /// </summary>
        public Dictionary<Direction, Cell?> AdjecentCellSpaces { get; } = new Dictionary<Direction, Cell?>();

        /// <summary>
        /// Gets a list of <c>Cell</c>s that are directly linked to this <c>Cell</c>.
        /// </summary>
        /// <remarks>
        /// If <c>Cell</c>s are linked to each other, that means that there is no wall between them.
        /// </remarks>
        public List<Cell> LinkedCells { get; } = new List<Cell>();

        /// <summary>
        /// Initializes a <c>Cell</c> with a row index and a column index.
        /// </summary>
        /// <param name="rowIndex">Row index in <c>Maze</c>.</param>
        /// <param name="colIndex">Column index in <c>Maze</c>.</param>
        /// <exception cref="ArgumentException">Thrown when rowIndex or colIndex are below 0.</exception>
        public Cell(int rowIndex, int colIndex)
        {
            if (rowIndex < 0)
                throw new ArgumentException("Row index in a cell must be equal or above 0.");

            if (colIndex < 0)
                throw new ArgumentException("Column index in a cell must be equal or above 0.");

            RowIndex = rowIndex;
            ColumnIndex = colIndex;
        }


        /// <summary>
        /// Returns true if this <c>Cell</c> is linked to its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction"><c>Direction</c> of the neighbour.</param>
        /// <returns>True if this <c>Cell</c> is linked to its neighbour in specified <c>Direction</c>.</returns>
        public bool IsLinkedToNeighbourInDirection(Direction direction)
        {
            return this.IsLinked(this.AdjecentCellSpaces[direction]);
        }

        /// <summary>
        /// Returns true if this <c>Cell</c> is linked to <paramref name="anotherCell"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="anotherCell"/> is null, then returns false.
        /// </remarks>
        /// <param name="anotherCell">Neighbour <c>Cell</c> to be checked.</param>
        /// <returns>True if this <c>Cell</c> is linked to <paramref name="anotherCell"/>.</returns>
        public bool IsLinked(Cell? anotherCell)
        {
            if (anotherCell is null)
                return false;

            return LinkedCells.Contains(anotherCell);
        }

        /// <summary>
        /// Links this <c>Cell</c> to its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the neighbour <c>Cell</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when this <c>Cell</c> has no neighbour in specified <c>Direction</c>.</exception>
        public void LinkToNeighbour(Direction direction)
        {
            Cell? neighbourCell = this.AdjecentCellSpaces[direction];
            this.LinkTo(neighbourCell);
        }

        /// <summary>
        /// Links this <c>Cell</c> to <paramref name="anotherCell"/>.
        /// </summary>
        /// <param name="anotherCell">Neighbour <c>Cell</c> that will be linked to this <c>Cell</c>.</param>
        /// <param name="bidirectional">True if this method should also be called on <paramref name="anotherCell"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="anotherCell"/> is null.</exception>
        public void LinkTo(Cell anotherCell, bool bidirectional = true)
        {
            if (anotherCell is null)
                throw new ArgumentNullException("Can't link cell to a null space.");

            LinkedCells.Add(anotherCell);

            if (bidirectional)
                anotherCell.LinkTo(this, false);
        }

        /// <summary>
        /// Uninks this <c>Cell</c> from its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the neighbour <c>Cell</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when this <c>Cell</c> has no neighbour in specified <c>Direction</c>.</exception>
        public void UnlinkFromNeighbour(Direction direction)
        {
            Cell? neighbourCell = this.AdjecentCellSpaces[direction];
            this.UnlinkFrom(neighbourCell);
        }

        /// <summary>
        /// Unlinks this <c>Cell</c> from <paramref name="anotherCell"/>.
        /// </summary>
        /// <param name="anotherCell">Neighbour <c>Cell</c> that will be unlinked from this <c>Cell</c>.</param>
        /// <param name="bidirectional">True if this method should also be called on <paramref name="anotherCell"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="anotherCell"/> is null.</exception>
        public void UnlinkFrom(Cell anotherCell, bool bidirectional = true)
        {
            if (anotherCell is null)
                throw new ArgumentNullException("Can't unlink cell from empty space.");

            LinkedCells.Remove(anotherCell);

            if (bidirectional)
                anotherCell.UnlinkFrom(this, false);
        }

        /// <summary>
        /// Returns a collection of this <c>Cell</c>'s neighbours.
        /// </summary>
        /// <returns>Collection of this <c>Cell</c>'s neighbours.</returns>
        public IEnumerable<Cell> GetAdjecentCells() => AdjecentCellSpaces.Values.Where(c => c is not null).Select(c => c!);

        /// <summary>
        /// Returns a collection of this <c>Cell</c>'s neighbours in <c>Direction</c>s included in <paramref name="directions"/>.
        /// </summary>
        /// <param name="directions">Array containing the included <c>Direction</c>s.</param>
        /// <returns>Collection of this <c>Cell</c>'s neighbours in <c>Direction</c>s included in <paramref name="directions"/>.</returns>
        public IEnumerable<Cell> GetAdjecentCells(params Direction[] directions) => AdjecentCellSpaces.Where(c => directions.Contains(c.Key))
                                                                                                      .Where(c => c.Value is not null)
                                                                                                      .Select(c => c.Value!);

        /// <summary>
        /// Returns true if this <c>Cell</c> has neighbour in a <c>Direction</c> specified by <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the searched for neighbour <c>Cell</c>.</param>
        /// <returns>True if this <c>Cell</c> has neighbour in a <c>Direction</c> specified by <paramref name="direction"/>.</returns>
        public bool HasNeighbour(Direction direction) => this.AdjecentCellSpaces[direction] is not null;

        /// <summary>
        /// Returns <c>Direction</c> of a <paramref name="neighbour"/> <c>Cell</c>.
        /// </summary>
        /// <param name="neighbour">Neighbour <c>Cell</c> of this <c>Cell</c>.</param>
        /// <returns><c>Direction</c> of a <paramref name="neighbour"/> <c>Cell</c>.</returns>
        public Direction GetNeighbourDirection(Cell neighbour) => this.AdjecentCellSpaces.First(s => s.Value == neighbour).Key;

        /// <summary>
        /// Returns true if this <c>Cell</c> is on <c>Maze</c> border.
        /// </summary>
        /// <param name="rows">Amount of rows in a <c>Maze</c>.</param>
        /// <param name="cols">Amount of columns in a <c>Maze</c>.</param>
        /// <returns>True if this <c>Cell</c> is on <c>Maze</c> border.</returns>
        public bool IsOnBorder(int rows, int cols) => RowIndex == 0 || RowIndex == rows - 1 || ColumnIndex == 0 || ColumnIndex == cols - 1;
    }
}
﻿using System.Collections.ObjectModel;
using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Exceptions;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    /// <summary>
    /// The <c>Cell</c> class represents a single cell in a <c>Maze</c>, which can be linked to other adjecent <c>Cell</c>s,
    /// creating a travel path between them.
    /// </summary>
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
        /// Dictionary whose keys are <c>Direction</c>s and whose values represent a potential neighbour <c>Cell</c> in that <c>Direction</c>.
        /// </summary>
        private Dictionary<Direction, Cell?> _adjecentCellSpaces = new Dictionary<Direction, Cell?>()
        {
            { Direction.North, null },
            { Direction.South, null },
            { Direction.East, null },
            { Direction.West, null },
        };

        /// <summary>
        /// List of <c>Cell</c>s that are directly linked to this <c>Cell</c>.
        /// </summary>
        /// <remarks>
        /// If <c>Cell</c>s are linked to each other, that means that there is no wall between them.
        /// </remarks>
        private List<Cell> _linkedCells = new List<Cell>();

        /// <summary>
        /// Initializes a <c>Cell</c> with a row index and a column index.
        /// </summary>
        /// <param name="rowIndex">Row index in <c>Maze</c>.</param>
        /// <param name="colIndex">Column index in <c>Maze</c>.</param>
        /// <exception cref="ArgumentException">Thrown when rowIndex or colIndex are less than 0 or more than 49.</exception>
        public Cell(int rowIndex, int colIndex)
        {
            if (rowIndex < 0 || rowIndex >= 50)
                throw new ArgumentException("Row index is not in correct range.");

            if (colIndex < 0 || colIndex >= 50)
                throw new ArgumentException("Column index is not in correct range.");

            RowIndex = rowIndex;
            ColumnIndex = colIndex;
        }

        /// <summary>
        /// Returns neighbour <c>Cell</c> in the <paramref name="direction"/> specified or null if space is empty.
        /// </summary>
        /// <param name="direction"><c>Direction</c> of the neighbour.</param>
        /// <returns>Neighbour <c>Cell</c> in the <paramref name="direction"/> specified or null if space is empty.</returns>
        public Cell? GetAdjecentCellSpace(Direction direction) => this._adjecentCellSpaces[direction];

        /// <summary>
        /// Returns read only dictionary of adjecent cell spaces.
        /// </summary>
        /// <returns>Read only dictionary of adjecent cell spaces.</returns>
        public ReadOnlyDictionary<Direction, Cell?> GetAdjecentCellSpaces() => this._adjecentCellSpaces.AsReadOnly();

        /// <summary>
        /// Sets <paramref name="anotherCell"/> as an adjecent <c>Cell</c> at specified <c>Direction</c> depending on <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction"><c>Direction</c> of <paramref name="anotherCell"/> from this <c>Cell</c>.</param>
        /// <param name="anotherCell">Neighbour <c>Cell</c>.</param>
        /// <param name="bidirectional">True if this method should also be called on <paramref name="anotherCell"/>.</param>
        /// <exception cref="CellException">Thrown when <paramref name="anotherCell"/> indexes do not match <paramref name="direction"/>.</exception>
        public void SetCellAsNeighbour(Direction direction, Cell? anotherCell, bool bidirectional = true)
        {
            if (anotherCell is null)
            {
                this._adjecentCellSpaces[direction] = null;
                return;
            }

            int targetRow = direction.CalculateRow(this.RowIndex);
            int targetColumn = direction.CalculateColumn(this.ColumnIndex);

            if (targetRow != anotherCell.RowIndex || targetColumn != anotherCell.ColumnIndex)
                throw new CellException("Adjecent cell coordinates do not match", anotherCell.RowIndex, anotherCell.ColumnIndex);

            this._adjecentCellSpaces[direction] = anotherCell;

            if (bidirectional)
                anotherCell.SetCellAsNeighbour(direction.Reverse(), this, false);
        }

        /// <summary>
        /// Sets <paramref name="anotherCell"/> as an adjecent <c>Cell</c>, with <c>Direction</c> depending on <paramref name="anotherCell"/> row and column indexes.
        /// </summary>
        /// <param name="anotherCell">Neighbour <c>Cell</c>.</param>
        /// <param name="bidirectional">True if this method should also be called on <paramref name="anotherCell"/>.</param>
        /// <exception cref="CellException">Thrown when <paramref name="anotherCell"/> indexes are not adjecent.</exception>
        public void SetCellAsNeighbour(Cell anotherCell, bool bidirectional = true)
        {
            int rowDifference = anotherCell.RowIndex - this.RowIndex;
            int columnDifference = anotherCell.ColumnIndex - this.ColumnIndex;

            if (!IsInRange(rowDifference, -1, 1) || !IsInRange(columnDifference, -1, 1))
                throw new CellException("Adjecent cell coordinates do not match", anotherCell.RowIndex, anotherCell.ColumnIndex);

            Direction direction = FindCardinalDirection(rowDifference, columnDifference) ?? throw new CellException("Adjecent cell coordinates do not match", anotherCell.RowIndex, anotherCell.ColumnIndex);

            this._adjecentCellSpaces[direction] = anotherCell;

            if (bidirectional)
                anotherCell.SetCellAsNeighbour(direction.Reverse(), this, false);
        }

        /// <summary>
        /// Returns true if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, edges including.
        /// </summary>
        /// <param name="value">Checked value.</param>
        /// <param name="min">Beginning of range.</param>
        /// <param name="max">End of range.</param>
        /// <returns>True if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, edges including.</returns>
        private bool IsInRange(int value, int min, int max) => value >= min && value <= max;

        /// <summary>
        /// Returns a <c>Direction</c> based on difference between row and column indexes.
        /// If there is no difference between indexes or result is not a <b>cardinal</b> direction, returns null.
        /// </summary>
        /// <param name="rowDifference">Difference between target row and original row.</param>
        /// <param name="columnDifference">Difference between target column and original column.</param>
        /// <returns><c>Direction</c> representing cardinal direction</returns>
        private Direction? FindCardinalDirection(int rowDifference, int columnDifference)
        {
            return (rowDifference, columnDifference) switch
            {
                (0, 0) => null,
                ( > 0, 0) => Direction.South,
                ( < 0, 0) => Direction.North,
                (0, > 0) => Direction.East,
                (0, < 0) => Direction.West,
                _ => null
            };
        }

        /// <summary>
        /// Returns a read only list of cells linked to this cell.
        /// </summary>
        /// <returns>Read only list of cells linked to this cell.</returns>
        public IEnumerable<Cell> GetLinkedCells() => this._linkedCells.AsReadOnly();

        /// <summary>
        /// Returns true if this <c>Cell</c> is linked to its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction"><c>Direction</c> of the neighbour.</param>
        /// <returns>True if this <c>Cell</c> is linked to its neighbour in specified <c>Direction</c>.</returns>
        public bool IsLinked(Direction direction) => this.IsLinked(this._adjecentCellSpaces[direction]);

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

            return _linkedCells.Contains(anotherCell);
        }

        /// <summary>
        /// Links this <c>Cell</c> to its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the neighbour <c>Cell</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when this <c>Cell</c> has no neighbour in specified <c>Direction</c>.</exception>
        public void LinkTo(Direction direction)
        {
            Cell? neighbourCell = this._adjecentCellSpaces[direction];
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

            if (!_adjecentCellSpaces.ContainsValue(anotherCell))
                throw new ArgumentException("To-be-linked cell needs to be in an adjecent cell space.");

            if (!_linkedCells.Contains(anotherCell))
                _linkedCells.Add(anotherCell);

            if (bidirectional)
                anotherCell.LinkTo(this, false);
        }

        /// <summary>
        /// Uninks this <c>Cell</c> from its neighbour in specified <c>Direction</c>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the neighbour <c>Cell</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when this <c>Cell</c> has no neighbour in specified <c>Direction</c>.</exception>
        public void UnlinkFrom(Direction direction)
        {
            Cell? neighbourCell = this._adjecentCellSpaces[direction];
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

            _linkedCells.Remove(anotherCell);

            if (bidirectional)
                anotherCell.UnlinkFrom(this, false);
        }

        /// <summary>
        /// Returns a collection of this <c>Cell</c>'s neighbours.
        /// </summary>
        /// <returns>Collection of this <c>Cell</c>'s neighbours.</returns>
        public IEnumerable<Cell> GetNeighbours() => _adjecentCellSpaces.Values.Where(c => c is not null).Select(c => c!);

        /// <summary>
        /// Returns a collection of this <c>Cell</c>'s neighbours in <c>Direction</c>s included in <paramref name="directions"/>.
        /// </summary>
        /// <param name="directions">Array containing the included <c>Direction</c>s.</param>
        /// <returns>Collection of this <c>Cell</c>'s neighbours in <c>Direction</c>s included in <paramref name="directions"/>.</returns>
        public IEnumerable<Cell> GetNeighbours(params Direction[] directions) => _adjecentCellSpaces.Where(c => directions.Contains(c.Key))
                                                                                                      .Where(c => c.Value is not null)
                                                                                                      .Select(c => c.Value!);

        /// <summary>
        /// Returns true if this <c>Cell</c> has neighbour in a <c>Direction</c> specified by <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The <c>Direction</c> of the searched for neighbour <c>Cell</c>.</param>
        /// <returns>True if this <c>Cell</c> has neighbour in a <c>Direction</c> specified by <paramref name="direction"/>.</returns>
        public bool HasNeighbour(Direction direction) => this._adjecentCellSpaces[direction] is not null;

        /// <summary>
        /// Returns <c>Direction</c> of a <paramref name="neighbour"/> <c>Cell</c>.
        /// </summary>
        /// <param name="neighbour">Neighbour <c>Cell</c> of this <c>Cell</c>.</param>
        /// <returns><c>Direction</c> of a <paramref name="neighbour"/> <c>Cell</c>.</returns>
        public Direction GetNeighbourDirection(Cell neighbour) => this._adjecentCellSpaces.First(s => s.Value == neighbour).Key;

        /// <summary>
        /// Returns true if this <c>Cell</c> is on <c>Maze</c> border.
        /// </summary>
        /// <param name="rows">Amount of rows in a <c>Maze</c>.</param>
        /// <param name="cols">Amount of columns in a <c>Maze</c>.</param>
        /// <returns>True if this <c>Cell</c> is on <c>Maze</c> border.</returns>
        public bool IsOnBorder(int rows, int cols) => RowIndex == 0 || RowIndex == rows - 1 || ColumnIndex == 0 || ColumnIndex == cols - 1;
    }
}
using System.Collections;
using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    /// <summary>
    /// The <c>Maze</c> class represents maze structure built with a matrix of <c>Cell</c>s.
    /// </summary>
    public class Maze : IEnumerable<Cell>
    {
        /// <summary>
        /// Gets the amount of rows in the <c>Maze</c>.
        /// </summary>
        public int RowAmount { get; }

        /// <summary>
        /// Gets the amount of columns in the <c>Maze</c>.
        /// </summary>
        public int ColumnAmount { get; }

        /// <summary>
        /// Gets the amount of all cells in maze.
        /// </summary>
        /// <remarks>
        /// This value should always be equal to <see cref="RowAmount"/> multiplied by <see cref="ColumnAmount"/>.
        /// </remarks>
        public int CellAmount { get; }

        /// <summary>
        /// 2D matrix of <c>Cell</c> objects that form the <c>Maze</c>.
        /// </summary>
        private List<List<Cell>> _cellMatrix = new List<List<Cell>>();

        /// <summary>
        /// Initializes <c>Maze</c> with an amount of rows and amount of columns.
        /// </summary>
        /// <param name="rows">Amount of rows.</param>
        /// <param name="columns">Amount of columns.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="rows"/> or <paramref name="columns"/> is less than 3 or more than 50.</exception>
        public Maze(int rows, int columns)
        {
            if (rows < 3 || rows > 50)
                throw new ArgumentException("Row amount is not in correct range.");

            if (columns < 3 || columns > 50)
                throw new ArgumentException("Column amount is not in correct range.");

            RowAmount = rows;
            ColumnAmount = columns;
            CellAmount = rows * columns;

            CreateCellMatrix();
            ConfigureCellMatrix();
        }

        /// <summary>
        /// Initializes <see cref="_cellMatrix"/>.
        /// </summary>
        private void CreateCellMatrix()
        {
            for (int row = 0; row < RowAmount; row++)
            {
                _cellMatrix.Add(new List<Cell>());
                FillRowWithCells(row);
            }
        }

        /// <summary>
        /// Creates and adds new <c>Cell</c>s to <paramref name="row"/>.
        /// </summary>
        /// <param name="row">The row to which new <c>Cell</c>s will be added to.</param>
        private void FillRowWithCells(int row)
        {
            for (int col = 0; col < ColumnAmount; col++)
            {
                Cell newCell = new Cell(row, col);
                _cellMatrix[row].Add(newCell);
            }
        }

        /// <summary>
        /// Creates references between neighbouring <c>Cell</c>s.
        /// </summary>
        private void ConfigureCellMatrix()
        {
            foreach (var cell in this)
            {
                int row = cell.RowIndex;
                int col = cell.ColumnIndex;

                cell.SetCellAsNeighbour(Direction.North, GetCell(row - 1, col), false);
                cell.SetCellAsNeighbour(Direction.South, GetCell(row + 1, col), false);
                cell.SetCellAsNeighbour(Direction.West, GetCell(row, col - 1), false);
                cell.SetCellAsNeighbour(Direction.East, GetCell(row, col + 1), false);
            }
        }

        /// <summary>
        /// Yields a <c>Cell</c> from <see cref="_cellMatrix"/>.
        /// </summary>
        /// <remarks>
        /// Travels from left to right, top to bottom.
        /// </remarks>
        /// <returns><c>Cell</c> from <see cref="_cellMatrix"/>.</returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var matrixRow in _cellMatrix)
            {
                foreach (var cell in matrixRow)
                {
                    yield return cell;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Yields row of <see cref="_cellMatrix"/>.
        /// </summary>
        /// <returns><see cref="_cellMatrix"/> row.</returns>
        public IEnumerable<List<Cell>> IterateRows()
        {
            foreach (List<Cell> row in _cellMatrix)
            {
                yield return row;
            }
        }

        /// <summary>
        /// Returns a <c>Cell</c> from <see cref="_cellMatrix"/> with the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates consisting of row index and height index.</param>
        /// <returns><c>Cell</c> from <see cref="_cellMatrix"/> with the specified coordinates or null if it is not found.</returns>
        public Cell? GetCell((int Row, int Column) coordinates) => this.GetCell(coordinates.Row, coordinates.Column);

        /// <summary>
        /// Returns a <c>Cell</c> from <see cref="_cellMatrix"/> with the specified coordinates.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns><c>Cell</c> from <see cref="_cellMatrix"/> with the specified coordinates or null if it is not found.</returns>
        public Cell? GetCell(int row, int column)
        {
            if (row < 0 || row >= RowAmount) return null;
            if (column < 0 || column >= ColumnAmount) return null;

            return _cellMatrix[row][column];
        }

        /// <summary>
        /// Gets a random <c>Cell</c> from <see cref="_cellMatrix"/>.
        /// </summary>
        /// <param name="rnd">Random seed.</param>
        /// <returns>Random <c>Cell</c> from <see cref="_cellMatrix"/>.</returns>
        public Cell GetRandomCell(Random rnd)
        {
            int rowIndex = rnd.Next(0, RowAmount);
            int columnIndex = rnd.Next(0, ColumnAmount);

            return GetCell(rowIndex, columnIndex)!;
        }

        /// <summary>
        /// Gets <c>Cell</c>s from <see cref="_cellMatrix"/> that are on the border of the <c>Maze</c>.
        /// </summary>
        /// <returns><c>Cell</c>s from <see cref="_cellMatrix"/> that are on the border of the <c>Maze</c>.</returns>
        public IEnumerable<Cell> GetBorderCells() => this.Where(c => c.IsOnBorder(RowAmount, ColumnAmount));

        /// <summary>
        /// Returns <paramref name="cellList"/> with the exclusion of <c>Cell</c>s that are too close to <paramref name="rootCell"/>.
        /// </summary>
        /// <param name="rootCell"><c>Cell</c> in the middle of exclusion zone.</param>
        /// <param name="cellList">The <c>Cell</c> list to be potentially truncated.</param>
        /// <returns><paramref name="cellList"/> with the exclusion of <c>Cell</c>s that are too close to <paramref name="rootCell"/>.</returns>
        public IEnumerable<Cell> ExcludeCellsCloseTo(Cell rootCell, IEnumerable<Cell> cellList)
        {
            var rowExclusionZone = CalculateExclusionZone(rootCell.RowIndex, this.RowAmount);
            var columnExclusionZone = CalculateExclusionZone(rootCell.ColumnIndex, this.ColumnAmount);

            return cellList.Where(c => (IsOutsideOfExclusionZone(c.RowIndex, rowExclusionZone)) &&
                                       (IsOutsideOfExclusionZone(c.ColumnIndex, columnExclusionZone)));
        }

        /// <summary>
        /// Calculates the beginning index and end index of an exclusion zone.
        /// </summary>
        /// <param name="index">Row or column index of the root.</param>
        /// <param name="dimensionLength">Length of the dimension of which exclusion zone should be calculated.</param>
        /// <returns>Beginning index and end index of an exclusion zone.</returns>
        private (int Beginning, int End) CalculateExclusionZone(int index, int dimensionLength)
        {
            return (Beginning: index - dimensionLength / 3, End: index + dimensionLength / 3);
        }

        /// <summary>
        /// Returns true if <paramref name="index"/> is outside of <paramref name="exclusionZone"/>.
        /// </summary>
        /// <param name="index">The tested index.</param>
        /// <param name="exclusionZone">Contains beginning index and end index of exclusion zone.</param>
        /// <returns>true if <paramref name="index"/> is outside of <paramref name="exclusionZone"/>.</returns>
        private bool IsOutsideOfExclusionZone(int index, (int Beginning, int End) exclusionZone)
        {
            return (index <= exclusionZone.Beginning || index >= exclusionZone.End);
        }
    }
}
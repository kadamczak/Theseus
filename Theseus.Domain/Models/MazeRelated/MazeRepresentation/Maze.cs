﻿using System.Collections;
using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    public class Maze : IEnumerable<Cell>
    {
        //Grid info
        public int RowAmount { get; }           //Y
        public int ColumnAmount { get; }        //X
        public int CellAmount { get; }

        //Storage
        List<List<Cell>> CellMatrix { get; } = new List<List<Cell>>();

        public Maze(int rows, int columns)
        {
            if (rows < 2)
                throw new ArgumentException("Row amount must be at least 2.");

            if (columns < 2)
                throw new ArgumentException("Column amount must be at least 2.");

            RowAmount = rows;
            ColumnAmount = columns;
            CellAmount = rows * columns;

            CreateCellMatrix();
            ConfigureCellMatrix();
        }

        private void CreateCellMatrix()
        {
            for (int row = 0; row < RowAmount; row++)
            {
                CellMatrix.Add(new List<Cell>());
                FillRowWithCells(row);
            }
        }

        private void FillRowWithCells(int row)
        {
            for (int col = 0; col < ColumnAmount; col++)
            {
                Cell newCell = new Cell(row, col);
                CellMatrix[row].Add(newCell);
            }
        }

        private void ConfigureCellMatrix()
        {
            foreach (var cell in this)
            {
                int row = cell.RowIndex;
                int col = cell.ColumnIndex;

                cell.AdjecentCellSpaces[Direction.North] = GetCell(row - 1, col);
                cell.AdjecentCellSpaces[Direction.South] = GetCell(row + 1, col);
                cell.AdjecentCellSpaces[Direction.West] = GetCell(row, col - 1);
                cell.AdjecentCellSpaces[Direction.East] = GetCell(row, col + 1);
            }
        }

        //ITERATORS==================================
        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var matrixRow in CellMatrix)
            {
                foreach (var cell in matrixRow)
                {
                    yield return cell;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<List<Cell>> IterateRows()
        {
            foreach (List<Cell> row in CellMatrix)
            {
                yield return row;
            }
        }

        //INTERACTION===============================
        public Cell? GetCell((int, int) coordinates) => this.GetCell(coordinates.Item1, coordinates.Item2);

        public Cell? GetCell(int row, int column)
        {
            if (row < 0 || row >= RowAmount) return null;
            if (column < 0 || column >= ColumnAmount) return null;

            return CellMatrix[row][column];
        }

        public Cell GetRandomCell()
        {
            Random rnd = new Random();
            int rowIndex = rnd.Next(0, RowAmount);
            int columnIndex = rnd.Next(0, ColumnAmount);

            return GetCell(rowIndex, columnIndex)!;
        }

        public IEnumerable<Cell> GetBorderCells() => this.Where(c => c.IsOnBorder(RowAmount, CellAmount));

        public IEnumerable<Cell> ExcludeCellsCloseTo(Cell rootCell, IEnumerable<Cell> cellList)
        {
            var rowExclusionZone = CalculateExclusionZone(rootCell.RowIndex, this.RowAmount);
            var columnExclusionZone = CalculateExclusionZone(rootCell.ColumnIndex, this.ColumnAmount);

            return cellList.Where(c => (IsOutsideOfExclusionZone(c.RowIndex, rowExclusionZone)) &&
                                       (IsOutsideOfExclusionZone(c.ColumnIndex, columnExclusionZone)));
        }

        private (int Beginning, int End) CalculateExclusionZone(int index, int dimensionLength)
        {
            return (Beginning: index - dimensionLength / 3, End: index + dimensionLength / 3);
        }

        private bool IsOutsideOfExclusionZone(int index, (int Beginning, int End) exclusionZone)
        {
            return (index <= exclusionZone.Beginning || index >= exclusionZone.End);
        }

        //VISUALIZATION ASCII==============================================
        public override string ToString()
        {
            string gridText = GetUpperBorder();

            foreach (var row in IterateRows())
            {
                string rowEastText = "|";
                string rowSouthText = "+";

                foreach (var cell in row)
                {
                    rowEastText += GetCellEastText(cell);
                    rowSouthText += GetCellSouthText(cell);
                }

                gridText += rowEastText + "\n";
                gridText += rowSouthText + "\n";
            }

            return gridText;
        }

        private string GetUpperBorder()
        {
            string upperBorder = "+";
            for (int i = 0; i < ColumnAmount; i++)
            {
                upperBorder += "---+";
            }
            upperBorder += "\n";
            return upperBorder;
        }

        private string GetCellEastText(Cell cell)
        {
            string cellMiddle = "   ";
            string cellEastBoundary = cell.IsLinkedToNeighbour(Direction.East) ? " " : "|";

            return cellMiddle + cellEastBoundary;
        }

        private string GetCellSouthText(Cell cell)
        {
            string cellSouthBoundary = cell.IsLinkedToNeighbour(Direction.South) ? "   " : "---";
            string cellCorner = "+";

            return cellSouthBoundary + cellCorner;
        }

    }
}

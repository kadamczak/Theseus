using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.HelperClasses;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for MazeSolutionCanvasView.xaml
    /// </summary>
    public partial class SolutionCanvasView : UserControl
    {
        private readonly LineDrawer _lineDrawer;
        public Canvas Canvas { get; }
        Direction[] directions = new Direction[4] { Direction.West, Direction.North, Direction.East, Direction.South };

        private readonly Color lineColor = Colors.Lavender;
        private readonly int lineThickness = 15;

        public SolutionCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("SolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
        }

        public void DrawSolutionPath()
        {
            var viewModel = (SolutionCanvasViewModel)this.DataContext;
            Canvas.Children.Clear();

            int cellSize = 30;

            Cell? previousCell = null;
            for (int i = 0; i < viewModel.SolutionPath.Count(); i++)
            {
                Cell currentCell = viewModel.SolutionPath[i];
                Cell? nextCell = viewModel.SolutionPath.ElementAtOrDefault(i + 1);

                Direction? mazeEntryDirection = FindMazeEntryDirectionInCell(currentCell, viewModel.SolutionPath, viewModel.StartDirection, viewModel.EndDirection);
                DrawSolutionPathInCell(previousCell, currentCell, nextCell, mazeEntryDirection, cellSize);
                previousCell = currentCell;
            }
        }

        private Direction? FindMazeEntryDirectionInCell(Cell cell, List<Cell> solutionPath, Direction start, Direction end)
        {
            Direction? mazeEntry = (cell == solutionPath.First()) ? start : null;
            return (cell == solutionPath.Last()) ? end : mazeEntry;
        }

        private void DrawSolutionPathInCell(Cell? previousCell, Cell currentCell, Cell? nextCell, Direction? mazeEntryDirection, int cellSize)
        {
            (int x, int y) centerPoint = CalculateCellCenter(currentCell, cellSize);
            (int x, int y) entryPoint = FindCellBorderPoint(previousCell, currentCell, centerPoint, mazeEntryDirection, cellSize);
            (int x, int y) exitPoint = FindCellBorderPoint(nextCell, currentCell, centerPoint, mazeEntryDirection, cellSize);

            _lineDrawer.DrawLine(entryPoint, centerPoint, lineColor, lineThickness);
            _lineDrawer.DrawLine(centerPoint, exitPoint, lineColor, lineThickness);
        }

        private (int, int) FindCellBorderPoint(Cell? comparedCell, Cell currentCell, (int x, int y) cellCenterPoint, Direction? entryDirection, int cellSize)
        {
            if (comparedCell is null)
                return CalculateCellBorderPoint(entryDirection!.Value, cellCenterPoint, cellSize);

            foreach (var direction in directions)
            {
                if (comparedCell == currentCell.AdjecentCellSpaces[direction])
                    return CalculateCellBorderPoint(direction, cellCenterPoint, cellSize);
            }
            throw new ArgumentException("Compared cell must be adjecent to current cell.");
        }


        private (int x, int y) CalculateCellCenter(Cell cell, int cellSize) => (x: cell.ColumnIndex * cellSize + cellSize / 2,
                                                                                y: cell.RowIndex * cellSize + cellSize / 2);

        private (int x, int y) CalculateCellBorderPoint(Direction direction, (int x, int y) center, int cellSize)
        {
            int halfCellSize = cellSize / 2;
            return direction switch
            {
                Direction.West => (x: center.x - halfCellSize, y: center.y),
                Direction.North => (x: center.x, y: center.y - halfCellSize),
                Direction.East => (x: center.x + halfCellSize, y: center.y),
                Direction.South => (x: center.x, y: center.y + halfCellSize),
            };
        }
    }
}
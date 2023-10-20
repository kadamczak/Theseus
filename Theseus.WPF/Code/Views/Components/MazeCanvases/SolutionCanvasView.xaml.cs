using System;
using System.Collections.Generic;
using System.Drawing;
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

        private readonly System.Windows.Media.Color lineColor = Colors.Lavender;
        private readonly int lineThickness = 15;

        public SolutionCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("SolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
        }

        public void Clear() => Canvas.Children.Clear();

        public void DrawEntryArrows()
        {
            var viewModel = (SolutionCanvasViewModel)this.DataContext;
            Cell startCell = viewModel.SolutionPath.First();
            Cell endCell = viewModel.SolutionPath.Last();

            //(int x, int y)

            //Canvas.Children.Add();
        }

        public void DrawSolutionPath()
        {
            var viewModel = (SolutionCanvasViewModel)this.DataContext;
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
            Point centerPoint = CalculateCellCenter(currentCell, cellSize);
            Point entryPoint = FindCellBorderPoint(previousCell, currentCell, centerPoint, mazeEntryDirection, cellSize);
            Point exitPoint = FindCellBorderPoint(nextCell, currentCell, centerPoint, mazeEntryDirection, cellSize);

            _lineDrawer.DrawLine(entryPoint, centerPoint, lineColor, lineThickness);
            _lineDrawer.DrawLine(centerPoint, exitPoint, lineColor, lineThickness);
        }

        private Point FindCellBorderPoint(Cell? comparedCell, Cell currentCell, Point cellCenterPoint, Direction? entryDirection, int cellSize)
        {
            if (comparedCell is null)
                return CalculateCellBorderPoint(entryDirection!.Value, cellCenterPoint, cellSize);

            Direction directionToComparedCell = currentCell.GetNeighbourDirection(comparedCell);
            return CalculateCellBorderPoint(directionToComparedCell, cellCenterPoint, cellSize);
        }


        private Point CalculateCellCenter(Cell cell, int cellSize) => new Point(x: cell.ColumnIndex * cellSize + cellSize / 2,
                                                                                y: cell.RowIndex * cellSize + cellSize / 2);

        private Point CalculateCellBorderPoint(Direction direction, Point center, int cellSize)
        {
            int halfCellSize = cellSize / 2;
            return direction switch
            {
                Direction.West => new Point(x: center.X - halfCellSize, y: center.Y),
                Direction.North => new Point(x: center.X, y: center.Y - halfCellSize),
                Direction.East => new Point(x: center.X + halfCellSize, y: center.Y),
                Direction.South => new Point(x: center.X, y: center.Y + halfCellSize),
            };
        }
    }
}
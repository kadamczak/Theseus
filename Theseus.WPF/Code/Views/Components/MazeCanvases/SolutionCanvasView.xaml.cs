using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Theseus.Domain.Extensions;
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
            int cellSize = 30;
            DrawArrow(viewModel.SolutionPath.First(), viewModel.StartDirection, cellSize, pointToMaze: true);
            DrawArrow(viewModel.SolutionPath.Last(), viewModel.EndDirection, cellSize, pointToMaze: false);
        }

        private void DrawArrow(Cell cell, Direction entryDirection, int cellSize, bool pointToMaze)
        {
            Point cellCenter = CalculateCellCenter(cell, cellSize);
            Point borderPoint = CalculatePointInDirection(entryDirection, origin: cellCenter, distance: cellSize);
            Point awayPoint = CalculatePointInDirection(entryDirection, origin: borderPoint, distance: cellSize);

            Direction arrowDirection = (pointToMaze) ? entryDirection.Reverse() : entryDirection;
            Point arrowBeginning = (pointToMaze) ? awayPoint : borderPoint;
            Point arrowTip = (pointToMaze) ? borderPoint : awayPoint;

            Point arrowLeftWing = CalculateLeftWingPoint(arrowTip, arrowDirection, cellSize / 3);
            Point arrowRightWing = CalculateRightWingPoint(arrowTip, arrowDirection, cellSize / 3);

            _lineDrawer.DrawLine(arrowBeginning, arrowTip);
            _lineDrawer.DrawLine(arrowTip, arrowLeftWing);
            _lineDrawer.DrawLine(arrowTip, arrowRightWing);
        }

        private Point CalculateLeftWingPoint(Point arrowTip, Direction direction, int wingDistance)
        {
            return direction switch
            {
                Direction.North or Direction.East => new Point(arrowTip.X - wingDistance, arrowTip.Y + wingDistance),
                Direction.South => new Point(arrowTip.X + wingDistance, arrowTip.Y - wingDistance),
                Direction.West => new Point(arrowTip.X + wingDistance, arrowTip.Y + wingDistance)
            };
        }

        private Point CalculateRightWingPoint(Point arrowTip, Direction direction, int wingDistance)
        {
            return direction switch
            {
                Direction.South or Direction.East => new Point(arrowTip.X - wingDistance, arrowTip.Y - wingDistance),
                Direction.North => new Point(arrowTip.X + wingDistance, arrowTip.Y + wingDistance),
                Direction.West => new Point(arrowTip.X + wingDistance, arrowTip.Y - wingDistance)
            };
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
                return CalculatePointInDirection(entryDirection!.Value, cellCenterPoint, cellSize / 2);

            Direction directionToComparedCell = currentCell.GetNeighbourDirection(comparedCell);
            return CalculatePointInDirection(directionToComparedCell, cellCenterPoint, cellSize / 2);
        }


        private Point CalculateCellCenter(Cell cell, int cellSize) => new Point(x: cell.ColumnIndex * cellSize + cellSize / 2,
                                                                                y: cell.RowIndex * cellSize + cellSize / 2);

        private Point CalculatePointInDirection(Direction direction, Point origin, int distance)
        {
            return direction switch
            {
                Direction.West => new Point(x: origin.X - distance, y: origin.Y),
                Direction.North => new Point(x: origin.X, y: origin.Y - distance),
                Direction.East => new Point(x: origin.X + distance, y: origin.Y),
                Direction.South => new Point(x: origin.X, y: origin.Y + distance),
            };
        }
    }
}
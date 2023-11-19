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
        public Canvas Canvas { get; }
        private readonly LineDrawer _lineDrawer;
        private readonly PointCalculator _pointCalculator;

        private readonly Direction[] _directions = new Direction[4] { Direction.West, Direction.North, Direction.East, Direction.South };
        private readonly System.Windows.Media.Color lineColor = Colors.LightBlue;

        public SolutionCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("SolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
            this._pointCalculator = new PointCalculator();
        }

        public void Clear() => Canvas.Children.Clear();

        public void DrawEntryArrows(float cellSize)
        {
            var viewModel = GetDataContext();
            DrawArrow(viewModel.SolutionPath.First(), viewModel.StartDirection, cellSize, pointToMaze: true);
            DrawArrow(viewModel.SolutionPath.Last(), viewModel.EndDirection, cellSize, pointToMaze: false);
        }

        private void DrawArrow(Cell cell, Direction entryDirection, float cellSize, bool pointToMaze)
        {
            PointF cellCenter = _pointCalculator.CalculateCellCenter(cell, cellSize);
            PointF borderPoint = _pointCalculator.CalculatePointInDirection(entryDirection, origin: cellCenter, distance: cellSize);
            PointF awayPoint = _pointCalculator.CalculatePointInDirection(entryDirection, origin: borderPoint, distance: cellSize * 0.7f);

            Direction arrowDirection = (pointToMaze) ? entryDirection.Reverse() : entryDirection;
            PointF arrowBeginning = (pointToMaze) ? awayPoint : borderPoint;
            PointF arrowTip = (pointToMaze) ? borderPoint : awayPoint;

            PointF arrowLeftWing = CalculateLeftWingPoint(arrowTip, arrowDirection, cellSize / 4);
            PointF arrowRightWing = CalculateRightWingPoint(arrowTip, arrowDirection, cellSize / 4);

            _lineDrawer.DrawLine(arrowBeginning, arrowTip);
            _lineDrawer.DrawLine(arrowTip, arrowLeftWing);
            _lineDrawer.DrawLine(arrowTip, arrowRightWing);
        }

        private PointF CalculateLeftWingPoint(PointF arrowTip, Direction direction, float wingDistance)
        {
            return direction switch
            {
                Direction.North or Direction.East => new PointF(arrowTip.X - wingDistance, arrowTip.Y + wingDistance),
                Direction.South => new PointF(arrowTip.X + wingDistance, arrowTip.Y - wingDistance),
                Direction.West => new PointF(arrowTip.X + wingDistance, arrowTip.Y + wingDistance)
            };
        }

        private PointF CalculateRightWingPoint(PointF arrowTip, Direction direction, float wingDistance)
        {
            return direction switch
            {
                Direction.South or Direction.East => new PointF(arrowTip.X - wingDistance, arrowTip.Y - wingDistance),
                Direction.North => new PointF(arrowTip.X + wingDistance, arrowTip.Y + wingDistance),
                Direction.West => new PointF(arrowTip.X + wingDistance, arrowTip.Y - wingDistance)
            };
        }

        public void DrawSolutionPath(float cellSize)
        {
            var viewModel = GetDataContext();

            Cell? previousCell = null;
            for (int i = 0; i < viewModel.SolutionPath.Count; i++)
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

        private void DrawSolutionPathInCell(Cell? previousCell, Cell currentCell, Cell? nextCell, Direction? mazeEntryDirection, float cellSize)
        {
            PointF centerPoint = _pointCalculator.CalculateCellCenter(currentCell, cellSize);
            PointF entryPoint = FindCellBorderPoint(previousCell, currentCell, centerPoint, mazeEntryDirection, cellSize);
            PointF exitPoint = FindCellBorderPoint(nextCell, currentCell, centerPoint, mazeEntryDirection, cellSize);

            float lineThickness = cellSize * 0.6f;
            _lineDrawer.DrawLine(entryPoint, centerPoint, lineColor, lineThickness);
            _lineDrawer.DrawLine(centerPoint, exitPoint, lineColor, lineThickness);
        }

        private PointF FindCellBorderPoint(Cell? comparedCell, Cell currentCell, PointF cellCenterPoint, Direction? entryDirection, float cellSize)
        {
            if (comparedCell is null)
                return _pointCalculator.CalculatePointInDirection(entryDirection!.Value, cellCenterPoint, cellSize / 2);

            Direction directionToComparedCell = currentCell.GetNeighbourDirection(comparedCell);
            return _pointCalculator.CalculatePointInDirection(directionToComparedCell, cellCenterPoint, cellSize / 2);
        }

        private SolutionCanvasViewModel GetDataContext() => (SolutionCanvasViewModel)this.DataContext;
    }
}
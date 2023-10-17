using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeCanvas.xaml
    /// </summary>
    public partial class MazeCanvasView : UserControl
    {
        Direction[] directions = new Direction[4] { Direction.West, Direction.North, Direction.East, Direction.South };

        public MazeCanvasView()
        {
            InitializeComponent();
        }

        private void Maze_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DrawMaze();
        }

        private void DrawMaze()
        {
            var viewModel = (MazeCanvasViewModel)this.DataContext;
            MazeWithSolution maze = viewModel.Maze;

            Canvas canvas = this.FindName("Maze") as Canvas;
            canvas.Children.Clear();

            int cellSize = 30;

            int imageHeight = cellSize * maze.Grid.RowAmount;
            int imageWidth = cellSize * maze.Grid.ColumnAmount;

            if (imageHeight > canvas.Height || imageWidth > canvas.Width)
                return;

            foreach (var cell in maze.Grid)
            {
                Direction? mazeEntry = FindMazeEntryDirectionInCell(cell, maze);
                DrawWallsOfCell(canvas, cell, cellSize, mazeEntry);
            }

            Cell? previousCell = null;
            for(int i = 0; i < maze.SolutionPath.Count(); i++)
            {
                Cell currentCell = maze.SolutionPath[i];
                Cell? nextCell = (i != maze.SolutionPath.Count() - 1) ? maze.SolutionPath[i + 1] : null;

                Direction? mazeEntry = FindMazeEntryDirectionInCell(currentCell, maze);
                DrawSolutionPathInCell(previousCell, currentCell, nextCell, mazeEntry, cellSize, canvas);
                previousCell = currentCell;
            }
        }

        private Direction? FindMazeEntryDirectionInCell(Cell cell, MazeWithSolution maze)
        {
            Direction? mazeEntry = (cell == maze.SolutionPath.First()) ? maze.StartDirection : null;
            return (cell == maze.SolutionPath.Last()) ? maze.EndDirection : mazeEntry;
        }

        private void DrawWallsOfCell(Canvas canvas, Cell cell, int cellSize, Direction? mazeEntry)
        {
            (int x, int y) start = (x: cell.ColumnIndex * cellSize,
                                      y: cell.RowIndex * cellSize);

            (int x, int y) end = (x: start.x + cellSize,
                                          y: start.y + cellSize);

            if (OnClosedBorder(cell, Direction.North, mazeEntry))
                this.DrawLine(canvas, start.x, start.y, end.x, start.y);

            if (OnClosedBorder(cell, Direction.West, mazeEntry))
                this.DrawLine(canvas, start.x, start.y, start.x, end.y);

            if (NotLinkedToNeighbour(cell, Direction.East, mazeEntry))
                this.DrawLine(canvas, end.x, start.y, end.x, end.y);

            if (NotLinkedToNeighbour(cell, Direction.South, mazeEntry))
                this.DrawLine(canvas, start.x, end.y, end.x, end.y);
        }

        private bool OnClosedBorder(Cell cell, Direction neighbourDirection, Direction? mazeEntry)
        {
            return !cell.HasNeighbour(neighbourDirection) && neighbourDirection != mazeEntry;
        }

        private bool NotLinkedToNeighbour(Cell cell, Direction neighbourDirection, Direction? mazeEntry)
        {
            return !cell.IsLinkedToNeighbour(neighbourDirection) && neighbourDirection != mazeEntry;
        }

        //x1,y1---->x2,y1
        //|
        //|
        //V
        //x1,y2     x2,y2
        private void DrawLine(Canvas canvas, int x1, int y1, int x2, int y2, Color? color = null, int strokeThickness = 2)
        {
            DrawLine(canvas, (x1, y1), (x2, y2), color, strokeThickness);
        }

        private void DrawLine(Canvas canvas, (int x, int y) start, (int x, int y) end, Color? color = null, int strokeThickness = 2)
        {
            Line line = new Line()
            {
                X1 = start.x,
                Y1 = start.y,
                X2 = end.x,
                Y2 = end.y
            };

            line.StrokeDashCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;

            line.StrokeThickness = strokeThickness;
            line.Stroke = new SolidColorBrush(color ?? Colors.Black);

            canvas.Children.Add(line);
        }

        private void DrawSolutionPathInCell(Cell? previousCell, Cell currentCell, Cell? nextCell, Direction? mazeEntry, int cellSize, Canvas canvas)
        {
            int halfCellSize = cellSize / 2;
            (int x, int y) cellCenterPoint = (x: currentCell.ColumnIndex * cellSize + halfCellSize,
                                              y: currentCell.RowIndex * cellSize + halfCellSize);

            (int x, int y) cellSolutionEntryPoint = FindBorderPoint(previousCell, currentCell, mazeEntry, cellCenterPoint, halfCellSize);
            (int x, int y) cellSolutionExitPoint = FindBorderPoint(nextCell, currentCell, mazeEntry, cellCenterPoint, halfCellSize);

            DrawLine(canvas, cellSolutionEntryPoint, cellCenterPoint, Colors.Lavender, 15);
            DrawLine(canvas, cellCenterPoint, cellSolutionExitPoint, Colors.Lavender, 15);
        }

        private (int, int) FindBorderPoint(Cell? comparedCell, Cell currentCell, Direction? mazeEntry, (int x, int y) cellCenterPoint, int halfCellSize)
        {
            if (comparedCell is null)
                return CalculateSolutionCellBorderPoint(mazeEntry!.Value, cellCenterPoint, halfCellSize);

            foreach (var direction in directions)
            {
                if (comparedCell == currentCell.AdjecentCellSpaces[direction])
                {
                    return CalculateSolutionCellBorderPoint(direction, cellCenterPoint, halfCellSize);
                }
            }
            throw new ArgumentException("lol");
        }

        private (int x, int y) CalculateSolutionCellBorderPoint(Direction direction, (int x, int y) center, int halfCellSize)
        {
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

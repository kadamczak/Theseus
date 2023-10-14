using System;
using System.Linq;
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
                Direction? mazeEntry;
                mazeEntry = (cell == maze.SolutionPath.First()) ? maze.StartDirection : null;
                mazeEntry = (cell == maze.SolutionPath.Last()) ? maze.EndDirection : mazeEntry;
                DrawWallsOfCell(canvas, cell, cellSize, mazeEntry);

                if(maze.SolutionPath.Contains(cell))
                {
                    int x1 = cell.ColumnIndex * cellSize;
                    int y1 = cell.RowIndex * cellSize;

                    int x2 = x1 + cellSize;
                    int y2 = y1 + cellSize;

                    this.AddWall(canvas, x1, y1, x2, y2, Colors.LightGray);
                    this.AddWall(canvas, x2, y1, x1, y2, Colors.LightGray);
                }
            }
        }

        private void DrawWallsOfCell(Canvas canvas, Cell cell, int cellSize, Direction? mazeEntry)
        {
            int x1 = cell.ColumnIndex * cellSize;
            int y1 = cell.RowIndex * cellSize;

            int x2 = x1 + cellSize;
            int y2 = y1 + cellSize;

            if (OnClosedBorder(cell, Direction.North, mazeEntry))
                this.AddWall(canvas, x1, y1, x2, y1);

            if (OnClosedBorder(cell, Direction.West, mazeEntry))
                this.AddWall(canvas, x1, y1, x1, y2);

            if (NotLinkedToNeighbour(cell, Direction.East, mazeEntry))
                this.AddWall(canvas, x2, y1, x2, y2);

            if (NotLinkedToNeighbour(cell, Direction.South, mazeEntry))
                this.AddWall(canvas, x1, y2, x2, y2);
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
        private void AddWall(Canvas canvas, int startX, int startY, int endX, int endY, Color? color = null)
        {
            Line wall = new Line()
            {
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY
            };

            wall.StrokeThickness = 2;
            wall.Stroke = new SolidColorBrush(color ?? Colors.Black);

            canvas.Children.Add(wall);
        }
    }
}

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
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
            Maze maze = viewModel.Maze;

            Canvas canvas = this.FindName("Maze") as Canvas;
            canvas.Children.Clear();

            int cellSize = 30;

            int imageHeight = cellSize * maze.RowAmount;
            int imageWidth = cellSize * maze.ColumnAmount;

            if (imageHeight > canvas.Height || imageWidth > canvas.Width)
                return;

            foreach (var cell in maze)
            {
                int x1 = cell.ColumnIndex * cellSize;
                int y1 = cell.RowIndex * cellSize;

                int x2 = x1 + cellSize;
                int y2 = y1 + cellSize;

                if (!cell.HasNeighbour(Direction.North))
                    this.AddWall(canvas, x1, y1, x2, y1);

                if (!cell.HasNeighbour(Direction.West))
                    this.AddWall(canvas, x1, y1, x1, y2);

                if (!cell.IsLinkedToNeighbour(Direction.East))
                    this.AddWall(canvas, x2, y1, x2, y2);

                if (!cell.IsLinkedToNeighbour(Direction.South))
                    this.AddWall(canvas, x1, y2, x2, y2);

            }
        }


        //x1,y1---->x2,y1
        //|
        //|
        //V
        //x1,y2     x2,y2
        private void AddWall(Canvas canvas, int startX, int startY, int endX, int endY)
        {
            Line wall = new Line()
            {
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY
            };

            wall.StrokeThickness = 2;
            wall.Stroke = new SolidColorBrush(Colors.Black);

            canvas.Children.Add(wall);
        }
    }
}

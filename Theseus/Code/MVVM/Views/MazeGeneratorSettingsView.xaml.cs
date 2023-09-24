using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.Generators;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MazeGeneratorSettingsView.xaml
    /// </summary>
    public partial class MazeGeneratorSettingsView : UserControl
    {
        public MazeGeneratorSettingsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int mazeHeight = 20;
            int mazeWidth = 25;

            MazeGenerator generator = MazeGeneratorFactory.Create(MazeGenAlgorithm.Sidewinder);
            MazeGrid maze = generator.GenerateMaze(mazeHeight, mazeWidth);

            Canvas canvas = this.FindName("MazeCanvas") as Canvas;

            canvas.Children.Clear();
            
            int cellSize = 15;

            int imageHeight = cellSize * mazeHeight;
            int imageWidth = cellSize * mazeWidth;

            if (imageHeight > canvas.Height || imageWidth > canvas.Width)
                return;

            //x1,y1---->x2,y1
            //|
            //|
            //V
            //x1,y2     x2,y2

            foreach(var cell in maze)
            {
                int x1 = cell.ColumnIndex * cellSize;
                int y1 = cell.RowIndex * cellSize;

                int x2 = x1 + cellSize;
                int y2 = y1 + cellSize;

                if (!cell.HasNeighbour(Direction.North))
                    this.AddWall(canvas, x1, y1, x2, y1);

                if (!cell.HasNeighbour(Direction.West))
                    this.AddWall(canvas, x1, y1, x1, y2);

                if (!cell.IsLinked(cell.AdjecentCellSpaces[Direction.East]))
                    this.AddWall(canvas, x2, y1, x2, y2);

                if (!cell.IsLinked(cell.AdjecentCellSpaces[Direction.South]))
                    this.AddWall(canvas, x1, y2, x2, y2);

            }
        }

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

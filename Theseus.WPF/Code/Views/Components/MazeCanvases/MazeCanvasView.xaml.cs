using System.Drawing;
using System.Windows.Controls;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.HelperClasses;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for MazeCanvas.xaml
    /// </summary>
    public partial class MazeCanvasView : UserControl
    {
        private readonly LineDrawer _lineDrawer;
        public Canvas Canvas { get; }

        public MazeCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("MazeCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
        }

        public int CalculateCellSize(int minCellSize)
        {
            Maze maze = GetDataContext().Maze;
            bool resizeToHeight = maze.RowAmount > maze.ColumnAmount;
            int cellSize = CalculateCellSizeBasingOnDimension(maze, resizeToHeight);
            cellSize = RecalculateCellSizeIfOtherDimensionDoesntFit(maze, resizeToHeight, cellSize);

            if (cellSize < minCellSize)
            {
                cellSize = minCellSize;
                //TODO
            }
            return cellSize;
        }

        private int CalculateCellSizeBasingOnDimension(Maze maze, bool resizeToHeight)
        {
            double dimensionLength = (resizeToHeight) ? this.ActualHeight : this.ActualWidth;
            int cellsInDimension = (resizeToHeight) ? maze.RowAmount : maze.ColumnAmount;
            return (int)(dimensionLength / cellsInDimension);
        }

        private int RecalculateCellSizeIfOtherDimensionDoesntFit(Maze maze, bool resizeToHeight, int cellSize)
        {
            int otherDimensionMazeLength = (resizeToHeight) ? cellSize * maze.ColumnAmount : cellSize * maze.RowAmount;
            double otherDimensionLength = (resizeToHeight) ? this.ActualWidth : this.ActualHeight;
            return (otherDimensionMazeLength <= otherDimensionLength) ? cellSize : CalculateCellSizeBasingOnDimension(maze, !resizeToHeight);
        }

        public void DrawScaledMaze(int minCellSize)
        {
            int cellSize = CalculateCellSize(minCellSize);
            DrawMaze(cellSize);
        }

        public void DrawMaze(int cellSize)
        {
            var viewModel = (MazeCanvasViewModel)this.DataContext;
            Maze maze = viewModel.Maze;
            Canvas.Children.Clear();

            foreach (var cell in maze)
            {
                DrawWallsOfCell(cell, cellSize);
            }
        }

        private void DrawWallsOfCell(Cell cell, int cellSize)
        {
            Point start = new Point(x: cell.ColumnIndex * cellSize,
                                    y: cell.RowIndex * cellSize);

            Point end = new Point(x: start.X + cellSize,
                                  y: start.Y + cellSize);

            string cellTag = $"{cell.RowIndex}-{cell.ColumnIndex}";

            if (!cell.HasNeighbour(Direction.North))
                _lineDrawer.DrawLine(start.X, start.Y, end.X, start.Y, tag: CreateWallTag(cellTag, Direction.North));

            if (!cell.HasNeighbour(Direction.West))
                _lineDrawer.DrawLine(start.X, start.Y, start.X, end.Y, tag: CreateWallTag(cellTag, Direction.West));

            if (!cell.IsLinkedToNeighbour(Direction.East))
                _lineDrawer.DrawLine(end.X, start.Y, end.X, end.Y, tag: CreateWallTag(cellTag, Direction.East));

            if (!cell.IsLinkedToNeighbour(Direction.South))
                _lineDrawer.DrawLine(start.X, end.Y, end.X, end.Y, tag: CreateWallTag(cellTag, Direction.South));
        }

        private string CreateWallTag(string cellTag, Direction direction) => cellTag + "-" + (int)direction;
        private MazeCanvasViewModel GetDataContext() => (MazeCanvasViewModel)this.DataContext;
    }
}
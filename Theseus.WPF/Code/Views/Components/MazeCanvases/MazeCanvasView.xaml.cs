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
        public Canvas Canvas { get; }
        private readonly LineDrawer _lineDrawer;

        public MazeCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("MazeCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
        }

        public void DrawScaledMaze(float minCellSize)
        {
            float cellSize = CalculateCellSize(minCellSize);
            DrawMaze(cellSize);
        }

        public void DrawMaze(float cellSize)
        {
            Maze maze = GetMazeFromDataContext();
            Canvas.Children.Clear();

            foreach (var cell in maze)
            {
                DrawWallsOfCell(cell, cellSize);
            }
        }

        private void DrawWallsOfCell(Cell cell, float cellSize)
        {
            PointF start = new PointF(x: cell.ColumnIndex * cellSize,
                                      y: cell.RowIndex * cellSize);

            PointF end = new PointF(x: start.X + cellSize,
                                    y: start.Y + cellSize);

            string cellTag = $"{cell.RowIndex}-{cell.ColumnIndex}";

            if (!cell.HasNeighbour(Direction.North))
                _lineDrawer.DrawLine(start.X, start.Y, end.X, start.Y, tag: CreateWallTag(cellTag, Direction.North));

            if (!cell.HasNeighbour(Direction.West))
                _lineDrawer.DrawLine(start.X, start.Y, start.X, end.Y, tag: CreateWallTag(cellTag, Direction.West));

            if (!cell.IsLinked(Direction.East))
                _lineDrawer.DrawLine(end.X, start.Y, end.X, end.Y, tag: CreateWallTag(cellTag, Direction.East));

            if (!cell.IsLinked(Direction.South))
                _lineDrawer.DrawLine(start.X, end.Y, end.X, end.Y, tag: CreateWallTag(cellTag, Direction.South));
        }

        private string CreateWallTag(string cellTag, Direction direction) => cellTag + "-" + (int)direction;

        public float CalculateCellSize(float minCellSize)
        {
            Maze maze = GetMazeFromDataContext();
            bool resizeToHeight = maze.RowAmount > maze.ColumnAmount;
            float cellSize = CalculateCellSizeBasingOnDimension(maze, resizeToHeight);
            cellSize = RecalculateCellSizeIfOtherDimensionDoesntFit(maze, resizeToHeight, cellSize);

            if (cellSize < minCellSize)
            {
                cellSize = minCellSize;
                //TODO
            }
            return cellSize;
        }

        private float CalculateCellSizeBasingOnDimension(Maze maze, bool resizeToHeight)
        {
            float dimensionLength = (float)((resizeToHeight) ? this.ActualHeight : this.ActualWidth);
            int cellsInDimension = (resizeToHeight) ? maze.RowAmount : maze.ColumnAmount;
            return dimensionLength / cellsInDimension;
        }

        private float RecalculateCellSizeIfOtherDimensionDoesntFit(Maze maze, bool resizeToHeight, float cellSize)
        {
            float otherDimensionMazeLength = (resizeToHeight) ? CalculateMazeWidth(cellSize) : CalculateMazeHeight(cellSize);
            double otherDimensionLength = (resizeToHeight) ? this.ActualWidth : this.ActualHeight;
            return (otherDimensionMazeLength <= otherDimensionLength) ? cellSize : CalculateCellSizeBasingOnDimension(maze, !resizeToHeight);
        }

        public float CalculateMazeHeight(float cellSize) => GetMazeRowAmount() * cellSize;
        public float CalculateMazeWidth(float cellSize) => GetMazeColumnAmount() * cellSize;
        public int GetMazeRowAmount() => GetMazeFromDataContext().RowAmount;
        public int GetMazeColumnAmount() => GetMazeFromDataContext().ColumnAmount;

        private MazeCanvasViewModel GetDataContext() => (MazeCanvasViewModel)this.DataContext;
        private Maze GetMazeFromDataContext() => GetDataContext().Maze;
    }
}
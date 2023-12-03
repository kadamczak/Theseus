using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for MazeWithSolutionCanvasView.xaml
    /// </summary>
    public partial class MazeWithSolutionCanvasView : UserControl
    {
        private readonly MazeCanvasView _mazeCanvasView;
        private readonly SolutionCanvasView _solutionCanvasView;

        public MazeWithSolutionCanvasView()
        {
            InitializeComponent();
            this._mazeCanvasView = this.FindName("MazeCanvasView")! as MazeCanvasView;
            this._solutionCanvasView = this.FindName("SolutionCanvasView")! as SolutionCanvasView;
        }

        public void InitializeDataContexts()
        {
            var mazeWithSolutionCanvasViewModel = GetDataContext();
            _mazeCanvasView.DataContext = mazeWithSolutionCanvasViewModel.MazeCanvasViewModel;
            _solutionCanvasView.DataContext = mazeWithSolutionCanvasViewModel.SolutionCanvasViewModel;
        }

        private MazeWithSolutionCanvasViewModel GetDataContext() => (MazeWithSolutionCanvasViewModel)this.DataContext;

        public void DrawScaledMazeWithVisibleSolutionPath(float minCellSize, bool centerMaze = true, bool drawArrows = true)
        {
            float cellSize = CalculateCellSize(minCellSize);
            DrawMazeWithVisibleSolutionPath(cellSize, centerMaze, drawArrows);
        }

        public void DrawScaledMaze(float minCellSize, bool centerMaze = true, bool drawArrows = true)
        {
            float cellSize = CalculateCellSize(minCellSize);
            DrawMaze(cellSize, centerMaze, drawArrows);
        }

        public void DrawMazeWithVisibleSolutionPath(float cellSize, bool centerMaze = true, bool drawArrows = true)
        {
            DrawMaze(cellSize, centerMaze, drawArrows);
            _solutionCanvasView.DrawSolutionPath(cellSize);
        }

        public void DrawMaze(float cellSize, bool centerMaze = true, bool drawArrows = true)
        {
            _mazeCanvasView.DrawMaze(cellSize);
            RemoveMazeEntryWalls();
            _solutionCanvasView.Clear();

            if(drawArrows)
            _solutionCanvasView.DrawEntryArrows(cellSize);

            this.Margin = (centerMaze) ? CalculateCenterMargin(cellSize) : new Thickness(0);
        }

        public float CalculateCellSize(float minCellSize) => this._mazeCanvasView.CalculateCellSize(minCellSize);

        public Thickness CalculateCenterMargin(float cellSize)
        {
            float availableWidth = (float)this.ActualWidth;
            float mazeWidth = _mazeCanvasView.CalculateMazeWidth(cellSize);

            float leftMargin = availableWidth / 2 - mazeWidth / 2 + cellSize;
            return new Thickness(leftMargin, cellSize, cellSize, cellSize);
        }

        private void RemoveMazeEntryWalls()
        {
            MazeWithSolution mazeWithSolution = GetDataContext().MazeWithSolution;
            (Line StartWall, Line EndWall) = FindMazeEntryWalls(mazeWithSolution);
            _mazeCanvasView.Canvas.Children.Remove(StartWall);
            _mazeCanvasView.Canvas.Children.Remove(EndWall);
        }

        private (Line StartWall, Line EndWall) FindMazeEntryWalls(MazeWithSolution mazeWithSolution)
        {
            string startWallTag = CreateWallTag(mazeWithSolution.SolutionPath.First(), mazeWithSolution.StartDirection);
            string endWallTag = CreateWallTag(mazeWithSolution.SolutionPath.Last(), mazeWithSolution.EndDirection);

            return (GetWallByTag(startWallTag), GetWallByTag(endWallTag));
        }

        private Line GetWallByTag(string tag)
        {
            var walls = _mazeCanvasView.Canvas.Children.OfType<Line>();
            return walls.Where(w => w.Tag.ToString() == tag).First();
        }

        private string CreateWallTag(Cell cell, Direction direction) => $"{cell.RowIndex}-{cell.ColumnIndex}-{(int)direction}";
    }
}

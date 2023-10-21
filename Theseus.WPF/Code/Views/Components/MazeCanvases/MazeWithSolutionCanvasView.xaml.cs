﻿using System.Linq;
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

        public void DrawScaledMazeWithVisibleSolutionPath(float minCellSize)
        {
            float cellSize = DrawScaledMaze(minCellSize);
            _solutionCanvasView.DrawSolutionPath(cellSize);
        }

        public float DrawScaledMaze(float minCellSize)
        {
            this.MaxWidth = (this._mazeCanvasView.GetMazeRowAmount() >= this._mazeCanvasView.GetMazeColumnAmount()) ? 500 : double.PositiveInfinity;
            UpdateLayout();

            float cellSize = _mazeCanvasView.CalculateCellSize(minCellSize);
            this.Margin = CalculateMargin(cellSize);

            _mazeCanvasView.DrawMaze(cellSize);
            RemoveMazeEntryWalls();
            _solutionCanvasView.Clear();
            _solutionCanvasView.DrawEntryArrows(cellSize);

            return cellSize;
        }

        private System.Windows.Thickness CalculateMargin(float cellSize)
        {
            float availableWidth = (float)this.ActualWidth;
            float mazeWidth = _mazeCanvasView.CalculateMazeWidth(cellSize);
          
            float leftMargin = availableWidth / 2 - mazeWidth / 2 + cellSize;
            return new System.Windows.Thickness(leftMargin, cellSize, cellSize, cellSize);
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

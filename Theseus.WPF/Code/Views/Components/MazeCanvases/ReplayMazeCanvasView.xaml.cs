using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels.Components;
using Theseus.WPF.Code.Views.HelperClasses;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for ReplayMazeCanvasView.xaml
    /// </summary>
    public partial class ReplayMazeCanvasView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;
        private readonly Canvas _examSolutionCanvas;
        private readonly LineDrawer _lineDrawer;
        private readonly PointCalculator _pointCalculator;
        private float _cellSize = 10;

        public ReplayMazeCanvasView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView")! as MazeWithSolutionCanvasView;
            this._examSolutionCanvas = this.FindName("ExamSolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(_examSolutionCanvas!);
            this._pointCalculator = new PointCalculator();
        }

        public void InitializeDataContexts()
        {
            var examMazeCanvasViewModel = GetDataContext();
            _mazeWithSolutionCanvasView.DataContext = examMazeCanvasViewModel.MazeWithSolutionCanvasViewModel;
            _mazeWithSolutionCanvasView.InitializeDataContexts();

            examMazeCanvasViewModel.SuccesfullyMoved += DrawUserSolution;
            examMazeCanvasViewModel.MovedToEndCell += EndMazeExam;
        }

        public void DrawScaledExamMaze(float minCellSize)
        {
            this._cellSize = this._mazeWithSolutionCanvasView.CalculateCellSize();
            Thickness margin = this._mazeWithSolutionCanvasView.CalculateCenterMargin(_cellSize);

            this._mazeWithSolutionCanvasView.DrawMaze(_cellSize, centerMaze: false);
            DrawUserSolution();

            this.Margin = margin;
        }

        private void DrawUserSolution()
        {
            this._examSolutionCanvas.Children.Clear();
            List<Cell> userSolution = GetDataContext().UserSolution;

            Cell startCell = GetDataContext().UserSolution.First();
            DrawLineToBorder(startCell, GetDataContext().StartDirection);

            for (int i = 0; i < userSolution.Count - 1; i++)
            {
                DrawLineBetweenCellCenters(userSolution[i], userSolution[i + 1]);
            }
        }

        private void DrawLineToBorder(Cell cell, Direction direction)
        {
            PointF cellCenter = _pointCalculator.CalculateCellCenter(cell, _cellSize);
            PointF borderCenter = _pointCalculator.CalculatePointInDirection(direction, cellCenter, _cellSize / 2);

            DrawSolutionLine(cellCenter, borderCenter);
        }

        private void DrawLineBetweenCellCenters(Cell previousCell, Cell nextCell)
        {
            PointF previousCellCenter = _pointCalculator.CalculateCellCenter(previousCell, _cellSize);
            PointF nextCellCenter = _pointCalculator.CalculateCellCenter(nextCell, _cellSize);
            DrawSolutionLine(previousCellCenter, nextCellCenter);
        }

        private void EndMazeExam()
        {
            DrawLineToBorder(GetDataContext().TargetCell, GetDataContext().EndDirection);
        }

        private void DrawSolutionLine(PointF start, PointF end) => this._lineDrawer.DrawLine(start, end, Colors.LightBlue, _cellSize * 0.6f);

        private ReplayMazeCanvasViewModel GetDataContext() => (ReplayMazeCanvasViewModel)this.DataContext;
    }
}

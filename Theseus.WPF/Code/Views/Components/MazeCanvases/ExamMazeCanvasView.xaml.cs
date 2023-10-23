using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.ViewModels.Components;
using Theseus.WPF.Code.Views.HelperClasses;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for ExamMazeCanvasView.xaml
    /// </summary>
    public partial class ExamMazeCanvasView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeCanvasView;
        private readonly Canvas _examSolutionCanvas;
        private readonly LineDrawer _lineDrawer;
        private readonly PointCalculator _pointCalculator;
        private float _cellSize = 10;

        public ExamMazeCanvasView()
        {
            InitializeComponent();
            this._mazeCanvasView = this.FindName("MazeWithSolutionCanvasView")! as MazeWithSolutionCanvasView;
            this._examSolutionCanvas = this.FindName("ExamSolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(_examSolutionCanvas!);
            this._pointCalculator = new PointCalculator();
        }

        public void InitializeDataContexts()
        {
            var examMazeCanvasViewModel = GetDataContext();
            _mazeCanvasView.DataContext = examMazeCanvasViewModel.MazeWithSolutionCanvasViewModel;
        }

        public void DrawScaledExamMaze(float minCellSize)
        {
            this._cellSize = this._mazeCanvasView.CalculateCellSize(minCellSize);
            Thickness margin = this._mazeCanvasView.CalculateCenterMargin(_cellSize);

            this._mazeCanvasView.DrawMaze(_cellSize, centerMaze: false);
            DrawStartLine();
            this.Margin = margin;
        }

        private void DrawStartLine()
        {
            Cell startCell = GetDataContext().CurrentCell;
            PointF startCellCenter = _pointCalculator.CalculateCellCenter(startCell, _cellSize);
            PointF borderCenter = _pointCalculator.CalculatePointInDirection(GetMazeWithSolution().StartDirection, startCellCenter, _cellSize / 2);


        }

        private ExamMazeCanvasViewModel GetDataContext() => (ExamMazeCanvasViewModel)this.DataContext;
        private MazeWithSolution GetMazeWithSolution() => GetDataContext().MazeWithSolutionCanvasViewModel.MazeWithSolution;
    }
}
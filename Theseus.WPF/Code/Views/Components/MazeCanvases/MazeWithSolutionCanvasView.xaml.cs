using System.Linq;
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

        public void DrawMazeWithSolution()
        {
            _mazeCanvasView.DrawMaze();
            RemoveMazeEntryWalls();
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

using System.Collections;
using System.Collections.Generic;
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

        public MazeWithSolutionCanvasView()
        {
            InitializeComponent();
            this._mazeCanvasView = this.FindName("MazeCanvasView")! as MazeCanvasView;
        }

        public void InitializeDataContexts()
        {
            var mazeWithSolutionCanvasViewModel = GetDataContext();
            _mazeCanvasView.DataContext = mazeWithSolutionCanvasViewModel.MazeCanvasViewModel;
        }

        public void DrawMazeWithSolution()
        {
            _mazeCanvasView.DrawMaze();
            RemoveMazeEntryWalls();
        }

        private void RemoveMazeEntryWalls()
        {
            MazeWithSolution mazeWithSolution = GetDataContext().MazeWithSolution;
            Cell firstCell = mazeWithSolution.SolutionPath.First();
            Cell lastCell = mazeWithSolution.SolutionPath.Last();
            Direction startDirection = mazeWithSolution.StartDirection;
            Direction endDirection = mazeWithSolution.EndDirection;

            string entryWallTag = CreateMazeWallTag(firstCell.RowIndex, firstCell.ColumnIndex, startDirection);
            string exitWallTag = CreateMazeWallTag(lastCell.RowIndex, lastCell.ColumnIndex, endDirection);

            var walls = _mazeCanvasView.Canvas.Children.OfType<Line>();
            Line entryWall = walls.Where(w => w.Tag.ToString() == entryWallTag).First();
            Line exitWall = walls.Where(w => w.Tag.ToString() == exitWallTag).First();

            _mazeCanvasView.Canvas.Children.Remove(entryWall);
            _mazeCanvasView.Canvas.Children.Remove(exitWall);
        }

        private MazeWithSolutionCanvasViewModel GetDataContext() => (MazeWithSolutionCanvasViewModel)this.DataContext;
        private string CreateMazeWallTag(int row, int column, Direction direction) => $"{row}-{column}-{(int)direction}";
    }
}

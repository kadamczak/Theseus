using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeDetailsView.xaml
    /// </summary>
    public partial class MazeDetailsView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;
        private bool _mazeCanvasLoaded = false;

        public MazeDetailsView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView") as MazeWithSolutionCanvasView;
        }

        private void MazeWithSolutionCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            float minCellSize = 2;

            _mazeWithSolutionCanvasView.InitializeDataContexts();
            _mazeWithSolutionCanvasView.DrawScaledMazeWithVisibleSolutionPath(minCellSize);
            _mazeCanvasLoaded = true;
        }

        private void Grid_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            float minCellSize = 2;

            if(_mazeCanvasLoaded)
                _mazeWithSolutionCanvasView.DrawScaledMazeWithVisibleSolutionPath(minCellSize);
        }
    }
}
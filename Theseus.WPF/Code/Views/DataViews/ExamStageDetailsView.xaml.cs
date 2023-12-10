using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for ExamStageDetailsView.xaml
    /// </summary>
    public partial class ExamStageDetailsView : UserControl
    {
        private readonly ReplayMazeCanvasView _replayMazeCanvasView;
        private bool _mazeCanvasLoaded = false;

        public ExamStageDetailsView()
        {
            InitializeComponent();
            this._replayMazeCanvasView = this.FindName("ReplayMazeCanvasView") as ReplayMazeCanvasView;
        }

        private void ReplayMazeCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _replayMazeCanvasView.InitializeDataContexts();
            _replayMazeCanvasView.Focus();
            _mazeCanvasLoaded = true;
            RedrawMaze();
            UpdateLayout();
            RedrawMaze();
        }

        private void Grid_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            RedrawMaze();
        }

        private void RedrawMaze()
        {
            float minCellSize = 2;

            if (_mazeCanvasLoaded)
                _replayMazeCanvasView.DrawScaledExamMaze(minCellSize);
        }
    }
}
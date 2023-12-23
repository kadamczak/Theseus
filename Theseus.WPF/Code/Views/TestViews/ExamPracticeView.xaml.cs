using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for ExamPracticeView.xaml
    /// </summary>
    public partial class ExamPracticeView : UserControl
    {
        private readonly ExamMazeCanvasView _examMazeCanvasView;
        private bool _mazeCanvasLoaded = false;

        public ExamPracticeView()
        {
            InitializeComponent();
            this._examMazeCanvasView = this.FindName("ExamMazeCanvasView") as ExamMazeCanvasView;
        }

        private void ExamMazeCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _examMazeCanvasView.InitializeDataContexts();
            _examMazeCanvasView.Focus();
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
                _examMazeCanvasView.DrawScaledExamMaze(minCellSize);
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for ExamStageCommandListView.xaml
    /// </summary>
    public partial class ExamStageCommandListView : UserControl
    {
        public ExamStageCommandListView()
        {
            InitializeComponent();
        }

        private void MazesWithSolutionCanvasView_Loaded(object sender, RoutedEventArgs e)
        {
            MazeWithSolutionCanvasView mazeCanvas = sender as MazeWithSolutionCanvasView;
            mazeCanvas.InitializeDataContexts();
            mazeCanvas.DrawScaledMazeWithVisibleSolutionPath(2, centerMaze: true, drawArrows: false);
        }
    }
}
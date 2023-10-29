using System.Windows;
using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeCommandListView.xaml
    /// </summary>
    public partial class MazeCommandListView : UserControl
    {
        public MazeCommandListView()
        {
            InitializeComponent();
        }

        private void MazesWithSolutionCanvasView_Loaded(object sender, RoutedEventArgs e)
        {
            MazeWithSolutionCanvasView mazeCanvas = sender as MazeWithSolutionCanvasView;
            mazeCanvas.InitializeDataContexts();
            mazeCanvas.DrawScaledMazeWithVisibleSolutionPath(2);
        }
    }
}
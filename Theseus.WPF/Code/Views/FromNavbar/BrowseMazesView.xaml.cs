using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for BrowseMazesView.xaml
    /// </summary>
    public partial class BrowseMazesView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;

        public BrowseMazesView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView") as MazeWithSolutionCanvasView;
        }

        private void MazeWithSolutionCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _mazeWithSolutionCanvasView.InitializeDataContexts();
            _mazeWithSolutionCanvasView.DrawMazeWithVisibleSolutionPath();
        }
    }
}

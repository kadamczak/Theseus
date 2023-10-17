using System.Windows.Controls;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeDetailsView.xaml
    /// </summary>
    public partial class MazeDetailsView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;

        public MazeDetailsView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView") as MazeWithSolutionCanvasView;
        }

        private void MazeWithSolutionCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _mazeWithSolutionCanvasView.InitializeDataContexts();
            _mazeWithSolutionCanvasView.DrawMazeWithSolution();
        }
    }
}
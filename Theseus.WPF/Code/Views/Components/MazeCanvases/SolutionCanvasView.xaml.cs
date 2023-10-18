using System.Windows.Controls;
using Theseus.WPF.Code.Views.HelperClasses;

namespace Theseus.WPF.Code.Views.Components.MazeCanvases
{
    /// <summary>
    /// Interaction logic for MazeSolutionCanvasView.xaml
    /// </summary>
    public partial class SolutionCanvasView : UserControl
    {
        private readonly LineDrawer _lineDrawer;
        public Canvas Canvas { get; }

        public SolutionCanvasView()
        {
            InitializeComponent();
            this.Canvas = this.FindName("SolutionCanvas")! as Canvas;
            this._lineDrawer = new LineDrawer(this.Canvas!);
        }
    }
}
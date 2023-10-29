using System.Windows.Controls;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for BrowseMazesView.xaml
    /// </summary>
    public partial class BrowseMazesView : UserControl
    {
        public MazeCommandListView MazeCommandListView { get; }

        public BrowseMazesView()
        {
            InitializeComponent();
            this.MazeCommandListView = this.FindName("ShowDetailsMazeCommandListView") as MazeCommandListView;
        }
    }
}
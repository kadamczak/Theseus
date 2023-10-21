using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for BrowseMazesView.xaml
    /// </summary>
    public partial class BrowseMazesView : UserControl
    {
        private bool _mazeCanvasesLoaded = false;
        private IEnumerable<MazeWithSolutionCanvasView> _mazeWithSolutionViews;

        public BrowseMazesView()
        {
            InitializeComponent();
        }

        public void Grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._mazeWithSolutionViews = GetMazeWithSolutionViews();
            foreach (var mazeView in _mazeWithSolutionViews)
            {
                mazeView.InitializeDataContexts();
            }

            _mazeCanvasesLoaded = true;
            RedrawMazes();
            UpdateLayout();
            RedrawMazes();
        }

        private void Grid_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            RedrawMazes();
        }

        private void RedrawMazes()
        {
            if (!_mazeCanvasesLoaded)
                return;

            foreach (var mazeView in _mazeWithSolutionViews)
            {
                mazeView.DrawScaledMazeWithVisibleSolutionPath(2);
            }
        }

        private BrowseMazesViewModel GetDataContext() => (BrowseMazesViewModel)this.DataContext;
        
        private List<MazeWithSolutionCanvasView> GetMazeWithSolutionViews()
        {
            List<MazeWithSolutionCanvasView> mazeCanvasList = new List<MazeWithSolutionCanvasView>();

            var mazesListBox = this.FindName("MazesWithSolutionListBox") as ListBox;
            for(int i = 0; i <  mazesListBox.Items.Count; i++)
            {
                ListBoxItem mazeItem = (ListBoxItem)(mazesListBox.ItemContainerGenerator.ContainerFromItem(mazesListBox.Items[i]));
                ContentPresenter mazeContentPresenter = FindVisualChild<ContentPresenter>(mazeItem);
                DataTemplate mazeDataTemplate = mazeContentPresenter.ContentTemplate;
                var mazeCanvas = (MazeWithSolutionCanvasView)mazeDataTemplate.FindName("MazeCanvas", mazeContentPresenter);
                mazeCanvasList.Add(mazeCanvas);
            }

            return mazeCanvasList;
        }


        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
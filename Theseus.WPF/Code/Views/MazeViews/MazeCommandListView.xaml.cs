using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeCommandListView.xaml
    /// </summary>
    public partial class MazeCommandListView : UserControl
    {
        private bool _mazeCanvasesLoaded = false;
        private IEnumerable<MazeWithSolutionCanvasView> _mazeWithSolutionViews;

        public MazeCommandListView()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this._mazeWithSolutionViews = GetMazeWithSolutionViews();
            foreach (var mazeView in _mazeWithSolutionViews)
            {
                mazeView.InitializeDataContexts();
            }

            _mazeCanvasesLoaded = true;
            DrawMazes();
        }


        private void DrawMazes()
        {
            foreach (var mazeView in _mazeWithSolutionViews)
            {
                mazeView.DrawScaledMazeWithVisibleSolutionPath(2);
            }
        }

        private List<MazeWithSolutionCanvasView> GetMazeWithSolutionViews()
        {
            List<MazeWithSolutionCanvasView> mazeCanvasList = new List<MazeWithSolutionCanvasView>();

            var mazesListBox = this.FindName("MazesWithSolutionListBox") as ListBox;
            for (int i = 0; i < mazesListBox.Items.Count; i++)
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

        private void MazesWithSolutionListBox_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
    }
}

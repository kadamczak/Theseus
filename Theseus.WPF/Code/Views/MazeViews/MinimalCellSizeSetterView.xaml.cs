using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MinimalCellSizeSetterView.xaml
    /// </summary>
    public partial class MinimalCellSizeSetterView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;
        private bool _mazeCanvasLoaded = false;

        public MinimalCellSizeSetterView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView") as MazeWithSolutionCanvasView;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            float cellSize = float.Parse(GetMinimalCellSize());
            GetDataContext().PropertyChanged += RedrawIfValidValue;

            _mazeWithSolutionCanvasView.InitializeDataContexts();
            _mazeCanvasLoaded = true;
            RedrawMaze(cellSize);
            UpdateLayout();
            RedrawMaze(cellSize);
        }

        private void RedrawIfValidValue(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "MinimalCellSize")
            {
                if(float.TryParse(GetMinimalCellSize(), out float cellSize))
                {
                    RedrawMaze(cellSize);
                }
            }
        }

        private void RedrawMaze(float cellSize)
        {
            if (_mazeCanvasLoaded)
                _mazeWithSolutionCanvasView.DrawMazeWithVisibleSolutionPath(cellSize, centerMaze: false);
        }

        private MinimalCellSizeSetterViewModel GetDataContext() => (MinimalCellSizeSetterViewModel)this.DataContext;
        private string GetMinimalCellSize() => GetDataContext().MinimalCellSize;
    }
}

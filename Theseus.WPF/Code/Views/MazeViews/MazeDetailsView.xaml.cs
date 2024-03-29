﻿using System.Windows.Controls;
using Theseus.WPF.Code.Views.Components.MazeCanvases;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for MazeDetailsView.xaml
    /// </summary>
    public partial class MazeDetailsView : UserControl
    {
        private readonly MazeWithSolutionCanvasView _mazeWithSolutionCanvasView;
        private bool _mazeCanvasLoaded = false;

        public MazeDetailsView()
        {
            InitializeComponent();
            this._mazeWithSolutionCanvasView = this.FindName("MazeWithSolutionCanvasView") as MazeWithSolutionCanvasView;
        }

        private void MazeWithSolutionCanvasView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _mazeWithSolutionCanvasView.InitializeDataContexts();
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
                _mazeWithSolutionCanvasView.DrawScaledMazeWithVisibleSolutionPath(minCellSize);
        }
    }
}
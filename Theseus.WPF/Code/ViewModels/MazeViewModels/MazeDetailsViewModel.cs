using System;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeDetailsViewModel : ViewModelBase
    {
        public MazeCanvasViewModel CanvasViewModel { get; }

        private readonly MazeDetailsStore _mazeDetailsStore;

        public Maze SelectedMaze => _mazeDetailsStore.SelectedMaze;

        public MazeDetailsViewModel(MazeDetailsStore mazeDetailsStore, MazeCanvasViewModel canvasViewModel)
        {
            _mazeDetailsStore = mazeDetailsStore;
            CanvasViewModel = canvasViewModel;

            CanvasViewModel.Maze = SelectedMaze;
        }
    }
}

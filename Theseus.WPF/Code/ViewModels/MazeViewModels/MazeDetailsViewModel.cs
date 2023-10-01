using System;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeDetailsViewModel : ViewModelBase
    {
        public MazeCanvasViewModel CanvasViewModel { get; }
        private readonly MazeDetailsStore _mazeDetailsStore;

        public ICommand SaveMaze { get; }

        public MazeDetailsViewModel(MazeDetailsStore mazeDetailsStore, MazeCanvasViewModel canvasViewModel, ICreateMazeCommand createMazeCommand)
        {
            _mazeDetailsStore = mazeDetailsStore;

            CanvasViewModel = canvasViewModel;
            CanvasViewModel.Maze = _mazeDetailsStore.SelectedMaze;

            SaveMaze = new SaveMazeCommand(this, mazeDetailsStore, createMazeCommand);
        }
    }
}
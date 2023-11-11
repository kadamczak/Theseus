using System.Windows.Input;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeDetailsViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;

        public ICommand SaveMaze { get; }

        public MazeDetailsViewModel(SelectedMazeDetailsStore mazeDetailsStore, ICreateOrUpdateMazeWithSolutionCommand createMazeCommand)
        {
            _mazeDetailsStore = mazeDetailsStore;
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(_mazeDetailsStore.SelectedMazeWithSolution);
            SaveMaze = new SaveMazeCommand(this, mazeDetailsStore, createMazeCommand);
        }
    }
}
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeDetailsViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        private readonly MazeDetailsStore _mazeDetailsStore;

        public ICommand SaveMaze { get; }

        public MazeDetailsViewModel(MazeDetailsStore mazeDetailsStore, ICreateOrUpdateMazeWithSolutionCommand createMazeCommand)
        {
            _mazeDetailsStore = mazeDetailsStore;
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(_mazeDetailsStore.SelectedMazeWithSolution);
            SaveMaze = new SaveMazeCommand(this, mazeDetailsStore, createMazeCommand);
        }
    }
}
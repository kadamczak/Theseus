using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeDetailsViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;

        private bool _canSaveMaze = false;
        public bool CanSaveMaze
        {
            get => _canSaveMaze;
            set
            {
                _canSaveMaze = value;
                OnPropertyChanged(nameof(CanSaveMaze));
            }
        }

        public ICommand SaveMaze { get; }
        public ICommand GoBack { get; }

        public MazeDetailsViewModel(SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                    ICreateMazeWithSolutionCommand createMazeCommand,
                                    MazeReturnServiceStore mazeReturnServiceStore)
        {
            CanSaveMaze = mazeDetailsStore.SelectedModel.MazeWithSolution.Id is null;

            _mazeDetailsStore = mazeDetailsStore;
            this.MazeWithSolutionCanvasViewModel = _mazeDetailsStore.SelectedModel;
            SaveMaze = new SaveMazeCommand(this, mazeDetailsStore, createMazeCommand);
            GoBack = new NavigateCommand<ViewModelBase>(mazeReturnServiceStore.MazeReturnNavigationService);
        }
    }
}
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
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
        private readonly SelectedModelDetailsStore<MazeWithSolution> _mazeDetailsStore;

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

        public MazeDetailsViewModel(SelectedModelDetailsStore<MazeWithSolution> mazeDetailsStore,
                                    ICreateOrUpdateMazeWithSolutionCommand createMazeCommand,
                                    MazeReturnServiceStore mazeReturnServiceStore)
        {
            CanSaveMaze = mazeDetailsStore.SelectedModel.Id is null;

            _mazeDetailsStore = mazeDetailsStore;
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(_mazeDetailsStore.SelectedModel);
            SaveMaze = new SaveMazeCommand(this, mazeDetailsStore, createMazeCommand);
            GoBack = new NavigateCommand<ViewModelBase>(mazeReturnServiceStore.MazeReturnNavigationService);
        }
    }
}
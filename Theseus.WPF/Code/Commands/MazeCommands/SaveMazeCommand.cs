using System.Threading.Tasks;
using Theseus.Domain.MazeCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    public class SaveMazeCommand : AsyncCommandBase
    {
        private readonly MazeDetailsViewModel _mazeDetailsViewModel;
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly ICreateOrUpdateMazeWithSolutionCommand _createOrUpdateMazeCommand;

        public SaveMazeCommand(MazeDetailsViewModel mazeDetailViewModel, SelectedMazeDetailsStore mazeDetailsStore, ICreateOrUpdateMazeWithSolutionCommand createOrUpdateMazeCommand)
        {
            _mazeDetailsViewModel = mazeDetailViewModel;
            _mazeDetailsStore = mazeDetailsStore;
            _createOrUpdateMazeCommand = createOrUpdateMazeCommand;

            _mazeDetailsStore.SaveStateChanged += OnMazeSaveStateChanged;
        }

        protected override void Dispose()
        {
            _mazeDetailsStore.SaveStateChanged -= OnMazeSaveStateChanged;
            base.Dispose();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _createOrUpdateMazeCommand.CreateOrUpdateMazeWithSolution(_mazeDetailsStore.SelectedMazeWithSolution);
            _mazeDetailsStore.HasUnsavedChanges = false;
        }

        public override bool CanExecute(object parameter)
        {
            return _mazeDetailsStore.HasUnsavedChanges && base.CanExecute(parameter);
        }

        private void OnMazeSaveStateChanged()
        {
            OnCanExecuteChanged();
        }
    }
}
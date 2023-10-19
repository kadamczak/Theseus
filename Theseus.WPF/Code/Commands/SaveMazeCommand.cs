using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class SaveMazeCommand : AsyncCommandBase
    {
        private readonly MazeDetailsViewModel _mazeDetailsViewModel;
        private readonly MazeDetailsStore _mazeDetailsStore;
        private readonly ICreateOrUpdateMazeWithSolutionCommand _createOrUpdateMazeCommand;

        public SaveMazeCommand(MazeDetailsViewModel mazeDetailViewModel, MazeDetailsStore mazeDetailsStore, ICreateOrUpdateMazeWithSolutionCommand createOrUpdateMazeCommand)
        {
            this._mazeDetailsViewModel = mazeDetailViewModel;
            this._mazeDetailsStore = mazeDetailsStore;
            this._createOrUpdateMazeCommand = createOrUpdateMazeCommand;

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
            this._mazeDetailsStore.HasUnsavedChanges = false;
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
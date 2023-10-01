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
        private readonly ICreateMazeCommand _createMazeCommand;

        public SaveMazeCommand(MazeDetailsViewModel mazeDetailViewModel, MazeDetailsStore mazeDetailsStore, ICreateMazeCommand createMazeCommand)
        {
            this._mazeDetailsViewModel = mazeDetailViewModel;
            this._mazeDetailsStore = mazeDetailsStore;
            this._createMazeCommand = createMazeCommand;

            _mazeDetailsStore.SaveStateChanged += OnMazeSaveStateChanged;
        }

        //protected override void Dispose()
        //{
        //    //TODO?
        //}

        public override async Task ExecuteAsync(object parameter)
        {
            await _createMazeCommand.CreateMaze(_mazeDetailsStore.SelectedMaze);
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

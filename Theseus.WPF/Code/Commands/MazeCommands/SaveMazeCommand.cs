using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    public class SaveMazeCommand : AsyncCommandBase
    {
        private readonly MazeDetailsViewModel _mazeDetailsViewModel;
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
        private readonly ICreateOrUpdateMazeWithSolutionCommand _createOrUpdateMazeCommand;

        public SaveMazeCommand(MazeDetailsViewModel mazeDetailViewModel, SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore, ICreateOrUpdateMazeWithSolutionCommand createOrUpdateMazeCommand)
        {
            _mazeDetailsViewModel = mazeDetailViewModel;
            _mazeDetailsStore = mazeDetailsStore;
            _createOrUpdateMazeCommand = createOrUpdateMazeCommand;

            _mazeDetailsViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _mazeDetailsViewModel.PropertyChanged -= ViewModelPropertyChanged;
            base.Dispose();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _createOrUpdateMazeCommand.CreateOrUpdateMazeWithSolution(_mazeDetailsStore.SelectedModel.MazeWithSolution);
            _mazeDetailsViewModel.CanSaveMaze = false;
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mazeDetailsViewModel.CanSaveMaze))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _mazeDetailsViewModel.CanSaveMaze && base.CanExecute(parameter);
        }
    }
}
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Services.ExamDataServices;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.DataCommands;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamStageDetailsViewModel : ViewModelBase
    {
        public ReplayMazeCanvasViewModel ReplayMazeCanvasViewModel { get; set; }

        private bool _replayDone = false;
        public bool ReplayDone
        {
            get => _replayDone;
            set
            {
                _replayDone = value;
                OnPropertyChanged(nameof(ReplayDone));
            }
        }

        public ICommand StopReplay { get; }
        public ICommand GoBack { get; }

        private readonly NavigationEnabledStore _navigationEnabledStore;

        private bool _navigationEnabled = false;
        public bool NavigationEnabled
        {
            get => _navigationEnabled;
            set
            {
                _navigationEnabled = value;
                OnPropertyChanged(nameof(NavigationEnabled));
            }
        }

        public ExamStageDetailsViewModel(SelectedModelDetailsStore<ExamStageWithMazeViewModel> examStageDetailsStore,
                                        InputListToTimedCellPathConverter inputConverter,
                                        NavigationEnabledStore navigationEnabledStore,
                                        NavigationService<ExamDetailsViewModel> examDetailsNavigationService)
        {
            _navigationEnabledStore = navigationEnabledStore;
            _navigationEnabledStore.NavigationEnabled = false;
            _navigationEnabledStore.NavigationEnabledChanged += OnNavigationEnabledChanged;

            var maze = examStageDetailsStore.SelectedModel.MazeCanvasViewModel.MazeWithSolution;

            var sortedSteps = examStageDetailsStore.SelectedModel.ExamStage.Steps.OrderBy(s => s.Index);
            var userSolution = inputConverter.ConvertInputListToTimedCellList(sortedSteps, maze);

            ReplayMazeCanvasViewModel = new ReplayMazeCanvasViewModel(maze, userSolution, examStageDetailsStore.SelectedModel.ExamStage.Completed);
            ReplayMazeCanvasViewModel.ReplayDone += StopMazeReplay;
            StopReplay = new StopMazeReplayCommand(this);
            GoBack = new NavigateCommand<ExamDetailsViewModel>(examDetailsNavigationService);

            if (userSolution.Count == 1)
                StopMazeReplay();
        }

        public void StopMazeReplay()
        {
            ReplayMazeCanvasViewModel.StopTimer();
            ReplayDone = true;
            _navigationEnabledStore.NavigationEnabled = true;
        }

        private void OnNavigationEnabledChanged()
        {
            NavigationEnabled = _navigationEnabledStore.NavigationEnabled;
        }
    }
}
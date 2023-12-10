using System;
using System.Linq;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
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

        public ExamStageDetailsViewModel(SelectedModelDetailsStore<ExamStageWithMazeViewModel> examStageDetailsStore,
                                        InputListToTimedCellPathConverter inputConverter)
        {
            var maze = examStageDetailsStore.SelectedModel.MazeCanvasViewModel.MazeWithSolution;

            var sortedSteps = examStageDetailsStore.SelectedModel.ExamStage.Steps.OrderBy(s => s.Index);
            var userSolution = inputConverter.ConvertInputListToTimedCellList(sortedSteps, maze);

            ReplayMazeCanvasViewModel = new ReplayMazeCanvasViewModel(maze, userSolution, examStageDetailsStore.SelectedModel.ExamStage.Completed);
            ReplayMazeCanvasViewModel.ReplayDone += MazeReplayDone;
        }

        private void MazeReplayDone()
        {
            ReplayDone = true;
        }
    }
}
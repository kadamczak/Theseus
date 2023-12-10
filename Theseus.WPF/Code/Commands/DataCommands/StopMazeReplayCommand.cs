using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class StopMazeReplayCommand : CommandBase
    {
        private readonly ExamStageDetailsViewModel _viewModel;

        public StopMazeReplayCommand(ExamStageDetailsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.StopMazeReplay();
        }
    }
}
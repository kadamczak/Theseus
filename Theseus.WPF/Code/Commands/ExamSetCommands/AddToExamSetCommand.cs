using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class AddToExamSetCommand : CommandBase
    {
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeWithSolutionCommandViewModel;
        private MazesInExamSetStore _mazesInExamSetStore;

        public AddToExamSetCommand(CommandViewModel<MazeWithSolutionCanvasViewModel> mazeWithSolutionCommandViewModel,
                                   MazesInExamSetStore mazesInExamSetStore)
        {
            _mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            _mazesInExamSetStore = mazesInExamSetStore;
        }

        public override void Execute(object? parameter)
        {
            MazeWithSolution mazeWithSolution = _mazeWithSolutionCommandViewModel.Model.MazeWithSolution;

            if (_mazeWithSolutionCommandViewModel.Selected)
            {
                DeselectMaze(mazeWithSolution);
            }
            else
            {
                SelectMaze(mazeWithSolution);
            }
        }

        private void DeselectMaze(MazeWithSolution mazeWithSolution)
        {
            _mazesInExamSetStore.SelectedMazes.Remove(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = false;
            //_mazeWithSolutionCommandViewModel.Command1Name = "Add";
        }

        private void SelectMaze(MazeWithSolution mazeWithSolution)
        {
            _mazesInExamSetStore.SelectedMazes.Add(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = true;
            //_mazeWithSolutionCommandViewModel.Command1Name = "Remove";
        }
    }
}
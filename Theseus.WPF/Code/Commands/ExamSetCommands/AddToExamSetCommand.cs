using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class AddToExamSetCommand : CommandBase
    {
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeWithSolutionCommandViewModel;
        private AddToSetMazeCommandListViewModel _addToSetMazeCommandListViewModel;

        public AddToExamSetCommand(CommandViewModel<MazeWithSolutionCanvasViewModel> mazeWithSolutionCommandViewModel,
                                   AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            _mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            _addToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
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
            _addToSetMazeCommandListViewModel.SelectedMazes.Remove(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = false;
            _mazeWithSolutionCommandViewModel.Command1Name = "Add";
        }

        private void SelectMaze(MazeWithSolution mazeWithSolution)
        {
            _addToSetMazeCommandListViewModel.SelectedMazes.Add(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = true;
            _mazeWithSolutionCommandViewModel.Command1Name = "Remove";
        }
    }
}
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    class AddToExamSetCommand : CommandBase
    {
        private readonly MazeWithSolutionCommandViewModel _mazeWithSolutionCommandViewModel;
        private AddToSetMazeCommandListViewModel _addToSetMazeCommandListViewModel;

        public AddToExamSetCommand(MazeWithSolutionCommandViewModel mazeWithSolutionCommandViewModel,
                               AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            _mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            _addToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
        }

        public override void Execute(object? parameter)
        {
            MazeWithSolution mazeWithSolution = _mazeWithSolutionCommandViewModel.MazeWithSolutionCanvasViewModel.MazeWithSolution;

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
            _mazeWithSolutionCommandViewModel.CommandName = "Add";
        }

        private void SelectMaze(MazeWithSolution mazeWithSolution)
        {
            _addToSetMazeCommandListViewModel.SelectedMazes.Add(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = true;
            _mazeWithSolutionCommandViewModel.CommandName = "Remove";
        }
    }
}
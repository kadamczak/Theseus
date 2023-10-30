using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    class AddToSetCommand : CommandBase
    {
        private readonly MazeWithSolutionCommandViewModel _mazeWithSolutionCommandViewModel;
        private AddToSetMazeCommandListViewModel _addToSetMazeCommandListViewModel;

        public AddToSetCommand(MazeWithSolutionCommandViewModel mazeWithSolutionCommandViewModel,
                               AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            this._mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            this._addToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
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
            this._addToSetMazeCommandListViewModel.SelectedMazes.Remove(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = false;
            _mazeWithSolutionCommandViewModel.CommandName = "Add";
        }

        private void SelectMaze(MazeWithSolution mazeWithSolution)
        {
            this._addToSetMazeCommandListViewModel.SelectedMazes.Add(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = true;
            _mazeWithSolutionCommandViewModel.CommandName = "Remove";
        }
    }
}
using System.Collections.ObjectModel;
using System.Linq;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class AddToExamSetCommand : CommandBase
    {
        private readonly ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> _mazeCommandList;
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeWithSolutionCommandViewModel;
        private MazesInExamSetStore _mazesInExamSetStore;

        public AddToExamSetCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> mazeCommandList,
                                   CommandViewModel<MazeWithSolutionCanvasViewModel> mazeWithSolutionCommandViewModel,
                                   MazesInExamSetStore mazesInExamSetStore)
        {
            _mazeCommandList = mazeCommandList;
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
            _mazeWithSolutionCommandViewModel.Info = string.Empty;
            UpdateDisplayedIndexes();
        }

        private void SelectMaze(MazeWithSolution mazeWithSolution)
        {
            _mazesInExamSetStore.SelectedMazes.Add(mazeWithSolution);
            _mazeWithSolutionCommandViewModel.Selected = true;
            UpdateDisplayedIndexes();
        }

        private void UpdateDisplayedIndexes()
        {
            for(int i = 0; i < _mazesInExamSetStore.SelectedMazes.Count; i++)
            {
                var selectedMaze = _mazesInExamSetStore.SelectedMazes[i];
                var commandViewModel = _mazeCommandList.Where(m => m.Model.MazeWithSolution.Id == selectedMaze.Id).First();
                commandViewModel.Info = "PlaceInExam:".Resource() + (i + 1);
            }
        }
    }
}
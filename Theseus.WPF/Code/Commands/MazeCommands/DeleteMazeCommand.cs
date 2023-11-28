using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    public class DeleteMazeCommand : AsyncCommandBase
    {
        private readonly ShowDetailsDeleteMazeCommandListViewModel _mazeCommandListViewModel;
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeCanvasViewModel;
        private readonly IDeleteMazeWithSolutionCommand _removeMazeCommand;
        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

        public DeleteMazeCommand(ShowDetailsDeleteMazeCommandListViewModel mazeCommandListViewModel,
                                 CommandViewModel<MazeWithSolutionCanvasViewModel> mazeCanvasViewModel,
                                 IDeleteMazeWithSolutionCommand removeMazeCommand,
                                 IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery)
        {
            _mazeCommandListViewModel = mazeCommandListViewModel;
            _mazeCanvasViewModel = mazeCanvasViewModel;
            _removeMazeCommand = removeMazeCommand;
            _getExamSetsWithMazeQuery = getExamSetsWithMazeQuery;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid mazeId = _mazeCanvasViewModel.Model.MazeWithSolution.Id!.Value;
            var examSets = _getExamSetsWithMazeQuery.GetExamSets(mazeId);

            if (examSets.Any())
            {
                ShowErrorMessageBox();
                return;
            }

            MessageBoxResult result = ShowConfirmationMessageBox();
            if (result == MessageBoxResult.Yes)
            {
                await DeleteMaze(mazeId);
            }
        }

        private MessageBoxResult ShowErrorMessageBox()
        {
            string messageBoxText = "This maze can't be deleted because it is present in Exam Sets.";
            string caption = "Maze Deletion";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Exclamation;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "Do you want to delete this maze?";
            string caption = "Maze Deletion";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteMaze(Guid mazeId)
        {
            await _removeMazeCommand.Remove(mazeId);
            _mazeCommandListViewModel.ActionableModels.Remove(_mazeCanvasViewModel);
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    public class DeleteMazeCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> _collection;
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeCanvasViewModel;
        private readonly IDeleteMazeWithSolutionCommand _removeMazeCommand;
        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

        public DeleteMazeCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> collection,
                                 CommandViewModel<MazeWithSolutionCanvasViewModel> mazeCanvasViewModel,
                                 IDeleteMazeWithSolutionCommand removeMazeCommand,
                                 IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery)
        {
            _collection = collection;
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
            string messageBoxText = "MazeCantBeDeleted".Resource();
            string caption = "MazeDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Exclamation;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "DoYouWantToDeleteMaze".Resource();
            string caption = "MazeDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteMaze(Guid mazeId)
        {
            await _removeMazeCommand.Remove(mazeId);
            _collection.Remove(_mazeCanvasViewModel);
        }
    }
}

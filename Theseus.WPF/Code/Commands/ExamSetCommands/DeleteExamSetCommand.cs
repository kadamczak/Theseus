using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    /// <summary>
    /// The <c>DeleteExamSetCommand</c> class deletes the <c>ExamSet</c> instance linked to chosen <c>CommandViewModel</c>.
    /// </summary>
    public class DeleteExamSetCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<ExamSet>> _commandList;
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private readonly IDeleteExamSetCommand _removeExamSetCommand;
        private readonly IGetExamsOfExamSetQuery _getExamsQuery;

        public DeleteExamSetCommand(ObservableCollection<CommandViewModel<ExamSet>> commandList,
                                    CommandViewModel<ExamSet> examSetCommandViewModel,
                                    IDeleteExamSetCommand removeExamSetCommand,
                                    IGetExamsOfExamSetQuery getExamsQuery)
        {
            _commandList = commandList;
            _examSetCommandViewModel = examSetCommandViewModel;
            _removeExamSetCommand = removeExamSetCommand;
            _getExamsQuery = getExamsQuery;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid examSetId = _examSetCommandViewModel.Model.Id;
            var exams = _getExamsQuery.GetExams(examSetId);

            if (exams.Any())
            {
                ShowErrorMessageBox();
                return;
            }

            MessageBoxResult result = ShowConfirmationMessageBox();

            if (result == MessageBoxResult.Yes)
            {
                await DeleteExamSet(examSetId);
            }
        }

        private MessageBoxResult ShowErrorMessageBox()
        {
            string messageBoxText = "ThisExamSetCantBeDeleted".Resource();
            string caption = "ExamSetDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Exclamation;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "DoYouWantToDeleteThisExamSet".Resource();
            string caption = "ExamSetDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteExamSet(Guid examSetId)
        {
            await _removeExamSetCommand.Delete(examSetId);
            _commandList.Remove(_examSetCommandViewModel);
        }
    }
}
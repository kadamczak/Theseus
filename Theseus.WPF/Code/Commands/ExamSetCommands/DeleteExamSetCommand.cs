using System;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class DeleteExamSetCommand : AsyncCommandBase
    {
        private readonly ShowDetailsDeleteExamSetCommandListViewModel _commandListViewModel;
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private readonly IRemoveExamSetCommand _removeExamSetCommand;

        public DeleteExamSetCommand(ShowDetailsDeleteExamSetCommandListViewModel commandListViewModel,
                                    CommandViewModel<ExamSet> examSetCommandViewModel,
                                    IRemoveExamSetCommand removeExamSetCommand)
        {
            _commandListViewModel = commandListViewModel;
            _examSetCommandViewModel = examSetCommandViewModel;
            _removeExamSetCommand = removeExamSetCommand;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid examSetId = _examSetCommandViewModel.Model.Id;
            MessageBoxResult result = ShowConfirmationMessageBox();

            if (result == MessageBoxResult.Yes)
            {
                await DeleteExamSet(examSetId);
            }
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "Do you want to delete this exam set? Mazes will be unaffected.";
            string caption = "Exam Set Deletion";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteExamSet(Guid examSetId)
        {
            await _removeExamSetCommand.Remove(examSetId);
            _commandListViewModel.ActionableModels.Remove(_examSetCommandViewModel);
        }
    }
}
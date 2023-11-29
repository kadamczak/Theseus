using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class DeleteExamSetCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<ExamSet>> _commandList;
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private readonly IDeleteExamSetCommand _removeExamSetCommand;

        public DeleteExamSetCommand(ObservableCollection<CommandViewModel<ExamSet>> commandList,
                                    CommandViewModel<ExamSet> examSetCommandViewModel,
                                    IDeleteExamSetCommand removeExamSetCommand)
        {
            _commandList = commandList;
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
            await _removeExamSetCommand.Delete(examSetId);
            _commandList.Remove(_examSetCommandViewModel);
        }
    }
}
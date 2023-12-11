using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class DeleteExamCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<Exam>> _commandList;
        private readonly CommandViewModel<Exam> _examCommandViewModel;
        private readonly IDeleteExamCommand _deleteExamCommand;

        public DeleteExamCommand(ObservableCollection<CommandViewModel<Exam>> commandList,
                                 CommandViewModel<Exam> examCommandViewModel,
                                 IDeleteExamCommand deleteExamCommand)
        {
            _commandList = commandList;
            _examCommandViewModel = examCommandViewModel;
            _deleteExamCommand = deleteExamCommand;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid examId = _examCommandViewModel.Model.Id;
            MessageBoxResult result = ShowConfirmationMessageBox();

            if (result == MessageBoxResult.Yes)
            {
                await DeleteExam(examId);
            }
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "DoYouWantToDeleteThisExam".Resource();
            string caption = "ExamDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteExam(Guid examId)
        {
            await _deleteExamCommand.Delete(examId);
            _commandList.Remove(_examCommandViewModel);
        }
    }
}
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemoveExamSetFromGroupCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<ExamSet>> _commandList;
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private readonly IRemoveExamSetFromGroupCommand _removeExamSetFromGroupCommand;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;

        public RemoveExamSetFromGroupCommand(ObservableCollection<CommandViewModel<ExamSet>> commandList,
                                      CommandViewModel<ExamSet> examSetCommandViewModel,
                                      IRemoveExamSetFromGroupCommand removeExamSetFromGroupCommand,
                                      SelectedModelDetailsStore<Group> selectedGroupDetailsStore)
        {
            _commandList = commandList;
            _examSetCommandViewModel = examSetCommandViewModel;
            _removeExamSetFromGroupCommand = removeExamSetFromGroupCommand;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            string messageBoxText = "DoYouWantToRemoveExamSet".Resource();
            string caption = "ExamSetRemoval".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Guid groupId = _selectedGroupDetailsStore.SelectedModel.Id;

                try
                {
                    await _removeExamSetFromGroupCommand.RemoveFromGroup(_examSetCommandViewModel.Model, groupId);
                    _commandList.Remove(_examSetCommandViewModel);
                }
                catch(SqlException)
                {
                    string messageBoxText2 = "CouldNotConnectToDatabase".Resource();
                    string caption2 = "ActionFailed".Resource();
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText2, caption2, button2, icon2, MessageBoxResult.OK);
                }
            }
        }
    }
}
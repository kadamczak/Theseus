using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    /// <summary>
    /// The <c>DeleteGroupCommand</c> class deletes the <c>Group</c> instance linked to chosen <c>CommandViewModel</c>.
    /// </summary>
    public class DeleteGroupCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<Group>> _groupCommandList;
        private readonly CommandViewModel<Group> _groupCommandViewModel;
        private readonly IDeleteGroupCommand _deleteGroupCommand;

        public DeleteGroupCommand(ObservableCollection<CommandViewModel<Group>> groupCommandList,
                                  CommandViewModel<Group> groupCommandViewModel,
                                  IDeleteGroupCommand deleteGroupCommand)
        {
            _groupCommandList = groupCommandList;
            _groupCommandViewModel = groupCommandViewModel;
            _deleteGroupCommand = deleteGroupCommand;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid groupId = _groupCommandViewModel.Model.Id;
            MessageBoxResult result = ShowConfirmationMessageBox();

            if (result == MessageBoxResult.Yes)
            {
                await DeleteGroup(groupId);
            }
        }

        private MessageBoxResult ShowConfirmationMessageBox()
        {
            string messageBoxText = "DoYouWantToDeleteThisGroup".Resource();
            string caption = "GroupDeletion".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteGroup(Guid groupId)
        {
            try
            {
                await _deleteGroupCommand.Delete(groupId);
                _groupCommandList.Remove(_groupCommandViewModel);
            }
            catch(SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }
    }
}
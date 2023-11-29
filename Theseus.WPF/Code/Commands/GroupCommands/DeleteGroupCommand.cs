using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
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
            string messageBoxText = "Do you want to delete this group? Included exam sets will remain.";
            string caption = "Group Deletion";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task DeleteGroup(Guid groupId)
        {
            await _deleteGroupCommand.Delete(groupId);
            _groupCommandList.Remove(_groupCommandViewModel);
        }
    }
}
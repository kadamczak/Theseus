using System;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class LeaveGroupCommand : AsyncCommandBase
    {
        private readonly ShowDetailsDeleteGroupCommandListViewModel _groupCommandListViewModel;
        private readonly CommandViewModel<Group> _groupCommandViewModel;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly StaffMember _currentStaffMember;

        public LeaveGroupCommand(ShowDetailsDeleteGroupCommandListViewModel groupCommandListViewModel,
                                 CommandViewModel<Group> groupCommandViewModel,
                                 IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                 StaffMember currentStaffMember)
        {
            _groupCommandListViewModel = groupCommandListViewModel;
            _groupCommandViewModel = groupCommandViewModel;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMember = currentStaffMember;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = "Do you want to leave this group?";
            string caption = "Leaving Group";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Guid groupId = _groupCommandViewModel.Model.Id;
                await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_currentStaffMember, groupId);
                _groupCommandListViewModel.ActionableModels.Remove(_groupCommandViewModel);
            }
        }
    }
}
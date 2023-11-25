using System;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemoveStaffMemberFromGroupCommand : AsyncCommandBase
    {
        private readonly RemoveStaffMemberCommandListViewModel _removeStaffMemberCommandListViewModel;
        private readonly CommandViewModel<StaffMember> _staffMemberCommandViewModel;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;

        public RemoveStaffMemberFromGroupCommand(RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel,
                                                 CommandViewModel<StaffMember> staffMemberCommandViewModel,
                                                 IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                 SelectedGroupDetailsStore selectedGroupDetailsStore)
        {
            _removeStaffMemberCommandListViewModel = removeStaffMemberCommandListViewModel;
            _staffMemberCommandViewModel = staffMemberCommandViewModel;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            string messageBoxText = "Do you want to remove staff member from this group?";
            string caption = "Staff Member Removal";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if(result == MessageBoxResult.Yes)
            {
                Guid groupId = _selectedGroupDetailsStore.SelectedGroup.Id;
                await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_staffMemberCommandViewModel.Model, groupId);
                _removeStaffMemberCommandListViewModel.ActionableStaffMembers.Remove(_staffMemberCommandViewModel);
            }
        }
    }
}
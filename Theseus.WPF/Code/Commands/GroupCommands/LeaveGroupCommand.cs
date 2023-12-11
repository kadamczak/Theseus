using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class LeaveGroupCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<Group>> _groupCommandList;
        private readonly CommandViewModel<Group> _groupCommandViewModel;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;

        public LeaveGroupCommand(ObservableCollection<CommandViewModel<Group>> groupCommandList,
                                 CommandViewModel<Group> groupCommandViewModel,
                                 IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                 ICurrentStaffMemberStore currentStaffMemberStore)
        {
            _groupCommandList = groupCommandList;
            _groupCommandViewModel = groupCommandViewModel;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = "DoYouWantToLeaveThisGroup".Resource();
            string caption = "LeavingGroup".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Guid groupId = _groupCommandViewModel.Model.Id;
                await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_currentStaffMemberStore.StaffMember, groupId);
                _groupCommandList.Remove(_groupCommandViewModel);
            }
        }
    }
}
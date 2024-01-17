using Microsoft.Data.SqlClient;
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
    /// <summary>
    /// The <c>LeaveGroupCommand</c> class removes the currently logged in <c>StaffMember</c>
    /// from <c>Group</c> instance linked to chosen <c>CommandViewModel</c>.
    /// </summary>
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

                try
                {
                    await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_currentStaffMemberStore.StaffMember, groupId);
                    _groupCommandList.Remove(_groupCommandViewModel);
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
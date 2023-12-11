using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemoveStaffMemberFromGroupCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<StaffMember>> _staffMemberCommandListl;
        private readonly CommandViewModel<StaffMember> _staffMemberCommandViewModel;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;

        public RemoveStaffMemberFromGroupCommand(ObservableCollection<CommandViewModel<StaffMember>> staffMemberCommandList,
                                                 CommandViewModel<StaffMember> staffMemberCommandViewModel,
                                                 IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                 SelectedModelDetailsStore<Group> selectedGroupDetailsStore)
        {
            _staffMemberCommandListl = staffMemberCommandList;
            _staffMemberCommandViewModel = staffMemberCommandViewModel;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            string messageBoxText = "DoYouWantToRemoveStaffMember".Resource();
            string caption = "StaffMemberRemoval".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if(result == MessageBoxResult.Yes)
            {
                Guid groupId = _selectedGroupDetailsStore.SelectedModel.Id;
                await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_staffMemberCommandViewModel.Model, groupId);
                _staffMemberCommandListl.Remove(_staffMemberCommandViewModel);
            }
        }
    }
}
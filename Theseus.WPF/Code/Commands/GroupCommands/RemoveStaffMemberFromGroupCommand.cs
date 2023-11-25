using System;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemoveStaffMemberFromGroupCommand : AsyncCommandBase
    {
        private readonly RemoveStaffMemberCommandListViewModel _removeStaffMemberCommandListViewModel;
        private readonly StaffMemberCommandViewModel _staffMemberCommandViewModel;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;

        public RemoveStaffMemberFromGroupCommand(RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel,
                                                 StaffMemberCommandViewModel staffMemberCommandViewModel,
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
            Guid groupId = _selectedGroupDetailsStore.SelectedGroup.Id;
            await _removeStaffMemberFromGroupCommand.RemoveFromGroup(_staffMemberCommandViewModel.StaffMember, groupId);
            _removeStaffMemberCommandListViewModel.ActionableStaffMembers.Remove(_staffMemberCommandViewModel);
        }
    }
}
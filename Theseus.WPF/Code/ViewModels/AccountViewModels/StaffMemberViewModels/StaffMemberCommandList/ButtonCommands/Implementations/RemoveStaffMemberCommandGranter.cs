using System;
using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Commands.GroupCommands;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations
{
    public class RemoveStaffMemberCommandGranter : CommandGranter<StaffMember>
    {
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;

        private Guid _groupOwnerId;
        private readonly bool _currentStaffMemberCanRemoveMembers = false;

        public RemoveStaffMemberCommandGranter(IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                               ICurrentStaffMemberStore currentStaffMemberStore,
                                               SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                               IGetOwnerOfGroupQuery getOwnerOfGroupQuery)
        {
            if (selectedGroupDetailsStore.SelectedModel is null)
                return;

            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;

            Group currentGroup = selectedGroupDetailsStore.SelectedModel;
            _groupOwnerId = getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            _currentStaffMemberCanRemoveMembers = IsOwner(currentStaffMemberStore.StaffMember.Id);
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<StaffMember>> collection,
                                                     CommandViewModel<StaffMember> commandViewModel)
        {
            return StaffMemberIsRemovable(commandViewModel.Model.Id) ?
                new ButtonViewModel(show: true, "Remove", new RemoveStaffMemberFromGroupCommand(collection, commandViewModel, _removeStaffMemberFromGroupCommand, _selectedGroupDetailsStore)) :
                new ButtonViewModel(show: false);
        }

        private bool StaffMemberIsRemovable(Guid staffMemberId) => _currentStaffMemberCanRemoveMembers && !IsOwner(staffMemberId);
        private bool IsOwner(Guid guid) => guid == _groupOwnerId;
    }
}
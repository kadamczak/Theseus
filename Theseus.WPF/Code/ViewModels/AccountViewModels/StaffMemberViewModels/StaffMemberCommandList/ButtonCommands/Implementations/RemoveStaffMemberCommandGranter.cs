using System;
using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations
{
    public class RemoveStaffMemberCommandGranter : CommandGranter<StaffMember>
    {
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly IGetOwnerOfGroupQuery _getOwnerOfGroupQuery;

        public RemoveStaffMemberCommandGranter(IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                               ICurrentStaffMemberStore currentStaffMemberStore,
                                               SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                               IGetOwnerOfGroupQuery getOwnerOfGroupQuery)
        {
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _getOwnerOfGroupQuery = getOwnerOfGroupQuery;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<StaffMember>> collection,
                                                     CommandViewModel<StaffMember> commandViewModel)
        {
            Guid commandStaffMemberId = commandViewModel.Model.Id;
            Guid currentStaffMemberId = _currentStaffMemberStore.StaffMember.Id;

            return StaffMemberIsRemovable(commandStaffMemberId, currentStaffMemberId) ?
                new ButtonViewModel(show: true, "Remove".Resource(), new RemoveStaffMemberFromGroupCommand(collection, commandViewModel, _removeStaffMemberFromGroupCommand, _selectedGroupDetailsStore)) :
                new ButtonViewModel(show: false);
        }

        private bool StaffMemberIsRemovable(Guid commandStaffMemberId, Guid currentStaffMemberId) => commandStaffMemberId != currentStaffMemberId && IsOwner(currentStaffMemberId);

        private bool IsOwner(Guid staffMemberId)
        {
            Group currentGroup = _selectedGroupDetailsStore.SelectedModel;
            Guid _groupOwnerId = _getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            return staffMemberId == _groupOwnerId;
        }
    }
}
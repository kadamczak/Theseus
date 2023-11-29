using System;
using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class DeleteOrLeaveGroupCommandGranter : CommandGranter<Group>
    {
        private readonly IGetOwnerOfGroupQuery _getOwnerOfGroupQuery;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly IDeleteGroupCommand _deleteGroupCommand;

        public DeleteOrLeaveGroupCommandGranter(IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                                ICurrentStaffMemberStore currentStaffMemberStore,
                                                IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                IDeleteGroupCommand deleteGroupCommand)
        {
            _getOwnerOfGroupQuery = getOwnerOfGroupQuery;
            _currentStaffMemberStore = currentStaffMemberStore;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _deleteGroupCommand = deleteGroupCommand;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Group>> collection,
                                                     CommandViewModel<Group> commandViewModel)
        {
            Group group = commandViewModel.Model;

            return CurrentStaffMemberIsGroupOwner(group) ?
                GrantDeleteGroupCommand(collection, commandViewModel) :
                GrantLeaveGroupCommand(collection, commandViewModel);
        }

        private bool CurrentStaffMemberIsGroupOwner(Group group)
        {
            Guid ownerId = _getOwnerOfGroupQuery.GetOwner(group.Id).Id;
            return ownerId == _currentStaffMemberStore.StaffMember.Id;
        }

        private ButtonViewModel GrantDeleteGroupCommand(ObservableCollection<CommandViewModel<Group>> collection, CommandViewModel<Group> groupCommandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Delete",
                                       new DeleteGroupCommand(collection, groupCommandViewModel, _deleteGroupCommand));
        }

        private ButtonViewModel GrantLeaveGroupCommand(ObservableCollection<CommandViewModel<Group>> collection, CommandViewModel<Group> groupCommandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Leave",
                                       new LeaveGroupCommand(collection, groupCommandViewModel, _removeStaffMemberFromGroupCommand, _currentStaffMemberStore));
        }
    }
}
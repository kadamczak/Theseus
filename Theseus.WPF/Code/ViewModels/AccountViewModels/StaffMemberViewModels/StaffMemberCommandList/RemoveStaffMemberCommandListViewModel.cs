using System;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class RemoveStaffMemberCommandListViewModel : CommandListViewModel<StaffMember>
    {
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;

        private Guid _groupOwnerId;
        private readonly bool _currentStaffMemberCanRemoveMembers = false;

        public RemoveStaffMemberCommandListViewModel(SelectedModelListStore<StaffMember> selectedStaffMemberListStore,
                                                     IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                     ICurrentStaffMemberStore currentStaffMemberStore,
                                                     SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery) : base(selectedStaffMemberListStore)
        {
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;

            Group currentGroup = selectedGroupDetailsStore.SelectedModel;
            _groupOwnerId = getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            _currentStaffMemberCanRemoveMembers = IsOwner(currentStaffMemberStore.StaffMember.Id);
        }

        protected override void AddModelToActionableModels(StaffMember staffMember)
        {
            CommandViewModel<StaffMember> staffMemberCommandViewModel = new CommandViewModel<StaffMember>(staffMember);

            if (StaffMemberIsRemovable(staffMember.Id))
            {
                GrantRemoveCommand(staffMemberCommandViewModel);
            }

            ActionableModels.Add(staffMemberCommandViewModel);
        }

        private bool StaffMemberIsRemovable(Guid staffMemberId) => _currentStaffMemberCanRemoveMembers && !IsOwner(staffMemberId);

        private bool IsOwner(Guid guid) => guid == _groupOwnerId;

        private void GrantRemoveCommand(CommandViewModel<StaffMember> staffMemberCommandViewModel)
        {
            staffMemberCommandViewModel.ShowCommand1 = true;
            staffMemberCommandViewModel.Command1Name = "Remove";
            staffMemberCommandViewModel.Command1 = new RemoveStaffMemberFromGroupCommand(this,
                                                                                         staffMemberCommandViewModel,
                                                                                         _removeStaffMemberFromGroupCommand,
                                                                                         _selectedGroupDetailsStore);
        }
    }
}
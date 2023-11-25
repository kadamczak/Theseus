using System;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.Stores.StaffMembers;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class RemoveStaffMemberCommandListViewModel : StaffMemberCommandListViewModel
    {
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;

        private Guid _groupOwnerId;
        private readonly bool _currentStaffMemberCanRemoveMembers = false;

        public RemoveStaffMemberCommandListViewModel(SelectedStaffMemberListStore selectedStaffMemberListStore,
                                                     IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                     ICurrentStaffMemberStore currentStaffMemberStore,
                                                     SelectedGroupDetailsStore selectedGroupDetailsStore,
                                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery) : base(selectedStaffMemberListStore)
        {
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;

            Group currentGroup = selectedGroupDetailsStore.SelectedGroup;
            _groupOwnerId = getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            _currentStaffMemberCanRemoveMembers = IsOwner(currentStaffMemberStore.StaffMember.Id);
        }

        protected override void AddStaffMemberToActionableStaffMembers(StaffMember staffMember)
        {
            CommandViewModel<StaffMember> staffMemberCommandViewModel = new CommandViewModel<StaffMember>(staffMember);

            if(_currentStaffMemberCanRemoveMembers && !IsOwner(staffMember.Id))
            {
                staffMemberCommandViewModel.ShowCommand1 = true;
                staffMemberCommandViewModel.Command1Name = "Remove";
                staffMemberCommandViewModel.Command1 = new RemoveStaffMemberFromGroupCommand(this,
                                                                                             staffMemberCommandViewModel,
                                                                                             _removeStaffMemberFromGroupCommand,
                                                                                             _selectedGroupDetailsStore);
            }

            ActionableStaffMembers.Add(staffMemberCommandViewModel);
        }

        private bool IsOwner(Guid guid) => guid == _groupOwnerId;
    }
}
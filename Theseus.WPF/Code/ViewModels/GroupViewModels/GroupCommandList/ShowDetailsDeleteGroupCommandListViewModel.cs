using System;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsDeleteGroupCommandListViewModel : CommandListViewModel<Group>
    {
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _navigationService;
        private readonly IGetOwnerOfGroupQuery _getOwnerOfGroupQuery;
        private readonly IRemoveStaffMemberFromGroupCommand _removeStaffMemberFromGroupCommand;
        private readonly IDeleteGroupCommand _deleteGroupCommand;
        private readonly StaffMember _currentStaffMember; 

        public ShowDetailsDeleteGroupCommandListViewModel(SelectedModelListStore<Group> selectedGroupListStore,
                                                    SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                    NavigationService<GroupDetailsViewModel> navigationService,
                                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                                    IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                                    IRemoveStaffMemberFromGroupCommand removeStaffMemberFromGroupCommand,
                                                    IDeleteGroupCommand deleteGroupCommand) : base(selectedGroupListStore)
        {
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _navigationService = navigationService;
            _getOwnerOfGroupQuery = getOwnerOfGroupQuery;
            _removeStaffMemberFromGroupCommand = removeStaffMemberFromGroupCommand;
            _deleteGroupCommand = deleteGroupCommand;

            _currentStaffMember = currentStaffMemberStore.StaffMember;
        }

        protected override void AddModelToActionableModels(Group group)
        {
            CommandViewModel<Group> groupCommandViewModel = new CommandViewModel<Group>(group)
            {
                Command1Name = "Details",
                ShowCommand1 = true
            };

            groupCommandViewModel.Command1 = new ShowGroupDetailsCommand(groupCommandViewModel,
                                                                         _selectedGroupDetailsStore,
                                                                         _navigationService);

            if (CurrentStaffMemberIsGroupOwner(group))
            {
                GrantDeleteGroupCommand(groupCommandViewModel);
            }
            else
            {
                GrantLeaveGroupCommand(groupCommandViewModel);
            }

            this.ActionableModels.Add(groupCommandViewModel);
        }

        private bool CurrentStaffMemberIsGroupOwner(Group group)
        {
            Guid ownerId = _getOwnerOfGroupQuery.GetOwner(group.Id).Id;
            return ownerId == _currentStaffMember.Id;
        }

        private void GrantLeaveGroupCommand(CommandViewModel<Group> groupCommandViewModel)
        {
            groupCommandViewModel.Command2Name = "Leave";
            groupCommandViewModel.ShowCommand2 = true;
            groupCommandViewModel.Command2 = new LeaveGroupCommand(this, groupCommandViewModel, _removeStaffMemberFromGroupCommand, _currentStaffMember);
        }

        private void GrantDeleteGroupCommand(CommandViewModel<Group> groupCommandViewModel)
        {
            groupCommandViewModel.Command2Name = "Delete";
            groupCommandViewModel.ShowCommand2 = true;
            groupCommandViewModel.Command2 = new DeleteGroupCommand(this, groupCommandViewModel, _deleteGroupCommand);
        }
    }
}
using System;
using System.Windows.Input;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>StaffMemberGroupDashboardViewModel</c> class contains bindings for StaffMember Group Dashboard View.
    /// </summary>
    public class StaffMemberGroupDashboardViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }

        public StaffMemberCommandListViewModel RemoveStaffMemberCommandListViewModel { get; set; }
        public bool AddStaffMemberAvailable { get; } = false;
        public ICommand AddStaffMember { get; }

        public StaffMemberGroupDashboardViewModel(IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                                  SelectedModelListStore<StaffMember> selectedStaffMemberListStore,
                                                  ICurrentStaffMemberStore currentStaffMemberStore,
                                                  StaffMemberCommandListViewModelFactory removeStaffMemberCommandListViewModel,
                                                  SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                  IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                                  NavigationService<AddStaffMemberToGroupViewModel> addStaffMemberToGroupNavigationService)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedModel;
            CurrentGroup.Owner = getOwnerOfGroupQuery.GetOwner(CurrentGroup.Id);

            AddStaffMemberAvailable = (currentStaffMemberStore.StaffMember.Id == CurrentGroup.Owner.Id);
            CreateStaffMemberCommandList(getStaffMembersOfGroupQuery, selectedStaffMemberListStore, removeStaffMemberCommandListViewModel);
            AddStaffMember = new NavigateCommand<AddStaffMemberToGroupViewModel>(addStaffMemberToGroupNavigationService);
        }

        private void CreateStaffMemberCommandList(IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                                  SelectedModelListStore<StaffMember> selectedStaffMemberListStore,
                                                  StaffMemberCommandListViewModelFactory removeStaffMemberCommandListViewModel)
        {
            LoadStaffMembersFromGroupToStore(getStaffMembersOfGroupQuery, CurrentGroup.Id, selectedStaffMemberListStore);
            RemoveStaffMemberCommandListViewModel = removeStaffMemberCommandListViewModel.Create(StaffMemberButtonCommand.Remove, StaffMemberButtonCommand.None, StaffMemberInfo.None);
            RemoveStaffMemberCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadStaffMembersFromGroupToStore(IGetStaffMembersOfGroupQuery query,
                                              Guid groupId,
                                              SelectedModelListStore<StaffMember> selectedStaffMemberListStore)
        {
            var staffMembers = query.GetStaffMembers(groupId);
            selectedStaffMemberListStore.ModelList = staffMembers;
        }
    }
}
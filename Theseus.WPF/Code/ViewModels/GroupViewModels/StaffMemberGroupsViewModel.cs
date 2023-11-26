using System;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberGroupsViewModel : ViewModelBase
    {
        public ShowDetailsDeleteGroupCommandListViewModel ShowDetailsGroupCommandListViewModel { get; }

        public StaffMemberGroupsViewModel(SelectedModelListStore<Group> selectedGroupListStore,
                                          IGetGroupsOfStaffMemberQuery getGroupsOfStaffMemberQuery,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          ShowDetailsDeleteGroupCommandListViewModel showDetailsGroupCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadGroupsOfStaffMember(getGroupsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedGroupListStore);

            ShowDetailsGroupCommandListViewModel = showDetailsGroupCommandListViewModel;
            ShowDetailsGroupCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadGroupsOfStaffMember(IGetGroupsOfStaffMemberQuery query, Guid staffMemberId, SelectedModelListStore<Group> selectedGroupListStore)
        {
            var groupList = query.GetGroups(staffMemberId);
            selectedGroupListStore.ModelList = groupList;
        }
    }
}
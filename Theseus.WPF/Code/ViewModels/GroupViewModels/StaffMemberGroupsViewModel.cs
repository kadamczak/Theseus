using System;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Groups;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberGroupsViewModel : ViewModelBase
    {
        public ShowDetailsGroupCommandListViewModel ShowDetailsGroupCommandListViewModel { get; }

        public StaffMemberGroupsViewModel(SelectedGroupListStore selectedGroupListStore,
                                          IGetGroupsOfStaffMemberQuery getGroupsOfStaffMemberQuery,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          ShowDetailsGroupCommandListViewModel showDetailsGroupCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadGroupsOfStaffMember(getGroupsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedGroupListStore);

            ShowDetailsGroupCommandListViewModel = showDetailsGroupCommandListViewModel;
            ShowDetailsGroupCommandListViewModel.CreateGroupCommandViewModels();
        }

        private void LoadGroupsOfStaffMember(IGetGroupsOfStaffMemberQuery query, Guid staffMemberId, SelectedGroupListStore selectedGroupListStore)
        {
            var groupList = query.GetGroups(staffMemberId);
            selectedGroupListStore.Groups = groupList;
        }
    }
}
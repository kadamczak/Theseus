using System;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberGroupsViewModel : ViewModelBase
    {
        public ShowDetailsDeleteGroupCommandListViewModel ShowDetailsGroupCommandListViewModel { get; }

        private string _groupName = string.Empty;
        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
                OnPropertyChanged(nameof(CanCreate));
            }
        }

        public ICommand CreateGroup { get; }
        public bool CanCreate => !string.IsNullOrEmpty(GroupName);

        public StaffMemberGroupsViewModel(SelectedModelListStore<Group> selectedGroupListStore,
                                          IGetGroupsOfStaffMemberQuery getGroupsOfStaffMemberQuery,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          ICreateGroupCommand createGroupCommand,
                                          ShowDetailsDeleteGroupCommandListViewModel showDetailsGroupCommandListViewModel)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            CreateGroup = new CreateGroupCommand(this, currentStaffMemberStore, createGroupCommand);
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
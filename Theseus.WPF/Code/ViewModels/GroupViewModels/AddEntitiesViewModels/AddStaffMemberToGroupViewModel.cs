using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class AddStaffMemberToGroupViewModel : ViewModelBase
    {
        private string _enteredUsername = string.Empty;
        public string EnteredUsername
        {
            get => _enteredUsername;
            set
            {
                _enteredUsername = value;
                OnPropertyChanged(nameof(EnteredUsername));
            }
        }

        private StaffMember? _staffMember;
        public StaffMember? StaffMember
        {
            get => _staffMember;
            set
            {
                _staffMember = value;
                OnPropertyChanged(nameof(StaffMember));
            }
        }

        private bool _staffMemberWasFound = false;
        public bool StaffMemberWasFound
        {
            get => _staffMemberWasFound;
            set
            {
                _staffMemberWasFound = value;
                OnPropertyChanged(nameof(StaffMemberWasFound));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private bool _staffMemberIsInCurrentGroup = false;
        public bool StaffMemberIsInCurrentGroup
        {
            get => _staffMemberIsInCurrentGroup;
            set
            {
                _staffMemberIsInCurrentGroup = value;
                OnPropertyChanged(nameof(StaffMemberIsInCurrentGroup));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        public bool CanAdd => StaffMemberWasFound && !StaffMemberIsInCurrentGroup;

        public Guid SelectedGroupId { get; }
        public ICommand SearchForStaffMember { get; }
        public ICommand AddStaffMemberToGroup { get; }

        public AddStaffMemberToGroupViewModel(SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                              IGetStaffMemberByUsernameQuery getStaffMemberByUsernameQuery,
                                              IGetGroupsOfStaffMemberQuery getGroupsOfStaffMemberQuery,
                                              IAddStaffMemberToGroupCommand addStaffMemberToGroupCommand,
                                              NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            PropertyChanged += OnPropertyChanged;

            SelectedGroupId = selectedGroupDetailsStore.SelectedModel.Id;
            SearchForStaffMember = new SearchForStaffMemberCommand(this, getStaffMemberByUsernameQuery, getGroupsOfStaffMemberQuery);
            AddStaffMemberToGroup = new AddStaffMemberToGroupCommand(this, addStaffMemberToGroupCommand, groupDetailsNavigationService);
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StaffMember))
            {
                StaffMemberWasFound = StaffMember is not null;
                StaffMemberIsInCurrentGroup = StaffMember?.Groups.Where(g => g.Id  == SelectedGroupId).Any() ?? false;
            }
        }
    }
}
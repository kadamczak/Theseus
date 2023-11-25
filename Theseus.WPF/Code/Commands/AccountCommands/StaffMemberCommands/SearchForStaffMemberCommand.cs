﻿using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class SearchForStaffMemberCommand : AsyncCommandBase
    {
        private readonly AddStaffMemberToGroupViewModel _addStaffMemberToGroupViewModel;
        private readonly IGetStaffMemberByUsernameQuery _getStaffMemberByUsernameQuery;
        private readonly IGetGroupsOfStaffMemberQuery _getGroupsOfStaffMemberQuery;

        public SearchForStaffMemberCommand(AddStaffMemberToGroupViewModel addStaffMemberToGroupViewModel,
                                           IGetStaffMemberByUsernameQuery getStaffMemberByUsernameQuery,
                                           IGetGroupsOfStaffMemberQuery getGroupsOfStaffMemberQuery)
        {
            _addStaffMemberToGroupViewModel = addStaffMemberToGroupViewModel;
            _getStaffMemberByUsernameQuery = getStaffMemberByUsernameQuery;
            _getGroupsOfStaffMemberQuery = getGroupsOfStaffMemberQuery;

            _addStaffMemberToGroupViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            StaffMember? staffMember = await _getStaffMemberByUsernameQuery.GetStaffMember(_addStaffMemberToGroupViewModel.EnteredUsername.Trim());

            if (staffMember is not null)
                staffMember.Groups = _getGroupsOfStaffMemberQuery.GetGroups(staffMember.Id).ToList();

            _addStaffMemberToGroupViewModel.StaffMember = staffMember;
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addStaffMemberToGroupViewModel.EnteredUsername))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_addStaffMemberToGroupViewModel.EnteredUsername) && base.CanExecute(parameter);
        }
    }
}

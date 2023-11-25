using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class AddStaffMemberToGroupCommand : AsyncCommandBase
    {
        private readonly AddStaffMemberToGroupViewModel _addStaffMemberToGroupViewModel;
        private readonly IAddStaffMemberToGroupCommand _addStaffMemberToGroupCommand;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public AddStaffMemberToGroupCommand(AddStaffMemberToGroupViewModel addStaffMemberToGroupViewModel,
                                            IAddStaffMemberToGroupCommand addStaffMemberToGroupCommand,
                                            NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _addStaffMemberToGroupViewModel = addStaffMemberToGroupViewModel;
            _addStaffMemberToGroupCommand = addStaffMemberToGroupCommand;
            _groupDetailsNavigationService = groupDetailsNavigationService;

            _addStaffMemberToGroupViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            StaffMember staffMember = _addStaffMemberToGroupViewModel.StaffMember!;
            Guid groupId = _addStaffMemberToGroupViewModel.SelectedGroupId;

            await _addStaffMemberToGroupCommand.AddToGroup(staffMember, groupId);
            _groupDetailsNavigationService.Navigate();
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addStaffMemberToGroupViewModel.CanAdd))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _addStaffMemberToGroupViewModel.CanAdd && base.CanExecute(parameter);
        }
    }
}
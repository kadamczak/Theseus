﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class AddPatientToGroupCommand : AsyncCommandBase
    {
        private readonly AddPatientToGroupViewModel _addPatientToGroupViewModel;
        private readonly IAddPatientToGroupCommand _addPatientToGroupCommand;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public AddPatientToGroupCommand(AddPatientToGroupViewModel addPatientToGroupViewModel,
                                        IAddPatientToGroupCommand addPatientToGroupCommand,
                                        NavigationService<GroupDetailsViewModel> groupDetailsViewModel)
        {
            _addPatientToGroupViewModel = addPatientToGroupViewModel;
            _addPatientToGroupCommand = addPatientToGroupCommand;
            _groupDetailsNavigationService = groupDetailsViewModel;

            _addPatientToGroupViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Patient patient = _addPatientToGroupViewModel.Patient!;
            Guid groupId = _addPatientToGroupViewModel.SelectedGroupId;

            await _addPatientToGroupCommand.AddToGroup(patient, groupId);
            _groupDetailsNavigationService.Navigate();
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addPatientToGroupViewModel.CanAdd))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _addPatientToGroupViewModel.CanAdd && base.CanExecute(parameter);
        }
    }
}
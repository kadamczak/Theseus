using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class SearchForPatientCommand : AsyncCommandBase
    {
        private readonly AddPatientToGroupViewModel _addPatientToGroupViewModel;
        private readonly IGetPatientByUsernameQuery _getPatientByUsernameQuery;
        private readonly IGetGroupByPatientQuery _getGroupByPatientQuery;

        public SearchForPatientCommand(AddPatientToGroupViewModel addPatientToGroupViewModel,
                                       IGetPatientByUsernameQuery getPatientByUsernameQuery,
                                       IGetGroupByPatientQuery getGroupByPatientQuery)
        {
            _addPatientToGroupViewModel = addPatientToGroupViewModel;
            _getPatientByUsernameQuery = getPatientByUsernameQuery;
            _getGroupByPatientQuery = getGroupByPatientQuery;

            _addPatientToGroupViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Patient? patient = await _getPatientByUsernameQuery.GetPatient(_addPatientToGroupViewModel.EnteredUsername.Trim());
            
            if (patient is not null)
                patient.Group = _getGroupByPatientQuery.GetGroup(patient.Id);

            _addPatientToGroupViewModel.Patient = patient;
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addPatientToGroupViewModel.EnteredUsername))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_addPatientToGroupViewModel.EnteredUsername) && base.CanExecute(parameter);
        }
    }
}
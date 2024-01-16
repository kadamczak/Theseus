using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    /// <summary>
    /// The <c>SearchForPatientCommand</c> class attempts to return a <c>Patient</c>
    /// with username corresponding to data stored in <c>AddPatientToGroupViewModel</c>.
    /// </summary>
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
            try
            {
                Patient? patient = await _getPatientByUsernameQuery.GetPatient(_addPatientToGroupViewModel.EnteredUsername.Trim());

                if (patient is not null)
                    patient.Group = _getGroupByPatientQuery.GetGroup(patient.Id);

                _addPatientToGroupViewModel.Patient = patient;
            }
            catch (SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
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
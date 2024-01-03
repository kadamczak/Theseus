using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class SavePatientInfoCommand : AsyncCommandBase
    {
        private readonly PatientDetailsLoggedInViewModel _patientDetailsLoggedInViewModel;
        private readonly IUpdatePatientCommand _updatePatientCommand;

        public SavePatientInfoCommand(PatientDetailsLoggedInViewModel patientDetailsLoggedInViewModel,
                                      IUpdatePatientCommand updatePatientCommand)
        {
            _patientDetailsLoggedInViewModel = patientDetailsLoggedInViewModel;
            _updatePatientCommand = updatePatientCommand;

            _patientDetailsLoggedInViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _updatePatientCommand.Update(_patientDetailsLoggedInViewModel.CurrentPatient);
                _patientDetailsLoggedInViewModel.UpdateCurrentPatientInfoFromViewModel();
            }
            catch(SqlException)
            {
                string messageBoxText = "CouldNotConnectToDatabase".Resource();
                string caption = "ActionFailed".Resource();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_patientDetailsLoggedInViewModel.CanUpdatePatient))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _patientDetailsLoggedInViewModel.CanUpdatePatient && base.CanExecute(parameter);
        }
    }
}
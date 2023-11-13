using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces;
using Theseus.WPF.Code.Bases;
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
            _patientDetailsLoggedInViewModel.UpdateCurrentPatientInfoFromViewModel();
            await _updatePatientCommand.Update(_patientDetailsLoggedInViewModel.CurrentPatient);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _patientDetailsLoggedInViewModel.CheckIfPatientCanSaveChanges() && base.CanExecute(parameter);
        }
    }
}
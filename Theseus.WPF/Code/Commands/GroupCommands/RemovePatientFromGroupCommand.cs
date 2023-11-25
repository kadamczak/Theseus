using System;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemovePatientFromGroupCommand : AsyncCommandBase
    {
        private readonly RemovePatientCommandListViewModel _removePatientCommandListViewModel;
        private readonly PatientCommandViewModel _patientCommandViewModel;
        private readonly IRemovePatientFromGroupCommand _removePatientFromGroupCommand;

        public RemovePatientFromGroupCommand(RemovePatientCommandListViewModel removePatientCommandListViewModel,
                                             PatientCommandViewModel patientCommandViewModel,
                                             IRemovePatientFromGroupCommand removePatientFromGroupCommand)
        {
            _removePatientCommandListViewModel = removePatientCommandListViewModel;
            _patientCommandViewModel = patientCommandViewModel;
            _removePatientFromGroupCommand = removePatientFromGroupCommand;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            string messageBoxText = "Do you want to remove patient from this group?";
            string caption = "Patient Removal";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                await _removePatientFromGroupCommand.RemoveFromGroup(_patientCommandViewModel.Patient);
                _removePatientCommandListViewModel.ActionablePatients.Remove(_patientCommandViewModel);
            }
        }
    }
}
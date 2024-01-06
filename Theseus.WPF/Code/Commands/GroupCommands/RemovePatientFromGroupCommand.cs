using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class RemovePatientFromGroupCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<CommandViewModel<Patient>> _patientCommandList;
        private readonly CommandViewModel<Patient> _patientCommandViewModel;
        private readonly IRemovePatientFromGroupCommand _removePatientFromGroupCommand;

        public RemovePatientFromGroupCommand(ObservableCollection<CommandViewModel<Patient>> patientCommandList,
                                             CommandViewModel<Patient> patientCommandViewModel,
                                             IRemovePatientFromGroupCommand removePatientFromGroupCommand)
        {
            _patientCommandList = patientCommandList;
            _patientCommandViewModel = patientCommandViewModel;
            _removePatientFromGroupCommand = removePatientFromGroupCommand;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            MessageBoxResult result = ShowMessageBox();

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await RemovePatientFromGroup();
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
        }

        private MessageBoxResult ShowMessageBox()
        {
            string messageBoxText = "DoYouWantToRemovePatient".Resource();
            string caption = "PatientRemoval".Resource();
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.No);
        }

        private async Task RemovePatientFromGroup()
        {
            await _removePatientFromGroupCommand.RemoveFromGroup(_patientCommandViewModel.Model);
            _patientCommandList.Remove(_patientCommandViewModel);
        }
    }
}
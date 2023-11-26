using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class RemovePatientCommandListViewModel : CommandListViewModel<Patient>
    {
        private readonly IRemovePatientFromGroupCommand _removePatientFromGroupCommand;

        public RemovePatientCommandListViewModel(SelectedModelListStore<Patient> selectedPatientListStore,
                                                 IRemovePatientFromGroupCommand removePatientFromGroupCommand) : base(selectedPatientListStore)
        {
            _removePatientFromGroupCommand = removePatientFromGroupCommand;
        }


        public override void AddModelToActionableModels(Patient patient)
        {
            CommandViewModel<Patient> patientCommandViewModel = new CommandViewModel<Patient>(patient)
            {
                Command1Name = "Remove",
                ShowCommand1 = true,
                ShowCommand2 = false
            };

            patientCommandViewModel.Command1 = new RemovePatientFromGroupCommand(this, patientCommandViewModel, _removePatientFromGroupCommand);
            this.ActionableModels.Add(patientCommandViewModel);
        }
    }
}
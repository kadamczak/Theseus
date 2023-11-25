using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores.Patients;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class RemovePatientCommandListViewModel : PatientCommandListViewModel
    {
        private readonly IRemovePatientFromGroupCommand _removePatientFromGroupCommand;

        public RemovePatientCommandListViewModel(SelectedPatientListStore selectedPatientListStore,
                                                 IRemovePatientFromGroupCommand removePatientFromGroupCommand) : base(selectedPatientListStore)
        {
            _removePatientFromGroupCommand = removePatientFromGroupCommand;
        }

        
        protected override void AddPatientToActionablePatients(Patient patient)
        {
            PatientCommandViewModel patientCommandViewModel = new PatientCommandViewModel(patient)
            {
                Command1Name = "Remove",
                ShowCommand1 = true,
                ShowCommand2 = false
            };

            patientCommandViewModel.Command1 = new Commands.GroupCommands.RemovePatientFromGroupCommand(this, patientCommandViewModel, _removePatientFromGroupCommand);
            this.ActionablePatients.Add(patientCommandViewModel);
        }
    }
}
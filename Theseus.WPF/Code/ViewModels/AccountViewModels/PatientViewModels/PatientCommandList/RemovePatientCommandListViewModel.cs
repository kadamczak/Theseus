using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores.Patients;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class RemovePatientCommandListViewModel : PatientCommandListViewModel
    {
        public RemovePatientCommandListViewModel(SelectedPatientListStore selectedPatientListStore) : base(selectedPatientListStore)
        {
        }

        
        protected override void AddPatientToActionablePatients(Patient patient)
        {
            PatientCommandViewModel patientCommandViewModel = new PatientCommandViewModel(patient)
            {
                Command1Name = "Remove",
                ShowCommand2 = false
            };

            patientCommandViewModel.Command1 = new RemovePatientFromGroupCommand();

            this.ActionablePatients.Add(patientCommandViewModel);
        }
    }
}
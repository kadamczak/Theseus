using System.Collections.ObjectModel;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Patients;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class PatientCommandListViewModel : ViewModelBase
    {
        public SelectedPatientListStore SelectedPatientListStore { get; }
        public ObservableCollection<CommandViewModel<Patient>> ActionablePatients { get; } = new ObservableCollection<CommandViewModel<Patient>>();

        public PatientCommandListViewModel(SelectedPatientListStore selectedPatientListStore)
        {
            this.SelectedPatientListStore = selectedPatientListStore;
        }

        public void CreatePatientCommandViewModels()
        {
            this.ActionablePatients.Clear();

            foreach (var patient in SelectedPatientListStore.Patients)
            {
                AddPatientToActionablePatients(patient);
            }
        }

        protected abstract void AddPatientToActionablePatients(Patient patient);
    }
}
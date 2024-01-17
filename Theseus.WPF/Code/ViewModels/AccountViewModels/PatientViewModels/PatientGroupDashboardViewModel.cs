using System;
using System.Windows.Input;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels
{
    /// <summary>
    /// The <c>PatientGroupDashboardViewModel</c> class contains bindings for Patient Group Dashboard View.
    /// </summary>
    public class PatientGroupDashboardViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }
        public PatientCommandListViewModel RemovePatientCommandListViewModel { get; set; }
        public ICommand AddPatient { get; }

        public PatientGroupDashboardViewModel(NavigationService<AddPatientToGroupViewModel> addPatientToGroupNavigationService,
                                              IGetPatientsOfGroupWithFullIncludeQuery getPatientsOfGroupQuery,
                                              SelectedModelListStore<Patient> selectedPatientListStore,
                                              PatientCommandListViewModelFactory removePatientCommandListViewModel,
                                              SelectedModelDetailsStore<Group> selectedGroupDetailsStore)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedModel;

            CreatePatientCommandList(getPatientsOfGroupQuery, selectedPatientListStore, removePatientCommandListViewModel);
            AddPatient = new NavigateCommand<AddPatientToGroupViewModel>(addPatientToGroupNavigationService);
        }

        private void CreatePatientCommandList(IGetPatientsOfGroupWithFullIncludeQuery getPatientsOfGroupQuery,
                                              SelectedModelListStore<Patient> selectedPatientListStore,
                                              PatientCommandListViewModelFactory removePatientCommandListViewModel)
        {
            LoadPatientsFromGroupToStore(getPatientsOfGroupQuery, CurrentGroup.Id, selectedPatientListStore);
            RemovePatientCommandListViewModel = removePatientCommandListViewModel.Create(PatientButtonCommand.Remove, PatientButtonCommand.None, PatientInfo.None);
            RemovePatientCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadPatientsFromGroupToStore(IGetPatientsOfGroupWithFullIncludeQuery query,
                                                 Guid groupId,
                                                 SelectedModelListStore<Patient> selectedPatientListStore)
        {
            var patients = query.GetPatients(groupId);
            selectedPatientListStore.ModelList = patients;
        }
    }
}
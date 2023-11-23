using System;
using System.Windows.Input;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.Stores.Patients;

namespace Theseus.WPF.Code.ViewModels
{
    public class GroupDetailsViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }
        public RemovePatientCommandListViewModel RemovePatientCommandListViewModel { get; set; }
        public ShowDetailsExamSetCommandListViewModel ShowDetailsExamSetCommandListViewModel { get; set; }

        public ICommand AddPatient { get; }

        public GroupDetailsViewModel(SelectedGroupDetailsStore selectedGroupDetailsStore,
                                     IGetPatientsOfGroupQuery getPatientsOfGroupQuery, 
                                     SelectedPatientListStore selectedPatientListStore,
                                     RemovePatientCommandListViewModel removePatientCommandListViewModel,
                                     IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                     SelectedExamSetListStore selectedExamSetListStore,
                                     ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel,
                                     NavigationService<AddPatientToGroupViewModel> addPatientToGroupNavigationService)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedGroup;

            CreatePatientCommandList(getPatientsOfGroupQuery, selectedPatientListStore, removePatientCommandListViewModel);
            CreateExamSetCommandList(getExamSetsOfGroupQuery, selectedExamSetListStore, showDetailsExamSetCommandListViewModel);

            AddPatient = new NavigateCommand<AddPatientToGroupViewModel>(addPatientToGroupNavigationService);
        }

        private void CreatePatientCommandList(IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                              SelectedPatientListStore selectedPatientListStore,
                                              RemovePatientCommandListViewModel removePatientCommandListViewModel)
        {
            LoadPatientFromGroupToStore(getPatientsOfGroupQuery, CurrentGroup.Id, selectedPatientListStore);
            RemovePatientCommandListViewModel = removePatientCommandListViewModel;
            RemovePatientCommandListViewModel.CreatePatientCommandViewModels();
        }

        private void LoadPatientFromGroupToStore(IGetPatientsOfGroupQuery query,
                                                 Guid groupId,
                                                 SelectedPatientListStore selectedPatientListStore)
        {
            var patients = query.GetPatients(groupId);
            selectedPatientListStore.Patients = patients;
        }

        private void CreateExamSetCommandList(IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                              SelectedExamSetListStore selectedExamSetListStore,
                                              ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel)
        {
            LoadExamSetsFromGroupToStore(getExamSetsOfGroupQuery, CurrentGroup.Id, selectedExamSetListStore);
            ShowDetailsExamSetCommandListViewModel = showDetailsExamSetCommandListViewModel;
            ShowDetailsExamSetCommandListViewModel.CreateExamSetCommandViewModels();
        }

        private void LoadExamSetsFromGroupToStore(IGetExamSetsOfGroupQuery query,
                                                  Guid groupId,
                                                  SelectedExamSetListStore selectedExamSetListStore)
        {
            var examSets = query.GetExamSets(groupId);
            selectedExamSetListStore.ExamSets = examSets;
        }
    }
}
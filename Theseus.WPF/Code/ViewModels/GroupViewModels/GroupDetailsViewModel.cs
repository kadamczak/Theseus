using System;
using System.Windows.Input;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.Stores.Patients;
using Theseus.WPF.Code.Stores.StaffMembers;

namespace Theseus.WPF.Code.ViewModels
{
    public class GroupDetailsViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }
        public string GroupOwnerName { get; } = string.Empty;

        public RemoveStaffMemberCommandListViewModel RemoveStaffMemberCommandListViewModel { get; set; }
        public RemovePatientCommandListViewModel RemovePatientCommandListViewModel { get; set; }
        public ShowDetailsExamSetCommandListViewModel ShowDetailsExamSetCommandListViewModel { get; set; }

        public bool AddStaffMemberAvailable { get; } = false;

        public ICommand AddStaffMember { get; }
        public ICommand AddPatient { get; }

        public GroupDetailsViewModel(SelectedGroupDetailsStore selectedGroupDetailsStore,
                                     ICurrentStaffMemberStore currentStaffMemberStore,        
                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                     IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                     SelectedStaffMemberListStore selectedStaffMemberListStore,
                                     RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel,
                                     IGetPatientsOfGroupQuery getPatientsOfGroupQuery, 
                                     SelectedPatientListStore selectedPatientListStore,
                                     RemovePatientCommandListViewModel removePatientCommandListViewModel,
                                     IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                     SelectedExamSetListStore selectedExamSetListStore,
                                     ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel,
                                     NavigationService<AddStaffMemberToGroupViewModel> addStaffMemberToGroupNavigationService,
                                     NavigationService<AddPatientToGroupViewModel> addPatientToGroupNavigationService)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedGroup;
            CurrentGroup.Owner = getOwnerOfGroupQuery.GetOwner(CurrentGroup.Id);
            AddStaffMemberAvailable = (currentStaffMemberStore.StaffMember.Id == CurrentGroup.Owner.Id);
            GroupOwnerName = CurrentGroup.Owner.Username;

            CreateStaffMemberCommandList(getStaffMembersOfGroupQuery, selectedStaffMemberListStore, removeStaffMemberCommandListViewModel);
            CreatePatientCommandList(getPatientsOfGroupQuery, selectedPatientListStore, removePatientCommandListViewModel);
            CreateExamSetCommandList(getExamSetsOfGroupQuery, selectedExamSetListStore, showDetailsExamSetCommandListViewModel);

            AddStaffMember = new NavigateCommand<AddStaffMemberToGroupViewModel>(addStaffMemberToGroupNavigationService);
            AddPatient = new NavigateCommand<AddPatientToGroupViewModel>(addPatientToGroupNavigationService);
        }

        private void CreateStaffMemberCommandList(IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                                  SelectedStaffMemberListStore selectedStaffMemberListStore,
                                                  RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel)
        {
            LoadStaffMembersFromGroupToStore(getStaffMembersOfGroupQuery, CurrentGroup.Id, selectedStaffMemberListStore);
            RemoveStaffMemberCommandListViewModel = removeStaffMemberCommandListViewModel;
            RemoveStaffMemberCommandListViewModel.CreateStaffMemberCommandViewModels();
        }

        private void LoadStaffMembersFromGroupToStore(IGetStaffMembersOfGroupQuery query,
                                                      Guid groupId,
                                                      SelectedStaffMemberListStore selectedStaffMemberListStore)
        {
            var staffMembers = query.GetStaffMembers(groupId);
            selectedStaffMemberListStore.StaffMembers = staffMembers;
        }

        private void CreatePatientCommandList(IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                              SelectedPatientListStore selectedPatientListStore,
                                              RemovePatientCommandListViewModel removePatientCommandListViewModel)
        {
            LoadPatientsFromGroupToStore(getPatientsOfGroupQuery, CurrentGroup.Id, selectedPatientListStore);
            RemovePatientCommandListViewModel = removePatientCommandListViewModel;
            RemovePatientCommandListViewModel.CreatePatientCommandViewModels();
        }

        private void LoadPatientsFromGroupToStore(IGetPatientsOfGroupQuery query,
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
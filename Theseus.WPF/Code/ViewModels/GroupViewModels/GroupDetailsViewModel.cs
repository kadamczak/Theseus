using System;
using System.Windows.Input;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

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

        public GroupDetailsViewModel(SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                     ICurrentStaffMemberStore currentStaffMemberStore,        
                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                     IGetStaffMembersOfGroupQuery getStaffMembersOfGroupQuery,
                                     SelectedModelListStore<StaffMember> selectedStaffMemberListStore,
                                     RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel,
                                     IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                     SelectedModelListStore<Patient> selectedPatientListStore,
                                     RemovePatientCommandListViewModel removePatientCommandListViewModel,
                                     IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                     SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                     ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel,
                                     NavigationService<AddStaffMemberToGroupViewModel> addStaffMemberToGroupNavigationService,
                                     NavigationService<AddPatientToGroupViewModel> addPatientToGroupNavigationService)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedModel;
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
                                                  SelectedModelListStore<StaffMember> selectedStaffMemberListStore,
                                                  RemoveStaffMemberCommandListViewModel removeStaffMemberCommandListViewModel)
        {
            LoadStaffMembersFromGroupToStore(getStaffMembersOfGroupQuery, CurrentGroup.Id, selectedStaffMemberListStore);
            RemoveStaffMemberCommandListViewModel = removeStaffMemberCommandListViewModel;
            RemoveStaffMemberCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadStaffMembersFromGroupToStore(IGetStaffMembersOfGroupQuery query,
                                                      Guid groupId,
                                                      SelectedModelListStore<StaffMember> selectedStaffMemberListStore)
        {
            var staffMembers = query.GetStaffMembers(groupId);
            selectedStaffMemberListStore.ModelList = staffMembers;
        }

        private void CreatePatientCommandList(IGetPatientsOfGroupQuery getPatientsOfGroupQuery,
                                              SelectedModelListStore<Patient> selectedPatientListStore,
                                              RemovePatientCommandListViewModel removePatientCommandListViewModel)
        {
            LoadPatientsFromGroupToStore(getPatientsOfGroupQuery, CurrentGroup.Id, selectedPatientListStore);
            RemovePatientCommandListViewModel = removePatientCommandListViewModel;
            RemovePatientCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadPatientsFromGroupToStore(IGetPatientsOfGroupQuery query,
                                                 Guid groupId,
                                                 SelectedModelListStore<Patient> selectedPatientListStore)
        {
            var patients = query.GetPatients(groupId);
            selectedPatientListStore.ModelList = patients;
        }

        private void CreateExamSetCommandList(IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                              SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                              ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel)
        {
            LoadExamSetsFromGroupToStore(getExamSetsOfGroupQuery, CurrentGroup.Id, selectedExamSetListStore);
            ShowDetailsExamSetCommandListViewModel = showDetailsExamSetCommandListViewModel;
            ShowDetailsExamSetCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamSetsFromGroupToStore(IGetExamSetsOfGroupQuery query,
                                                  Guid groupId,
                                                  SelectedModelListStore<ExamSet> selectedExamSetListStore)
        {
            var examSets = query.GetExamSets(groupId);
            selectedExamSetListStore.ModelList = examSets;
        }
    }
}
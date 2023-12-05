using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetGroupDashboardViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }
        public ExamSetCommandListViewModel ExamSetCommandListViewModel { get; set; }
        public ICommand ChangeExamSets { get; }

        public ExamSetGroupDashboardViewModel(SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                              ICurrentStaffMemberStore currentStaffMemberStore,
                                              IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                              SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                              ExamSetCommandListViewModelFactory examSetCommandListViewModel,
                                              ExamSetsInGroupStore examSetsInGroupStore,
                                              IGetExamSetsOfStaffMemberInGroupQuery getExamSetsQuery,
                                              NavigationService<SelectExamSetsInGroupViewModel> selectExamSetsNavigationService,
                                              ExamSetReturnServiceStore examSetReturnServiceStore,
                                              NavigationStore navigationStore,
                                              Func<GroupDetailsViewModel> viewModelGenerator)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedModel;
            examSetReturnServiceStore.ExamSetNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);

            CreateExamSetCommandList(getExamSetsOfGroupQuery, selectedExamSetListStore, examSetCommandListViewModel);
            SaveExamSetsBelongingToCurrentStaffMemberInStore(getExamSetsQuery, currentStaffMemberStore.StaffMember.Id, CurrentGroup.Id, examSetsInGroupStore);

            ChangeExamSets = new NavigateCommand<SelectExamSetsInGroupViewModel>(selectExamSetsNavigationService);
        }

        private void CreateExamSetCommandList(IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                              SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                              ExamSetCommandListViewModelFactory examSetCommandListViewModel)
        {
            LoadExamSetsFromGroupToStore(getExamSetsOfGroupQuery, CurrentGroup.Id, selectedExamSetListStore);
            ExamSetCommandListViewModel = examSetCommandListViewModel.Create(ExamSetButtonCommand.ShowDetails, ExamSetButtonCommand.RemoveFromGroup, ExamSetInfo.OwnerInfo);
            ExamSetCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamSetsFromGroupToStore(IGetExamSetsOfGroupQuery query,
                                                  Guid groupId,
                                                  SelectedModelListStore<ExamSet> selectedExamSetListStore)
        {
            var examSets = query.GetExamSets(groupId);
            selectedExamSetListStore.ModelList = examSets;
        }

        private void SaveExamSetsBelongingToCurrentStaffMemberInStore(IGetExamSetsOfStaffMemberInGroupQuery query, Guid staffMemberId, Guid groupId, ExamSetsInGroupStore examSetsInGroup)
        {
            var examSets = query.GetExamSets(staffMemberId, groupId);
            examSetsInGroup.SelectedExamSets = new ObservableCollection<ExamSet>(examSets);
        }
    }
}
using System;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.Stores.Groups;

namespace Theseus.WPF.Code.ViewModels
{
    public class GroupDetailsViewModel : ViewModelBase
    {
        public Group CurrentGroup { get; }

        public RemovePatientCommandListViewModel RemovePatientCommandListViewModel { get; set; }
        public ShowDetailsExamSetCommandListViewModel ShowDetailsExamSetCommandListViewModel { get; set; }

        public GroupDetailsViewModel(SelectedGroupDetailsStore selectedGroupDetailsStore,
                                     //IGetPatientsOfGroupQuery getPatientsOfGroupQuery, 
                                     IGetExamSetsOfGroupQuery getExamSetsOfGroupQuery,
                                     SelectedExamSetListStore selectedExamSetListStore,
                                     ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel)
        {
            CurrentGroup = selectedGroupDetailsStore.SelectedGroup;

            CreatePatientCommandList();
            CreateExamSetCommandList(getExamSetsOfGroupQuery, selectedExamSetListStore, showDetailsExamSetCommandListViewModel);
        }

        private void CreatePatientCommandList()
        {

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
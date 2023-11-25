using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseSetsViewModel : ViewModelBase
    {
        public ShowDetailsExamSetCommandListViewModel ShowDetailsExamSetCommandListViewModel { get; }

        public BrowseSetsViewModel(ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel,
                                   SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                   IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery,
                                   ICurrentStaffMemberStore currentStaffMemberStore)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadExamSetsOfStaffMember(getAllExamSetsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedExamSetListStore);

            ShowDetailsExamSetCommandListViewModel = showDetailsExamSetCommandListViewModel;
            ShowDetailsExamSetCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamSetsOfStaffMember(IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery,
                                               Guid staffMemberId,
                                               SelectedModelListStore<ExamSet> selectedExamSetListStore)
        {
            var examSetList = getAllExamSetsOfStaffMemberQuery.GetExamSets(staffMemberId);
            selectedExamSetListStore.ModelList = examSetList;
        }
    }
}
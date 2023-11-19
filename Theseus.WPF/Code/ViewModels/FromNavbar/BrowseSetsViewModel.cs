using System;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseSetsViewModel : ViewModelBase
    {
        public ShowDetailsExamSetCommandListViewModel ShowDetailsExamSetCommandListViewModel { get; }

        public BrowseSetsViewModel(ShowDetailsExamSetCommandListViewModel showDetailsExamSetCommandListViewModel,
                                   SelectedExamSetListStore selectedExamSetListStore,
                                   IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery,
                                   ICurrentStaffMemberStore currentStaffMemberStore)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadExamSetsOfStaffMember(getAllExamSetsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedExamSetListStore);

            ShowDetailsExamSetCommandListViewModel = showDetailsExamSetCommandListViewModel;
            ShowDetailsExamSetCommandListViewModel.CreateExamSetCommandViewModels();
        }

        private void LoadExamSetsOfStaffMember(IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery, Guid staffMemberId, SelectedExamSetListStore selectedExamSetListStore)
        {
            var examSetList = getAllExamSetsOfStaffMemberQuery.GetExamSets(staffMemberId);
            selectedExamSetListStore.ExamSets = examSetList;
        }
    }
}
using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseSetsViewModel : ViewModelBase
    {
        public ShowDetailsDeleteExamSetCommandListViewModel ShowDetailsDeleteExamSetCommandListViewModel { get; }

        public BrowseSetsViewModel(ShowDetailsDeleteExamSetCommandListViewModel showDetailsDeleteExamSetCommandListViewModel,
                                   SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                   IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery,
                                   ICurrentStaffMemberStore currentStaffMemberStore)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadExamSetsOfStaffMember(getAllExamSetsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedExamSetListStore);

            ShowDetailsDeleteExamSetCommandListViewModel = showDetailsDeleteExamSetCommandListViewModel;
            ShowDetailsDeleteExamSetCommandListViewModel.CreateModelCommandViewModels();
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
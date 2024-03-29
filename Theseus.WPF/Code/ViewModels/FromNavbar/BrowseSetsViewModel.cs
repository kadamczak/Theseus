﻿using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseSetsViewModel : ViewModelBase
    {
        public ExamSetCommandListViewModel ShowDetailsDeleteExamSetCommandListViewModel { get; }

        public BrowseSetsViewModel(ExamSetCommandListViewModelFactory showDetailsDeleteExamSetCommandListViewModel,
                                   SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                   IGetAllExamSetsOfStaffMemberQuery getAllExamSetsOfStaffMemberQuery,
                                   ICurrentStaffMemberStore currentStaffMemberStore,
                                   ExamSetReturnServiceStore examSetReturnServiceStore,
                                   NavigationStore navigationStore,
                                   Func<BrowseSetsViewModel> viewModelGenerator)
        {
            if (!currentStaffMemberStore.IsStaffMemberLoggedIn)
                throw new StaffMemberNotLoggedInException();

            LoadExamSetsOfStaffMember(getAllExamSetsOfStaffMemberQuery, currentStaffMemberStore.StaffMember!.Id, selectedExamSetListStore);
            examSetReturnServiceStore.ExamSetNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelGenerator);

            ShowDetailsDeleteExamSetCommandListViewModel = showDetailsDeleteExamSetCommandListViewModel.Create(ExamSetButtonCommand.ShowDetails, ExamSetButtonCommand.Delete, ExamSetInfo.GeneralInfo);
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
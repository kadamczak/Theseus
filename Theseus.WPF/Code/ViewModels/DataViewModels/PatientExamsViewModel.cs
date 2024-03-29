﻿using System.Windows.Input;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientExamsViewModel : ViewModelBase
    {
        public ExamCommandListViewModel ExamCommandListViewModel { get; set; }
        public ICommand GoBack { get; }

        public PatientExamsViewModel(ExamCommandListViewModelFactory examCommandListViewModelFactory,
                                    SelectedModelDetailsStore<Patient> patientStore,
                                    IGetExamsOfPatientWithFullIncludeQuery getExamsQuery,
                                    ExamSetGroupStatCalculator examSetStatCalculator,
                                    SelectedModelListStore<Exam> selectedExamListStore,
                                    NavigationService<ViewDataViewModel> viewDataNavigationService)
        {
            LoadExamsOfCurrentStaffMember(getExamsQuery, patientStore, selectedExamListStore);
            examSetStatCalculator.Calculate(selectedExamListStore.ModelList, calculateAverages: false);

            GoBack = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);

            ExamCommandListViewModel = examCommandListViewModelFactory.Create(ExamButtonCommand.Delete, ExamButtonCommand.ViewStages, ExamInfo.GeneralInfo);
            ExamCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamsOfCurrentStaffMember(IGetExamsOfPatientWithFullIncludeQuery getExamsQuery,
                                                   SelectedModelDetailsStore<Patient> patientStore,
                                                   SelectedModelListStore<Exam> selectedExamListStore)
        {
            var exams = getExamsQuery.GetExams(patientStore.SelectedModel.Id);
            selectedExamListStore.ModelList = exams;
        }
    }
}

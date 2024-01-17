using System;
using System.ComponentModel;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    /// <summary>
    /// The <c>BeginExamCommand</c> class is called upon exam start in order to initialize <c>CurrentExamStore</c> state
    /// and then navigate to Exam Practice View.
    /// </summary>
    public class BeginExamCommand : CommandBase
    {
        private readonly BeginTestViewModel _beginTestViewModel;
        private readonly CurrentExamStore _currentExamStore;
        private readonly ICurrentPatientStore _currentPatientStore;
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazesOfExamSetQuery;
        private readonly NavigationService<ExamPracticeViewModel> _examPracticeNavigationService;
        private readonly NavigationEnabledStore _navigationEnabledStore;

        public BeginExamCommand(BeginTestViewModel beginTestViewModel,
                                CurrentExamStore currentExamStore,
                                ICurrentPatientStore currentPatientStore,
                                IGetOrderedMazesWithSolutionOfExamSetQuery getMazesOfExamSetQuery,
                                NavigationService<ExamPracticeViewModel> examPracticeNavigationService,
                                NavigationEnabledStore navigationEnabledStore)
        {
            _beginTestViewModel = beginTestViewModel;
            _currentExamStore = currentExamStore;
            _currentPatientStore = currentPatientStore;
            _getMazesOfExamSetQuery = getMazesOfExamSetQuery;
            _examPracticeNavigationService = examPracticeNavigationService;
            _navigationEnabledStore = navigationEnabledStore;

            _beginTestViewModel.PropertyChanged += OnPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            SetupExamStore();

            _navigationEnabledStore.NavigationEnabled = false;

            _examPracticeNavigationService.Navigate();
        }

        private void SetupExamStore()
        {
            _currentExamStore.Mazes = _getMazesOfExamSetQuery.GetMazesWithSolution(_beginTestViewModel.SelectedExamSet.Id);
            _currentExamStore.CurrentIndex = 0;
            _currentExamStore.CurrentExam = new Exam(Guid.NewGuid())
            {
                Patient = _currentPatientStore.Patient,
                ExamSet = _beginTestViewModel.SelectedExamSet
            };

            ExamStage firstStage = new ExamStage(Guid.NewGuid())
            {
                Exam = _currentExamStore.CurrentExam,
                Index = 0
            };

            _currentExamStore.CurrentExam.Stages.Add(firstStage);
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_beginTestViewModel.SelectedExamSet))
                OnCanExecuteChanged();

            else if (e.PropertyName == nameof(_beginTestViewModel.IsPatientLoggedIn))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _beginTestViewModel.SelectedExamSet is not null && _beginTestViewModel.IsPatientLoggedIn && base.CanExecute(parameter);
        }
    }
}
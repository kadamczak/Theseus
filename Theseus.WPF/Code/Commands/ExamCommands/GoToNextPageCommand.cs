using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class GoToNextPageCommand : CommandBase
    {
        private readonly ExamPageViewModel _viewModel;
        private readonly CurrentExamStore _currentExamStore;
        private readonly NavigationService<ExamTransitionViewModel> _examTransitionNavigationService;
        private readonly NavigationService<ExamEndViewModel> _examEndNavigationService;

        public bool Active { get; set; } = true;

        public GoToNextPageCommand(ExamPageViewModel viewModel,
                                   CurrentExamStore currentExamStore,
                                   NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                   NavigationService<ExamEndViewModel> examEndNavigationService)
        {
            _viewModel = viewModel;
            _currentExamStore = currentExamStore;
            _examTransitionNavigationService = examTransitionNavigationService;
            _examEndNavigationService = examEndNavigationService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            SaveStageCompletionStatus(parameter);

            if (LastMazeFinished())
            {
                _examEndNavigationService.Navigate();
            }
            else
            {
                _currentExamStore.CurrentIndex++;
                CreateNextExamStage();
                _examTransitionNavigationService.Navigate();
            }
        }

        private void SaveStageCompletionStatus(object? parameter)
        {
            string parameterText = (string)parameter!;
            bool mazeCompleted = bool.Parse(parameterText);

            ExamStage currentExamStage = _currentExamStore.CurrentExam.Stages.Last();
            currentExamStage.Completed = mazeCompleted;
        }

        private void CreateNextExamStage()
        {
            ExamStage stage = new ExamStage(Guid.NewGuid())
            {
                Exam = _currentExamStore.CurrentExam,
                Index = _currentExamStore.CurrentIndex
            };

            _currentExamStore.CurrentExam.Stages.Add(stage);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanGoToNextPage))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CanGoToNextPage && base.CanExecute(parameter);
        }

        private bool LastMazeFinished() => _currentExamStore.CurrentIndex == _currentExamStore.Mazes.Count() - 1;
    }
}
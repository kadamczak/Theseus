using System;
using System.ComponentModel;
using System.Linq;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class GoToNextPageCommand : CommandBase
    {
        private readonly ExamViewModel _viewModel;
        private readonly CurrentExamStore _currentExamStore;
        private readonly ICreateExamCommand _createExamCommand;
        private readonly NavigationService<ExamTransitionViewModel> _examTransitionNavigationService;
        private readonly NavigationService<ExamEndViewModel> _examEndNavigationService;
        private readonly bool _saveStageInfo;

        public bool Active { get; set; } = true;

        public GoToNextPageCommand(ExamViewModel viewModel,
                                   CurrentExamStore currentExamStore,
                                   NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                   NavigationService<ExamEndViewModel> examEndNavigationService,
                                   ICreateExamCommand createExamCommand,
                                   bool saveStageInfo)
        {
            _viewModel = viewModel;
            _currentExamStore = currentExamStore;
            _createExamCommand = createExamCommand;
            _examTransitionNavigationService = examTransitionNavigationService;
            _examEndNavigationService = examEndNavigationService;
            _saveStageInfo = saveStageInfo;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            if(_saveStageInfo)
                SaveStageCompletionStatus(parameter);

            if (LastMazeFinished())
            {
                _createExamCommand.Create(_currentExamStore.CurrentExam);
                _examEndNavigationService.Navigate();
            }
            else
            {
                if (_saveStageInfo)
                {
                    _currentExamStore.CurrentIndex++;
                    CreateNextExamStage();
                }
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
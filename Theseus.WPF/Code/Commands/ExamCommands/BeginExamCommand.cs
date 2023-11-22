using System.ComponentModel;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class BeginExamCommand : CommandBase
    {
        private readonly BeginTestViewModel _beginTestViewModel;
        private readonly CurrentExamStore _currentExamStore;
        private readonly IGetMazesWithSolutionOfExamSetQuery _getMazesOfExamSetQuery;
        private readonly NavigationService<ExamPageViewModel> _examPageNavigationService;

        public BeginExamCommand(BeginTestViewModel beginTestViewModel,
                                CurrentExamStore currentExamStore,
                                IGetMazesWithSolutionOfExamSetQuery getMazesOfExamSetQuery,
                                NavigationService<ExamPageViewModel> examPageNavigationService)
        {
            _beginTestViewModel = beginTestViewModel;
            _currentExamStore = currentExamStore;
            _getMazesOfExamSetQuery = getMazesOfExamSetQuery;
            _examPageNavigationService = examPageNavigationService;

            _beginTestViewModel.PropertyChanged += OnPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _currentExamStore.Mazes = _getMazesOfExamSetQuery.GetMazesWithSolution(_beginTestViewModel.SelectedExamSet.Id);
            _currentExamStore.CurrentIndex = 0;

            _examPageNavigationService.Navigate();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_beginTestViewModel.SelectedExamSet))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _beginTestViewModel.SelectedExamSet is not null && base.CanExecute(parameter);
        }
    }
}

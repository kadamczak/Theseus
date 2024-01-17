using System.ComponentModel;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    /// <summary>
    /// The <c>GenerateExamSetCommand</c> class uses <c>ExamSetCreator</c> to generate an <c>ExamSet</c> following
    /// settings saved in <c>ExamSetGeneratorViewModel</c>.
    /// </summary>
    public class GenerateExamSetCommand : CommandBase
    {
        private readonly ExamSetGeneratorViewModel _examSetGeneratorViewModel;
        private readonly ExamSetCreator _examSetCreator;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetGeneratorResultViewModel> _resultNavigationService;

        public GenerateExamSetCommand(ExamSetGeneratorViewModel examSetGeneratorViewModel,
                                      ExamSetCreator examSetCreator,
                                      ICurrentStaffMemberStore currentStaffMemberStore,
                                      SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                      NavigationService<ExamSetGeneratorResultViewModel> resultNavigationService)
        {
            _examSetGeneratorViewModel = examSetGeneratorViewModel;
            _examSetCreator = examSetCreator;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _resultNavigationService = resultNavigationService;

            _examSetGeneratorViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _examSetGeneratorViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            int mazeAmount = int.Parse(_examSetGeneratorViewModel.MazeAmount);
            int beginningMaxDimension = int.Parse(_examSetGeneratorViewModel.BeginningMaxMazeDimension);
            int endingMaxDimension = int.Parse(_examSetGeneratorViewModel.EndingMaxMazeDimension);

            ExamSet examSet = _examSetCreator.Create(mazeAmount, beginningMaxDimension, endingMaxDimension, _currentStaffMemberStore.StaffMember);

            _selectedExamSetDetailsStore.SelectedModel = examSet;
            _resultNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_examSetGeneratorViewModel.CanGenerateSet))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _examSetGeneratorViewModel.CanGenerateSet && base.CanExecute(parameter);
        }
    }
}
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class ShowExamSetDetailsCommand : CommandBase
    {
        private readonly ExamSetCommandViewModel _examSetCommandViewModel;
        private readonly SelectedExamSetDetailsStore _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _examSetDetailsNavigationService;

        public ShowExamSetDetailsCommand(ExamSetCommandViewModel examSetCommandViewModel,
                                         SelectedExamSetDetailsStore selectedExamSetDetailsStore,
                                         NavigationService<ExamSetDetailsViewModel> examSetDetailsNavigationService)
        {
            _examSetCommandViewModel = examSetCommandViewModel;
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _examSetDetailsNavigationService = examSetDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            ExamSet examSet = _examSetCommandViewModel.ExamSet;
            _selectedExamSetDetailsStore.SelectedExamSet = examSet;
            _examSetDetailsNavigationService.Navigate();
        }
    }
}
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class ShowExamSetDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _examSetDetailsNavigationService;

        public ShowExamSetDetailsCommand(CommandViewModel<ExamSet> examSetCommandViewModel,
                                         SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                         NavigationService<ExamSetDetailsViewModel> examSetDetailsNavigationService)
        {
            _examSetCommandViewModel = examSetCommandViewModel;
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _examSetDetailsNavigationService = examSetDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            ExamSet examSet = _examSetCommandViewModel.Model;
            _selectedExamSetDetailsStore.SelectedModel = examSet;
            _examSetDetailsNavigationService.Navigate();
        }
    }
}
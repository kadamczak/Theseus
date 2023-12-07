using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class ShowExamDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<Exam> _examCommandViewModel;
        private readonly SelectedModelDetailsStore<Exam> _selectedExamDetailsStore;
        private readonly NavigationService<ExamDetailsViewModel> _examDetailsNavigationService;

        public ShowExamDetailsCommand(CommandViewModel<Exam> examCommandViewModel,
                                      SelectedModelDetailsStore<Exam> selectedExamDetailsStore,
                                      NavigationService<ExamDetailsViewModel> examDetailsNavigationService)
        {
            _examCommandViewModel = examCommandViewModel;
            _selectedExamDetailsStore = selectedExamDetailsStore;
            _examDetailsNavigationService = examDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Exam exam = _examCommandViewModel.Model;
            _selectedExamDetailsStore.SelectedModel = exam;
            _examDetailsNavigationService.Navigate();
        }
    }
}
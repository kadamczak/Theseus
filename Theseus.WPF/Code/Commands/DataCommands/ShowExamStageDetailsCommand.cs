using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    /// <summary>
    /// The <c>ShowExamStageDetailsCommand</c> class reads the <c>ExamStage</c> object stored in linked <c>CommandViewModel</c> instance,
    /// saves it in <c>SelectedModelDetailsStore</c> and then opens the Exam Stage Details View.
    /// </summary>
    public class ShowExamStageDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<ExamStageWithMazeViewModel> _examStageCommandViewModel;
        private readonly SelectedModelDetailsStore<ExamStageWithMazeViewModel> _selectedExamStageDetailsStore;
        private readonly NavigationService<ExamStageDetailsViewModel> _examStageNavigationService;

        public ShowExamStageDetailsCommand(CommandViewModel<ExamStageWithMazeViewModel> examStageCommandViewModel,
                                           SelectedModelDetailsStore<ExamStageWithMazeViewModel> selectedExamStageDetailsStore,
                                           NavigationService<ExamStageDetailsViewModel> examStageNavigationService)
        {
            _examStageCommandViewModel = examStageCommandViewModel;
            _selectedExamStageDetailsStore = selectedExamStageDetailsStore;
            _examStageNavigationService = examStageNavigationService;
        }

        public override void Execute(object? parameter)
        {
            _selectedExamStageDetailsStore.SelectedModel = _examStageCommandViewModel.Model;
            _examStageNavigationService.Navigate();
        }
    }
}
using System.Collections.ObjectModel;
using Theseus.WPF.Code.Commands.DataCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations
{
    public class ShowDetailsExamStageCommandGranter : CommandGranter<ExamStageWithMazeViewModel>
    {
        private readonly SelectedModelDetailsStore<ExamStageWithMazeViewModel> _selectedExamStageDetailsStore;
        private readonly NavigationService<ExamStageDetailsViewModel> _examStageNavigationService;

        public ShowDetailsExamStageCommandGranter(SelectedModelDetailsStore<ExamStageWithMazeViewModel> selectedExamStageDetailsStore,
                                                  NavigationService<ExamStageDetailsViewModel> examStageNavigationService)
        {
            _selectedExamStageDetailsStore = selectedExamStageDetailsStore;
            _examStageNavigationService = examStageNavigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamStageWithMazeViewModel>> collection,
                                                     CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            return new ButtonViewModel(show: true, "Replay".Resource(), new ShowExamStageDetailsCommand(commandViewModel, _selectedExamStageDetailsStore, _examStageNavigationService));
        }
    }
}
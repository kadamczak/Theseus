using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Commands.DataCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations
{
    public class ShowDetailsExamCommandGranter : CommandGranter<Exam>
    {
        private readonly SelectedModelDetailsStore<Exam> _selectedExamDetailsStore;
        private readonly NavigationService<ExamDetailsViewModel> _examDetailsNavigationService;

        public ShowDetailsExamCommandGranter(SelectedModelDetailsStore<Exam> selectedExamDetailsStore,
                                             NavigationService<ExamDetailsViewModel> examDetailsNavigationService)
        {
            _selectedExamDetailsStore = selectedExamDetailsStore;
            _examDetailsNavigationService = examDetailsNavigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Exam>> collection, CommandViewModel<Exam> commandViewModel)
        {
            return new ButtonViewModel(show: true, "Details", new ShowExamDetailsCommand(commandViewModel, _selectedExamDetailsStore, _examDetailsNavigationService));
        }
    }
}
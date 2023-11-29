using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class ShowDetailsExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _navigationService;

        public ShowDetailsExamSetCommandGranter(SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore, NavigationService<ExamSetDetailsViewModel> navigationService)
        {
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _navigationService = navigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection,
                                                     CommandViewModel<ExamSet> commandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Details",
                                       new ShowExamSetDetailsCommand(commandViewModel, _selectedExamSetDetailsStore, _navigationService));
        }
    }
}
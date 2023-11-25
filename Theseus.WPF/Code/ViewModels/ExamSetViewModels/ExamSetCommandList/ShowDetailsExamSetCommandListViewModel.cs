using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsExamSetCommandListViewModel : ExamSetCommandListViewModel
    {
        private readonly SelectedExamSetDetailsStore _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _navigationService;

        public ShowDetailsExamSetCommandListViewModel(SelectedExamSetListStore selectedExamSetListStore,
                                                      SelectedExamSetDetailsStore selectedExamSetDetailsStore,
                                                      NavigationService<ExamSetDetailsViewModel> navigationService) : base(selectedExamSetListStore)
        {
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _navigationService = navigationService;
        }

        protected override void AddExamSetToActionableExamSets(ExamSet examSet)
        {
            CommandViewModel<ExamSet> examSetCommandViewModel = new CommandViewModel<ExamSet>(examSet)
            {
                Command1Name = "Details",
                ShowCommand1 = true,
                ShowCommand2 = false
            };
            examSetCommandViewModel.Command1 = new ShowExamSetDetailsCommand(examSetCommandViewModel,
                                                                             _selectedExamSetDetailsStore,
                                                                             _navigationService);
            this.ActionableExamSets.Add(examSetCommandViewModel);
        }
    }
}
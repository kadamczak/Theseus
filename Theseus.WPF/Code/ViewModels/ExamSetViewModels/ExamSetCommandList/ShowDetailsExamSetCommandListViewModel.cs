using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsExamSetCommandListViewModel : CommandListViewModel<ExamSet>
    {
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _navigationService;

        public ShowDetailsExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                                      SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                                      NavigationService<ExamSetDetailsViewModel> navigationService) : base(selectedExamSetListStore)
        {
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _navigationService = navigationService;
        }

        protected override void AddModelToActionableModels(ExamSet examSet)
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
            this.ActionableModels.Add(examSetCommandViewModel);
        }
    }
}
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsDeleteExamSetCommandListViewModel : CommandListViewModel<ExamSet>
    {
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _navigationService;
        private readonly IDeleteExamSetCommand _removeExamSetCommand;

        public ShowDetailsDeleteExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedModelListStore,
                                                            SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                                            NavigationService<ExamSetDetailsViewModel> navigationService,
                                                            IDeleteExamSetCommand removeExamSetCommand) : base(selectedModelListStore)
        {
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _navigationService = navigationService;
            _removeExamSetCommand = removeExamSetCommand;
        }

        public override void AddModelToActionableModels(ExamSet model)
        {
            CommandViewModel<ExamSet> examSetCommandViewModel = new CommandViewModel<ExamSet>(model)
            {
                Command1Name = "Details",
                ShowCommand1 = true,
                Command2Name = "Delete",
                ShowCommand2 = true
            };
            examSetCommandViewModel.Command1 = new ShowExamSetDetailsCommand(examSetCommandViewModel,
                                                                             _selectedExamSetDetailsStore,
                                                                             _navigationService);
            examSetCommandViewModel.Command2 = new DeleteExamSetCommand(this, examSetCommandViewModel, _removeExamSetCommand);

            this.ActionableModels.Add(examSetCommandViewModel);
        }
    }
}

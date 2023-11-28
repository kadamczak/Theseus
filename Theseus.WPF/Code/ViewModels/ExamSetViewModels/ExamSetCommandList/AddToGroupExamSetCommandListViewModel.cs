using System;
using System.Linq;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList
{
    public class AddToGroupExamSetCommandListViewModel : CommandListViewModel<ExamSet>
    {
        public ExamSetsInGroupStore ExamSetsInGroupStore { get; }
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _detailsNavigationService;

        public AddToGroupExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                                     ExamSetsInGroupStore examSetsInGroupStore,
                                                     SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                                     NavigationService<ExamSetDetailsViewModel> navigationService) : base(selectedExamSetListStore)
        {
            ExamSetsInGroupStore = examSetsInGroupStore;
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _detailsNavigationService = navigationService;
        }

        public override void AddModelToActionableModels(ExamSet examSet)
        {
            CommandViewModel<ExamSet> examSetCommandViewModel = new CommandViewModel<ExamSet>(examSet)
            {
                ShowCommand1 = true,
                Command1Name = "Details",
                ShowCommand2 = true
            };
            examSetCommandViewModel.Command1 = new ShowExamSetDetailsCommand(examSetCommandViewModel,
                                                                             _selectedExamSetDetailsStore,
                                                                             _detailsNavigationService);
            examSetCommandViewModel.Command2 = new AddExamSetToGroupCommand(examSetCommandViewModel, ExamSetsInGroupStore);

            if (IsSelected(examSet))
            {
                MarkAsSelected(examSetCommandViewModel);
            }
            else
            {
                MarkAsNotSelected(examSetCommandViewModel);
            }

            this.ActionableModels.Add(examSetCommandViewModel);
        }

        private void MarkAsSelected(CommandViewModel<ExamSet> examSetCommandViewModel)
        {
            examSetCommandViewModel.Selected = true;
            examSetCommandViewModel.Command2Name = "Remove";
        }

        private void MarkAsNotSelected(CommandViewModel<ExamSet> examSetCommandViewModel)
        {
            examSetCommandViewModel.Selected = false;
            examSetCommandViewModel.Command2Name = "Add";
        }

        private bool IsSelected(ExamSet examSet) => ExamSetsInGroupStore.SelectedExamSets.Where(e => e.Id == examSet.Id).Any();
    }
}
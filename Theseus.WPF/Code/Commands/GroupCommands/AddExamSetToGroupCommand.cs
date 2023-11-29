using System.Linq;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class AddExamSetToGroupCommand : CommandBase
    {
        private readonly CommandViewModel<ExamSet> _examSetCommandViewModel;
        private ExamSetsInGroupStore _examSetsInGroupStore;

        public AddExamSetToGroupCommand(CommandViewModel<ExamSet> examSetCommandViewModel,
                                        ExamSetsInGroupStore examSetsInGroupStore)
        {
            _examSetCommandViewModel = examSetCommandViewModel;
            _examSetsInGroupStore = examSetsInGroupStore;
        }

        public override void Execute(object? parameter)
        {
            ExamSet examSet = _examSetCommandViewModel.Model;

            if (_examSetCommandViewModel.Selected)
            {
                DeselectExamSet(examSet);
            }
            else
            {
                SelectExamSet(examSet);
            }
        }

        private void DeselectExamSet(ExamSet examSet)
        {
            ExamSet deselectedExamSet = _examSetsInGroupStore.SelectedExamSets.First(e => e.Id == examSet.Id);
            _examSetsInGroupStore.SelectedExamSets.Remove(deselectedExamSet);
            _examSetCommandViewModel.Selected = false;
            //_examSetCommandViewModel.Command2Name = "Add";
        }

        private void SelectExamSet(ExamSet examSet)
        {
            _examSetsInGroupStore.SelectedExamSets.Add(examSet);
            _examSetCommandViewModel.Selected = true;
            //_examSetCommandViewModel.Command2Name = "Remove";
        }
    }
}
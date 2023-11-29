using System.Collections.ObjectModel;
using System.Linq;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class AddToGroupExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly ExamSetsInGroupStore _examSetsInGroupStore;

        public AddToGroupExamSetCommandGranter(ExamSetsInGroupStore examSetsInGroupStore)
        {
            _examSetsInGroupStore = examSetsInGroupStore;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection, CommandViewModel<ExamSet> commandViewModel)
        {
            string buttonText = string.Empty;

            if(IsSelected(commandViewModel.Model))
            {
                commandViewModel.Selected = true;
                buttonText = "Deselect";
            }
            else
            {
                commandViewModel.Selected = false;
                buttonText = "Select";
            }

            return new ButtonViewModel(true, buttonText, new AddExamSetToGroupCommand(commandViewModel, _examSetsInGroupStore));
        }

        private bool IsSelected(ExamSet examSet) => _examSetsInGroupStore.SelectedExamSets.Where(e => e.Id == examSet.Id).Any();
    }
}
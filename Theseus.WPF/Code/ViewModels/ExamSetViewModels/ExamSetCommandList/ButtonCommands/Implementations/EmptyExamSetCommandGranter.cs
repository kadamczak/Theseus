using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class EmptyExamSetCommandGranter : CommandGranter<ExamSet>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection, CommandViewModel<ExamSet> commandViewModel)
        {
            return new ButtonViewModel(false);
        }
    }
}
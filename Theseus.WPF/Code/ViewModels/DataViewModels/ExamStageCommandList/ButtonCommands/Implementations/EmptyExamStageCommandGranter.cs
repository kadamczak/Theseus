using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations
{
    public class EmptyExamStageCommandGranter : CommandGranter<ExamStage>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamStage>> collection, CommandViewModel<ExamStage> commandViewModel)
        {
            return new ButtonViewModel(show: false);
        }
    }
}
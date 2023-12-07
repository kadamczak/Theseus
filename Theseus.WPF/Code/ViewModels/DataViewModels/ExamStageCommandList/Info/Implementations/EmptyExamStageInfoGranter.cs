using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations
{
    public class EmptyExamStageInfoGranter : InfoGranter<ExamStage>
    {
        public override string GrantInfo(CommandViewModel<ExamStage> commandViewModel)
        {
            return string.Empty;
        }
    }
}
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations
{
    public class EmptyExamSetInfoGranter : InfoGranter<ExamSet>
    {
        public override string GrantInfo(CommandViewModel<ExamSet> commandViewModel)
        {
            return string.Empty;
        }
    }
}
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class EmptyExamInfoGranter : InfoGranter<Exam>
    {
        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            return string.Empty;
        }
    }
}
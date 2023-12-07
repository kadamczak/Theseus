using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations
{
    public class EmptyExamStageInfoGranter : InfoGranter<ExamStageWithMazeViewModel>
    {
        public override string GrantInfo(CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            return string.Empty;
        }
    }
}
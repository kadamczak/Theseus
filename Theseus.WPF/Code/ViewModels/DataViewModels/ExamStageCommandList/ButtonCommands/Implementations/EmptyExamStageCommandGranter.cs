using System.Collections.ObjectModel;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations
{
    public class EmptyExamStageCommandGranter : CommandGranter<ExamStageWithMazeViewModel>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamStageWithMazeViewModel>> collection, CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            return new ButtonViewModel(show: false);
        }
    }
}
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList
{
    public class ExamStageCommandListViewModel : CommandListViewModel<ExamStageWithMazeViewModel, ExamStageButtonCommand, ExamStageInfo>
    {
        public ExamStageCommandListViewModel(SelectedModelListStore<ExamStageWithMazeViewModel> selectedModelListStore,
                                             CommandGranterFactory<ExamStageWithMazeViewModel, ExamStageButtonCommand> commandGranterFactory,
                                             InfoGranterFactory<ExamStageWithMazeViewModel, ExamStageInfo> infoGranterFactory,
                                             ExamStageButtonCommand command1,
                                             ExamStageButtonCommand command2,
                                             ExamStageInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}
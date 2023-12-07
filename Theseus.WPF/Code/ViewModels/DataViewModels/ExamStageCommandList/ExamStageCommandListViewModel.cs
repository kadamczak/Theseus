using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList
{
    public class ExamStageCommandListViewModel : CommandListViewModel<ExamStage, ExamStageButtonCommand, ExamStageInfo>
    {
        public ExamStageCommandListViewModel(SelectedModelListStore<ExamStage> selectedModelListStore,
                                             CommandGranterFactory<ExamStage, ExamStageButtonCommand> commandGranterFactory,
                                             InfoGranterFactory<ExamStage, ExamStageInfo> infoGranterFactory,
                                             ExamStageButtonCommand command1,
                                             ExamStageButtonCommand command2,
                                             ExamStageInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}
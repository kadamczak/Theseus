using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetCommandListViewModel : CommandListViewModel<ExamSet, ExamSetButtonCommand, ExamSetInfo>
    {
        public ExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedModelListStore,
                                           CommandGranterFactory<ExamSet, ExamSetButtonCommand> commandGranterFactory,
                                           InfoGranterFactory<ExamSet, ExamSetInfo> infoGranterFactory,
                                           ExamSetButtonCommand command1,
                                           ExamSetButtonCommand command2,
                                           ExamSetInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}
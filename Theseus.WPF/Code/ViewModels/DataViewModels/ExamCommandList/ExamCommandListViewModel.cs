using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList
{
    public class ExamCommandListViewModel : CommandListViewModel<Exam, ExamButtonCommand, ExamInfo>
    {
        public ExamCommandListViewModel(SelectedModelListStore<Exam> selectedModelListStore,
                                        CommandGranterFactory<Exam, ExamButtonCommand> commandGranterFactory,
                                        InfoGranterFactory<Exam, ExamInfo> infoGranterFactory,
                                        ExamButtonCommand command1,
                                        ExamButtonCommand command2,
                                        ExamInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}
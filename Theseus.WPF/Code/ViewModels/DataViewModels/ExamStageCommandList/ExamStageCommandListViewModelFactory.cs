using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList
{
    public class ExamStageCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<ExamStageWithMazeViewModel> _selectedListStore;
        private readonly ExamStageCommandGranterFactory _commandGranterFactory;
        private readonly ExamStageInfoGranterFactory _infoGranterFactory;

        public ExamStageCommandListViewModelFactory(SelectedModelListStore<ExamStageWithMazeViewModel> selectedListStore,
                                                    ExamStageCommandGranterFactory commandGranterFactory,
                                                    ExamStageInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public ExamStageCommandListViewModel Create(ExamStageButtonCommand command1Type, ExamStageButtonCommand command2Type, ExamStageInfo infoType)
        {
            return new ExamStageCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}
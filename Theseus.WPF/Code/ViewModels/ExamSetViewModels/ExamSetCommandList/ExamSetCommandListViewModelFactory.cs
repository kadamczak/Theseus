using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList
{
    public class ExamSetCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<ExamSet> _selectedListStore;
        private readonly ExamSetCommandGranterFactory _commandGranterFactory;
        private readonly ExamSetInfoGranterFactory _infoGranterFactory;

        public ExamSetCommandListViewModelFactory(SelectedModelListStore<ExamSet> selectedListStore,
                                                  ExamSetCommandGranterFactory commandGranterFactory,
                                                  ExamSetInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public ExamSetCommandListViewModel Create(ExamSetButtonCommand command1Type, ExamSetButtonCommand command2Type, ExamSetInfo infoType)
        {
            return new ExamSetCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}
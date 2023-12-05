using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList
{
    public class ExamCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<Exam> _selectedListStore;
        private readonly ExamCommandGranterFactory _commandGranterFactory;
        private readonly ExamInfoGranterFactory _infoGranterFactory;

        public ExamCommandListViewModelFactory(SelectedModelListStore<Exam> selectedListStore,
                                               ExamCommandGranterFactory commandGranterFactory,
                                               ExamInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public ExamCommandListViewModel Create(ExamButtonCommand command1Type, ExamButtonCommand command2Type, ExamInfo infoType)
        {
            return new ExamCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}
//using Theseus.Domain.Models.ExamSetRelated;
//using Theseus.Infrastructure.Commands.ExamSetCommands;
//using Theseus.WPF.Code.Stores;
//using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
//using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
//using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

//namespace Theseus.WPF.Code.ViewModels.GroupViewModels
//{
//    public class GroupCommandListViewModelFactory
//    {
//        private readonly SelectedModelListStore<ExamSet> _selectedListStore;
//        private readonly CommandGranterFactory<ExamSet, ExamSetButtonCommand> _commandGranterFactory;
//        private readonly InfoGranterFactory<ExamSet, ExamSetInfo> _infoGranterFactory;

//        public GroupCommandListViewModelFactory(SelectedModelListStore<ExamSet> selectedListStore,
//                                                CommandGranterFactory<ExamSet, ExamSetButtonCommand> commandGranterFactory,
//                                                InfoGranterFactory<ExamSet, ExamSetInfo> infoGranterFactory)
//        {
//            _selectedListStore = selectedListStore;
//            _commandGranterFactory = commandGranterFactory;
//            _infoGranterFactory = infoGranterFactory;
//        }

//        public ExamSetCommandListViewModel Create(ExamSetButtonCommand command1Type, ExamSetButtonCommand command2Type, ExamSetInfo infoType)
//        {
//            return new ExamSetCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
//        }
//    }
//}
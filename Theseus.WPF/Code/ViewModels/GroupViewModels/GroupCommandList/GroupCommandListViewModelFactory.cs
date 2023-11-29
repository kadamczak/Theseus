using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList
{
    public class GroupCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<Group> _selectedListStore;
        private readonly GroupCommandGranterFactory _commandGranterFactory;
        private readonly GroupInfoGranterFactory _infoGranterFactory;

        public GroupCommandListViewModelFactory(SelectedModelListStore<Group> selectedListStore,
                                                GroupCommandGranterFactory commandGranterFactory,
                                                GroupInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public GroupCommandListViewModel Create(GroupButtonCommand command1Type, GroupButtonCommand command2Type, GroupInfo infoType)
        {
            return new GroupCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}
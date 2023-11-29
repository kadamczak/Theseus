using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList
{
    public class StaffMemberCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<StaffMember> _selectedListStore;
        private readonly StaffMemberCommandGranterFactory _commandGranterFactory;
        private readonly StaffMemberInfoGranterFactory _infoGranterFactory;

        public StaffMemberCommandListViewModelFactory(SelectedModelListStore<StaffMember> selectedListStore,
                                                      StaffMemberCommandGranterFactory commandGranterFactory,
                                                      StaffMemberInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public StaffMemberCommandListViewModel Create(StaffMemberButtonCommand command1Type, StaffMemberButtonCommand command2Type, StaffMemberInfo infoType)
        {
            return new StaffMemberCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}
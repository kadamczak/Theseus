using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberCommandListViewModel : CommandListViewModel<StaffMember, StaffMemberButtonCommand, StaffMemberInfo>
    {
        public StaffMemberCommandListViewModel(SelectedModelListStore<StaffMember> selectedModelListStore,
                                               CommandGranterFactory<StaffMember, StaffMemberButtonCommand> commandGranterFactory,
                                               InfoGranterFactory<StaffMember,
                                               StaffMemberInfo> infoGranterFactory,
                                               StaffMemberButtonCommand command1,
                                               StaffMemberButtonCommand command2,
                                               StaffMemberInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}
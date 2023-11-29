using System.Collections.ObjectModel;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations
{
    public class EmptyStaffMemberCommandGranter : CommandGranter<StaffMember>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<StaffMember>> collection, CommandViewModel<StaffMember> commandViewModel)
        {
            return new ButtonViewModel(show: false);
        }
    }
}
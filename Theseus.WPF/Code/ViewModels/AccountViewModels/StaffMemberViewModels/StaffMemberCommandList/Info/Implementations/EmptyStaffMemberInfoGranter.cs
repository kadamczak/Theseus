using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info.Implementations
{
    public class EmptyStaffMemberInfoGranter : InfoGranter<StaffMember>
    {
        public override string GrantInfo(CommandViewModel<StaffMember> commandViewModel)
        {
            return string.Empty;
        }
    }
}
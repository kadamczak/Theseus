using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.ViewModels.Bindings.AccountBindings
{
    public class StaffMemberCommandViewModel : CommandViewModel
    {
        public StaffMember StaffMember { get; }

        public StaffMemberCommandViewModel(StaffMember staffMember)
        {
            StaffMember = staffMember;
        }
    }
}
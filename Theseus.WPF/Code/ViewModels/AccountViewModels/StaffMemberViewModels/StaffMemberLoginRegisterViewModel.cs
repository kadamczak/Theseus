using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberLoginRegisterViewModel : ViewModelBase
    {
        public StaffMemberLoginViewModel StaffMemberLoginViewModel { get; }
        public StaffMemberRegisterViewModel StaffMemberRegisterViewModel { get; }

        public StaffMemberLoginRegisterViewModel(StaffMemberLoginViewModel staffMemberLoginViewModel, StaffMemberRegisterViewModel staffMemberRegisterViewModel)
        {
            StaffMemberLoginViewModel = staffMemberLoginViewModel;
            StaffMemberRegisterViewModel = staffMemberRegisterViewModel;
        }
    }
}
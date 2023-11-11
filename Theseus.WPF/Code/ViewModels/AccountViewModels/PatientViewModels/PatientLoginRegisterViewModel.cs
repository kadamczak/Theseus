using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientLoginRegisterViewModel : ViewModelBase
    {
        public StaffMemberLoginViewModel StaffMemberLoginViewModel { get; }
        public StaffMemberRegisterViewModel StaffMemberRegisterViewModel { get; }

        public PatientLoginRegisterViewModel(StaffMemberLoginViewModel staffMemberLoginViewModel,
                                             StaffMemberRegisterViewModel staffMemberRegisterViewModel)
        {
            StaffMemberLoginViewModel = staffMemberLoginViewModel;
            StaffMemberRegisterViewModel = staffMemberRegisterViewModel;
        }
    }
}
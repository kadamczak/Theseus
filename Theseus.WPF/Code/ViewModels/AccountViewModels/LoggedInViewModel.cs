using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class LoggedInViewModel : ViewModelBase
    {
        public PatientLoggedInViewModel PatientLoggedInViewModel { get; }
        public StaffMemberLoggedInViewModel StaffMemberLoggedInViewModel { get; }

        public LoggedInViewModel(PatientLoggedInViewModel patientLoggedInViewModel,
                                 StaffMemberLoggedInViewModel staffMemberLoggedInViewModel)
        {
            PatientLoggedInViewModel = patientLoggedInViewModel;
            StaffMemberLoggedInViewModel = staffMemberLoggedInViewModel;
        }
    }
}
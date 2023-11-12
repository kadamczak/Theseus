using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;

namespace Theseus.WPF.Code.ViewModels
{
    public class LoggedInViewModel : ViewModelBase
    {
        public AccountDetailsViewModel PatientDetailsViewModel { get; }
        public AccountDetailsViewModel StaffMemberDetailsViewModel { get; }     

        public LoggedInViewModel(PatientDetailsLoggedInViewModel patientDetailsLoggedInViewModel,
                                 PatientDetailsNotLoggedInViewModel patientDetailsNotLoggedInViewModel,
                                 StaffMemberDetailsLoggedInViewModel staffMemberDetailsLoggedInViewModel,
                                 StaffMemberDetailsNotLoggedInViewModel staffMemberDetailsNotLoggedInViewModel,
                                 ICurrentStaffMemberStore currentStaffMemberStore,
                                 ICurrentPatientStore currentPatientStore)
        {
            PatientDetailsViewModel = (currentPatientStore.IsPatientLoggedIn) ? patientDetailsNotLoggedInViewModel
                                                                              : patientDetailsLoggedInViewModel;

            StaffMemberDetailsViewModel = (currentStaffMemberStore.IsStaffMemberLoggedIn) ? staffMemberDetailsNotLoggedInViewModel
                                                                                          : staffMemberDetailsLoggedInViewModel;
        }
    }
}
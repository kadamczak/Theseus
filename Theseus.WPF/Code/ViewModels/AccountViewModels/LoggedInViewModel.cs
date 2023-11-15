using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class LoggedInViewModel : ViewModelBase
    {
        public ViewModelBase PatientDetailsViewModel { get; }
        public ViewModelBase StaffMemberDetailsViewModel { get; }     

        public LoggedInViewModel(PatientDetailsLoggedInViewModel patientDetailsLoggedInViewModel,
                                 PatientDetailsNotLoggedInViewModel patientDetailsNotLoggedInViewModel,
                                 StaffMemberDetailsLoggedInViewModel staffMemberDetailsLoggedInViewModel,
                                 StaffMemberDetailsNotLoggedInViewModel staffMemberDetailsNotLoggedInViewModel,
                                 ICurrentStaffMemberStore currentStaffMemberStore,
                                 ICurrentPatientStore currentPatientStore)
        {
            PatientDetailsViewModel = (currentPatientStore.IsPatientLoggedIn) ? patientDetailsLoggedInViewModel
                                                                              : patientDetailsNotLoggedInViewModel;

            StaffMemberDetailsViewModel = (currentStaffMemberStore.IsStaffMemberLoggedIn) ? staffMemberDetailsLoggedInViewModel
                                                                                          : staffMemberDetailsNotLoggedInViewModel;
        }
    }
}
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;

namespace Theseus.WPF.Code.ViewModels
{
    public class LoggedInViewModel : ViewModelBase
    {
        public AccountDetailsViewModel PatientDetailsViewModel { get; }
        public AccountDetailsViewModel StaffMemberDetailsViewModel { get; }
        
        public ICurrentUserStore CurrentUserStore { get; }

        public LoggedInViewModel(PatientDetailsLoggedInViewModel patientDetailsLoggedInViewModel,
                                 PatientDetailsNotLoggedInViewModel patientDetailsNotLoggedInViewModel,
                                 StaffMemberDetailsLoggedInViewModel staffMemberDetailsLoggedInViewModel,
                                 StaffMemberDetailsNotLoggedInViewModel staffMemberDetailsNotLoggedInViewModel,
                                 ICurrentUserStore currentUserStore)
        {
            PatientDetailsViewModel = (currentUserStore.CurrentPatient is null) ? patientDetailsNotLoggedInViewModel
                                                                                  : patientDetailsLoggedInViewModel;

            StaffMemberDetailsViewModel = (currentUserStore.CurrentStaffMember is null) ? staffMemberDetailsNotLoggedInViewModel
                                                                                        : staffMemberDetailsLoggedInViewModel;
        }
    }
}
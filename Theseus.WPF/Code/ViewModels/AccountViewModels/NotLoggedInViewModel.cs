using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>NotLoggedInViewModel</c> class contains bindings for Not Logged In View.
    /// </summary>
    public class NotLoggedInViewModel : ViewModelBase
    {
        public PatientLoginViewModel PatientLoginViewModel { get; }
        public PatientRegisterViewModel PatientRegisterViewModel { get; }
        public ICommand NavigateToStaffMemberLoginRegister { get; }

        public NotLoggedInViewModel(PatientLoginViewModel patientLoginViewModel,
                                    PatientRegisterViewModel patientRegisterViewModel,
                                    NavigationService<StaffMemberLoginRegisterViewModel> navigateToStaffMemberLoginRegister)
        {
            PatientLoginViewModel = patientLoginViewModel;
            PatientRegisterViewModel = patientRegisterViewModel;
            NavigateToStaffMemberLoginRegister = new NavigateCommand<StaffMemberLoginRegisterViewModel>(navigateToStaffMemberLoginRegister);
        }
    }
}
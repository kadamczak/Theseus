using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientLoginRegisterViewModel : ViewModelBase
    {
        public PatientLoginViewModel PatientLoginViewModel { get; }
        public PatientRegisterViewModel PatientRegisterViewModel { get; }

        public ICommand GoBack { get; }

        public PatientLoginRegisterViewModel(PatientLoginViewModel patientLoginViewModel,
                                             PatientRegisterViewModel patientRegisterViewModel,
                                             NavigationService<LoggedInViewModel> loggedInNavigationService,
                                             NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                             ICurrentPatientStore currentPatientStore,
                                             ICurrentStaffMemberStore currentStaffMemberStore)
        {
            PatientLoginViewModel = patientLoginViewModel;
            PatientRegisterViewModel = patientRegisterViewModel;

            this.GoBack = new OpenAccountViewModelCommand(loggedInNavigationService,
                                                          notLoggedInNavigationService,
                                                          currentPatientStore,
                                                          currentStaffMemberStore);
        }
    }
}
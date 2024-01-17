using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>PatientDetailsNotLoggedInViewModel</c> class contains bindings for Patient Details Not Logged In View.
    /// </summary>
    public class PatientDetailsNotLoggedInViewModel : ViewModelBase
    {
        public ICommand NavigateToPatientLoginRegister { get; }

        public PatientDetailsNotLoggedInViewModel(NavigationService<PatientLoginRegisterViewModel> patientLoginRegisterNavigationService)
        {
            NavigateToPatientLoginRegister = new NavigateCommand<PatientLoginRegisterViewModel>(patientLoginRegisterNavigationService);
        }
    }
}
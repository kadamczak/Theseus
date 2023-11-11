using System.Windows.Input;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientDetailsNotLoggedInViewModel : AccountDetailsViewModel
    {
        public ICommand NavigateToPatientLoginRegister { get; }

        public PatientDetailsNotLoggedInViewModel(NavigationService<PatientLoginRegisterViewModel> patientLoginRegisterNavigationService)
        {
            NavigateToPatientLoginRegister = new NavigateCommand<PatientLoginRegisterViewModel>(patientLoginRegisterNavigationService);
        }
    }
}
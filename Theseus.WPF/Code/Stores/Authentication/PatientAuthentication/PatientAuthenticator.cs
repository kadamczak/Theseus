using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication;
using Theseus.Domain.Services.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    public class PatientAuthenticator : IPatientAuthenticator
    {
        private readonly IPatientAuthenticationService _patientAuthenticationService;
        private readonly ICurrentPatientStore _currentPatientStore;

        public PatientAuthenticator(IPatientAuthenticationService patientAuthenticationService,
                                    ICurrentPatientStore currentPatientStore)
        {
            _patientAuthenticationService = patientAuthenticationService;
            _currentPatientStore = currentPatientStore;
        }

        public Patient? CurrentPatient
        {
            get => _currentPatientStore.Patient;
            set
            {
                this._currentPatientStore.Patient = value;
                PatientStateChanged?.Invoke();
            }
        }

        public bool IsLoggedInAsPatient => _currentPatientStore.IsPatientLoggedIn;

        public event Action PatientStateChanged;

        public async Task Login(string username)
        {
            CurrentPatient = await _patientAuthenticationService.Login(username);
        }

        public void Logout()
        {
            CurrentPatient = null;
        }

        public async Task<RegistrationResult> Register(Patient newPatient, string staffMemberUsername)
        {
            return await _patientAuthenticationService.Register(newPatient, staffMemberUsername);
        }
    }
}

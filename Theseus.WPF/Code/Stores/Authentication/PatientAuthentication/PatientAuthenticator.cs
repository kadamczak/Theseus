using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>PatientAuthenticator</c> class defines login/register logic for <c>Patient</c> accounts.
    /// It manipulates the <c>ICurrentPatientStore</c> according to <c>IPatientAuthenticationService</c> response.
    /// </summary>
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

        public async Task Login(string username, string groupName)
        {
            CurrentPatient = await _patientAuthenticationService.Login(username, groupName);
        }

        public void Logout()
        {
            CurrentPatient = null;
        }

        public async Task<PatientRegistrationResult> Register(Patient newPatient, string groupName)
        {
            return await _patientAuthenticationService.Register(newPatient, groupName);
        }
    }
}

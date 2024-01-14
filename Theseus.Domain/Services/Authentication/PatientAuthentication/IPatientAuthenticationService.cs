using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>IPatientAuthenticationService</c> interface defines <c>Patient</c> login and registration.
    /// </summary>
    public interface IPatientAuthenticationService
    {
        Task<PatientRegistrationResult> Register(Patient newAccount, string groupName);
        Task<Patient> Login(string username, string groupName);
    }
}
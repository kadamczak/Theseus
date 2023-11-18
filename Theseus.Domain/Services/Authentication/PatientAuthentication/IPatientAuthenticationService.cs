using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    public interface IPatientAuthenticationService
    {
        Task<PatientRegistrationResult> Register(Patient newAccount, string groupName);
        Task<Patient> Login(string username, string groupName);
    }
}

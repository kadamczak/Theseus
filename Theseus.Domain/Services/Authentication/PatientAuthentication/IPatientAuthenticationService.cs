using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    public interface IPatientAuthenticationService
    {
        Task<RegistrationResult> Register(Patient newAccount, string staffMemberUsername);
        Task<Patient> Login(string username);
    }
}

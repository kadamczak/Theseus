using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    public interface IPatientAuthenticator
    {
        Patient? CurrentPatient { get; }
        bool IsLoggedInAsPatient { get; }
        event Action PatientStateChanged;

        Task<PatientRegistrationResult> Register(Patient newPatient, string staffMemberUsername);
        Task Login(string username);
        void Logout();
    }
}
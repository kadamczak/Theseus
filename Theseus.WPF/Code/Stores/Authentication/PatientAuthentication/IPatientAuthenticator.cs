using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>IPatientAuthenticator</c> interface defines login/register methods for <c>Patient</c>.
    /// </summary>
    public interface IPatientAuthenticator
    {
        Patient? CurrentPatient { get; }
        bool IsLoggedInAsPatient { get; }
        event Action PatientStateChanged;

        Task<PatientRegistrationResult> Register(Patient newPatient, string groupName);
        Task Login(string username, string groupName);
        void Logout();
    }
}
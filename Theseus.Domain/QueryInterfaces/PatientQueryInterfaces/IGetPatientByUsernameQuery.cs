using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.PatientQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Patient</c> with the specified username.
    /// </summary>
    public interface IGetPatientByUsernameQuery
    {
        Task<Patient?> GetPatient(string username);
    }
}
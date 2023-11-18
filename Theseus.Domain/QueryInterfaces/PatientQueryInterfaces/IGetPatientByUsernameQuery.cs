using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.PatientQueryInterfaces
{
    public interface IGetPatientByUsernameQuery
    {
        Task<Patient?> GetPatient(string username, bool loadGroup = false);
    }
}
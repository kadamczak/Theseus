using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.PatientQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Patient</c>s belonging to the specified <c>Group</c>.
    /// Related entities are included.
    /// </summary>
    public interface IGetPatientsOfGroupWithFullIncludeQuery
    {
        IEnumerable<Patient> GetPatients(Guid groupId);
    }
}
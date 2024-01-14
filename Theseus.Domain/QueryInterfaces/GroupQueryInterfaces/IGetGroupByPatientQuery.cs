using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Group</c> that has a specified <c>Patient</c>.
    /// </summary>
    public interface IGetGroupByPatientQuery
    {
        Group? GetGroup(Guid patientId);
    }
}

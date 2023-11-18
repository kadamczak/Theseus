using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    public interface IGetGroupByPatientQuery
    {
        Task<Group?> GetGroup(Guid patientId);
    }
}

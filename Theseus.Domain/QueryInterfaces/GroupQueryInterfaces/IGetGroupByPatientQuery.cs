using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    public interface IGetGroupByPatientQuery
    {
        Group? GetGroup(Guid patientId);
    }
}

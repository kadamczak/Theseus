using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.PatientQueryInterfaces
{
    public interface IGetPatientsOfGroupQuery
    {
        IEnumerable<Patient> GetPatients(Guid groupId);
    }
}
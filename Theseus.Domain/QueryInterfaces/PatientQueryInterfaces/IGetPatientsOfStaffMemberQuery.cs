using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.PatientQueryInterfaces
{
    public interface IGetPatientsOfStaffMemberQuery
    {
        IEnumerable<Patient> GetPatients(Guid staffMemberId);
    }
}
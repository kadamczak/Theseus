using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetStaffMemberByUsernameQuery
    {
        Task<StaffMember?> GetStaffMember(string username, bool loadExamSets = false, bool loadPatients = false, bool loadMazes = false);
    }
}
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetStaffMemberByUsernameQuery
    {
        Task<StaffMember?> GetStaffMember(string username);
    }
}
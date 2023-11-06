using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetStaffMemberByUsernameQuery
    {
        Task<StaffMember?> GetStaffMember(string username);
    }
}
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetStaffMemberByEmailQuery
    {
        Task<StaffMember?> GetStaffMember(string email);
    }
}

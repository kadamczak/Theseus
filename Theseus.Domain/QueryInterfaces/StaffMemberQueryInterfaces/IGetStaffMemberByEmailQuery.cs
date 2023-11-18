using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetStaffMemberByEmailQuery
    {
        Task<StaffMember?> GetStaffMember(string email);
    }
}

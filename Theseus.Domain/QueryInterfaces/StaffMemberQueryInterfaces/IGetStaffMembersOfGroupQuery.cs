using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetStaffMembersOfGroupQuery
    {
        IEnumerable<StaffMember> GetStaffMembers(Guid groupId);
    }
}
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>StaffMember</c>s that are included in the specified <c>Group</c>.
    /// </summary>
    public interface IGetStaffMembersOfGroupQuery
    {
        IEnumerable<StaffMember> GetStaffMembers(Guid groupId);
    }
}
using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Group</c>s of a specified <c>StaffMember</c>.
    /// </summary>
    public interface IGetGroupsOfStaffMemberQuery
    {
        IEnumerable<Group> GetGroups(Guid staffMemberId);
    }
}
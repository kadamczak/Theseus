using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    public interface IGetGroupsOfStaffMemberQuery
    {
        IEnumerable<Group> GetGroups(Guid staffMemberId);
    }
}
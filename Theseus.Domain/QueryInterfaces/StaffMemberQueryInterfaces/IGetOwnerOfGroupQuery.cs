using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>StaffMember</c> that is the owner of a <c>Group</c>.
    /// </summary>
    public interface IGetOwnerOfGroupQuery
    {
        StaffMember GetOwner(Guid groupId);
        Task<StaffMember> GetOwnerAsync(Guid groupId);
    }
}
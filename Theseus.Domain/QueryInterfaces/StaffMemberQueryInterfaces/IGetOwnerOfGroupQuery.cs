using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    public interface IGetOwnerOfGroupQuery
    {
        StaffMember GetOwner(Guid groupId);
        Task<StaffMember> GetOwnerAsync(Guid groupId);
    }
}
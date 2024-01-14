using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>StaffMember</c> with the specified username.
    /// </summary>
    public interface IGetStaffMemberByUsernameQuery
    {
        Task<StaffMember?> GetStaffMember(string username);
    }
}
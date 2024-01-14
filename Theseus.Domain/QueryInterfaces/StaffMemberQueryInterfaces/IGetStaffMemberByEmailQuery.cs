using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>StaffMember</c> with the specified email.
    /// </summary>
    public interface IGetStaffMemberByEmailQuery
    {
        Task<StaffMember?> GetStaffMember(string email);
    }
}

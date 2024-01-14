using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    /// <summary>
    /// Interface defining method of <c>StaffMember</c> data update.
    /// </summary>
    public interface IUpdateStaffMemberCommand
    {
        Task Update(StaffMember staffMember);
    }
}

using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    /// <summary>
    /// Interface defining method of <c>StaffMember</c> creation.
    /// </summary>
    public interface ICreateStaffMemberCommand
    {
        Task Create(StaffMember staffMember);
    }
}

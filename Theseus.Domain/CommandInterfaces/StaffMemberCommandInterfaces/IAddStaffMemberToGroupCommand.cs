using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>StaffMember</c> addition to <c>Group</c>.
    /// </summary>
    public interface IAddStaffMemberToGroupCommand
    {
        Task AddToGroup(StaffMember staffMember, Guid groupId);
    }
}
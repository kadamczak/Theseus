using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    /// <summary>
    /// Interface defining method of <c>StaffMember</c> removal from <c>Group</c>.
    /// </summary>
    public interface IRemoveStaffMemberFromGroupCommand
    {
        Task RemoveFromGroup(StaffMember staffMember, Guid groupId);
    }
}
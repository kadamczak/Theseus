using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    public interface IRemoveStaffMemberFromGroupCommand
    {
        Task RemoveFromGroup(StaffMember staffMember, Guid groupId);
    }
}
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    public interface IAddStaffMemberToGroupCommand
    {
        Task AddToGroup(StaffMember staffMember, Guid groupId);
    }
}
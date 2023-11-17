using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    public interface IUpdateStaffMemberCommand
    {
        Task Update(StaffMember staffMember);
    }
}

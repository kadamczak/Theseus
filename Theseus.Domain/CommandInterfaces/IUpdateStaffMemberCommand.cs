using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces
{
    public interface IUpdateStaffMemberCommand
    {
        Task Update(StaffMember staffMember);
    }
}

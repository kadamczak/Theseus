using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateStaffMemberCommand
    {
        Task Create(StaffMember staffMember);
    }
}

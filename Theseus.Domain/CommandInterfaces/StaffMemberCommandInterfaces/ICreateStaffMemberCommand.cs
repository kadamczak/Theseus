using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces
{
    public interface ICreateStaffMemberCommand
    {
        Task Create(StaffMember staffMember);
    }
}

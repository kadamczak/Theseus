using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.CommandInterfaces.GroupCommandInterfaces
{
    public interface ICreateGroupCommand
    {
        Task CreateGroup(Group group);
    }
}

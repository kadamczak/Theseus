using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.CommandInterfaces.GroupCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Group</c> creation method.
    /// </summary>
    public interface ICreateGroupCommand
    {
        Task CreateGroup(Group group);
    }
}

using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Group</c>s with a specified name.
    /// </summary>
    public interface IGetGroupByNameQuery
    {
        Task<Group?> GetGroup(string name);
    }
}
using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Domain.QueryInterfaces.GroupQueryInterfaces
{
    public interface IGetGroupByNameQuery
    {
        Task<Group?> GetGroup(string name, bool loadStaffMembers = false, bool loadPatients = false, bool loadExamSets = false);
    }
}
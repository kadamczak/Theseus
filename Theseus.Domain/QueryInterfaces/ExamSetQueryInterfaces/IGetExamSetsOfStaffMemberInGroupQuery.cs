using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamSet</c>s belonging to a specified <c>StaffMember</c> that are included in specified <c>Group</c>.
    /// </summary>
    public interface IGetExamSetsOfStaffMemberInGroupQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid staffMemberId, Guid groupId);
    }
}
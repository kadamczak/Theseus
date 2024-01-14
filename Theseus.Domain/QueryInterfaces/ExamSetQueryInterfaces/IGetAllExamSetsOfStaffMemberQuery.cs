using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamSet</c>s belonging to a specified <c>StaffMember</c>.
    /// </summary>
    public interface IGetAllExamSetsOfStaffMemberQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid staffMemberId);
    }
}
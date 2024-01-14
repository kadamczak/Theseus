using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Exam</c>s belonging to a specified <c>StaffMember</c>.
    /// Related entities are included.
    /// </summary>
    public interface IGetExamsOfStaffMemberWithFullIncludeQuery
    {
        IEnumerable<Exam> GetExams(Guid staffMemberId);
    }
}
using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamsOfStaffMemberQuery
    {
        IEnumerable<Exam> GetExams(Guid staffMemberId);
    }
}
using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Exam</c>s belonging to a specified <c>Group</c> and using a specified <c>ExamSet</c>.
    /// Related entities are included.
    /// </summary>
    public interface IGetExamsOfGroupOfExamSetWithFullIncludeQuery
    {
        IEnumerable<Exam> GetExams(Guid groupId, Guid examSetId);
    }
}
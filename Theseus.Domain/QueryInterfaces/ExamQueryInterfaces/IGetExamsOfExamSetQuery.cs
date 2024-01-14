using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Exam</c>s operating on a specified <c>ExamSet</c>.
    /// </summary>
    public interface IGetExamsOfExamSetQuery
    {
        IEnumerable<Exam> GetExams(Guid examSetId);
    }
}
using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamsOfExamSetQuery
    {
        IEnumerable<Exam> GetExams(Guid examSetId);
    }
}
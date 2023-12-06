using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamsOfGroupOfExamSetQuery
    {
        IEnumerable<Exam> GetExams(Guid groupId, Guid examSetId);
    }
}
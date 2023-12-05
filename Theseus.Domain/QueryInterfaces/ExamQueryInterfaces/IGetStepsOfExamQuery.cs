using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetStepsOfExamQuery
    {
        IEnumerable<ExamStep> GetSteps(Guid examId);
    }
}
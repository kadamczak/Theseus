using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamStage</c>s belonging to a specified <c>Exam</c>.
    /// </summary>
    public interface IGetStagesOfExamQuery
    {
        IEnumerable<ExamStage> GetStages(Guid examId);
    }
}
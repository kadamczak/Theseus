using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamStep</c>s belonging to a specified <c>ExamStage</c>.
    /// </summary>
    public interface IGetStepsOfExamQuery
    {
        IEnumerable<ExamStep> GetSteps(Guid examId);
    }
}
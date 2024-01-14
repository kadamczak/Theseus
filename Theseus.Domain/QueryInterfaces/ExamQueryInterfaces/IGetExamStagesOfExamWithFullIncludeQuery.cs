using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamStage</c>s belonging to a specified <c>Exam</c>,
    /// Related entities are included.
    /// </summary>
    public interface IGetExamStagesOfExamWithFullIncludeQuery
    {
        IEnumerable<ExamStage> GetExamStages(Guid examId);
    }
}
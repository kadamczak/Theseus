using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamStage</c>s belonging to a specified <c>Exam</c> and of a particular stage index.
    /// Related entities are included.
    /// </summary>
    public interface IGetExamStagesOfExamSetOfIndexWithFullIncludeQuery
    {
        IEnumerable<ExamStage> GetExamStages(Guid examSetId, int index, Guid groupId);
    }
}
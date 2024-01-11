using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamStagesOfExamSetOfIndexQuery
    {
        IEnumerable<ExamStage> GetExamStages(Guid examSetId, int index, Guid groupId);
    }
}
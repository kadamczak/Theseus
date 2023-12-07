using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamStagesOfExamQuery
    {
        IEnumerable<ExamStage> GetExamStages(Guid examId);
    }
}
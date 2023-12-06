using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetStagesOfExamQuery
    {
        IEnumerable<ExamStage> GetStages(Guid examId);
    }
}
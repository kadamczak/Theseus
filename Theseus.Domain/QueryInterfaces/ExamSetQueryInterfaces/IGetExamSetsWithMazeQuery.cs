using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    public interface IGetExamSetsWithMazeQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid mazeId);
    }
}

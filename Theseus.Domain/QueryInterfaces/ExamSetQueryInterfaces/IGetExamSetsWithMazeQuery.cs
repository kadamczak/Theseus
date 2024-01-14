using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamSet</c>s containing a specified <c>MazeWithSolution</c>.
    /// </summary>
    public interface IGetExamSetsWithMazeQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid mazeId);
    }
}

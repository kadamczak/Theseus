using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>MazeWithSolution</c>s used in a specified <c>ExamSet</c>, in the correct order.
    /// </summary>
    public interface IGetOrderedMazesWithSolutionOfExamSetQuery
    {
        IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId);
    }
}

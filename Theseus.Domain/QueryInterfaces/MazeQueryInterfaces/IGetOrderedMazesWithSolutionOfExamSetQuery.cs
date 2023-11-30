using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetOrderedMazesWithSolutionOfExamSetQuery
    {
        IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId);
    }
}

using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetMazesWithSolutionOfExamSetQuery
    {
        IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId);
    }
}

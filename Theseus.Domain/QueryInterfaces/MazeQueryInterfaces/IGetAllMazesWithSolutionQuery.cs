using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetAllMazesWithSolutionQuery
    {
        IEnumerable<MazeWithSolution> GetAllMazesWithSolution();
    }
}
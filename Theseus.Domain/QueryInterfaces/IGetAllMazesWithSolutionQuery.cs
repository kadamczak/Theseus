using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetAllMazesWithSolutionQuery
    {
        Task<IEnumerable<MazeWithSolution>> GetAllMazesWithSolution();
    }
}
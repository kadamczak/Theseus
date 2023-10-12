using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetAllMazesQuery
    {
        Task<IEnumerable<MazeWithSolution>> GetAllMazesWithSolution();
    }
}

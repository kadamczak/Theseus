using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetMazeWithSolutionByIdQuery
    {
        Task<MazeWithSolution> GetMazeWithSolutionById(Guid id);
    }
}
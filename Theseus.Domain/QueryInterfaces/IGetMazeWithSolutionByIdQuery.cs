using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetMazeWithSolutionByIdQuery
    {
        MazeWithSolution? GetMazeWithSolutionById(Guid id);
    }
}
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.MazeCommandInterfaces
{
    public interface ICreateOrUpdateMazeWithSolutionCommand
    {
        Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze);
    }
}
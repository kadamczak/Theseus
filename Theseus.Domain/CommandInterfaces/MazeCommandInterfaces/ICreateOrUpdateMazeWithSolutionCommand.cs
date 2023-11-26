using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    public interface ICreateOrUpdateMazeWithSolutionCommand
    {
        Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze);
    }
}
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateOrUpdateMazeWithSolutionCommand
    {
        Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze);
    }
}
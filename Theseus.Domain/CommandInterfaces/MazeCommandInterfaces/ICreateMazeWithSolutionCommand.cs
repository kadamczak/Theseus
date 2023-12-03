using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    public interface ICreateMazeWithSolutionCommand
    {
        Task Create(MazeWithSolution maze);
    }
}
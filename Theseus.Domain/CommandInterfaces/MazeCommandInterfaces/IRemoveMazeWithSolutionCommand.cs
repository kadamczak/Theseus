using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    public interface IRemoveMazeWithSolutionCommand
    {
        Task Remove(Guid mazeId);
    }
}
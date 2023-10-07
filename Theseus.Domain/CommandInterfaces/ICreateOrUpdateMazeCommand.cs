using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateOrUpdateMazeCommand
    {
        Task CreateOrUpdateMaze(Maze maze);
    }
}

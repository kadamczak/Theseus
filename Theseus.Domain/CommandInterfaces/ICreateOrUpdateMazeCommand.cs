using Theseus.Domain.Models.MazeRelated.Maze;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateOrUpdateMazeCommand
    {
        Task CreateOrUpdateMaze(MazeGrid maze);
    }
}

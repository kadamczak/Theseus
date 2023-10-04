using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateOrUpdateMazeCommand
    {
        Task CreateOrUpdateMaze(Maze maze);
    }
}

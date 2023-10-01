using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateMazeCommand
    {
        Task CreateMaze(Maze maze);
    }
}

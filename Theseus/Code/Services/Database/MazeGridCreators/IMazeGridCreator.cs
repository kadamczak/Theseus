using System.Threading.Tasks;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.Services.Database.MazeGridCreators
{
    public interface IMazeGridCreator
    {
        Task CreateMazeGrid(MazeGrid maze);
    }
}

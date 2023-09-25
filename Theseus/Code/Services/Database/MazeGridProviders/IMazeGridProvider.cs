using System.Collections.Generic;
using System.Threading.Tasks;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.Services.Database.MazeGridProviders
{
    public interface IMazeGridProvider
    {
        Task<IEnumerable<MazeGrid>> GetAllMazeGrids();

    }
}

using Theseus.Domain.Models.MazeRelated.Maze;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetAllMazesQuery
    {
        Task<IEnumerable<MazeGrid>> GetAllMazes();
    }
}

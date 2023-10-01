using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetAllMazesQuery
    {
        Task<IEnumerable<Maze>> GetAllMazes();
    }
}

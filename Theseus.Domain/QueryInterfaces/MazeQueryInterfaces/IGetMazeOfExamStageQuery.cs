using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetMazeOfExamStageQuery
    {
        MazeWithSolution GetMaze(Guid examStageId);
    }
}
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>MazeWithSolution</c>s used in a specific <c>ExamStage</c>.
    /// </summary>
    public interface IGetMazeOfExamStageQuery
    {
        MazeWithSolution GetMaze(Guid examStageId);
    }
}
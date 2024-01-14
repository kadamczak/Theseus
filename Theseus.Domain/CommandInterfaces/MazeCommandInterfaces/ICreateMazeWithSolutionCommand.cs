using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>MazeWithSolution</c> creation method.
    /// </summary>
    public interface ICreateMazeWithSolutionCommand
    {
        Task Create(MazeWithSolution maze);
    }
}
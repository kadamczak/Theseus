namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>MazeWithSolution</c> deletion method.
    /// </summary>
    public interface IDeleteMazeWithSolutionCommand
    {
        Task Remove(Guid mazeId);
    }
}
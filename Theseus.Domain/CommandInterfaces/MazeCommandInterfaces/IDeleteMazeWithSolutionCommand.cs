namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    public interface IDeleteMazeWithSolutionCommand
    {
        Task Remove(Guid mazeId);
    }
}
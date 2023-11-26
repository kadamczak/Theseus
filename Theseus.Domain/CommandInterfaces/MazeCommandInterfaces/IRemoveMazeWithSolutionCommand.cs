namespace Theseus.Domain.CommandInterfaces.MazeCommandInterfaces
{
    public interface IRemoveMazeWithSolutionCommand
    {
        Task Remove(Guid mazeId);
    }
}
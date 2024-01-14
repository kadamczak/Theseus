using AutoMapper;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    /// <summary>
    /// Class implementing <c>MazeWithSolution</c> deletion method,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class DeleteMazeWithSolutionCommand : MazeCommand, IDeleteMazeWithSolutionCommand
    {
        public DeleteMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Remove(Guid mazeId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                MazeDto? maze = await context.Mazes.FindAsync(mazeId);

                if (maze is null)
                    return;

                context.Mazes.Remove(maze);
                await context.SaveChangesAsync();
            }
        }
    }
}
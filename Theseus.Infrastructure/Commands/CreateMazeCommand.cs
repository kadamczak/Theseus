using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.Commands
{
    public class CreateMazeCommand : ICreateMazeCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;

        public CreateMazeCommand(TheseusDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateMaze(Maze maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = MazeToMazeDtoConverter.Convert(maze);
                context.Mazes.Add(mazeDto);
                await context.SaveChangesAsync();
            }
        }
    }
}

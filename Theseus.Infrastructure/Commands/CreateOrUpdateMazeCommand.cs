using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.Commands
{
    public class CreateOrUpdateMazeCommand : ICreateOrUpdateMazeCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;

        public CreateOrUpdateMazeCommand(TheseusDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateOrUpdateMaze(Maze maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = MazeToMazeDtoConverter.Convert(maze);
                context.Mazes.Update(mazeDto);
                await context.SaveChangesAsync();

                maze.Id = mazeDto.Id;
            }
        }
    }
}

using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.Infrastructure.Commands
{
    public class CreateOrUpdateMazeWithSolutionCommand : ICreateOrUpdateMazeWithSolutionCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly MazeWithSolutionToMazeDtoConverter _toMazeDtoConverter;

        public CreateOrUpdateMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory,
                                         MazeWithSolutionToMazeDtoConverter ToMazeDtoConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toMazeDtoConverter = ToMazeDtoConverter;
        }

        public async Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = this._toMazeDtoConverter.Convert(maze);
                context.Mazes.Update(mazeDto);
                await context.SaveChangesAsync();

                maze.Id = mazeDto.Id;
            }
        }
    }
}

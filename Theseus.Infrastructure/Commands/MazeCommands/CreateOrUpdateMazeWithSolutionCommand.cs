using AutoMapper;
using Theseus.Domain.MazeCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    public class CreateOrUpdateMazeWithSolutionCommand : MazeCommand, ICreateOrUpdateMazeWithSolutionCommand
    {
        public CreateOrUpdateMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = new MazeDto();
                Mapper.Map(maze, mazeDto);
                Mapper.Map(maze.StaffMember, mazeDto.Owner);

                AttachRelatedEntities(mazeDto, context);
                context.Mazes.Update(mazeDto);
                await context.SaveChangesAsync();

                maze.Id = mazeDto.Id;
            }
        }
    }
}
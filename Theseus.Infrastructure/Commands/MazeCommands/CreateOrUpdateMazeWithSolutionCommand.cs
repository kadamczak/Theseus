using AutoMapper;
using Theseus.Domain.MazeCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    public class CreateOrUpdateMazeWithSolutionCommand : MazeCommand, ICreateOrUpdateMazeWithSolutionCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreateOrUpdateMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory,
                                                     IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = _mapper.Map<MazeDto>(maze);
                mazeDto.Owner = _mapper.Map<StaffMemberDto>(maze.StaffMember);

                AttachRelatedEntities(mazeDto, context);

                context.Mazes.Update(mazeDto);
                await context.SaveChangesAsync();

                maze.Id = mazeDto.Id;
            }
        }
    }
}

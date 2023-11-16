using AutoMapper;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands
{
    public class CreateOrUpdateMazeWithSolutionCommand : ICreateOrUpdateMazeWithSolutionCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreateOrUpdateMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory,
                                                     IMapper mapper)
        {
            this._dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateMazeWithSolution(MazeWithSolution maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = this._mapper.Map<MazeDto>(maze);
                mazeDto.Owner = this._mapper.Map<StaffMemberDto>(maze.StaffMember);

                context.Attach(mazeDto.Owner);
                context.Mazes.Update(mazeDto);
                await context.SaveChangesAsync();

                maze.Id = mazeDto.Id;
            }
        }
    }
}

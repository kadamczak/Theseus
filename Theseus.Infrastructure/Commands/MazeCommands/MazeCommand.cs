using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    public abstract class MazeCommand : Command
    {
        protected MazeCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(MazeDto mazeDto, TheseusDbContext context)
        {
            context.Attach(mazeDto.Owner);

            if (mazeDto.ExamSetDtos is null)
                return;

            foreach (var examSet in mazeDto.ExamSetDtos)
            {
                context.Attach(examSet);
            }
        }
    }
}

using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public abstract class ExamSetCommand : Command
    {
        protected ExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(ExamSetDto examSetDto, TheseusDbContext context)
        {
            context.Attach(examSetDto.Owner);

            if (examSetDto.MazeDtos is null)
                return;

            foreach (var maze in examSetDto.MazeDtos)
            {
                context.Attach(maze);
            }
        }
    }
}

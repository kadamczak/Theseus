using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public abstract class StaffMemberCommand : Command
    {
        protected StaffMemberCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            if(staffMemberDto.GroupDtos is not null)
            {
                foreach (var group in staffMemberDto.GroupDtos)
                    context.Attach(group);
            }

            if (staffMemberDto.ExamSetDtos is not null)
            {
                foreach (var examSet in staffMemberDto.ExamSetDtos)
                    context.Attach(examSet);
            }

            if (staffMemberDto.MazeDtos is not null)
            {
                foreach (var maze in staffMemberDto.MazeDtos)
                    context.Attach(maze);
            }
        }
    }
}
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public abstract class StaffMemberCommand
    {
        protected void AttachRelatedEntities(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            foreach (var patient in staffMemberDto.PatientDtos)
            {
                context.Attach(patient);
            }
            foreach (var examSet in staffMemberDto.ExamSetDtos)
            {
                context.Attach(examSet);
            }
            foreach (var maze in staffMemberDto.MazeDtos)
            {
                context.Attach(maze);
            }
        }
    }
}

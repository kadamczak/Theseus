using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public abstract class StaffMemberCommand
    {
        protected void AttachRelatedEntities(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            if(staffMemberDto.PatientDtos is not null)
            {
                foreach (var patient in staffMemberDto.PatientDtos)
                    context.Attach(patient);
            }

            if (staffMemberDto.ExamSetDtos is not null)
            {
                foreach (var examSet in staffMemberDto.ExamSetDtos)
                    context.Attach(examSet);
            }

            if (staffMemberDto.PatientDtos is not null)
            {
                foreach (var maze in staffMemberDto.MazeDtos)
                    context.Attach(maze);
            }
        }
    }
}
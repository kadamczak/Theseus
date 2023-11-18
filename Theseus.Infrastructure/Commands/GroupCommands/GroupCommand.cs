using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.GroupCommands
{
    public abstract class GroupCommand : Command
    {
        protected GroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(GroupDto groupDto, TheseusDbContext context)
        {
            if (groupDto.StaffMemberDtos is not null)
            {
                foreach (var staffMember in groupDto.StaffMemberDtos)
                    context.Attach(staffMember);
            }

            if (groupDto.PatientDtos is not null)
            {
                foreach (var patient in groupDto.PatientDtos)
                    context.Attach(patient);
            }

            if (groupDto.ExamSetDtos is not null)
            {
                foreach (var examSet in groupDto.ExamSetDtos)
                    context.Attach(examSet);
            }

            if (groupDto.Owner is not null)
                context.Attach(groupDto.Owner);
        }
    }
}

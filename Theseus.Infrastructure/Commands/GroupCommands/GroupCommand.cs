using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.GroupCommands
{
    /// <summary>
    /// Abstract class defining attachment of related entities to the <c>GroupDto</c>.
    /// </summary>
    public abstract class GroupCommand : Command
    {
        protected GroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(GroupDto groupDto, TheseusDbContext context)
        {
            if (groupDto.StaffMemberDtos is not null)
                AttachStaffMemberDtos(groupDto, context);

            if (groupDto.PatientDtos is not null)
                AttachPatientDtos(groupDto, context);

            if (groupDto.ExamSetDtos is not null)
                AttachExamSetDtos(groupDto, context);

            if (groupDto.Owner is not null)
                context.Attach(groupDto.Owner);
        }

        private void AttachStaffMemberDtos(GroupDto groupDto, TheseusDbContext context)
        {
            foreach (var staffMember in groupDto.StaffMemberDtos)
                context.Attach(staffMember);
        }

        private void AttachPatientDtos(GroupDto groupDto, TheseusDbContext context)
        {
            foreach (var patient in groupDto.PatientDtos)
                context.Attach(patient);
        }

        private void AttachExamSetDtos(GroupDto groupDto, TheseusDbContext context)
        {
            foreach (var examSet in groupDto.ExamSetDtos)
                context.Attach(examSet);
        }
    }
}

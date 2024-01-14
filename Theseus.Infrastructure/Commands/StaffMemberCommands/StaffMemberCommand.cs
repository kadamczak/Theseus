using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    /// <summary>
    /// Abstract class defining attachment of related entities to the <c>StaffMemberDto</c>.
    /// </summary>
    public abstract class StaffMemberCommand : Command
    {
        protected StaffMemberCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            if(staffMemberDto.GroupDtos is not null)
                AttachGroupDtos(staffMemberDto, context);

            if (staffMemberDto.ExamSetDtos is not null)
                AttachExamSetDtos(staffMemberDto, context);

            if (staffMemberDto.MazeDtos is not null)
                AttachMazeDtos(staffMemberDto, context);

            if (staffMemberDto.OwnedGroupDtos is not null)
                AttachOwnedGroupDtos(staffMemberDto, context);
        }

        private void AttachGroupDtos(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            foreach (var group in staffMemberDto.GroupDtos)
                context.Attach(group);
        }

        private void AttachExamSetDtos(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            foreach (var examSet in staffMemberDto.ExamSetDtos)
                context.Attach(examSet);
        }

        private void AttachMazeDtos(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            foreach (var maze in staffMemberDto.MazeDtos)
                context.Attach(maze);
        }

        private void AttachOwnedGroupDtos(StaffMemberDto staffMemberDto, TheseusDbContext context)
        {
            foreach (var group in staffMemberDto.OwnedGroupDtos)
                context.Attach(group);
        }
    }
}
using AutoMapper;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public class CreateStaffMemberCommand : StaffMemberCommand, ICreateStaffMemberCommand
    {
        public CreateStaffMemberCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Create(StaffMember staffMember)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                StaffMemberDto staffMemberDto = new StaffMemberDto();
                Mapper.Map(staffMember, staffMemberDto);
                AttachRelatedEntities(staffMemberDto, context);
                context.StaffMembers.Add(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public class UpdateStaffMemberCommand : StaffMemberCommand, IUpdateStaffMemberCommand
    {
        public UpdateStaffMemberCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Update(StaffMember staffMember)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberWithSameEmail = context.StaffMembers.AsNoTracking().FirstOrDefault(s => s.Email == staffMember.Email);
                if (staffMemberWithSameEmail is not null && staffMemberWithSameEmail.Id != staffMember.Id)
                {
                    throw new ArgumentException("Email is already taken.");
                }

                StaffMemberDto staffMemberDto = new StaffMemberDto();
                Mapper.Map(staffMember, staffMemberDto);
                AttachRelatedEntities(staffMemberDto, context);
                context.StaffMembers.Update(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
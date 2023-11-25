using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public class RemoveStaffMemberFromGroupCommand : StaffMemberCommand, IRemoveStaffMemberFromGroupCommand
    {
        public RemoveStaffMemberFromGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task RemoveFromGroup(StaffMember staffMember, Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                StaffMemberDto staffMemberDto = context.StaffMembers.Include(p => p.GroupDtos).First(p => p.Id == staffMember.Id);
                GroupDto groupDto = context.Groups.Find(groupId);
                staffMemberDto.GroupDtos.Remove(groupDto);

                context.StaffMembers.Update(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}

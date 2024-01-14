using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.GroupCommands
{
    /// <summary>
    /// Class implementing <c>Group</c> deletion method,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class DeleteGroupCommand : GroupCommand, IDeleteGroupCommand
    {
        public DeleteGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Delete(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto groupDto = await context.Groups
                                           .Include(g => g.StaffMemberDtos)
                                           .Include(g => g.PatientDtos)
                                           .Include(g => g.ExamSetDtos)
                                           .Include(g => g.Owner)
                                           .Where(g => g.Id == groupId)
                                           .FirstAsync();

                groupDto.PatientDtos.Clear();
                groupDto.StaffMemberDtos.Clear();
                groupDto.ExamSetDtos.Clear();
                groupDto.Owner = null;

                context.Groups.Remove(groupDto);
                await context.SaveChangesAsync();
            }
        }
    }
}

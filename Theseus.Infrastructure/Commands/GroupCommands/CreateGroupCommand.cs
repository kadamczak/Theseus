using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.GroupCommands
{
    public class CreateGroupCommand : GroupCommand, ICreateGroupCommand
    {
        public CreateGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task CreateGroup(Group group)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                var groupWithSameName = await context.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.Name == group.Name);

                if (groupWithSameName is not null && groupWithSameName.Id != group.Id)
                {
                    throw new ArgumentException("Group with this name already exists.");
                }

                GroupDto groupDto = new GroupDto();
                Mapper.Map(group, groupDto);
                AttachRelatedEntities(groupDto, context);
                context.Groups.Add(groupDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
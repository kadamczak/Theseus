using AutoMapper;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.GroupCommands
{
    /// <summary>
    /// Class implementing <c>Group</c> creation method,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// All objects linked by foreign key need to already exist in database.
    /// </summary>
    public class CreateGroupCommand : GroupCommand, ICreateGroupCommand
    {
        public CreateGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task CreateGroup(Group group)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto groupDto = new GroupDto();
                Mapper.Map(group, groupDto);
                AttachRelatedEntities(groupDto, context);
                context.Groups.Add(groupDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
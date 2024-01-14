using AutoMapper;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.GroupQueries
{
    /// <summary>
    /// Abstract query class, with method for <c>GroupDto</c> to <c>Group</c> mapping.
    /// </summary>
    public abstract class GroupQuery : Query
    {
        protected GroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<Group> MapGroups(IEnumerable<GroupDto> groupDtos)
        {
            List<Group> groups = new List<Group>();
            foreach (var group in groupDtos)
            {
                groups.Add(MapGroup(group));
            }
            return groups;
        }

        protected Group MapGroup(GroupDto groupDto)
        {
            Group group = new Group();
            Mapper.Map(groupDto, group);
            return group;
        }
    }
}
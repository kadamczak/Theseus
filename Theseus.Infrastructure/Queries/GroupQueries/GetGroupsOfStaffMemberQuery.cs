using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.GroupQueries
{
    public class GetGroupsOfStaffMemberQuery : GroupQuery, IGetGroupsOfStaffMemberQuery
    {
        public GetGroupsOfStaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Group> GetGroups(Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<GroupDto> groupDtos = context.Groups.Where(m => m.StaffMemberDtos.Where(s => s.Id == staffMemberId).Any()).AsNoTracking();
                return MapGroups(groupDtos);    
            }
        }
    }
}
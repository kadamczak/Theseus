using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public class GetOwnerOfGroupQuery : StaffMemberQuery, IGetOwnerOfGroupQuery
    {
        public GetOwnerOfGroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task<StaffMember> GetOwnerAsync(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto group = await context.Groups.Include(g => g.Owner).FirstAsync(g => g.Id == groupId);
                return GetOwner(group);
            }
        }

        public StaffMember GetOwner(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto group = context.Groups.Include(g => g.Owner).First(g => g.Id == groupId);
                return GetOwner(group);
            }
        }

        private StaffMember GetOwner(GroupDto group)
        {
            StaffMemberDto owner = group.Owner;
            return MapStaffMember(owner);
        }
    }
}
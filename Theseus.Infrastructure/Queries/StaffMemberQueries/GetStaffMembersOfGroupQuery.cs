using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    /// <summary>
    /// Class defining retrieval of <c>StaffMember</c>s that are included in the specified <c>Group</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetStaffMembersOfGroupQuery : StaffMemberQuery, IGetStaffMembersOfGroupQuery
    {
        public GetStaffMembersOfGroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<StaffMember> GetStaffMembers(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<StaffMemberDto> staffMemberDtos = context.StaffMembers.AsNoTracking().Where(m => m.GroupDtos.Where(g => g.Id == groupId).Any());
                return MapStaffMembers(staffMemberDtos);
            }
        }
    }
}
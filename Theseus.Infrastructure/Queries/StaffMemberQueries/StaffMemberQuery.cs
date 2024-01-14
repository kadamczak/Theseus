using AutoMapper;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    /// <summary>
    /// Abstract query class, with method for <c>StaffMemberDto</c> to <c>StaffMember</c> mapping.
    /// </summary>
    public abstract class StaffMemberQuery : Query
    {
        protected StaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<StaffMember> MapStaffMembers(IEnumerable<StaffMemberDto> staffMemberDtos)
        {
            List<StaffMember> staffMembers = new List<StaffMember>();
            foreach (var staffMemberDto in staffMemberDtos)
            {
                staffMembers.Add(MapStaffMember(staffMemberDto));
            }
            return staffMembers;
        }

        protected StaffMember MapStaffMember(StaffMemberDto staffMemberDto)
        {
            StaffMember staffMember = new StaffMember();
            Mapper.Map(staffMemberDto, staffMember);
            return staffMember;
        }
    }
}
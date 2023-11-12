using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;
using Theseus.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public class GetStaffMemberByUsernameQuery : IGetStaffMemberByUsernameQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetStaffMemberByUsernameQuery(TheseusDbContextFactory dbContextFactory,
                                             IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<StaffMember?> GetStaffMember(string username)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Username == username);
                return staffMemberDto is null ? null : _mapper.Map<StaffMember>(staffMemberDto);
            }
        }
    }
}
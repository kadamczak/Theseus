using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public class GetStaffMemberByEmailQuery : IGetStaffMemberByEmailQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetStaffMemberByEmailQuery(TheseusDbContextFactory dbContextFactory,
                                          IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<StaffMember?> GetStaffMember(string email)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Email == email);
                return staffMemberDto is null ? null : _mapper.Map<StaffMember>(staffMemberDto);
            }
        }
    }
}
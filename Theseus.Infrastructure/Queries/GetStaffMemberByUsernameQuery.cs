using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;
using Theseus.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Theseus.Infrastructure.Queries
{
    public class GetStaffMemberByUsernameQuery : IGetStaffMemberByUsernameQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetStaffMemberByUsernameQuery(TheseusDbContextFactory dbContextFactory,
                                             IMapper mapper)
        {
            this._dbContextFactory = dbContextFactory;
            this._mapper = mapper;
        }

        public async Task<StaffMember?> GetStaffMember(string username)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Username == username);
                return (staffMemberDto is null) ? null : this._mapper.Map<StaffMember>(staffMemberDto);
            }
        }
    }
}
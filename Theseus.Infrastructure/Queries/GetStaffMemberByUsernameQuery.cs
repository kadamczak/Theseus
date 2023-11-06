using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;
using Theseus.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Theseus.Infrastructure.Queries
{
    public class GetStaffMemberByUsernameQuery : IGetStaffMemberByUsernameQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly StaffMemberDtoToStaffMemberConverter _toStaffMemberConverter;

        public GetStaffMemberByUsernameQuery(TheseusDbContextFactory dbContextFactory, StaffMemberDtoToStaffMemberConverter toStaffMemberConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toStaffMemberConverter = toStaffMemberConverter;
        }

        public async Task<StaffMember?> GetStaffMember(string username)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Username == username);
                return (staffMemberDto is null) ? null : this._toStaffMemberConverter.Convert(staffMemberDto);
            }
        }
    }
}
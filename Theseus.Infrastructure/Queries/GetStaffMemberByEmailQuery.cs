using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;

namespace Theseus.Infrastructure.Queries
{
    public class GetStaffMemberByEmailQuery : IGetStaffMemberByEmailQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly StaffMemberDtoToStaffMemberConverter _toStaffMemberConverter;

        public GetStaffMemberByEmailQuery(TheseusDbContextFactory dbContextFactory, StaffMemberDtoToStaffMemberConverter toStaffMemberConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toStaffMemberConverter = toStaffMemberConverter;
        }

        public async Task<StaffMember?> GetStaffMember(string email)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Email == email);
                return (staffMemberDto is null) ? null : this._toStaffMemberConverter.Convert(staffMemberDto);
            }
        }
    }
}
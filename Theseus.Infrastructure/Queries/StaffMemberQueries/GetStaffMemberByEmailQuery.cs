﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    /// <summary>
    /// Class defining retrieval of <c>StaffMember</c> with the specified email,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetStaffMemberByEmailQuery : StaffMemberQuery, IGetStaffMemberByEmailQuery
    {
        public GetStaffMemberByEmailQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public async Task<StaffMember?> GetStaffMember(string email)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
                return staffMemberDto is null ? null : MapStaffMember(staffMemberDto);
            }
        }
    }
}
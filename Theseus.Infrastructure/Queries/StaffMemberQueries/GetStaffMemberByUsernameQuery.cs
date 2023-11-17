﻿using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public class GetStaffMemberByUsernameQuery : StaffMemberQuery, IGetStaffMemberByUsernameQuery
    {
        public GetStaffMemberByUsernameQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public async Task<StaffMember?> GetStaffMember(string username, bool loadExamSets = false, bool loadPatients = false, bool loadMazes = false)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                StaffMemberDto? staffMemberDto = await context.StaffMembers.FirstOrDefaultAsync(user => user.Username == username);
                return staffMemberDto is null ? null : GetStaffMember(context, staffMemberDto, loadExamSets, loadPatients, loadMazes);
            }
        }
    }
}
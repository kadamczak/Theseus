﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.GroupQueries
{
    public class GetGroupByNameQuery : GroupQuery, IGetGroupByNameQuery
    {
        public GetGroupByNameQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task<Group?> GetGroup(string name)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto? groupDto = await context.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.Name == name);
                return groupDto is null ? null : MapGroup(groupDto);
            }
        }
    }
}
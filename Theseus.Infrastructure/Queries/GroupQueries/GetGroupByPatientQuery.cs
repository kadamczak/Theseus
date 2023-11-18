using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Queries.PatientQueries;

namespace Theseus.Infrastructure.Queries.GroupQueries
{
    public class GetGroupByPatientQuery : GroupQuery, IGetGroupByPatientQuery
    {
        public GetGroupByPatientQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task<Group?> GetGroup(Guid patientId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto? groupDto = await context.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.PatientDtos.Where(p => p.Id == patientId).Any());
                return groupDto is null ? null : MapGroup(groupDto);
            }
        }
    }
}

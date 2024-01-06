using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    public class GetPatientsOfGroupQuery : PatientQuery, IGetPatientsOfGroupQuery
    {
        public GetPatientsOfGroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Patient> GetPatients(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<PatientDto> patientDtos = context.Patients.AsNoTracking().Include(p => p.GroupDto).Where(m => m.GroupDto.Id == groupId);
                return MapPatients(patientDtos);
            }
        }
    }
}

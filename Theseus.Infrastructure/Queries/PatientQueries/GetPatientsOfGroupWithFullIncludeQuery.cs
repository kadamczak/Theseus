using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    /// <summary>
    /// Class defining retrieval of <c>Patient</c>s belonging to the specified <c>Group</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// Related entities are included.
    /// </summary>
    public class GetPatientsOfGroupWithFullIncludeQuery : PatientQuery, IGetPatientsOfGroupWithFullIncludeQuery
    {
        public GetPatientsOfGroupWithFullIncludeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
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

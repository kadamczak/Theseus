using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    /// <summary>
    /// Class defining retrieval of <c>Patient</c>s belonging to the specified <c>StaffMember</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// Related entities are included.
    /// </summary>
    public class GetPatientsOfStaffMemberWithFullIncludeQuery : PatientQuery, IGetPatientsOfStaffMemberWithFullIncludeQuery
    {
        public GetPatientsOfStaffMemberWithFullIncludeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Patient> GetPatients(Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<PatientDto> patientDtos = context.Patients
                                                             .AsNoTracking()
                                                             .Include(g => g.GroupDto)
                                                             .Where(m => m.GroupDto.StaffMemberDtos.Where(s => s.Id == staffMemberId).Any())
                                                             .OrderByDescending(m => m.ExamDtos.Max(e => e.CreatedAt));
                return MapPatients(patientDtos);
            }
        }
    }
}
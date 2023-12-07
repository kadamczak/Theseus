using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    public class GetPatientsOfStaffMemberQuery : PatientQuery, IGetPatientsOfStaffMemberQuery
    {
        public GetPatientsOfStaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Patient> GetPatients(Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<PatientDto> patientDtos = context.Patients
                                                             .AsNoTracking()
                                                             .Where(m => m.GroupDto.StaffMemberDtos.Where(s => s.Id == staffMemberId).Any())
                                                             .OrderByDescending(m => m.ExamDtos.Max(e => e.CreatedAt));
                return MapPatients(patientDtos);
            }
        }
    }
}